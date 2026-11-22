# Known Issues & Pre-Implementation Risks — CU-PFIS

Living tracker of known technical risks, requirement ambiguities, and architectural constraints for **CU-PFIS** (**Chula Pharmacy Faculty Information System** / **ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย**). Check here before starting work on any module so you don't make unverified assumptions or introduce bugs in high-risk areas. See [roadmap.md](roadmap.md) for the phased plan and `Q01–Q18` stakeholder questions.

---

## Open — Requirement & Integration Risks

### 1. Cross-System Scope & Data Sync with CUPPEP (`O01` / `D11`)
- [ ] **CUPPEP SQL deployment & new WebForms screens (`O01`) pending.** Legacy `chula_pps` schema has been mapped (`View_Student_Bio`, `Persons`, `RPT_Student_PracticeHistory`), SQL contract scripts (`01_create_pfis_integration_views.sql`, `02_create_pfis_student_extension_tables.sql`) are ready, and the standalone `.NET 10` Clean Architecture `CU-PPEP Integration API` is scaffolded at `D:\Projects\Chula\CU-PPEP\IntegrationApi\`. Remaining: deploy the SQL views/tables on `chula_pps`, build the new WebForms student input screens in `Chula-PPS`, and wire `ICuppepDataSource` in `CU-PFIS`.
- [ ] **Re-import overwrite risk on Staff-verified records.** Staff will review and edit data imported from CUPPEP and Google Sheets (e.g., manual employment recheck per `A07`). **Mitigation rule (mandatory):** all external imports must track `SourceSystem`, `SourceRecordId`, and `ImportedAt`, and route any conflict with a staff-modified record into `DataConflictQueue` rather than overwriting automatically.
- [ ] **Google Sheet row-number fragility.** Employer evaluation (`ผู้ใช้บัณฑิต`) relies on Google Sheets where rows may be edited, sorted, or deleted by staff. **Mitigation rule:** never use row index as an import key; require a stable response ID / timestamp + person identifier.

### 2. Training Budget Future-Quota Borrowing Edge Cases (`A06`)
- [ ] **Future borrowing cap & multi-year edge cases (`A06`).** `A06` confirmed faculty can borrow from their own future fiscal year's quota (1 Oct – 30 Sep). Before finalizing validation rules, confirm: (a) maximum borrowable amount (e.g., capped at 1 future fiscal year's quota, such as ≤ 10,000 THB), and (b) policy if a faculty member retires/resigns with a negative carry-forward balance. **Mitigation rule:** model `TrainingBudgetLedger` with explicit `FiscalYear` and `BorrowAdvance` / `CarryForwardDeduction` entry types so limits are policy-driven without schema changes.
- [ ] **Menu visibility separation.** Separate *Faculty Self-Service Training Request/History view* (Option B, `A04`) from *Staff Training & Budget Administration view* (scoped by department/role per `A03`).

### 3. Senior Project Publish Access vs. Student Login (`A03` vs. `A10`) & PDF Detection (`A11`)
- [ ] **Student access channel for Published Senior Projects (`A03` vs. `A10`).** `A03` states students/alumni do not log into CU-PFIS yet (CUPPEP only), while `A10` states Published Senior Projects should be searchable/viewable by Staff, Faculty, **and Students**. Confirm whether Students will search/view published Senior Projects via a CUPPEP-embedded/API view or via read-only student SSO login in Phase 4.
- [ ] **Scanned / image-only PDFs for Abstract auto-detection (`A11`).** Auto-detecting centered `"Abstract"` on the first line of pages 3–5 works reliably on text-based PDFs (`PdfPig`), but scanned/image-only legacy PDFs lack extractable text. **Mitigation rule:** run text-layout detection on upload for pages 3–5, auto-fill `AbstractPageNumber` when found, and flag scanned/ambiguous PDFs for quick staff preview and manual page selection before compiling the annual Abstract Book (Cover Page + Abstract pages). See [technical-notes.md](technical-notes.md) (Section 1) for library choices and C# prototype code.

### 4. Scopus Integration Channel (`O03` / `A16`)
- [ ] **Scopus API credentials & institutional entitlement (`O03`).** `A16` confirmed the client wants faculty CV/publication records from Scopus, but API access details are not yet confirmed. **Mitigation rule:** never scrape Scopus web pages; verify whether Chulalongkorn University can provide Elsevier Scopus API credentials (`API Key` + `InstToken`), and design `FacultyPublication` import to also support standard **ORCID API** or **Scopus BibTeX/RIS/CSV file import** as a clean fallback. See [technical-notes.md](technical-notes.md) (Section 2) for API endpoints, headers, and phased rollout guide.

### 5. Dashboard KPIs & Report Templates (`O02` / `A14`)
- [ ] **KPI definitions and Word/PDF/Excel templates (`O02`).** Deferred to the next stakeholder meeting per `A14`. Collect actual sample templates before implementing Phase 5 exports and Phase 1 PDF CV templates.

### 6. Bidirectional Reference Between Student CV and Senior Project
- [ ] **Circular data ownership risk.** `StudentCv` displays Senior Project info while `SeniorProject` links to students and their CVs. **Mitigation rule:** establish `SeniorProject` (in the `SeniorProjects` bounded context) as the single source of truth for project title, advisors, members, and PDF file; `StudentCv` queries/projects from `SeniorProject` rather than maintaining a separate editable copy.

### 7. PDPA Consent & Sensitive Personal Data Protection (`A18`)
- [ ] **First-Login PDPA Consent & Sensitive Fields.** Per `A18`, CU-PFIS must enforce a First-Login PDPA Consent gate and persist user acceptance records (`PdpaConsent`). Additionally, bank account numbers in Training requests and Employer Evaluations must remain masked/restricted to authorized finance/department staff only, excluded from general exports/emails, and never written to plaintext logs.

---

## Resolved

- [x] **2026-11-21 — Resolved Stakeholder Questions `Q01–Q18` (`A01–A18`).** Confirmed Phase 1 priority (Frontend UX/UI + Faculty CV + Student CV), CUPPEP student input ownership, Department-scoped Staff roles, Training Option B + Excel migration import, budget deduction after Dean approval, Fiscal Year (1 Oct–30 Sep) with Future Quota Borrowing, manual Employment recheck, no certificate/license file attachments, graduate-count license pass `%` denominator, Senior Project Publish audience, Abstract page 3–5 detection + Cover page format, Utilization reminder timeline (1 yr from PublishedDate + legacy manual trigger + max 3 reminders), manual 10-year legacy entry, full TH/EN bilingual scope (including DB & emails), ≤ 10 concurrent users, and First-Login PDPA Consent.
- [x] **2026-11-18 — Finalized Official Project Name (`CU-PFIS`).** Updated all documentation to use the official project name: **ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย** / **Chula Pharmacy Faculty Information System** (**CU-PFIS**).
- [x] **2026-11-16 — Adapted `.ai/` and `requirements/` templates from SUTH Inventory to CU-PFIS (Chula Pharmacy Faculty Information System).** Replaced hospital inventory/procurement domains (`Procurement`, `Catalog`, `Contracts`, `Inventory`, `HOSXP`) with CU-PFIS bounded contexts (`Faculty`, `Training`, `Students`, `ProfessionalLicense`, `SeniorProjects`, `Integration`, `Reporting`, `ReferenceData`, `Security`), workflows, and pre-meeting analysis findings from `.doc/`.
