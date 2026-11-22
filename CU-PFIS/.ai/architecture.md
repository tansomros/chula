# Architecture — CU-PFIS

## What this system is

**CU-PFIS** (**Chula Pharmacy Faculty Information System** / **ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย**) is the core web application for the **Faculty of Pharmaceutical Sciences, Chulalongkorn University**. It serves as the **Master System (ระบบหลักในการดูแลข้อมูล)** for managing:

- **Faculty CV (`Faculty`)** — academic profiles, positions, education history, publications (with optional Scopus integration), and training history.
- **Training & Academic Budget (`Training`)** — 4 training categories, annual per-person budget tracking (e.g., 10,000 THB/person/year configurable rule), staff verification vs. Dean approval workflows, PDF document generation, and email notifications.
- **Student Profile, Achievements, Employment & Employer Evaluation (`Students`)** — student CVs, verified student achievements/awards, graduate employment reconciliation (comparing faculty round from CUPPEP against Chula's central Final dataset), and employer evaluations (ผู้ใช้บัณฑิต).
- **Professional License (`ProfessionalLicense`)** — tracking pharmacy license examination results (`CC1`, `CC2`, `IP1`, `IP2`, `PC1`, `PC2`) by cohort/academic year.
- **Senior Projects & Utilization (`SeniorProjects`)** — individual/group senior projects, actual PDF storage, publication control, automated annual Abstract Book compilation (extracting page 3 of project PDFs), and 1-to-3-year post-completion utilization reminders.
- **Reporting & Dashboard (`Reporting`)** — bilingual (TH/EN) support, at least 10 years of historical data by academic year, KPI dashboards, and template-based PDF/Word/Excel exports.

**CU-PFIS** operates alongside **CUPPEP** (the existing pharmacy professional practice system) and external sources (**Google Form/Sheet/Drive**, **Scopus**). Data accuracy, source traceability, conflict protection (never silently overwriting staff-verified data on re-import), budget integrity, and PDPA/privacy protection are the top design priorities — see the "Non-negotiable" section of [roadmap.md](roadmap.md).

## Repository & Two-API Integration Architecture (`D:\Projects\Chula`)

The parent repository `D:\Projects\Chula` groups three Faculty of Pharmaceutical Sciences projects:

- `CU-BCP/` — Existing Pharmacy Board Certification website (**strictly out of scope — DO NOT MODIFY**).
- `CU-PFIS/` — This Master System (`.NET 10` Clean Architecture + PostgreSQL + Vue 3).
- `CU-PPEP/` — Existing legacy web app (`.NET Framework` + MS SQL Server) where students log in and fill new/existing student input screens (`A02`), paired with a **separate `.NET 10` Clean Architecture `CU-PPEP Integration API`** (Decision `D11`).

### CU-PPEP ↔ CU-PFIS Data Flow (Option A — Confirmed)
```text
[CU-PPEP Legacy Web (.NET Framework)] ──writes──► [CU-PPEP MS SQL Server]
                                                        │ (Read-only SQL Views: vw_pfis_*)
                                                        ▼
                                     [CU-PPEP Integration API (.NET 10 Clean Architecture)]
                                                        ▲
                                                        │ HTTPS + API Key / Service Token
                                                        │ One-Way Pull + Incremental Sync (?updatedSince=...)
                                                        │ (Scheduled Job + Manual Staff Sync)
[CU-PFIS API (.NET 10 Clean Arch)] ◄──ICuppepDataSource─┘
       │
       ▼ (ImportBatch → StagingRecord → DataConflictQueue)
[CU-PFIS PostgreSQL]
```
- **Contract isolation:** `CU-PFIS` never connects directly to raw MS SQL Server tables; it consumes `CU-PPEP Integration API` via `ICuppepDataSource`.
- **Safe future extensibility:** If pushing confirmed statuses back to `CU-PPEP` (e.g., achievement revision notes or confirmed employment per `A07`) is later approved, dedicated write endpoints can be added cleanly to `CU-PPEP Integration API`.

## Stack (Baseline Template Convention)

> **Note:** Until the customer finalizes hosting/identity constraints in the requirement confirmation meeting, the repository uses our standard Clean Architecture + Vue 3 stack template below.

- **Backend**: C# / .NET 10, ASP.NET Core Web API, EF Core 10 + Npgsql (PostgreSQL), MediatR 14 (CQRS), FluentValidation, AutoMapper, Swashbuckle/NSwag (OpenAPI).
- **Frontend**: Vue 3 (Composition API, `<script setup>`, JavaScript), Vuetify 3 (Vuexy admin template), Pinia, vue-router 4 via file-based routing (`unplugin-vue-router`), `vue-i18n` (for TH/EN bilingual support), CASL for permission-based UI.
- **Auth**: External OIDC/SSO or CUPPEP-compatible authentication (pending confirmation in requirement meeting; supports local FakeAuth in Development).
- **Solution layout**: `chula.sln` / `.slnx`, central package management (`Directory.Packages.props`), `src/Domain`, `src/Application`, `src/Infrastructure`, `src/API`, `src/vuewebui` (frontend), `tests/*`.

## Clean Architecture layering

Dependency direction (enforced by `.csproj` references — do not violate):

```text
Domain          → no project dependencies
Application     → Domain
Infrastructure  → Application, Domain
API             → Application, Infrastructure
```

- **Domain** (`src/Domain`): entities, enums, value objects, domain exceptions. No EF, no MediatR request types (only `BaseEvent : INotification` for domain events). Entities enforce their own invariants (guard clauses via `Ardalis.GuardClauses`), not just property bags — see [coding-rules.md](coding-rules.md).
- **Application** (`src/Application`): CQRS use cases under `Features/{Context}/{Feature}/{Commands|Queries}/{UseCase}/`, each folder containing a Command/Query record + FluentValidation validator + MediatR handler. Talks to persistence only through `IChulaDbContext` (defined here, implemented in Infrastructure). Pipeline behaviours (`Common/Behaviours/`): `UnhandledExceptionBehaviour` → `AuthorizationBehaviour` → `ValidationBehaviour` → `PerformanceBehaviour` → `LoggingBehaviour`.
- **Infrastructure** (`src/Infrastructure`): `ChulaDbContext` (EF Core, PostgreSQL, snake_case naming via `EFCore.NamingConventions`), `IEntityTypeConfiguration<T>` classes under `Data/Configurations/`, migrations under `Data/Migrations/`, save-changes interceptors under `Data/Interceptors/`, external adapters (CUPPEP sync, Google Sheet importer, PDF Abstract extractor, Scopus client, file storage).
- **API** (`src/API`): thin controllers under `Controllers/` — inject `Mediator`, call `Send`, return the result. No business logic here. Global exception handling via `Filters/ApiExceptionFilterAttribute`.

## Bounded-context folder map

`Domain/Entities`, `Application/Features`, `Infrastructure/Data/Configurations`, and `requirements/` are organized by the following bounded contexts:

- `ReferenceData/` — `AcademicYear` (ปีการศึกษา), `Department` (ภาควิชา/หน่วยงาน), `AcademicPosition` (ตำแหน่งทางวิชาการ), `AdmissionRound` (รอบรับเข้า), `DocumentTemplate` (แม่แบบรายงาน/เอกสาร), and lookup tables.
- `Faculty/` — `FacultyMember` (ข้อมูลอาจารย์), `FacultyEducation` (วุฒิการศึกษา), `FacultyPublication` (ผลงานตีพิมพ์ / Scopus sync), `FacultyCvExport` (การส่งออก CV อาจารย์และแจ้งเตือนอัปเดต).
- `Training/` — `TrainingRequest` (คำขอ/ประวัติการอบรม 4 ประเภท), `TrainingParticipant` (ผู้เข้าร่วมอบรม), `TrainingBudgetPolicy` (วงเงินงบประจำปี เช่น 10,000 บาท/คน/ปี), `TrainingBudgetLedger` (append-only ledger สำหรับกันงบ/ตัดงบ/คืนงบ), `TrainingApprovalHistory` (ประวัติการตรวจความถูกต้องของเจ้าหน้าที่และการแจ้งอนุมัติหลังคณบดีลงนาม).
- `Students/` — `Student` (ข้อมูลนิสิตและ CV นิสิต), `StudentAchievement` (ผลงานและรางวัลนิสิต พร้อมสถานะตรวจรับรอง), `GraduateEmployment` (ข้อมูลการได้งานทำรอบคณะเทียบกับชุด Final จุฬาฯ), `EmployerEvaluation` (ข้อมูลผู้ใช้บัณฑิตจาก Google Form/Sheet).
- `ProfessionalLicense/` — `LicenseExamResult` (ผลสอบใบประกอบวิชาชีพ `CC1`, `CC2`, `IP1`, `IP2`, `PC1`, `PC2` รายบุคคลและสรุปร้อยละตามรุ่น).
- `SeniorProjects/` — `SeniorProject` (โครงงานนิสิตรายบุคคล/กลุ่ม), `SeniorProjectMember`, `SeniorProjectAdvisor`, `SeniorProjectFile` (metadata ของไฟล์ PDF โครงงานจริงและสถานะเผยแพร่), `AbstractCompilation` (การรวมเล่มหน้า Abstract รายปี), `ProjectUtilization` (การติดตามการใช้ประโยชน์และการแจ้งเตือนปีที่ 1–3).
- `Integration/` — `ImportBatch`, `StagingRecord`, `DataConflictQueue` (พักข้อมูลที่นำเข้าจาก CUPPEP / Google Sheet / ไฟล์จุฬาฯ ที่ขัดแย้งกับข้อมูลที่เจ้าหน้าที่แก้ไขแล้วในระบบหลัก เพื่อรอการตรวจสอบและตัดสินใจ ไม่เขียนทับอัตโนมัติ).
- `Reporting/` — Dashboard KPI queries, cross-module annual reports, PDF/Word/Excel export services.
- `Security/` — `Permission` (`module.action` catalog), `RolePermission` (Admin, Staff/เจ้าหน้าที่, Faculty/อาจารย์, and resource-ownership policies).
- `Domain/Common/` — cross-cutting base types: `EntityBase`, `SoftDeletableEntityBase`, `IHasActiveFlag`, `AuditLog`, `AuditAction`.

## CQRS / MediatR conventions

- One folder per use case: `Features/{Context}/{Feature}/Commands/{VerbNoun}/` or `.../Queries/{GetNoun}/`.
- Command/Query, Validator, and Handler live together in that folder (`{UseCase}Command.cs` for Request+Handler, `{UseCase}Validator.cs` in its own file).
- Controllers call `Mediator.Send(...)` and nothing else.

## Data model & integration conventions

- **Dual key**: every entity has an internal `int Id` (joins/FKs, never exposed) and a `Guid ExternalId` (UUIDv7, the only identifier exposed via API routes/DTOs).
- **Stable person/record matching**: cross-system records from CUPPEP, Chula Central, or Google Sheets must match on stable identifiers (e.g., `StudentCode`, `PersonnelId`, `SourceSystem` + `SourceRecordId`), never by full name alone.
- **Staging & Non-destructive Sync**: importing data from CUPPEP, Google Sheets, or Chula Final datasets must preserve `SourceSystem`, `SourceRecordId`, and `ImportedAt`. If an incoming update conflicts with a field already verified/edited by staff in **CU-PFIS**, it must be queued/flagged for review rather than silently overwriting the master record.
- **Audit fields & AuditLog**: `CreatedBy`, `LastModifiedBy`, `CreatedOn`, `LastModified` on every entity via `EntityBase`, plus property-level `{old, new}` diffs recorded to the append-only `AuditLog` table by `AuditableEntitySaveChangesInterceptors`.
- **Optimistic concurrency**: every mutable entity carries the Postgres `xmin` system column as a shadow-property concurrency token (`ConfigureEntityBase<T>()`).
- **Money/budget fields are never mutated in place**: training budget usage and remaining balance are derived from the append-only `TrainingBudgetLedger` (reserve, consume, release/refund) — never a single mutable balance column.
- **File storage separation**: PDF files (Senior Projects, Training approval memos, Abstract books) are stored in managed file/object storage with metadata (`FileName`, `ContentType`, `FileSize`, `Sha256Hash`, `PageCount`, `StoragePath`) in database tables.
- **Permissions & PDPA**: enforced via `[RequirePermission("module.action")]` and resource ownership (e.g., a faculty member editing their own CV vs. staff managing all records). Sensitive personal data (such as bank account numbers for training reimbursement or supervisor evaluations) must be strictly scoped and excluded from general exports/emails unless explicitly authorized.

## Frontend architecture

- Dedicated `create/`, `edit/[id]`, `view/[id]`, `list/` routes per feature (not modal dialogs) — file-based routing under `src/pages/{feature}/`.
- Bilingual support (`TH` / `EN`) via `vue-i18n` for UI labels, forms, and reports.
- One HTTP client generated/standardized against the backend's lower-kebab-case OpenAPI routes.
- CASL permission rules driven by the backend's real permission system (`module.action`).

## Where things live — quick index

| Concern | Path |
|---|---|
| Original Client Scope Docs | `.doc/` (`req071169.pdf`, `CuProjectName.md`) |
| Feature Requirements (Thai) | `requirements/{Context}/{feature}.md` |
| Domain entities | `src/Domain/Entities/{Context}/` |
| Domain base types | `src/Domain/Common/` (`EntityBase`, `SoftDeletableEntityBase`, `IHasActiveFlag`, `AuditLog`) |
| CQRS use cases | `src/Application/Features/{Context}/{Feature}/{Commands\|Queries}/{UseCase}/` |
| Pipeline behaviours | `src/Application/Common/Behaviours/` |
| DbContext + interface | `src/Infrastructure/Data/ChulaDbContext.cs`, `src/Application/Common/Interfaces/IChulaDbContext.cs` |
| EF configurations | `src/Infrastructure/Data/Configurations/{Context}/` |
| Migrations | `src/Infrastructure/Data/Migrations/` |
| API controllers | `src/API/Controllers/` |
| Frontend pages | `src/vuewebui/src/pages/{feature}/{create,edit,view,list}/` |

See also: [coding-rules.md](coding-rules.md), [ai-agent-guide.md](ai-agent-guide.md), [domain.md](domain.md), [known-issues.md](known-issues.md), [roadmap.md](roadmap.md).
