# Requirements — CU-PFIS : ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย (Chula Pharmacy Faculty Information System)

โฟลเดอร์นี้เก็บ **requirement ของแต่ละ feature** ในโปรเจกต์ **CU-PFIS** (**ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย** / **Chula Pharmacy Faculty Information System**) — เขียนโดยทีม (PM, BA, เจ้าของระบบ หรือโปรแกรมเมอร์) เป็นภาษาไทย ตามโครงสร้างเดียวกันทุกไฟล์ แล้วใช้เป็นบริบทให้ทั้ง **คนในทีม** (เข้าใจว่าต้องทำอะไรและมีคำถามใดรอยืนยัน) และ **AI Agent** (ใช้ร่วมกับ [`.ai/prompt-templates.md`](../.ai/prompt-templates.md) เพื่อวางแผน เขียนโค้ด และเขียนเทส) อ่านชุดเดียวกัน

> **ทำไมต้องมีโฟลเดอร์นี้:** โปรเจกต์นี้ใช้ AI เป็นตัวช่วยหลักในการพัฒนาและเขียนเทส และมีรายละเอียดเชื่อมโยงทั้งระบบหลัก (**CU-PFIS**) และระบบเดิม (**CUPPEP**, **Google Form/Sheet**, **ข้อมูลชุด Final จากจุฬาฯ**) การเขียน requirement แยกตามโมดูลที่มีโครงสร้างชัดเจนไว้ล่วงหน้า ช่วยป้องกันความเข้าใจคลาดเคลื่อน ป้องกัน AI เขียนโค้ดเกินขอบเขต (scope creep) และเห็นชัดเจนว่าจุดไหนยืนยันแล้วหรือยังรอคำตอบจากลูกค้า (`Q01–Q18`)

## ใครเขียน

ใครก็ได้ที่รู้ว่า feature นี้ต้องทำอะไร — BA, PM, เจ้าของ feature หรือโปรแกรมเมอร์ **ไม่จำเป็นต้องเขียนโค้ดเป็น** แต่ควรตอบคำถามในเทมเพลตให้ครบและชัดเจนที่สุดเท่าที่ทำได้ ส่วนไหนที่ยังรอสรุปจากที่ประชุมลูกค้า ให้ระบุรหัสคำถาม (เช่น `รอสรุป Q05 จากที่ประชุมลูกค้า`) แทนการเดาเอง

## เริ่มเขียน requirement ใหม่

1. คัดลอกไฟล์ [`_template.md`](_template.md)
2. นำไปวางในโฟลเดอร์ย่อยตาม **Bounded Context** ที่ feature นั้นเกี่ยวข้อง (ดูความหมายแต่ละ context ที่ [`.ai/architecture.md`](../.ai/architecture.md) หัวข้อ "Bounded-context folder map"):

   - `ReferenceData/` — ปีการศึกษา (`AcademicYear`), ภาควิชา/หน่วยงาน (`Department`), สาขาวิชา (`Major`), ตำแหน่งทางวิชาการ (`AcademicPosition`), รอบรับเข้า (`AdmissionRound`), แม่แบบเอกสาร/รายงาน (`DocumentTemplate`) → [`ReferenceData/reference-data.md`](ReferenceData/reference-data.md)
   - `Faculty/` — ข้อมูลอาจารย์ (`FacultyMember`), CV อาจารย์, วุฒิการศึกษา, ผลงานตีพิมพ์ (และการดึงข้อมูลจาก Scopus), การส่งออก PDF และการแจ้งเตือนอัปเดต CV → [`Faculty/faculty-cv.md`](Faculty/faculty-cv.md)
   - `Training/` — การอบรม 4 ประเภท (`TrainingRequest`), การตั้งค่าวงเงินงบประมาณรายปี (`TrainingBudgetPolicy`), สมุดคุมงบประมาณ (`TrainingBudgetLedger`), ขั้นตอนตรวจความถูกต้องและแจ้งอนุมัติหลังคณบดีลงนาม → [`Training/training-request.md`](Training/training-request.md)
   - `Students/` — ข้อมูลนิสิตและ CV นิสิต (`Student`), ผลงานและรางวัลนิสิต (`StudentAchievement`), การได้งานทำรอบคณะเทียบชุด Final จุฬาฯ (`GraduateEmployment`), ข้อมูลผู้ใช้บัณฑิต (`EmployerEvaluation`) → [`Students/student-cv.md`](Students/student-cv.md), [`Students/student-achievement.md`](Students/student-achievement.md), [`Students/graduate-employment.md`](Students/graduate-employment.md), [`Students/employer-evaluation.md`](Students/employer-evaluation.md)
   - `ProfessionalLicense/` — ผลสอบใบประกอบวิชาชีพ (`CC1`, `CC2`, `IP1`, `IP2`, `PC1`, `PC2`) และรายงานสรุปจำนวน/ร้อยละตามรุ่น → [`ProfessionalLicense/license-exam-result.md`](ProfessionalLicense/license-exam-result.md)
   - `SeniorProjects/` — โครงงานนิสิตรายบุคคล/กลุ่ม (`SeniorProject`), การเก็บไฟล์ PDF จริงและสถานะเผยแพร่, การรวมเล่มหน้า Abstract (หน้าที่ 3) รายปี, การติดตามการใช้ประโยชน์และแจ้งเตือนปีที่ 1–3 → [`SeniorProjects/senior-project-and-utilization.md`](SeniorProjects/senior-project-and-utilization.md)
   - `Integration/` — การดึง/นำเข้าข้อมูลจาก CUPPEP, Google Form/Sheet, ไฟล์ชุด Final จุฬาฯ และการจัดการคิวข้อมูลขัดแย้ง (`DataConflictQueue`) → [`Integration/external-data-sync.md`](Integration/external-data-sync.md)
   - `Reporting/` — Dashboard ภาพรวม และการส่งออกรายงานประจำปี (PDF / Word / Excel) → [`Reporting/dashboard-and-annual-reports.md`](Reporting/dashboard-and-annual-reports.md)
   - `Security/` — สิทธิ์การเข้าถึงตามบทบาท (`Admin`, `เจ้าหน้าที่`, `อาจารย์`), เจ้าของข้อมูล และการคุ้มครองข้อมูลส่วนบุคคล (PDPA) → [`Security/access-control-and-pdpa.md`](Security/access-control-and-pdpa.md)

3. ตั้งชื่อไฟล์เป็นภาษาอังกฤษ kebab-case สื่อความหมาย เช่น `faculty-cv.md`, `training-request.md`, `senior-project-abstract.md`
4. กรอกทุกหัวข้อในเทมเพลตให้ครบที่สุดเท่าที่ทำได้
5. ดูตัวอย่างไฟล์ที่กรอกตามบริบทโปรเจกต์ **CU-PFIS** ได้ที่ [`Training/training-request.md`](Training/training-request.md)

## สถานะของ requirement

ระบุไว้ในตารางหัวไฟล์ (แถว "สถานะ"):

| สถานะ | ความหมาย |
| --- | --- |
| `ร่าง` | ยังเขียนไม่ครบ หรือยังมีประเด็นรอสรุปจากลูกค้า (`Q01–Q18`) — **ยังไม่พร้อมสั่ง AI/dev เริ่มเขียนโค้ดจริง** (แต่ใช้ให้ AI ช่วยวิเคราะห์/วางแผนได้) |
| `พร้อมพัฒนา` | กรอกครบ ยืนยันกติกาธุรกิจกับลูกค้า/ทีมเรียบร้อยแล้ว พร้อมเริ่ม implement |
| `กำลังพัฒนา` | มีคนหรือ AI เริ่มพัฒนาแล้ว |
| `เสร็จสิ้น` | พัฒนา + ทดสอบผ่านตามเกณฑ์การยอมรับ (Acceptance Criteria) ในไฟล์แล้ว |

## ใช้ร่วมกับ AI ยังไง

เมื่อต้องการให้ AI ช่วยงานจากไฟล์ requirement ให้ทำตาม [`.ai/prompt-templates.md`](../.ai/prompt-templates.md):

1. วาง **Shared Preamble** (บริบทมาตรฐานของโปรเจกต์ **CU-PFIS**) ก่อนเสมอ
2. วาง Prompt Template ของสเตจที่จะทำ (Domain entity, CQRS feature, unit test, functional test, frontend, e2e)
3. ในช่อง `Requirement: {path}` ท้าย template นั้น ให้ใส่ **path ของไฟล์ requirement** (เช่น `requirements/Training/training-request.md`)
4. **ให้ AI เสนอแผนก่อน อย่าเพิ่งให้แก้โค้ดทันที** — ตรวจสอบว่าแผนของ AI ไม่ฝ่าฝืนกฎ Non-negotiable (เช่น ต้องใช้ Ledger สำหรับงบอบรม, ห้ามเขียนทับข้อมูลที่เจ้าหน้าที่ตรวจแล้วเมื่อนำเข้าซ้ำจาก CUPPEP/Sheet) แล้วจึงให้เริ่มลงมือเขียนโค้ด
