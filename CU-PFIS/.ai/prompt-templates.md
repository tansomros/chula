# AI Prompt Templates — CU-PFIS

Reusable prompts for kicking off each stage of feature work in **CU-PFIS** (**Chula Pharmacy Faculty Information System** / **ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย**) with an AI agent, so nobody has to retype project context every session. Paste the **shared preamble** once at the start of a chat, then paste the template matching your task, and fill in the requirement at the bottom — usually by pointing at (or pasting) a file from [`requirements/`](../requirements/), which is where feature specs written by the team live (see [`requirements/README.md`](../requirements/README.md)).

If a template's assumptions turn out to be wrong or stale, update the template in the same change.

---

## Shared preamble (paste at the top of every session)

```text
You are working on CU-PFIS (Chula Pharmacy Faculty Information System /
ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย).

Before making any change:
1. Read .ai/known-issues.md (are there open risks or requirement gaps?) and
   .ai/roadmap.md (which phase is this, what does it depend on, and are any
   Q01–Q18 stakeholder questions still open for this feature?).
2. Read .ai/coding-rules.md and .ai/ai-agent-guide.md — these are binding
   conventions, not suggestions. Follow them exactly.
3. Check .ai/domain.md for established bilingual domain terms and workflows
   (Faculty, Training 4 types & budget ledger, Students, Achievements,
   Graduate Employment, Employer Evaluation, License Exam CC1/CC2/IP1/IP2/PC1/PC2,
   Senior Project PDF & Page 3 Abstract compilation).
4. If a requirements/{Context}/{feature}.md file is referenced below, read
   it in full before doing anything else — it is the primary spec written
   by the team in Thai. Treat its "Out of scope" section as a hard boundary,
   its "Money & Budget Impact" answer as authoritative on whether the ledger
   rule applies, and its "External Data Sync" section as authoritative on
   CUPPEP / Google Sheet / Chula Final reconciliation rules.

Non-negotiable (see .ai/roadmap.md "Non-negotiable" section):
- Never mutate a training budget or balance field in place — derive it from an
  append-only ledger (TrainingBudgetLedger), and never hardcode annual budget caps
  (e.g. 10,000 THB/person/year must be configurable via TrainingBudgetPolicy).
- Never silently overwrite staff-verified or staff-edited records when
  re-importing from CUPPEP, Google Sheets, or external datasets — route conflicts
  to a review queue with full provenance (SourceSystem, SourceRecordId, ImportedAt).
- Every mutable entity needs a concurrency token (xmin); conflicting writes must
  fail loudly, never silently overwrite.
- Protect sensitive personal data (bank account numbers, employer evaluations)
  at the API/query level and never log PII in plaintext logs.
- Validation is layered: FluentValidation + domain guard clauses
  (Ardalis.GuardClauses) + DB constraints.

When done:
- Run `dotnet build chula.sln` and `dotnet test chula.sln` (backend),
  `pnpm run lint` and `pnpm run build` (frontend, from src/vuewebui) —
  report the results, don't just claim success.
- Update .ai/known-issues.md and .ai/roadmap.md only if something you did
  actually changes their status.
- Stay within the scope of the task below.
```

---

## 1. Domain entity + EF configuration

```text
Design a new domain entity: {EntityName}.

Fields: {list fields, types, required/optional, and any business rules
  — e.g. "TrainingEndDate >= TrainingStartDate", "Amount >= 0"}
Relationships: {e.g. "belongs to FacultyMember (required)", "has many SeniorProjectMembers"}
Bounded context: {ReferenceData | Faculty | Training | Students |
  ProfessionalLicense | SeniorProjects | Integration | Reporting | Security
  — see .ai/architecture.md for the folder map}

Do:
- Entity class in src/Domain/Entities/{Context}/{EntityName}.cs, inheriting
  EntityBase (or SoftDeletableEntityBase + IHasActiveFlag where appropriate).
  Constructor takes required fields; enforce invariants with Ardalis.GuardClauses
  — don't leave a property with a bare public setter if an invalid value or
  unlogged state transition would break a business rule.
- EF configuration in src/Infrastructure/Data/Configurations/{Context}/
  {EntityName}Configuration.cs: explicit HasOne/HasForeignKey/OnDelete for
  every relationship (Restrict unless cascade is deliberately correct),
  explicit IsRequired()/HasMaxLength() for every scalar. Register the DbSet in
  ChulaDbContext and IChulaDbContext.
- If this entity represents a running budget balance or approval history, it
  must use an append-only ledger/history table, not a mutable column — ask me
  if you're not sure before implementing.

Requirement: {path to requirements/{Context}/{feature}.md}

Don't generate a migration — I'll run `dotnet ef migrations add` myself
(see .ai/dev-workflow.md) after reviewing the model.
```

---

## 2. CQRS feature (Application + API)

```text
Implement a {command|query} for {EntityName}: {CreateX | UpdateX | VerifyX |
  ApproveX | GetX | GetXs | ...}.

Behavior: {what it does, required/optional inputs, validation rules,
  permission code, and workflow state transitions}

Do:
- src/Application/Features/{Context}/{Feature}/{Commands|Queries}/{UseCase}/
  {UseCase}Command.cs (or Query.cs) — the IRequest<T> record + the
  IRequestHandler in the same file, decorated with
  [RequirePermission("{module}.{action}")].
- {UseCase}Validator.cs in the same folder — AbstractValidator<T>, its own file.
- Controller action on the matching {Feature}Controller — thin, only
  Mediator.Send + return. Route follows [Route("[controller]")] + lower-kebab-case.
- If this maps to/from a ViewModel, add explicit .ForMember(...) mappings
  for anything whose name doesn't match the entity by convention, and ensure
  sensitive fields (e.g. bank account numbers) are excluded from general list DTOs.

Requirement: {path to requirements/{Context}/{feature}.md}
```

---

## 3. Domain unit test

```text
Write Domain.UnitTests coverage for {EntityName}.

Cover:
- The constructor with all required fields set correctly.
- Every guard-clause invariant (null/empty strings, negative amounts,
  invalid date ordering, invalid state transitions) — one test case per
  invariant, not one giant test.

File: tests/Domain.UnitTests/Entities/{Context}/{EntityName}Tests.cs,
using NUnit and Shouldly.
Run `dotnet test tests/Domain.UnitTests/` and confirm they pass before
reporting done.
```

---

## 4. Application functional test

```text
Write Application.FunctionalTests coverage for {UseCase} ({EntityName}).

This runs against the real local cupharm-test PostgreSQL database (not
Testcontainers — see .ai/coding-rules.md) via WebApplicationFactory. Cover:
- The successful path (including audit/history/ledger side effects if applicable).
- Validation failure (missing required field or business rule violation).
- Authorization/permission check or concurrency conflict where applicable.
- NotFound, if this looks up an existing entity by ExternalId.

File: tests/Application.FunctionalTests/{Context}/{Feature}/{UseCase}Tests.cs
(mirror the Application/Features path 1:1).
Run `dotnet test tests/Application.FunctionalTests/` and confirm they pass.

Requirement: {path to requirements/{Context}/{feature}.md}
```

---

## 5. Frontend feature (Vue)

```text
Build the frontend pages for {feature}: {create | edit | view | list |
  all four}.

Do:
- Dedicated routes under src/vuewebui/src/pages/{feature}/
  {create,edit,view,list}/ — no dialog-based CRUD, no dead stub routes.
- Support bilingual (TH/EN) labels via vue-i18n where required by the spec.
- API calls in lower-kebab-case matching the real backend route (e.g.
  /training-requests, /senior-projects) — verify the real route and response
  shape against the actual controller before wiring up the call.
- Add data-testid="{feature}-{element}" attributes for Playwright e2e tests.
- Reuse existing shared components (ConfirmProgressDialog, ProgressDialog,
  TablePagination, AppTextField/AppSelect/etc.).

Requirement: {path to requirements/{Context}/{feature}.md}

When done: run `pnpm run lint` and `pnpm run build` from src/vuewebui and
confirm both pass clean.
```

---

## 6. Playwright e2e spec

```text
Write a Playwright e2e spec for {feature}: {flow to cover, e.g. "create →
  appears in list → verify → approve"}.

Do:
- src/vuewebui/e2e/pages/{feature}/{screen}.page.ts extending BasePage.
- src/vuewebui/e2e/specs/{feature}/{screen}.spec.ts, tagged @{feature} so it
  can be run in isolation (`pnpm exec playwright test --grep "@{feature}"`).
- Use data-testid="{feature}-{element}" selectors.

Run `pnpm test:e2e --grep "@{feature}"` and confirm it passes before reporting done.
```
