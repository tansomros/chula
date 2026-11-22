# Roadmap — CU-PFIS (Chula Pharmacy Faculty Information System)

Living document for **CU-PFIS** (**ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย** / **Chula Pharmacy Faculty Information System**). Tick items as they actually land (not when planned) and keep the "Status" line per phase current — this is the single file a new session should read to know what's confirmed, what's pending stakeholder decisions, what's in progress, and what's next.

---

## Confirmed Decisions & Proposals Register (`D01–D10`, `O01–O03`)

- **D01 — Standalone Core Web Application (`CU-PFIS`):** Develop a new web application (**CU-PFIS**) for the Faculty of Pharmaceutical Sciences, Chulalongkorn University, separate from the existing **CUPPEP** system.
- **D02 — Master Data Ownership & CUPPEP Input Split (`A02`):** **CU-PFIS** acts as the Master System for staff/faculty workflows, verification, and reporting. Students enter all their own data (CV details, achievements, license checklist, 1st-round employment) via new/existing input screens in **CUPPEP**, which **CU-PFIS** pulls to display and verify — minimizing concurrent write collisions.
- **D03 — Workspace & Spec-Driven Workflow:** Project root is `D:\Projects\Chula\CU-PFIS` (inside repository `D:\Projects\Chula`), using `.doc/` for source documents (`req071169.pdf`), `.ai/` for architecture & AI agent rules, and `requirements/` for structured Thai feature specifications.
- **D04 — Official System Name:**
  - **ชื่อระบบภาษาไทย:** ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย
  - **ชื่อภาษาอังกฤษ:** Chula Pharmacy Faculty Information System
  - **ตัวย่อ หรือชื่อเรียกสั้น:** `CU-PFIS`
- **D05 — Release Phasing (`A01`):** Phase 1 delivers the **Frontend UX/UI structure (`src/vuewebui`)** plus working **Faculty CV (`Faculty`)** and **Student CV (`Students`)** modules. Phase 2 delivers **Training & Budget (`Training`)**, followed by the remaining modules (`Students` sub-workflows, `ProfessionalLicense`, `SeniorProjects`, `Reporting`).
- **D06 — Department-Scoped Staff Roles & PDPA Consent (`A03`, `A18`):** `Staff` permissions are scoped by **Department / Division** (`DepartmentId`). Students/Alumni do not log into CU-PFIS for now (CUPPEP only). All CU-PFIS users must accept a **First-Time PDPA Consent screen** on initial login, recorded persistently in the database.
- **D07 — Training Option B, Fiscal Year Budget & Future Quota Borrowing (`A04`, `A05`, `A06`):**
  - Confirmed **Option B** (faculty submit directly in CU-PFIS) **plus Excel import** for historical training data migration.
  - Budget is **deducted (`Consume`) only after Dean approval (`Approved`)** so unapproved requests can be cancelled/released cleanly.
  - Budget follows **Thai Fiscal Year (1 Oct – 30 Sep)** with configurable annual quota (default `10,000 THB/person/year`), resetting each fiscal year.
  - **Future Quota Borrowing:** Faculty may borrow from their own next fiscal year's quota (e.g., using 10,000 + borrowing 5,000 in FY 2570 leaves 5,000 in FY 2571), tracked via append-only `TrainingBudgetLedger`.
- **D08 — Employment Recheck, No Certificate Uploads & License Denominator (`A07`, `A08`, `A09`):**
  - Staff manually recheck Chula employment data against CUPPEP data displayed in CU-PFIS and edit/confirm directly in CU-PFIS (syncing back to CUPPEP deferred).
  - No certificate or license file uploads required; students self-confirm items from a checklist in CUPPEP.
  - License pass rate `%` baseline denominator = **total graduates (จำนวนผู้สำเร็จการศึกษา)** (retakes not counted unless later requested).
- **D09 — Senior Project Publish, Abstract Auto-Detect & Utilization Reminders (`A10`, `A11`, `A12`):**
  - Published Senior Projects are searchable/viewable by **Staff, Faculty, and Students only** (not public).
  - Abstract Book = **Cover Page (page 1)** followed directly by each project's Abstract page; auto-detect Abstract on **pages 3–5** by matching centered `"Abstract"` on the first line (with staff preview/manual override fallback for scanned/non-standard PDFs).
  - Utilization reminders start **1 year after Publication Date**; legacy projects support a **manual Staff trigger** (subsequent reminders count from `LastRemindedAt`); stop after **3 reminders / 3 years** or immediately once filled.
- **D10 — Full Bilingual Scope & Sizing (`A13`, `A15`, `A17`):**
  - **Full TH/EN bilingual support** across UI, key database fields, exports, and notification emails.
  - 10-year historical data (currently on paper/free websites) will be entered/verified manually by staff (plus Excel import for training).
  - Expected concurrency **≤ 10 concurrent users**; expandable hosting storage for PDFs.
- **D11 — Monorepo Boundaries & Two-API Integration (`CU-PPEP Integration API`):**
  - Under `D:\Projects\Chula`: `CU-BCP/` is an unrelated existing site (**DO NOT TOUCH**); `CU-PFIS/` is this `.NET 10` + PostgreSQL Master System; `CU-PPEP/` is the legacy `.NET Framework` + MS SQL Server student web app.
  - Confirmed **Option A (Two APIs):** Build a separate **`.NET 10` Clean Architecture `CU-PPEP Integration API`** reading CU-PPEP's MS SQL Server via read-only SQL Views (`vw_pfis_*`). `CU-PFIS` pulls data via **One-Way Pull + Incremental Sync (`?updatedSince=...`)** through `ICuppepDataSource` into `ImportBatch` → `StagingRecord` → `DataConflictQueue`.
- **P01 — Non-destructive External Sync:** External imports from CUPPEP or Google Sheets must preserve provenance (`SourceSystem`, `SourceRecordId`, `ImportedAt`) and route conflicts with staff-edited records in `CU-PFIS` to `DataConflictQueue` rather than overwriting silently.
- **O01 — CUPPEP Extension Screens & Live DB Wiring (In Progress):** Legacy `chula_pps` schema mapped (`View_Student_Bio`, `Persons`, `RPT_Student_PracticeHistory`), SQL contract scripts (`vw_pfis_*` + `PFIS_*` extension tables) created, and `.NET 10` Clean Architecture **`CU-PPEP Integration API`** scaffolded at `D:\Projects\Chula\CU-PPEP\IntegrationApi\`. Remaining: apply SQL scripts to `chula_pps`, add new student self-entry WebForms screens in `Chula-PPS`, and implement `HttpCuppepDataSource : ICuppepDataSource` in `CU-PFIS`.
- **O02 — Dashboard KPIs & Report Templates (Open — `A14`):** To be finalized in the next stakeholder meeting (collect sample Word/PDF/Excel templates).
- **O03 — Scopus Data Source Channel (Open — `A16`):** Confirm whether Chula institutional Scopus API credentials (API Key / InstToken) are available, or use ORCID / Scopus BibTeX-CSV import as interim/fallback.
- **Architectural Baseline (adopted from team template):**
  - Clean Architecture + CQRS (MediatR) + EF Core (PostgreSQL) + Vue 3 (dedicated `create/edit/view/list` routes, no modal-only CRUD).
  - Dual-key pattern on all entities (`int Id` internal, `Guid ExternalId` UUIDv7 external).
  - Lower-kebab-case API routes end-to-end.
  - Local PostgreSQL (`chula` and `chula-test`) for development and functional tests (no Testcontainers/Docker dependency).

---

## Non-negotiable: Data Integrity, Sync Safety, Budget Accuracy & PDPA

This system underlies faculty CVs, academic training budgets, graduate employment statistics, license pass rates, and senior project publications. These rules apply across every phase as blocking requirements:

1. **Training budget balances are never mutated in place** — always derived from the append-only `TrainingBudgetLedger` (reserve, consume on Dean approval, release on cancel, and future-year advance borrowing `BorrowAdvance` / `CarryForwardDeduction`). Annual budget caps (default `10,000 THB/person/fiscal year`, 1 Oct – 30 Sep) must be configurable per fiscal year (`TrainingBudgetPolicy`), never hardcoded.
2. **External data imports never silently overwrite staff-verified data** — re-importing from CUPPEP or Google Sheets must be idempotent, match by stable IDs (never by name alone or Sheet row number), preserve provenance (`SourceSystem`, `SourceRecordId`, `ImportedAt`), and route conflicts with staff-edited records to a review queue.
3. **Verification and Approval are distinct states** — especially in Training ("Staff verifies data accuracy" vs. "Staff records Dean's approval after physical signature — triggering budget deduction") and Student Achievements ("Submitted in CUPPEP" vs. "Verified by staff to show on CV").
4. **Optimistic concurrency (`xmin`) on every mutable entity** — conflicting writes fail loudly, never silently overwrite.
5. **Sensitive personal data protection & PDPA consent (`A18`)** — require and persist First-Login PDPA Consent; enforce department-scoped Staff permissions (`A03`); strictly permission-gate bank account numbers (training reimbursement) and employer evaluations (ผู้ใช้บัณฑิต) and exclude them from general exports/logs.
6. **Handler-level unit tests + functional tests are merge-blocking** for anything touching training budgets (including future quota borrowing), approval/verification state transitions, external data reconciliation, or access control.

---

## Phase 0 — Requirement Analysis & Project Setup

**Status: nearly complete (2026-11-21 — `Q01–Q18` answered via `A01–A18`; awaiting `A14` templates/KPIs & `O01`/`O03` technical details).**

- [x] Initial requirement analysis from `req071169.pdf` (12 pages) by PM, BA, and Tech Lead (consolidated into `.ai/roadmap.md`, `.ai/known-issues.md`, and `.ai/domain.md`).
- [x] Adapt `.ai/` documentation (`architecture.md`, `domain.md`, `coding-rules.md`, `ai-agent-guide.md`, `prompt-templates.md`, `dev-workflow.md`, `roadmap.md`, `known-issues.md`) for **CU-PFIS (Chula Pharmacy Faculty Information System)**.
- [x] Adapt `requirements/` structure (`README.md`, `_template.md`, bounded-context folders, and complete initial requirement specs across all 9 bounded contexts) and root `README.md`.
- [x] Finalize official project name: **CU-PFIS** (**ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย** / **Chula Pharmacy Faculty Information System**).
- [x] **Stakeholder Q&A (`Q01–Q18` → `A01–A18`):** recorded confirmed answers across `.ai/` and `requirements/` (2026-11-21).
- [ ] **Next Stakeholder Meeting:** finalize Dashboard KPIs & Word/PDF/Excel report templates (`A14`), confirm CUPPEP technical integration channel (`O01`), and confirm Scopus API credentials vs. import (`O03`).

---

## Phase 1 — Core Foundation, Frontend UX/UI, Faculty CV & Student CV (`A01` Priority)

**Status: in progress (2026-11-21).**

- [x] Scaffold Clean Architecture solution (`chula.sln`: `Domain`, `Application`, `Infrastructure`, `API`, and test projects `Domain.UnitTests`, `Application.FunctionalTests`).
- [x] Base infrastructure: `EntityBase` (Dual-key `Id` + `ExternalId`, `xmin` concurrency), `SoftDeletableEntityBase` (global query filter), `IHasActiveFlag`, `AuditLog` interceptor (`AuditableEntitySaveChangesInterceptors`).
- [x] Security & Auth foundation: `Permission` / `RolePermission` (`module.action`), `AuthorizationBehaviour`, Role definitions (`Admin`, `Staff`, `Faculty`), and `CurrentUserService`.
- [x] `ReferenceData` context: `AcademicYear` (supporting 10+ years history), `Department`, `Major`, `AcademicPosition`, `AdmissionRound` (2-tier hierarchy), `DocumentTemplate`, plus initial `AcademicYears` CQRS vertical slice.
- [x] Complete `Security` extensions per `A03`/`A18`: Department-scoped Staff access (`DepartmentExternalId` claim), First-Login **PDPA Consent** (`PdpaConsent`) append-only entity + CQRS (`GET /api/pdpa-consents/me`, `POST /api/pdpa-consents`), and `DevelopmentHeaderAuthMiddleware` for local RBAC testing.
- [x] Complete Phase 1 `ReferenceData` read slices (`Departments`, `Majors`, `AcademicPositions`, `AdmissionRounds`), initial EF Core PostgreSQL migration (`InitialPhase1Foundation`), idempotent `ChulaDbContextInitialiser` seed, and `Application.FunctionalTests` harness (`chula-test`, configurable via `CHULA_TEST_CONNECTION_STRING`).
- [x] **Frontend UX/UI Foundation (`src/vuewebui`):** Vue 3 + Vuetify 3 shell, layout, navigation, CASL permission wiring, full **TH/EN bilingual (`vue-i18n`)** support (`A01`, `A15`), **First-Login PDPA Consent** gate (`A18`), and dedicated `list/view/create/edit` UX/UI pages for **Faculty CV** (8 sections + Scopus CSV import) and **Student CV** (11 sections + 2-tier Admission Round dropdown) with preview Pinia stores (`vite build` verified).
- [x] **Faculty CV (`Faculty`) — Phase 1 Backend (`A01`, `A15`, `A16`):** `FacultyMember`, `FacultyEducation`, `FacultyPublication` domain entities + EF configs + CQRS CRUD (`GET/POST/PUT /api/faculty-members`) + idempotent Scopus import (`POST /api/faculty-members/{externalId}/import-scopus`) + Dept-scoped Staff & Faculty own-CV enforcement + Unit & Functional Tests.
- [x] **Student CV (`Students`) — Phase 1 Backend (`A01`, `A02`, `A03`):** `Student`, `StudentInternship` domain entities with CUPPEP provenance & `IsStaffVerifiedEdit` protection + EF configs + CQRS CRUD (`GET/POST/PUT /api/students`) + `ICuppepDataSource` port (`D11`) + Unit & Functional Tests.
- [ ] **Phase 1 Final Wiring:** Connect `src/vuewebui` Pinia stores to live `CU-PFIS` API endpoints, configure local PostgreSQL password for `chula`/`chula-test` and pass all 7 functional tests, plus PDF export templates (`A14`) and bilingual email reminder job.

---

## Phase 2 — Training & Fiscal-Year Budget Ledger (`Training`)

**Status: pending.**

- [ ] `TrainingBudgetPolicy` (configurable annual limit per person per **Fiscal Year: 1 Oct – 30 Sep**, default 10,000 THB/year, `A06`).
- [ ] Append-only `TrainingBudgetLedger` supporting `Reserve`, `Consume` (triggered **after Dean approval**, `A05`), `Release` (on cancellation), and **Future Fiscal-Year Quota Borrowing** (`BorrowAdvance` / `CarryForwardDeduction`, `A06`).
- [ ] `TrainingRequest` supporting all 4 training types (`AcademicFunded`, `NonAcademicFunded`, `FacultyOrganized`, `ShortTermOrOverseas`) via **Option B** (direct faculty entry in CU-PFIS, `A04`).
- [ ] Training workflow: submit → staff verification / revision request → PDF memo generation & bilingual email for Dean signature → record Dean approval → deduct budget ledger & reflect on `FacultyCv`.
- [ ] **Historical Training Excel Import (`A04`):** structured Excel migration tool with validation and non-destructive provenance tracking.
- [ ] Training budget & attendance reports (PDF/Word/Excel export).

---

## Phase 3 — Student Achievements, Employment Recheck, Employer Evaluation & License (`Students` & `ProfessionalLicense`)

**Status: pending.**

- [ ] CUPPEP & Google Sheet integration staging (`ImportBatch`, `StagingRecord`, `DataConflictQueue`).
- [ ] `StudentAchievement` workflow: sync self-confirmed items from CUPPEP (no file attachments, `A08`) → staff review (request revision / verify) → publish to `StudentCv`.
- [ ] `GraduateEmployment` workflow (`A07`): display CUPPEP 1st-round data in CU-PFIS → Staff manually rechecks against Chula Final data and edits/confirms authoritative values in CU-PFIS without destructive overwrite on CUPPEP re-import (push-back to CUPPEP deferred).
- [ ] `EmployerEvaluation` (ผู้ใช้บัณฑิต): import from Google Form/Sheet (stable ID, never row number), match to graduate, search/sort/export.
- [ ] `LicenseExamResult` (`CC1`, `CC2`, `IP1`, `IP2`, `PC1`, `PC2`): sync self-confirmed checklist from CUPPEP (no file attachments, `A08`), cohort pass count & `%` using **total graduates** denominator (`A09`), and reporting.

---

## Phase 4 — Senior Projects, Smart Abstract Book & Utilization Tracking (`SeniorProjects`)

**Status: pending.**

- [ ] `SeniorProject` CRUD (individual/group), linking students (`SeniorProjectMember`) and advisors (`SeniorProjectAdvisor`), connected to `StudentCv`.
- [ ] Actual PDF file upload, validation (page count, encryption, MIME), storage, and `Publish` status restricted to **Staff, Faculty, and Students only** (`A10`).
- [ ] Annual `AbstractCompilation` (`A11`): auto-detect Abstract page within **pages 3–5** (matching centered `"Abstract"` on first line) with manual staff preview/override for scanned/non-standard PDFs, and compile into PDF with **Cover Page (page 1)** followed directly by each project's Abstract page.
- [ ] `ProjectUtilization` tracking & bilingual reminder emails (`A12`): start **1 year from Publication Date**; manual Staff trigger button for legacy projects (subsequent reminders count from `LastRemindedAt`); stop immediately when filled or after **3 reminders / 3 years**.

---

## Phase 5 — Dashboard, Cross-Module Reports & 10-Year Historical Data (`Reporting`)

**Status: pending (KPIs & templates pending next meeting per `A14`).**

- [ ] Executive/Staff Dashboard with confirmed KPIs (`A14`) and academic-year filters (10+ years).
- [ ] Custom/template-based bilingual exports (PDF, Word, Excel) across modules (`A14`, `A15`).
- [ ] Historical data manual entry UX & verification tools for 10-year legacy paper/web records (`A13`), alongside Excel import for training (`A04`).

---

## Stakeholder Q&A Register (`Q01–Q18` / `A01–A18` — Answered 2026-11-21)

| Code | Topic | Confirmed Answer (`A01–A18`) | Status |
|---|---|---|---|
| **Q01 / A01** | Release Phasing | Phase 1 must deliver **Frontend UX/UI structure** + usable **Faculty CV** and **Student CV** modules first; followed by **Training**, then remaining modules. | Confirmed |
| **Q02 / A02** | CUPPEP Data Ownership | Students enter all their own data (CV, achievements, etc.) in **CUPPEP** (create new input screens in CUPPEP); CU-PFIS pulls to display/verify, avoiding data collisions. | Confirmed (tech channel `O01` open) |
| **Q03 / A03** | Roles & Access | **Staff** permissions must be separated by **Department/Division**. **Students/Alumni** do not log into CU-PFIS yet (use CUPPEP only for now). | Confirmed (see `A10` alignment note) |
| **Q04 / A04** | Training Input Channel | Confirmed **Option B** (faculty fill directly in CU-PFIS), **plus Excel import** for historical data migration. | Confirmed |
| **Q05 / A05** | Training Budget Timing | Budget is **deducted after Dean signs/approves**, because before approval the training/reservation can still be cancelled. | Confirmed |
| **Q06 / A06** | Budget Year & Borrowing | Default `10,000 THB/person/year` (configurable), following **Fiscal Year (1 Oct – 30 Sep)**, reset annually. **Faculty can borrow from their own future fiscal year's quota** (e.g., borrowing 5,000 in FY 2570 leaves 5,000 in FY 2571). | Confirmed (edge-case cap note in `known-issues.md`) |
| **Q07 / A07** | Chula Final Employment Data | Staff manually recheck Chula data by eye against CUPPEP data shown in CU-PFIS, and edit/confirm directly in CU-PFIS. Syncing confirmed data back to CUPPEP is deferred. | Confirmed |
| **Q08 / A08** | Achievements & License Proof | **No certificate/license file attachments.** Provide a checklist/list for students to self-confirm awards/exams passed (in CUPPEP). | Confirmed |
| **Q09 / A09** | License Pass Rate Formula | Baseline denominator = **total graduates (จำนวนผู้สำเร็จการศึกษา)**. Retakes not in original scope. | Confirmed |
| **Q10 / A10** | Senior Project "Publish" | Published Senior Projects can be searched/viewed by **Staff, Faculty, and Students only** (not public). | Confirmed (student access channel vs `A03` noted) |
| **Q11 / A11** | Abstract Page & Book Format | Abstract appears between **pages 3–5** with centered `"Abstract"` on the first line (auto-detect + manual fallback). Compiled book = **Cover page (page 1)** followed directly by each project's Abstract page. | Confirmed |
| **Q12 / A12** | Utilization Reminder Timeline | Count **1 year from Publication Date**. For older legacy projects, provide a **manual Staff trigger button** and count subsequent reminders from **last reminder date**. Max **3 years / 3 reminders**, or stop immediately once advisor fills it in. | Confirmed |
| **Q13 / A13** | 10-Year Historical Data | Currently stored on paper or free external websites; **Staff will enter and verify manually** (plus Excel import for training per `A04`). | Confirmed |
| **Q14 / A14** | Dashboard KPIs & Templates | **Pending next stakeholder meeting.** | Open (`O02`) |
| **Q15 / A15** | Bilingual Scope (TH/EN) | **Full TH/EN bilingual support:** UI, key database data fields, exports, and notification emails. | Confirmed |
| **Q16 / A16** | Scopus Integration | Client wants faculty Scopus CV/publication data in CU-PFIS; technical integration channel (Elsevier Scopus API vs. ORCID/BibTeX import) to be evaluated and confirmed. | Open (`O03`) |
| **Q17 / A17** | Non-Functional & Sizing | **≤ 10 concurrent users**; PDF sizes vary, hosting storage is sufficient and expandable (billable if expanded). | Confirmed |
| **Q18 / A18** | PDPA & Sensitive Fields | Require a **First-Login PDPA Consent screen** stored persistently in DB (in addition to role/field protection for bank accounts & employer evaluations). | Confirmed |

---

## Artifacts & Samples to Request from Stakeholders

1. **Templates & Forms:** ตัวอย่าง CV อาจารย์/นิสิต, แบบฟอร์มขออนุมัติอบรม, บันทึกข้อความเสนอคณบดี, รายงานสรุปงบประมาณ และรายงานประจำปี (Word / PDF / Excel)
2. **Google Form / Google Sheet:** โครงสร้างคอลัมน์จริงของแบบประเมินผู้ใช้บัณฑิตและข้อมูลการอบรมเดิม (ใช้ข้อมูลที่ปกปิดข้อมูลส่วนบุคคลแล้ว)
3. **Sample Datasets:** ตัวอย่างข้อมูลนิสิต ผลงานนิสิต ข้อมูลการได้งานทำรอบคณะ และไฟล์ชุด Final จากจุฬาฯ
4. **Sample Senior Project PDFs:** ไฟล์ PDF โครงงานนิสิตจริงจากหลายปีการศึกษา และตัวอย่างเล่มรวม Abstract เพื่อตรวจสอบโครงสร้างหน้าที่ 3
5. **CUPPEP Technical Context:** โครงสร้างฐานข้อมูล/API ของระบบ CUPPEP ช่องทางการเชื่อมต่อ และรหัสบุคคลที่ใช้จับคู่ (`StudentCode`, `PersonnelCode`)
