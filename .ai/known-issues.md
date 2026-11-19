# Known Issues & Pre-Implementation Risks — CU-PFIS

Living tracker of known technical risks, requirement ambiguities, and architectural constraints for **CU-PFIS** (**Chula Pharmacy Faculty Information System** / **ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย**). Check here before starting work on any module so you don't make unverified assumptions or introduce bugs in high-risk areas. See [roadmap.md](roadmap.md) for the phased plan and `Q01–Q18` stakeholder questions.

---

## Open — Requirement & Integration Risks

### 1. Cross-System Scope & Data Sync with CUPPEP
- [ ] **CUPPEP modification scope (`O01`) is not yet finalized.** The client document proposes having students enter achievements, 1st-round employment, and license exam results in CUPPEP, plus potentially admission round and project info. Before building sync adapters, confirm which fields already exist in CUPPEP, which require new CUPPEP screens, and what API/DB mechanism connects the two systems.
- [ ] **Re-import overwrite risk on Staff-verified records.** Staff will review and edit data imported from CUPPEP, Google Sheets, and Chula's Final employment dataset. A naive sync/import that overwrites existing rows by key will destroy staff corrections. **Mitigation rule (mandatory):** all external imports must track `SourceSystem`, `SourceRecordId`, and `ImportedAt`, and route any conflict with a staff-modified record into a review/staging state (`DataConflictQueue`) rather than overwriting automatically.
- [ ] **Google Sheet row-number fragility.** Employer evaluation (`ผู้ใช้บัณฑิต`) and Training Option A rely on Google Sheets where rows may be edited, sorted, or deleted by staff. **Mitigation rule:** never use row index as an import key; require a stable response ID / timestamp + person identifier.

### 2. Training Workflow & Budget Calculation Ambiguities
- [ ] **Menu visibility contradiction in client scope (pages 2–4 vs. 10–12).** Parts of the document describe the Training menu as "for staff only," yet under Option B faculty members must submit training requests and view their own individual budget/history. **Design direction:** separate the *Faculty Self-Service Training Request/History view* from the *Staff Training & Budget Administration view* via role/ownership permissions.
- [ ] **Budget timing & state transitions (`Q05`, `Q06`).** The exact moment when the 10,000 THB/person/year budget is reserved (`Reserve`), deducted (`Consume`), or refunded on cancellation (`Release`), and when a training entry appears on the `FacultyCv`, is awaiting stakeholder confirmation. **Mitigation rule:** model `TrainingBudgetLedger` with explicit movement types so trigger points can be configured/adjusted cleanly without schema redesign.

### 3. Senior Project PDF & Abstract Book Compilation Risks
- [ ] **PDF "Page 3 = Abstract" assumption (`Q11`).** Real-world student PDFs across 10+ years may have varying front-matter lengths (cover, approval sheet, acknowledgments), encryption, scanned images, or fewer than 3 pages. **Mitigation rule:** the PDF processing service must validate page count and readability on upload, allow staff to preview/override the Abstract page range if a file deviates from page 3, and report any problematic files before generating the annual Abstract Book.
- [ ] **"Publish" (`เผยแพร่`) vs. File Download Permissions (`Q10`).** The scope mentions a `Publish` button on Senior Projects while also stating project files are restricted to staff. Do not expose published project PDFs to unauthenticated/public users until `Q10` confirms exact visibility and download permissions.

### 4. Bidirectional Reference Between Student CV and Senior Project
- [ ] **Circular data ownership risk.** `StudentCv` displays Senior Project info while `SeniorProject` links to students and their CVs. **Mitigation rule:** establish `SeniorProject` (in the `SeniorProjects` bounded context) as the single source of truth for project title, advisors, members, and PDF file; `StudentCv` queries/projects from `SeniorProject` rather than maintaining a separate editable copy.

### 5. PDPA & Sensitive Personal Data Exposure
- [ ] **Bank account numbers in Training requests & Employer Evaluations (`Q18`).** Training reimbursement forms include faculty bank account numbers, and Employer Evaluations contain supervisor feedback on graduates. **Mitigation rule:** mask/restrict bank account fields to authorized finance/admin staff only, exclude them from general exports/emails, and never write PII to plaintext application logs.

---

## Resolved

- [x] **2026-11-18 — Finalized Official Project Name (`CU-PFIS`).** Updated all documentation to use the official project name: **ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย** / **Chula Pharmacy Faculty Information System** (**CU-PFIS**).
- [x] **2026-11-16 — Adapted `.ai/` and `requirements/` templates from SUTH Inventory to CU-PFIS (Chula Pharmacy Faculty Information System).** Replaced hospital inventory/procurement domains (`Procurement`, `Catalog`, `Contracts`, `Inventory`, `HOSXP`) with CU-PFIS bounded contexts (`Faculty`, `Training`, `Students`, `ProfessionalLicense`, `SeniorProjects`, `Integration`, `Reporting`, `ReferenceData`, `Security`), workflows, and pre-meeting analysis findings from `.doc/`.
