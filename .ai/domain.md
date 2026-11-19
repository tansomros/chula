# Domain Glossary & Workflows — CU-PFIS

Shared vocabulary for **CU-PFIS** (**Chula Pharmacy Faculty Information System** / **ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย**) — use these terms consistently in code (entity/property names), UI copy, requirements, and commit messages so a Thai business term always maps to the same English identifier. Where a detail is marked "pending confirmation," it requires stakeholder sign-off from the requirement meeting (see `Q01–Q18` in [roadmap.md](roadmap.md) and [known-issues.md](known-issues.md)).

---

## 1. Core Domain Entities & Bilingual Glossary

### System Identity
- **CU-PFIS** — official abbreviation / short name of the system.
  - **Thai Name:** ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย
  - **English Name:** Chula Pharmacy Faculty Information System

### Organization & Cross-Cutting (`ReferenceData`)
- **AcademicYear (ปีการศึกษา)** — the primary partitioning dimension for historical data (kept for at least 10 years), statistics, license pass rates, and annual Abstract compilations. Note: must distinguish between Academic Year (ปีการศึกษา), Fiscal Year (ปีงบประมาณ), and Calendar Year (ปีปฏิทิน) per module.
- **Department (ภาควิชา / หน่วยงาน / สังกัด)** — academic department or administrative unit within the Faculty of Pharmaceutical Sciences.
- **Major / Track (สาขาวิชา)** — student specialization tracks referenced in licensure and student records:
  1. `IndustrialPharmacy` (สาขาวิชาเภสัชกรรมอุตสาหการ — corresponds to `IP1`, `IP2`)
  2. `PharmaceuticalCare` (สาขาวิชาการบริบาลทางเภสัชกรรม — corresponds to `PC1`, `PC2`)
- **AdmissionRound (รอบสมัครเข้าศึกษา)** — 2-tier admission round hierarchy (`req071169.pdf` pages 4–5):
  1. **รอบ Portfolio:** `Port โอลิมปิกวิชาการ`, `Port ภาษาอังกฤษ`
  2. **รอบ Quota:** `Quota สาธิตจุฬาฯ`, `Quota กีฬาชาติ`, `Quota จุฬาชนบท`, `Quota มหาดไทย`, `Quota กำเนิดวิทย์`
  3. **รอบ กสพท.**
- **DocumentTemplate (แม่แบบเอกสาร/รายงาน)** — configurable or standard templates used for exporting CVs, training approval memos, annual Abstract books, and annual reports (PDF/Word/Excel).

### Faculty Profile (`Faculty`)
- **FacultyMember (อาจารย์)** — academic staff member of the faculty. Identified by a stable personnel/staff code (`PersonnelCode`), with bilingual name (`NameTh`, `NameEn`), academic position (`AcademicPosition` / ตำแหน่งทางวิชาการ), and contact info.
- **FacultyCv (ประวัติอาจารย์ / CV อาจารย์)** — composite profile of a `FacultyMember` comprising personal details, academic positions, education history (`FacultyEducation`), publications (`FacultyPublication`), and completed/approved trainings (`TrainingRequest`). Supports individual and bulk PDF export and periodic update reminders.
- **FacultyPublication (ผลงานตีพิมพ์)** — research papers/publications linked to a `FacultyMember`. May be entered manually/imported or synced from **Scopus** (pending API/credential confirmation).

### Training & Academic Budget (`Training`)
- **TrainingCategory (ประเภทการอบรม)** — 4 distinct categories defined in the scope (`req071169.pdf` pages 2–4):
  1. `AcademicFunded` (1.1 การอบรมที่ใช้เงินวิชาการ) — requires budget check, bank account info for finance, and approval workflow.
  2. `NonAcademicFunded` (1.2 การอบรมที่ไม่ใช้เงินวิชาการ) — requires approval workflow without cutting academic budget.
  3. `FacultyOrganized` (1.3 การอบรมที่คณะเป็นผู้จัด เช่น สัมมนาคณาจารย์, การอบรม UIC) — does **not** require approval; recorded by staff via a dedicated **"สำหรับเจ้าหน้าที่"** form selecting participating faculty members from a dropdown/multi-select.
  4. `ShortTermOrOverseas` (1.4 การอบรมระยะสั้น / ต่างประเทศทางวิชาการ) — does **not** require approval; recorded by faculty/HR/staff for CV and statistics.
- **TrainingWorkType (ประเภทของงานประชุม/อบรม)** — classification of the event topic, e.g., `วิจัย` (Research), `วิชาการ` (Academic).
- **TrainingRequest (คำขอ/รายการอบรม)** — a record of a faculty member's training attendance or request. Tracks event details, dates, expenses, bank account info (for reimbursement, strictly access-controlled), verification status (`กำลังดำเนินการ`), and approval status (`อนุมัติ`).
- **Staff Training Views (2 มุมมองหลักของเมนูการอบรม):**
  1. **เมนู "ข้อมูลอบรมของอาจารย์" (Per-Faculty Budget & Training Summary):** displays faculty name + academic title, department, annual support budget (default 10,000 THB/person/year, configurable per year), amount used, and remaining balance.
  2. **เมนู "ชื่องานประชุม/อบรมทั้งหมด" (Per-Event Summary):** displays event name, work type (วิจัย/วิชาการ), average cost per person, event dates, and expandable list/icon of participating faculty members with individual amounts used.
- **TrainingBudgetPolicy (เกณฑ์งบประมาณอบรมรายปี)** — configurable annual budget allocation per faculty member (baseline in scope document: **10,000 THB/person/year**, configurable per year rather than hardcoded).
- **TrainingBudgetLedger (สมุดคุมงบประมาณอบรม)** — append-only ledger tracking budget movements (`Reserve` / กันวงเงิน, `Consume` / ตัดยอดใช้จริง, `Release` / คืนวงเงินเมื่อยกเลิกหรือปรับปรุง) for a `FacultyMember` in a given budget year.

### Student Profile, Achievements & Graduate Tracking (`Students`)
- **Student (นิสิต / บัณฑิต)** — pharmacy student or graduate, identified by `StudentCode` (รหัสนิสิต) and cohort/admission year (`CohortYear` / รุ่นปีที่เข้าศึกษา).
- **StudentCv (CV นิสิต)** — comprehensive student profile combining general info, academic advisor (`Advisor`), project advisor (`ProjectAdvisor`), high school education, professional practice rotations (การฝึกงานจาก CUPPEP), admission round (`AdmissionRound` / รอบรับเข้า), senior project, verified achievements (`StudentAchievement`), license exam results (`LicenseExamResult`), graduate employment (`GraduateEmployment`), and employer evaluation (`EmployerEvaluation`).
- **StudentAchievement (ผลงานและรางวัลนิสิต)** — student awards, competitions, or scholarships submitted via CUPPEP and verified by faculty staff in this system before appearing on the `StudentCv`.
- **GraduateEmployment (ข้อมูลการได้งานทำ)** — graduate employment status tracked across two sources:
  1. **Faculty Round (ข้อมูลรอบคณะ)** — initial survey submitted via CUPPEP.
  2. **Chula Final Dataset (ข้อมูลชุด Final จากจุฬาฯ)** — central university employment survey, treated as the **authoritative baseline (ยึดข้อมูลจุฬาฯ เป็นหลัก)** and verified/reconciled by staff.
- **EmployerEvaluation (ข้อมูลผู้ใช้บัณฑิต)** — satisfaction/evaluation responses submitted by graduates' supervisors (ผู้บังคับบัญชา) via Google Form/Sheet, matched to the corresponding `Student`.

### Professional License (`ProfessionalLicense`)
- **LicenseExamResult (ผลสอบใบประกอบวิชาชีพ)** — pharmacy licensure exam results submitted by students via CUPPEP and aggregated in this system.
- **LicenseExamType (ประเภทการสอบใบประกอบ)** — exam components specified in the scope:
  - Core track: `CC1`, `CC2`
  - Industrial Pharmacy track: `IP1`, `IP2`
  - Pharmaceutical Care track: `PC1`, `PC2`
- **LicensePassSummary (สรุปผลสอบใบประกอบตามรุ่น)** — headcount and percentage of students passing each exam component and receiving their license, grouped by student cohort (`CohortYear`).

### Senior Projects & Utilization (`SeniorProjects`)
- **SeniorProject (โครงงานนิสิต / Senior Project)** — individual or group research project linked to student members (`SeniorProjectMember`) and faculty advisors (`SeniorProjectAdvisor`). Stores the actual project **PDF file** (not just a Google Drive link) and publication flag (`IsPublished` / สถานะเผยแพร่).
- **AbstractCompilation (เล่มรวมบทคัดย่อรายปี)** — automated compilation of project abstracts for a selected `AcademicYear` by extracting the abstract page (specified as **page 3 of the PDF** in the scope document, subject to validation against sample files).
- **ProjectUtilization (การใช้ประโยชน์จากโครงงาน)** — post-completion tracking filled in by the project advisor describing how the senior project has been utilized.

---

## 2. Core Business Workflows

### 2.1 Training Verification & Approval Workflow (`Training`)
> **Critical distinction:** "Staff Verification (ตรวจข้อมูลถูกต้อง)" and "Dean Approval Notification (แจ้งอนุมัติหลังคณบดีลงนาม)" are two separate steps.

- **Option A (Google Form/Sheet):** Faculty submits Google Form → Staff ticks verified in Google Sheet → System imports data.
- **Option B (Recommended in scope, pending confirmation):**
  1. Faculty fills training request in the system (`Draft` → `Submitted`).
  2. System notifies staff → Staff reviews and either **Requests Revision (ขอแก้ไข)** or **Marks Verified (รับข้อมูลถูกต้อง / กำลังดำเนินการ)**.
  3. System generates the official PDF memo and emails it to the faculty member to print/submit for the Dean's physical/external signature.
  4. Once signed by the Dean outside the system, staff records **Approved (อนุมัติ)** in the system → System notifies the faculty member, updates the `TrainingBudgetLedger`, and reflects the training on the `FacultyCv` (exact trigger points for budget reservation/deduction and CV display to be confirmed via `Q05`).
- Note: `FacultyOrganized` (ประเภทที่ 3) and `ShortTermOrOverseas` (ประเภทที่ 4) do **not** require the approval request workflow above.

### 2.2 Student Achievement Verification (`Students`)
```text
Student submits in CUPPEP  →  Synced to CU-PFIS (Pending Review)
  →  Staff reviews  →  [Request Revision] OR [Approve/Verify (ตกลง)]
  →  Displayed on Student CV
```

### 2.3 Graduate Employment Reconciliation (`Students`)
```text
1st Round (Faculty via CUPPEP)  +  2nd Round (Chula Final Dataset)
  →  System matches by StudentCode
  →  Staff compares, resolves discrepancies (Chula Final is primary), and marks Verified
  →  Re-imports must NEVER silently overwrite staff-verified records
```

### 2.4 Senior Project Abstract Compilation & Utilization Reminder (`SeniorProjects`)
1. **Project & PDF Upload:** Staff/authorized user creates `SeniorProject`, links students & advisors, uploads the full PDF, and sets publication status (`Publish`).
2. **Annual Abstract Book:** Staff selects an `AcademicYear` → System extracts page 3 (Abstract) from each project's PDF (validating page count/format first and flagging any malformed/short PDFs) → Merges into an annual Abstract Book PDF.
3. **Utilization Reminder Loop:**
   - Starts **1 year** after the project reference date (pending confirmation `Q12`: academic year end vs. submission date).
   - Sends an annual reminder email to the project advisor once per year.
   - Stops immediately once utilization is recorded **or** after **3 years** have elapsed.

---

## 3. External Systems & Integration Boundaries

- **CUPPEP (ระบบฝึกปฏิบัติงานวิชาชีพเภสัชกรรม):**
  - Existing system used by students and faculty for pharmacy practice rotations.
  - **Existing data pulled from CUPPEP:** Student general info, academic advisor, project advisor, high school education, and internship/rotation records.
  - **Proposed new inputs in CUPPEP (pending scope confirmation `O01`):** Admission round, project info, student achievements, 1st-round graduate employment, and license exam results (`CC1/CC2`, `IP1/IP2`, `PC1/PC2`).
  - **Sync rule:** Being the Master System does **not** mean blindly overwriting CUPPEP or letting repeated CUPPEP imports overwrite staff-corrected records in **CU-PFIS**. Conflicts go to a review queue with full provenance (`SourceSystem`, `SourceRecordId`, `ImportedAt`).
- **Google Form / Google Sheet / Google Drive:**
  - Used for **Employer Evaluation (ผู้ใช้บัณฑิต)** and potentially **Training Option A** or legacy data migration.
  - Sheet imports must use stable unique identifiers/timestamps, never row numbers (which shift when rows are edited/deleted).
- **Chula Central Employment Data (ข้อมูลการได้งานทำชุด Final จากจุฬาฯ):**
  - Imported to reconcile and serve as the primary truth for graduate employment.
- **Scopus:**
  - Proposed optional source for pulling `FacultyPublication` records into `FacultyCv` (subject to API key/institutional access confirmation `Q16`).

### 3.1 Cross-System Data Ownership & Integration Matrix

| ชุดข้อมูล (Dataset) | แหล่งข้อมูลตาม Scope (`req071169.pdf`) | หน้าที่ของระบบหลัก (`CU-PFIS`) | ขอบเขตฝั่ง `CUPPEP` / ภายนอกที่ต้องยืนยัน |
| --- | --- | --- | --- |
| ข้อมูลทั่วไปนิสิต / อาจารย์ที่ปรึกษา / การศึกษามัธยม / การฝึกงาน | `CUPPEP` (มีอยู่เดิม) | ดึงข้อมูลและเชื่อมโยงแสดงใน `StudentCv` | ตรวจสอบโครงสร้างฟิลด์ ช่องทางเชื่อมต่อ และรหัสจับคู่ (`StudentCode`) |
| รอบรับเข้า (`AdmissionRound`) / ข้อมูลโครงการเบื้องต้น | เสนอให้เพิ่มใน `CUPPEP` | ดูแลข้อมูลหลักและแสดงใน `StudentCv` | ยืนยันว่าจะเพิ่มหน้ากรอกใน `CUPPEP` หรือกรอกใน `CU-PFIS` (`Q02`) |
| ผลงานและรางวัลนิสิต (`StudentAchievement`) | เสนอให้นิสิตกรอกผ่าน `CUPPEP` | เจ้าหน้าที่ตรวจสอบ ขอแก้ไข หรือรับรอง (`Verified`) ก่อนแสดงใน `StudentCv` | ยืนยันเมนูที่ต้องเพิ่มใน `CUPPEP` และการส่งสถานะ/ความเห็นขอแก้ไขกลับ (`Q02`, `Q08`) |
| การได้งานทำ (`GraduateEmployment`) | รอบแรกจาก `CUPPEP` + รอบสองชุด Final จากจุฬาฯ | จับคู่ด้วย `StudentCode` เทียบข้อมูลโดยยึดชุด Final จุฬาฯ เป็นหลัก เจ้าหน้าที่ปรับปรุง/รับรองโดยไม่ถูกเขียนทับเมื่อนำเข้าซ้ำ | ยืนยันฟิลด์หน้ากรอกรอบแรกใน `CUPPEP` และรูปแบบไฟล์ชุด Final จากจุฬาฯ (`Q07`) |
| ผลสอบใบประกอบวิชาชีพ (`LicenseExamResult`) | เสนอให้นิสิตกรอกใน `CUPPEP` (`CC1/CC2`, `IP1/IP2`, `PC1/PC2`) | รวบรวม แสดงรายชื่อ และคำนวณจำนวน/ร้อยละผู้สอบผ่านตามรุ่น | ยืนยันหน้ากรอกใน `CUPPEP` หลักฐานผลสอบ และสูตรตัวหารร้อยละ (`Q08`, `Q09`) |
| การอบรมและงบประมาณ (`TrainingRequest`) | ทางเลือก A (`Google Form/Sheet`) หรือ ทางเลือก B (กรอกใน `CU-PFIS`) | ตรวจความถูกต้อง ออก PDF แจ้งอนุมัติหลังคณบดีลงนาม คุมงบผ่าน `TrainingBudgetLedger` และแสดงใน `FacultyCv` | ยืนยันการเลือกทางเลือก B หรือการใช้คู่กันช่วงเปลี่ยนผ่าน (`Q04`, `Q05`, `Q06`) |
| ข้อมูลผู้ใช้บัณฑิต (`EmployerEvaluation`) | ผู้บังคับบัญชาตอบผ่าน `Google Form/Sheet` | นำเข้า จับคู่กับบัณฑิต ค้นหา เรียงลำดับ และส่งออกรายงาน | ขอตัวอย่าง Sheet จริงและกำหนดคีย์อ้างอิงที่ไม่พึ่งเลขแถว (`Q18`) |
| โครงงานนิสิตและการใช้ประโยชน์ (`SeniorProject`) | เจ้าหน้าที่และอาจารย์บันทึกใน `CU-PFIS` | จัดเก็บไฟล์ PDF จริง กำหนดสถานะเผยแพร่ รวมเล่ม Abstract หน้าที่ 3 รายปี และแจ้งเตือนการใช้ประโยชน์ปีที่ 1–3 | ยืนยันสิทธิ์การดูไฟล์ที่เผยแพร่ และทดสอบโครงสร้างหน้า 3 จากไฟล์ PDF ตัวอย่างจริง (`Q10`, `Q11`, `Q12`) |
