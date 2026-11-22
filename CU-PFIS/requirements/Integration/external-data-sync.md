# Requirement: ระบบเชื่อมต่อข้อมูลภายนอกและการจัดการข้อมูลขัดแย้ง (External Data Integration & Non-Destructive Sync) — CU-PFIS

| | |
| --- | --- |
| Bounded Context | Integration |
| สถานะ | ร่าง |
| ผู้เขียน | ทีมวิเคราะห์ระบบ (อ้างอิง `.doc/req071169.pdf` หน้า 1–7, 10–11 และ `.ai/coding-rules.md`) |
| วันที่เขียน | 2026-11-18 |
| Roadmap Phase (ถ้ามี) | Phase 1–3 (CUPPEP Student Sync ใน Phase 1, Training Excel Import ใน Phase 2, Conflict Queue & Google Sheet ใน Phase 3) |
| คำถามที่รอยืนยันจากลูกค้า (ถ้ามี) | ยืนยันแล้ว: `A02` (นิสิตกรอกที่ CUPPEP), `A04` (นำเข้าข้อมูลอบรมเก่าจาก Excel), `A07` (จนท. Recheck การได้งานทำเองใน CU-PFIS), `A13` (ข้อมูลย้อนหลัง 10 ปี จนท. กรอก/ตรวจแบบ Manual) — รอสรุป: ช่องทางเชื่อมต่อเทคนิค CUPPEP (`O01`) และช่องทาง Scopus (`O03` / `A16`) |

## 1. เป้าหมายและบริบท (Goal & Context)

ระบบ **CU-PFIS (ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย / Chula Pharmacy Faculty Information System)** ทำหน้าที่เป็น **Master System** ที่ต้องรับและเชื่อมโยงข้อมูลจากแหล่งภายนอกหลายแหล่ง ได้แก่:
1. **ระบบ CUPPEP (`https://chulappep.com/`):** ข้อมูลพื้นฐานนิสิต, ที่ปรึกษา, การศึกษามัธยม, การฝึกงาน, รอบรับเข้า, ผลงานนิสิต, ข้อมูลการได้งานทำครั้งที่ 1 (คณะ), และผลสอบใบประกอบวิชาชีพ (`CC1/CC2`, `IP1/IP2`, `PC1/PC2`)
2. **Google Form / Google Sheet / Google Drive (`@pharm.chula.ac.th`):** ข้อมูลผู้ใช้บัณฑิต, ข้อมูลการอบรม (กรณีใช้แนวทาง A หรือนำเข้าข้อมูลเก่า)
3. **ข้อมูลการได้งานทำชุด Final จากจุฬาฯ:** ไฟล์ข้อมูลสำรวจภาวะการมีงานทำจากมหาวิทยาลัย
4. **ข้อมูลย้อนหลังอย่างน้อย 10 ปี (`Q13`) และ Scopus (`Q16`)**

เนื่องจากข้อมูลที่ดึงเข้ามาจะต้องผ่านการตรวจสอบและแก้ไขโดยเจ้าหน้าที่ในระบบ `CU-PFIS` จึงต้องมีกลไก **Staging & Non-Destructive Sync** เพื่อป้องกันไม่ให้การดึงข้อมูลซ้ำในรอบถัดไปเขียนทับข้อมูลที่เจ้าหน้าที่ตรวจสอบรับรองแล้วโดยอัตโนมัติ

## 2. ขอบเขต (Scope)

### สิ่งที่ต้องทำในรอบนี้ (In scope)

- บันทึกประวัติรอบการนำเข้า/ซิงก์ข้อมูล (`ImportBatch`) พร้อมระบุแหล่งที่มา (`SourceSystem`), วันเวลา (`ImportedAt`), ผู้สั่งนำเข้า และสรุปจำนวนรายการ (สำเร็จ / ข้ามเพราะไม่เปลี่ยนแปลง / รอตรวจสอบความขัดแย้ง / ผิดพลาด)
- บันทึกข้อมูลพัก (`StagingRecord`) พร้อมรหัสต้นทาง (`SourceRecordId`) และ Hash/Snapshot ของข้อมูลต้นทาง
- ระบบตรวจจับและจัดการคิวข้อมูลขัดแย้ง (`DataConflictQueue`):
  - หากข้อมูลต้นทางมีการเปลี่ยนแปลงในรายการที่เจ้าหน้าที่เคยกดรับรอง (`Verified` / `Rechecked`) หรือแก้ไขใน `CU-PFIS` ไปแล้ว ระบบจะพักรายการนั้นไว้ใน `DataConflictQueue` เพื่อให้เจ้าหน้าที่เปรียบเทียบค่าเดิมกับค่าใหม่และตัดสินใจว่าจะใช้ค่าใด
- รองรับการนำเข้าข้อมูลย้อนหลังอย่างน้อย 10 ปี แยกตามปีการศึกษา (ผ่านไฟล์ Excel/CSV หรือการกรอกเพิ่มเติมตามหน้า 1)

### สิ่งที่ไม่รวมในรอบนี้ (Out of scope)

- การเขียนข้อมูลกลับไปทับฐานข้อมูลของระบบ `CUPPEP` โดยอัตโนมัติ (ยกเว้นการแจ้งสถานะขอแก้ไขผลงานนิสิตตามที่ตกลงใน `Q02`)

## 3. ศัพท์ที่เกี่ยวข้อง (Domain terms)

- **`ImportBatch` (รอบการนำเข้าข้อมูล):** บันทึกการดึงหรืออัปโหลดข้อมูลจากแหล่งภายนอกแต่ละครั้ง
- **`StagingRecord` (ข้อมูลพักก่อนเข้าตารางหลัก):** ข้อมูลดิบที่รับมาจากภายนอกพร้อม `SourceSystem` และ `SourceRecordId`
- **`DataConflictQueue` (คิวตรวจสอบข้อมูลขัดแย้ง):** รายการที่ข้อมูลนำเข้าใหม่ขัดแย้งกับข้อมูลที่เจ้าหน้าที่แก้ไข/รับรองแล้วใน `CU-PFIS`

## 4. ข้อมูล/ฟิลด์ที่เกี่ยวข้อง (Data / Fields)

| ชื่อฟิลด์ (ไทย) | ชื่อฟิลด์ (English) | ประเภทข้อมูล | บังคับ/ไม่บังคับ | แหล่งข้อมูล (CU-PFIS / CUPPEP / Sheet) | กติกา/validation |
| --- | --- | --- | --- | --- | --- |
| ระบบต้นทาง | `SourceSystem` | Enum | บังคับ | CU-PFIS | `CUPPEP`, `GoogleSheet`, `ChulaFinalEmployment`, `Scopus`, `HistoricalImport` |
| รหัสรายการต้นทาง | `SourceRecordId` | ข้อความ | บังคับ | แหล่งภายนอก | ห้ามว่าง, ห้ามใช้เลขแถวของ Google Sheet เดี่ยว ๆ |
| รหัสจับคู่บุคคล | `PersonKey` (`StudentCode` / `PersonnelCode`) | ข้อความ | บังคับ | แหล่งภายนอก | ห้ามจับคู่ด้วยชื่อ-นามสกุลเพียงอย่างเดียว |
| ข้อมูลก่อนหน้า vs ข้อมูลใหม่ | `CurrentValueJson`, `IncomingValueJson` | JSON | บังคับเมื่อเกิด Conflict | CU-PFIS | ใช้แสดงเปรียบเทียบให้เจ้าหน้าที่ตัดสินใจ |
| สถานะการจัดการข้อขัดแย้ง | `ResolutionStatus` | Enum | ระบบกำหนด | CU-PFIS | `PendingReview`, `KeepMasterValue`, `AcceptIncomingValue`, `MergedManually` |

## 5. ความสัมพันธ์กับข้อมูลอื่นและการเชื่อมต่อภายนอก (Relationships & External Sync)

- เชื่อมต่อกับ Adapter ของ `CUPPEP`, `Google Sheet`, `ไฟล์ชุด Final จุฬาฯ` และ `Scopus`
- ส่งข้อมูลที่ผ่านการตรวจสอบเข้าสู่ `Students`, `ProfessionalLicense`, `Training`, และ `Faculty` Bounded Contexts

## 6. กติกาทางธุรกิจและลำดับสถานะ (Business Rules & Workflow States)

1. **Idempotency:** การนำเข้าข้อมูลชุดเดิมซ้ำต้องไม่สร้างแถวซ้ำในฐานข้อมูล
2. **Stable Key Matching:** จับคู่นิสิตด้วย `StudentCode` และอาจารย์ด้วย `PersonnelCode` เสมอ
3. **Never Silently Overwrite Staff-Verified Data:** หากเรคคอร์ดใน `CU-PFIS` ถูกแก้ไขโดยเจ้าหน้าที่หรือมีสถานะ `Verified` / `Rechecked` แล้ว ข้อมูลนำเข้าใหม่ที่มีค่าต่างไปต้องเข้าสู่ `DataConflictQueue` เสมอ

## 7. ผลกระทบต่องบประมาณ/ตัวเลขสะสม (Money & Budget Ledger Impact)

- [ ] ใช่ แตะงบประมาณ/วงเงิน (หากนำเข้าข้อมูลการอบรมที่ใช้เงินวิชาการตามแนวทาง A ต้องผ่านการสร้างรายการใน `TrainingBudgetLedger` เท่านั้น ห้ามแก้ยอดคงเหลือโดยตรง)
- [x] **ไม่แตะงบประมาณโดยตรงในโมดูลพักข้อมูล**

## 8. สิทธิ์การเข้าถึงและข้อมูลส่วนบุคคล (Permissions & PDPA)

- `integration.imports.run`, `integration.conflicts.resolve` — เฉพาะเจ้าหน้าที่ (`Staff`) และ `Admin`

## 9. หน้าจอและการส่งออกรายงาน (UI / Frontend & Exports)

- `list` — หน้าประวัติการดึง/นำเข้าข้อมูล (`ImportBatch`) และหน้ารายการข้อมูลขัดแย้งที่รอตรวจสอบ (`DataConflictQueue`)
- `view/[id]` — หน้าเปรียบเทียบข้อมูลเดิมในระบบกับข้อมูลที่นำเข้าใหม่ พร้อมปุ่มเลือกใช้ค่าเดิม หรือรับค่าใหม่

## 10. เกณฑ์การยอมรับ (Acceptance Criteria)

- [ ] เมื่อนำเข้าข้อมูลชุดเดิมซ้ำ 2 ครั้ง ระบบจะไม่สร้างข้อมูลซ้ำซ้อน
- [ ] เมื่อนำเข้าข้อมูลที่มีการเปลี่ยนแปลงในรายการที่เจ้าหน้าที่แก้ไขหรือรับรองแล้ว ระบบจะไม่เขียนทับอัตโนมัติ แต่สร้างรายการใน `DataConflictQueue` ให้เจ้าหน้าที่ตัดสินใจ
- [ ] ทุกเรคคอร์ดที่นำเข้าจากภายนอกต้องมี `SourceSystem`, `SourceRecordId` และ `ImportedAt` ครบถ้วน

## 11. เคสที่ต้องทดสอบ (Test Cases)

### กรณีสำเร็จ (Happy path)
- นำเข้าข้อมูลใหม่จาก CUPPEP / Sheet → สร้างเรคคอร์ดสำเร็จพร้อม Provenance → นำเข้าซ้ำโดยข้อมูลไม่เปลี่ยน → ระบบข้ามรายการอย่างถูกต้อง

### กรณีข้อมูลขัดแย้ง (Conflict Detection)
- เจ้าหน้าที่แก้ไขข้อมูลใน `CU-PFIS` แล้ว → นำเข้าข้อมูลใหม่จากต้นทางที่ค่าไม่ตรงกัน → ระบบส่งเข้า `DataConflictQueue` และรักษาค่าของเจ้าหน้าที่ไว้จนกว่าจะกดตัดสินใจ

## 12. เกี่ยวข้องกับ/ขึ้นกับ feature อื่น (Dependencies)

- `requirements/Students/student-cv.md`
- `requirements/Students/student-achievement.md`
- `requirements/Students/graduate-employment.md`
- `requirements/Students/employer-evaluation.md`
- `requirements/ProfessionalLicense/license-exam-result.md`

## 13. เอกสารอ้างอิงเพิ่มเติม (References)

- `.doc/req071169.pdf` (หน้า 1–7, 10–11)
- `.ai/coding-rules.md` (หัวข้อ External data sync & re-import safety)
- `.ai/roadmap.md` (กฎ Non-negotiable ข้อ 2)
