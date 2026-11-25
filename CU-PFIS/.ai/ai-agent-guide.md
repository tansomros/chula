# AI Agent Guide — CU-PFIS

Read this before making any change in the **CU-PFIS** (**Chula Pharmacy Faculty Information System** / **ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย**) repository. It tells you where things go and what not to do. For the "why", see [architecture.md](architecture.md) and [coding-rules.md](coding-rules.md). For domain concepts and workflows, check [domain.md](domain.md). For open questions, risks, and known issues, check [known-issues.md](known-issues.md) and [roadmap.md](roadmap.md). Starting a specific task? [prompt-templates.md](prompt-templates.md) has a ready-to-paste starting prompt per stage.

## Where to add things

| I need to... | Goes in |
|---|---|
| Read original client scope document | `.doc/req071169.pdf` (and `.doc/CuProjectName.md`) |
| Read or write a feature requirement spec | `requirements/{Context}/{feature}.md` — using [`requirements/_template.md`](../requirements/_template.md) |
| Add a new domain entity | `src/Domain/Entities/{Context}/{Entity}.cs` — pick the bounded context from [architecture.md](architecture.md) (`ReferenceData`, `Faculty`, `Training`, `Students`, `ProfessionalLicense`, `SeniorProjects`, `Integration`, `Reporting`, `Security`) |
| Add a shared domain base type | `src/Domain/Common/` |
| Add a new command (create/update/delete/verify/approve) | `src/Application/Features/{Context}/{Feature}/Commands/{VerbNoun}/` — `{VerbNoun}Command.cs` (Command + Handler together) and `{VerbNoun}Validator.cs` (Validator, its own file) |
| Add a new query | `src/Application/Features/{Context}/{Feature}/Queries/{GetNoun[s]}/` — `{UseCase}Query.cs` (Query + Handler), `{UseCase}Validator.cs` if validated |
| Add EF mapping for an entity | `src/Infrastructure/Data/Configurations/{Context}/{Entity}Configuration.cs`, register the `DbSet<T>` in `ChulaDbContext` and `IChulaDbContext` |
| Add a migration | From `src/Infrastructure`: `dotnet ef migrations add {Name} --startup-project ../API -o Data/Migrations` |
| Add an API endpoint | Add an action to the matching `src/API/Controllers/{Feature}Controller.cs` (or create one) — the action should only call `Mediator.Send(...)` |
| Add a frontend page for a feature | `src/vuewebui/src/pages/{feature}/{create,edit,view,list}/` — dedicated routes, not a dialog |
| Add a shared frontend composable | `src/vuewebui/src/composables/` |
| Add cross-feature reference data used by many forms | A Pinia store (e.g., AcademicYear, Department, TrainingCategory), not a per-page fetch |

## MUST

- MUST read the feature's `requirements/{Context}/{feature}.md` file and check `.ai/roadmap.md` (including Open Questions `Q01–Q18`) before implementing any feature. If a requirement depends on an unconfirmed stakeholder question, surface it in your plan rather than guessing silently.
- MUST put a new entity's Domain class, Application feature folder, EF configuration, and requirement file under the same bounded context (`{Context}` name matches across all).
- MUST give every new entity a `Guid ExternalId` and use it (never the internal `Id`) in any API route, request/response DTO, or frontend URL.
- MUST use `Ardalis.GuardClauses` (or explicit domain guard checks) to enforce entity invariants in constructors and state-transition behavior methods — don't rely on FluentValidation alone.
- MUST model training budget balances (`TrainingBudgetLedger`) as derived from an append-only ledger table — never a single mutable balance field. And never hardcode the `10,000 THB/person/year` limit in code; read it from configurable policy (`TrainingBudgetPolicy`).
- MUST keep "Staff Verification (ตรวจข้อมูลถูกต้อง)" and "Dean Approval Notification (แจ้งอนุมัติหลังคณบดีลงนาม)" as distinct workflow states in `TrainingRequest`.
- MUST preserve provenance (`SourceSystem`, `SourceRecordId`, `ImportedAt`) on data imported from CUPPEP, Google Sheets, or Chula Final datasets, and MUST queue conflicts for staff review rather than silently overwriting staff-edited records in the master system (`CU-PFIS`).
- MUST add a concurrency token (`xmin` via `ConfigureEntityBase<T>()`) to any new mutable entity.
- MUST wrap multi-aggregate writes in a single database transaction.
- MUST write a handler-level unit test and a functional test for any change touching training budgets, approval/verification workflows, external sync reconciliation, or access control.
- MUST keep controllers thin — no business logic, no direct DbContext access in `src/API`.
- MUST update [known-issues.md](known-issues.md) and [roadmap.md](roadmap.md) after completing a unit of work from the roadmap.

## MUST NOT

- MUST NOT modify anything inside the sibling folder `../CU-BCP/` (an unrelated existing Faculty website residing in the same `D:\Projects\Chula` repository).
- MUST NOT write to `CU-PPEP`'s MS SQL Server database (`chula_pps`) from `CU-PFIS`, and MUST NOT query SQL Server from `Domain`, `Application`, or `ChulaDbContext` (EF Core stays strictly PostgreSQL-only) — always pull read-only CUPPEP data via `ICuppepDataSource` (implemented in Infrastructure by `SqlCuppepDataSource` using `Microsoft.Data.SqlClient` + Dapper, per `D11 — Option 1B`).
- MUST NOT prefix new tables created in `chula_pps` with `PFIS_` — always use clean `PascalCase` domain table names matching existing `chula_pps` conventions (e.g., `dbo.StudentAchievements`, `dbo.StudentProfileExt`, `dbo.StudentLicenseResults`, `dbo.StudentEmploymentRound1`).
- MUST NOT silently overwrite staff-verified or staff-edited records in **CU-PFIS** when re-importing data from CUPPEP, Google Sheets, or external Excel/CSV files.
- MUST NOT match imported records by person name alone or by Google Sheet row number — always use stable identifiers (`StudentCode`, `PersonnelCode`, `SourceRecordId`).
- MUST NOT expose sensitive personal data (such as faculty bank account numbers or confidential employer evaluations) in unauthorized API responses, general exports, or plaintext logs.
- MUST NOT assume every Senior Project PDF has a valid page 3 for Abstract compilation without checking page count, encryption, and parseability first.
- MUST NOT give a domain entity property a bare public setter if an invalid value or unrecorded state transition would break a business rule — gate mutations through validated domain methods.
- MUST NOT put business logic in a controller, in a Vue page's inline script beyond simple UI state, or directly in a MediatR pipeline behaviour.
- MUST NOT add ad-hoc `Roles`/`Policies` constants — use the `module.action` permission convention and resource-ownership checks.
- MUST NOT expose the internal integer `Id` in any API surface — use `ExternalId`.

## Before you start any change

1. Read [known-issues.md](known-issues.md) — check open technical risks and unconfirmed requirement areas.
2. Read [roadmap.md](roadmap.md) — check which phase this change belongs to and whether any `Q01–Q18` stakeholder question blocks it.
3. Check [domain.md](domain.md) — use the established Thai/English terms for **CU-PFIS** entities and workflows.
4. Read the corresponding `requirements/{Context}/{feature}.md` file in full.
