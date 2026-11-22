# Coding Rules — CU-PFIS

These are binding conventions for **CU-PFIS** (**Chula Pharmacy Faculty Information System** / **ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย**), not suggestions — follow them strictly. See [architecture.md](architecture.md) for the layering these rules sit inside and [domain.md](domain.md) for the domain vocabulary.

## Naming

1. Interfaces start with capital `I`, e.g. `IChulaDbContext`.
2. Names must be meaningful and self-descriptive — classes, variables, methods. Prefer explicit business terms from [domain.md](domain.md).
3. `PascalCase` for class and method names, e.g. `ChulaDbContext`, `SaveChangesAsync()`.
4. `PascalCase` for constants (local and field), e.g. `ConnectionString = "Database"`.
5. `camelCase` for method arguments, local variables, and private fields, e.g. constructor parameter `connectionString`.
6. Private instance fields are prefixed with `_`, e.g. `_context`.
7. Method parameters: if 3 or fewer, keep them on one line; if more than 3, one parameter per line.

## Backend conventions

- **API routes are lower-kebab-case.** `Program.cs` registers a global `RouteTokenTransformerConvention(new SlugifyParameterTransformer())`, which automatically converts `[controller]`/`[action]` tokens to kebab-case (`TrainingRequestsController` → `/training-requests`, `SeniorProjectsController` → `/senior-projects`). Always write literal route segments in kebab-case yourself (`[HttpGet("by-academic-year")]`).
- **Dual key on every entity**: internal `int Id` for FKs/joins (never exposed outside the backend), `Guid ExternalId` (UUIDv7, generated on create) as the only identifier used in API routes, request/response DTOs, and frontend URLs.
- **CQRS use case shape**: one folder per use case (`Features/{Context}/{Feature}/{Commands|Queries}/{UseCase}/`), with each type in its own file:
  - `{UseCase}Command.cs` / `{UseCase}Query.cs` — the `IRequest<TResponse>` record and its `IRequestHandler<TRequest, TResponse>` in the **same file**.
  - `{UseCase}Validator.cs` — the `AbstractValidator<T>` gets its **own file**, always.
  Naming: `{Verb}{Noun}Command` / `Get{Noun}Query` (single) or `Get{Noun}sQuery` (list), `{Request}Handler`, `{Request}Validator`.
- **Controllers are thin**: inject `Mediator` (via the shared `BaseController`), call `Send`, return the result. No business logic, no direct `DbContext` access.
- **Domain entities enforce their own invariants.** Do not rely on validators alone. Use `Ardalis.GuardClauses` in entity constructors and behavior methods (e.g. `Guard.Against.NullOrWhiteSpace`, `Guard.Against.NegativeOrZero`) so an invalid entity cannot exist in memory. Prefer behavior methods (`trainingRequest.MarkVerified(staffId)`, `trainingRequest.ApproveByDean(staffId, approvedAt)`, `achievement.RequestRevision(reason)`) over raw property assignments scattered across handlers.
- **Money and budget fields are never mutated in place.** Training budget allocations, reservations, consumptions (triggered only **after Dean approval** per `A05`), cancellation releases, and **future fiscal-year quota borrowing (`A06`)** must be modeled in the append-only `TrainingBudgetLedger` — never a single column that gets directly incremented/decremented. Annual budget caps (default `10,000 THB/person/fiscal year`, **1 Oct – 30 Sep**) must come from configurable policy data (`TrainingBudgetPolicy`), never hardcoded constants.
- **State transitions are logged, not just stored.** Any entity with a verification/approval or publication workflow (`TrainingRequest`, `StudentAchievement`, `GraduateEmployment`, `SeniorProject`) must record who changed the state, from what, to what, when, and why (for revision requests/rejections) — as an append-only trail, not just an overwritten `Status` column.
- **External data sync & re-import safety (CUPPEP / Google Sheet / Excel Migration).**
  - Never match people across systems by name alone; use stable identifiers (`StudentCode`, `PersonnelCode`) plus `SourceSystem` and `SourceRecordId` (never Google Sheet row numbers).
  - Re-importing the same source record must be idempotent (no duplicate rows).
  - If an incoming external record conflicts with data already verified or edited by staff in **CU-PFIS**, **never silently overwrite the staff-verified value** — route the conflict to a staging/review state (`DataConflictQueue` / pending review) with full before/after provenance.
- **PDPA & Sensitive Data Protection (`A03`, `A18`).**
  - Enforce a **First-Login PDPA Consent** gate (`PdpaConsent`) before users access CU-PFIS data, recording acceptance timestamp and policy version.
  - Check permissions by role, **Department/Division scope for Staff (`A03`)**, resource ownership (e.g., Faculty editing own CV), and project relationship (Project Advisor), not just by hiding UI menus.
  - Sensitive personal data — especially **bank account numbers** (in training requests) and **employer evaluation details (ผู้ใช้บัณฑิต)** — must only be returned to authorized roles and must never leak into general exports, public views, or application logs.
  - Never log full names or sensitive PII in plaintext application logs (log `LoginName` / `ExternalId` instead).
- **PDF & File Processing Safeguards (`A11`).**
  - Store file metadata (`FileName`, `FileSize`, `Sha256Hash`, `PageCount`, `StoragePath`, `AbstractPageNumber`) separately from binary storage; validate MIME type, file header, encryption status, and page count on upload.
  - When locating the Abstract page for the annual Senior Project Abstract Book (Cover page + Abstract pages), auto-detect within **pages 3–5** using first-line centered `"Abstract"` text layout, and always allow staff preview/manual override for scanned, encrypted, or non-standard PDFs — never assume page 3 blindly.
- **Optimistic concurrency**: every mutable entity carries the Postgres `xmin` system column as a shadow-property concurrency token (`ConfigureEntityBase<T>()`). A conflicting concurrent write must fail loudly (`DbUpdateConcurrencyException`), never silently overwrite.
- **Multi-aggregate operations are transactional.** If a single logical action touches more than one aggregate (e.g. approving a `TrainingRequest` writes a `TrainingBudgetLedger` entry and updates CV visibility), wrap it in one database transaction.
- **Validation is layered, not singular.** FluentValidation at the Application boundary, invariant guard clauses in the Domain entity, and database `CHECK` constraints / unique indexes in the EF configuration.
- **Permissions**: use the `module.action` convention (e.g. `faculty-cv.view`, `training-requests.verify`, `training-requests.approve`, `senior-projects.publish`) with `[RequirePermission]` on commands/queries.
- **EF configuration**: Fluent API only (`IEntityTypeConfiguration<T>`), no data annotations on entities. Use `ConfigureEntityBase<T>()`, `ConfigureSoftDelete<T>()` (for `SoftDeletableEntityBase`), and `ConfigureActiveFlag<T>()` (for `IHasActiveFlag`). Never make append-only ledger or audit entities (`TrainingBudgetLedger`, `AuditLog`, `TrainingApprovalHistory`) soft-deletable or give them `IsActive`.

## Frontend conventions

- Vue 3 Composition API (`<script setup>`), JavaScript.
- Dedicated `create/`, `edit/[id].vue`, `view/[id].vue`, `list/index.vue` routes per feature — no modal-dialog-only CRUD or dead stub routes.
- **Bilingual (TH/EN) support**: UI copy, form labels, and validation messages must support Thai and English (`vue-i18n`), rather than hardcoding single-language strings where bilingual support is required.
- **API calls use lower-kebab-case paths**, matching the backend's slugified routes (`/faculty-members`, `/training-requests`, `/senior-projects`).
- One HTTP client only, generated/aligned with the backend's OpenAPI spec.
- Shared composables (`useCrud`, `useConfirmDelete`, etc.) for cross-cutting CRUD/UX behavior.

## Testing

- A handler-level unit test and a functional test are **merge-blocking** for any change touching training budget calculations/ledger, approval/verification workflows, external data reconciliation (CUPPEP / Chula Final), or access control.
- Domain entity unit tests must cover constructor/guard-clause invariants and state transition methods for every required field and business rule.
- Test files mirror the source path 1:1.
- **Functional/integration tests use a local, dedicated PostgreSQL database (`cupharm-test`) — never Testcontainers/Docker.**
- **Frontend e2e tests use Playwright**, under `src/vuewebui/e2e/`, using `data-testid="{feature}-{element}"` attributes.
