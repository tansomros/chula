# Roadmap — CU-PFIS (Chula Pharmacy Faculty Information System)

Living document for **CU-PFIS** (**ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย** / **Chula Pharmacy Faculty Information System**). Tick items as they actually land (not when planned) and keep the "Status" line per phase current — this is the single file a new session should read to know what's confirmed, what's pending stakeholder decisions, what's in progress, and what's next.

---

## Confirmed Decisions & Proposals Register (`D01–D04`, `P01–P02`, `O01–O02`)

- **D01 — Standalone Core Web Application (`CU-PFIS`):** Develop a new web application (**CU-PFIS**) for the Faculty of Pharmaceutical Sciences, Chulalongkorn University, separate from the existing **CUPPEP** system.
- **D02 — Master Data Ownership:** **CU-PFIS** acts as the primary/master system for managing and verifying data, while pulling selected data from CUPPEP (and potentially adding new student input screens in CUPPEP, subject to agreement).
- **D03 — Workspace & Spec-Driven Workflow:** Workspace is `D:\Projects\Chula`, using `.doc/` for source documents (`req071169.pdf`, `CuProjectName.md`), `.ai/` for architecture & AI agent rules, and `requirements/` for structured Thai feature specifications.
- **D04 — Official System Name:**
  - **ชื่อระบบภาษาไทย:** ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย
  - **ชื่อภาษาอังกฤษ:** Chula Pharmacy Faculty Information System
  - **ตัวย่อ หรือชื่อเรียกสั้น:** `CU-PFIS`
- **P01 — Non-destructive External Sync Proposal:** When data imported from CUPPEP, Google Sheets, or Chula Final datasets conflicts with records already verified/edited by staff in `CU-PFIS`, hold conflicts in a review queue (`DataConflictQueue`) with full provenance rather than overwriting automatically.
- **P02 — Training Workflow Option B Proposal:** Recommend Option B (faculty members submit training requests directly in `CU-PFIS` rather than Google Form/Sheet), separating "Staff Verification" from "Dean Approval Notification after signature."
- **O01 — CUPPEP Modification & Integration Scope (Open):** Pending exact list of screens/fields to add in CUPPEP and technical integration channel (API / DB / batch).
- **O02 — Phase 1 Scope, KPIs & Acceptance Criteria (Open):** Pending stakeholder confirmation meeting.
- **Architectural Baseline (adopted from team template):**
  - Clean Architecture + CQRS (MediatR) + EF Core (PostgreSQL) + Vue 3 (dedicated `create/edit/view/list` routes, no modal-only CRUD).
  - Dual-key pattern on all entities (`int Id` internal, `Guid ExternalId` UUIDv7 external).
  - Lower-kebab-case API routes end-to-end.
  - Local PostgreSQL (`chula` and `chula-test`) for development and functional tests (no Testcontainers/Docker dependency).

---

## Non-negotiable: Data Integrity, Sync Safety, Budget Accuracy & PDPA

This system underlies faculty CVs, academic training budgets, graduate employment statistics, license pass rates, and senior project publications. These rules apply across every phase as blocking requirements:

1. **Training budget balances are never mutated in place** — always derived from the append-only `TrainingBudgetLedger` (reserve, consume, release). Annual budget caps (e.g. `10,000 THB/person/year`) must be configurable per year (`TrainingBudgetPolicy`), never hardcoded.
2. **External data imports never silently overwrite staff-verified data** — re-importing from CUPPEP, Google Sheets, or Chula Final datasets must be idempotent, match by stable IDs (never by name alone or Sheet row number), preserve provenance (`SourceSystem`, `SourceRecordId`, `ImportedAt`), and route conflicts with staff-edited records to a review queue.
3. **Verification and Approval are distinct states** — especially in Training ("Staff verifies data accuracy" vs. "Staff records Dean's approval after physical signature") and Student Achievements ("Submitted in CUPPEP" vs. "Verified by staff to show on CV").
4. **Optimistic concurrency (`xmin`) on every mutable entity** — conflicting writes fail loudly, never silently overwrite.
5. **Sensitive personal data protection (PDPA)** — bank account numbers (training reimbursement) and employer evaluations (ผู้ใช้บัณฑิต) are strictly permission-gated and excluded from general exports/logs.
6. **Handler-level unit tests + functional tests are merge-blocking** for anything touching training budgets, approval/verification state transitions, or external data reconciliation.

---

## Phase 0 — Requirement Analysis & Project Setup

**Status: in progress (2026-11-16).**

- [x] Initial requirement analysis from `req071169.pdf` (12 pages) by PM, BA, and Tech Lead (consolidated into `.ai/roadmap.md`, `.ai/known-issues.md`, and `.ai/domain.md`).
- [x] Adapt `.ai/` documentation (`architecture.md`, `domain.md`, `coding-rules.md`, `ai-agent-guide.md`, `prompt-templates.md`, `dev-workflow.md`, `roadmap.md`, `known-issues.md`) for **CU-PFIS (Chula Pharmacy Faculty Information System)**.
- [x] Adapt `requirements/` structure (`README.md`, `_template.md`, bounded-context folders, and complete initial requirement specs across all 9 bounded contexts: `ReferenceData`, `Security`, `Faculty`, `Training`, `Students`, `ProfessionalLicense`, `SeniorProjects`, `Integration`, `Reporting`) and root `README.md`.
- [x] Finalize official project name: **CU-PFIS** (**ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย** / **Chula Pharmacy Faculty Information System**).
- [ ] **Stakeholder Requirement Confirmation Meeting:** resolve `Q01–Q18` below, collect sample forms/reports/PDFs/Google Sheets, and confirm Phase 1–5 delivery priorities.
- [ ] Finalize CUPPEP integration scope (`O01`) and sign off on baseline requirements (`O02`).

---

## Phase 1 — Solution Scaffold & Core Foundation

**Status: in progress (2026-11-18).**

- [x] Scaffold Clean Architecture solution (`chula.sln`: `Domain`, `Application`, `Infrastructure`, `API`, and test projects `Domain.UnitTests`, `Application.FunctionalTests`).
- [x] Base infrastructure: `EntityBase` (Dual-key `Id` + `ExternalId`, `xmin` concurrency), `SoftDeletableEntityBase` (global query filter), `IHasActiveFlag`, `AuditLog` interceptor (`AuditableEntitySaveChangesInterceptors`).
- [x] Security & Auth foundation: `Permission` / `RolePermission` (`module.action`), `AuthorizationBehaviour`, Role definitions (`Admin`, `Staff`, `Faculty`), and `CurrentUserService`.
- [x] `ReferenceData` context: `AcademicYear` (supporting 10+ years history), `Department`, `Major`, `AcademicPosition`, `AdmissionRound` (2-tier hierarchy), `DocumentTemplate`, plus initial `AcademicYears` CQRS vertical slice.
- [ ] Bilingual (`TH`/`EN`) infrastructure on frontend (`src/vuewebui` with `vue-i18n`).

---

## Phase 2 — Faculty CV & Training + Budget Ledger (`Faculty` & `Training`)

**Status: pending.**

- [ ] `FacultyMember`, `FacultyEducation`, `FacultyPublication` CRUD & search/sort.
- [ ] Faculty CV PDF export (individual & bulk) and periodic update reminder notification.
- [ ] Optional Scopus publication import/sync (if confirmed in `Q16`).
- [ ] `TrainingBudgetPolicy` (configurable annual limit per person, default 10,000 THB/year) & append-only `TrainingBudgetLedger`.
- [ ] `TrainingRequest` supporting all 4 training types (`AcademicFunded`, `NonAcademicFunded`, `FacultyOrganized`, `ShortTermOrOverseas`).
- [ ] Training workflow (Option B or confirmed hybrid): submit → staff verification / revision request → PDF memo generation & email for Dean signature → record Dean approval → budget ledger update & CV display.
- [ ] Training budget & attendance reports (PDF/Word/Excel export).

---

## Phase 3 — Student Profile, Achievements, Employment, Employer Evaluation & License (`Students` & `ProfessionalLicense`)

**Status: pending.**

- [ ] CUPPEP & Google Sheet integration staging (`ImportBatch`, `StagingRecord`, `DataConflictQueue`).
- [ ] `Student` master profile & `StudentCv` aggregation + PDF export.
- [ ] `StudentAchievement` workflow: sync from CUPPEP → staff review (request revision / verify) → publish to Student CV.
- [ ] `GraduateEmployment` workflow: import 1st round (CUPPEP) + import 2nd round (Chula Final dataset) → staff comparison/reconciliation (Chula Final as primary) without destructive overwrite on re-import.
- [ ] `EmployerEvaluation` (ผู้ใช้บัณฑิต): import from Google Form/Sheet, match to graduate, search/sort/export.
- [ ] `LicenseExamResult` (`CC1`, `CC2`, `IP1`, `IP2`, `PC1`, `PC2`): sync from CUPPEP, cohort pass count & percentage calculation, and reporting.

---

## Phase 4 — Senior Projects, Abstract Book & Utilization Tracking (`SeniorProjects`)

**Status: pending.**

- [ ] `SeniorProject` CRUD (individual/group), linking students (`SeniorProjectMember`) and advisors (`SeniorProjectAdvisor`), connected to `StudentCv`.
- [ ] Actual PDF file upload, validation (page count, encryption, MIME), storage, and publication status (`Publish`).
- [ ] Annual `AbstractCompilation`: extract page 3 (Abstract) from project PDFs for a selected academic year, flag non-conforming PDFs for staff review, and generate the compiled Abstract Book PDF.
- [ ] `ProjectUtilization` tracking & scheduled reminder emails to project advisors (start after 1 year, once per year, stop when filled or after 3 years elapsed).

---

## Phase 5 — Dashboard, Cross-Module Reports & 10-Year Historical Data Migration (`Reporting`)

**Status: pending.**

- [ ] Executive/Staff Dashboard with confirmed KPIs and academic-year filters.
- [ ] Custom/template-based exports (PDF, Word, Excel) across modules.
- [ ] Historical data migration (at least 10 years by academic year) + verification & reconciliation tools.

---

## Open Questions for Stakeholder Meeting (`Q01–Q18`)

These questions must be confirmed with the client and documented in the respective `requirements/` files before finalizing implementation of affected rules:

| Code | Topic | Question to Confirm |
|---|---|---|
| **Q01** | Release Phasing | Which modules are highest priority for Phase 1 delivery, and which can be phased? |
| **Q02** | CUPPEP Data Ownership | Which fields are entered/edited in CUPPEP vs. the new system, what new screens must be added to CUPPEP, and how are conflicts handled? |
| **Q03** | Roles & Access | Are staff permissions separated by department/division? Do students/alumni log into the new system directly or only CUPPEP? |
| **Q04** | Training Input Channel | Confirm Option B (fill directly in new system) vs. Option A (Google Form/Sheet) or a transition period. |
| **Q05** | Training Workflow & Budget Timing | Exact trigger points for: Staff Verified, Dean Signed/Approved, Budget Reserved, Budget Consumed, CV visibility, and handling cancellations/refunds/over-budget requests. |
| **Q06** | Budget Year & Carryover | Does the 10,000 THB/person/year budget follow Fiscal Year, Academic Year, or Calendar Year? Any carryover or multiple funding sources? |
| **Q07** | Chula Final Employment Data | What file format/channel does Chula's Final employment dataset arrive in, what key matches students, and how are missing/conflicting fields handled? |
| **Q08** | Student Achievements & License Proof | What are the verification criteria, scholarship fields, and required evidence files for student achievements and license results? |
| **Q09** | License Pass Rate Formula | What is the exact denominator for cohort pass percentage (total admitted vs. total graduated vs. total test-takers), and how are retakes counted? |
| **Q10** | Senior Project "Publish" Meaning | Who can view/download a "Published" Senior Project PDF (staff only, faculty/students, or public)? |
| **Q11** | Abstract Page 3 Rule | Is the Abstract strictly on PDF page 3 across all years/files, and does the compiled Abstract Book require a cover page, table of contents, or re-pagination? |
| **Q12** | Utilization Reminder Timeline | What is the exact reference start date for the 1-year utilization reminder, does filling it once stop future reminders, and who receives it if the advisor changes/retires? |
| **Q13** | 10-Year Historical Data | Where does the 10-year historical data currently reside, in what format/volume, and who validates the migrated data? |
| **Q14** | Dashboard KPIs & Report Templates | What are the exact KPI formulas, filters, and Word/PDF/Excel report templates required for acceptance? |
| **Q15** | Bilingual Scope (TH/EN) | Does bilingual support cover UI labels only, or also dual-language data fields, CV exports, report templates, and notification emails? |
| **Q16** | Scopus Integration | Is live Scopus API integration required (and does the faculty have API credentials/entitlements), or is file import/manual entry used? |
| **Q17** | Non-Functional & Sizing | Expected concurrent users, total PDF storage volume/growth per year, backup/retention policy, and hosting/SSO environment. |
| **Q18** | PDPA & Sensitive Fields | Retention and access/export policy for faculty bank account numbers and employer evaluation responses. |

---

## Artifacts & Samples to Request from Stakeholders

1. **Templates & Forms:** ตัวอย่าง CV อาจารย์/นิสิต, แบบฟอร์มขออนุมัติอบรม, บันทึกข้อความเสนอคณบดี, รายงานสรุปงบประมาณ และรายงานประจำปี (Word / PDF / Excel)
2. **Google Form / Google Sheet:** โครงสร้างคอลัมน์จริงของแบบประเมินผู้ใช้บัณฑิตและข้อมูลการอบรมเดิม (ใช้ข้อมูลที่ปกปิดข้อมูลส่วนบุคคลแล้ว)
3. **Sample Datasets:** ตัวอย่างข้อมูลนิสิต ผลงานนิสิต ข้อมูลการได้งานทำรอบคณะ และไฟล์ชุด Final จากจุฬาฯ
4. **Sample Senior Project PDFs:** ไฟล์ PDF โครงงานนิสิตจริงจากหลายปีการศึกษา และตัวอย่างเล่มรวม Abstract เพื่อตรวจสอบโครงสร้างหน้าที่ 3
5. **CUPPEP Technical Context:** โครงสร้างฐานข้อมูล/API ของระบบ CUPPEP ช่องทางการเชื่อมต่อ และรหัสบุคคลที่ใช้จับคู่ (`StudentCode`, `PersonnelCode`)
