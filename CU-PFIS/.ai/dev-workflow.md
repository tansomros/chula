# คู่มือการพัฒนา (Development Workflow) — CU-PFIS

คู่มือฉบับนี้เขียนขึ้นสำหรับ**คนในทีม** ที่พัฒนาโปรเจกต์ **CU-PFIS : ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย (Chula Pharmacy Faculty Information System)** — อธิบายสถาปัตยกรรมที่ใช้ การติดตั้งเครื่องมือ การรันโปรเจกต์ การเทส และขั้นตอนทำงานร่วมกับ AI Agent

> เอกสารสำหรับ AI Agent ดูที่ [prompt-templates.md](prompt-templates.md) และ [ai-agent-guide.md](ai-agent-guide.md)
> เอกสารขอบเขตต้นฉบับจากลูกค้าอยู่ที่ [`.doc/req071169.pdf`](../.doc/req071169.pdf)

---

## ส่วนที่ 0 — สถาปัตยกรรมซอฟต์แวร์ที่ใช้ในโปรเจกต์

โปรเจกต์นี้ใช้โครงสร้างมาตรฐานเดียวกับโปรเจกต์ต้นแบบของทีม (Clean Architecture + CQRS + Vue 3) เพื่อให้ทั้งทีมและ AI Agent ทำงานได้อย่างเป็นระบบ (รายละเอียดเชิงเทคนิคอยู่ที่ [architecture.md](architecture.md))

### 0.1 Clean Architecture (4 Layers)

```text
Domain          → ไม่ reference โปรเจกต์ไหนเลย (Entity, Enum, Guard Clauses, Domain Rules)
Application     → reference Domain (CQRS Commands/Queries, FluentValidation, Interfaces)
Infrastructure  → reference Application, Domain (EF Core PostgreSQL, CUPPEP/Google Sheet Adapters, PDF Service)
API             → reference Application, Infrastructure (Thin Controllers, Auth, Filters)
```

### 0.2 CQRS & MediatR Pipeline

แต่ละ use case แยกโฟลเดอร์ชัดเจนที่ `src/Application/Features/{Context}/{Feature}/{Commands|Queries}/{ชื่อ}/` โดยมี Pipeline Behaviours ทำงานอัตโนมัติทุก Request:

```text
UnhandledExceptionBehaviour → AuthorizationBehaviour → ValidationBehaviour
  → PerformanceBehaviour → LoggingBehaviour → (Handler จริง)
```

### 0.3 กฎสำคัญของระบบ CU-PFIS (Domain & Data Integrity)

1. **Dual Key (`Id` + `ExternalId`):** ใช้ `int Id` สำหรับ FK ภายในฐานข้อมูล และใช้ `Guid ExternalId` (UUIDv7) เท่านั้นสำหรับ API Route / DTO / URL หน้าเว็บ
2. **Training Budget Ledger:** งบประมาณการอบรมอาจารย์ (เช่น 10,000 บาท/คน/ปี ที่ตั้งค่ารายปีได้) ต้องคำนวณยอดคงเหลือจากตาราง `TrainingBudgetLedger` แบบ Append-only ห้ามบวกลบเลขทับคอลัมน์เดียวตรง ๆ
3. **Non-destructive External Sync (CUPPEP / Google Sheet / จุฬาฯ Final):** ข้อมูลที่นำเข้าซ้ำต้องไม่เขียนทับข้อมูลที่เจ้าหน้าที่ตรวจสอบ/แก้ไขแล้วในระบบ **CU-PFIS** โดยอัตโนมัติ หากข้อมูลขัดแย้งกันต้องส่งเข้าคิวตรวจสอบ (`DataConflictQueue`) พร้อมเก็บแหล่งที่มา (`SourceSystem`, `SourceRecordId`, `ImportedAt`)
4. **Optimistic Concurrency & AuditLog:** ทุก Entity มี `xmin` ป้องกันการบันทึกทับกันเมื่อแก้ไขพร้อมกัน และบันทึกประวัติการแก้ไขระดับฟิลด์ลง `AuditLog` อัตโนมัติ

### 0.4 ก่อนเริ่มเขียนโค้ดหรือแก้โค้ด ควรอ่านอะไรก่อน

1. **[`.doc/req071169.pdf`](../.doc/req071169.pdf)** — หากต้องการดูเอกสารขอบเขตต้นฉบับจากลูกค้า (ส่วนบทวิเคราะห์และคำถามก่อนประชุมสรุปรวมไว้ใน [roadmap.md](roadmap.md), [known-issues.md](known-issues.md) และ [domain.md](domain.md) แล้ว)
2. **[`requirements/{Context}/{feature}.md`](../requirements/)** — สเปกรายฟีเจอร์ที่สรุปขอบเขต ฟิลด์ กติกาธุรกิจ และเกณฑ์ตรวจรับ
3. **[known-issues.md](known-issues.md)** และ **[roadmap.md](roadmap.md)** — เช็คสถานะเฟสปัจจุบันและคำถามที่รอยืนยันจากลูกค้า (`Q01–Q18`)
4. **[domain.md](domain.md)** — คำศัพท์มาตรฐานของโปรเจกต์ **CU-PFIS** (ทั้งไทยและอังกฤษ)
5. **[coding-rules.md](coding-rules.md)** และ **[ai-agent-guide.md](ai-agent-guide.md)** — กฎการเขียนโค้ดและโครงสร้างโฟลเดอร์

---

## ส่วนที่ 1 — เครื่องมือที่ต้องติดตั้งก่อนเริ่มงาน

1. **Git + SourceTree** (หรือ Git client ที่ถนัด)
2. **Windows Terminal / PowerShell**
3. **.NET 10 SDK** (`dotnet --version`)
4. **dotnet-ef CLI** (`dotnet tool install --global dotnet-ef`)
5. **nvm + Node.js LTS** (`node --version`)
6. **pnpm** (`corepack enable && corepack prepare pnpm@latest --activate`)
7. **PostgreSQL 16+** (ติดตั้งลงเครื่องสำหรับฐานข้อมูล `cupharm` และ `cupharm-test` — ไม่ใช้ Docker/Testcontainers ในการรันเทสประจำวัน)
8. **Cursor / VS Code / Visual Studio 2026**

---

## ส่วนที่ 2 — การทำงานร่วมกับ AI ในแต่ละงาน (Hybrid AI Workflow)

โปรเจกต์นี้ใช้ AI Agent (ผ่าน Cursor / Claude Code) เป็นตัวช่วยหลักในการวิเคราะห์และพัฒนา โดยมีขั้นตอนมาตรฐานต่อ 1 ฟีเจอร์ดังนี้:

1. **เขียน/อัปเดตไฟล์ Requirement ก่อนเสมอ:**
   - คัดลอก [`requirements/_template.md`](../requirements/_template.md) ไปวางในโฟลเดอร์ Bounded Context ที่เกี่ยวข้อง เช่น `requirements/Faculty/faculty-profile.md`, `requirements/Training/training-request.md`
   - กรอกรายละเอียดให้ครบ หากส่วนไหนยังรอคำตอบจากลูกค้า (`Q01–Q18`) ให้ระบุไว้ชัดเจนในไฟล์
2. **เปิด Session กับ AI พร้อมบริบทมาตรฐาน:**
   - ใช้ **Shared Preamble** และ Prompt ตามสเตจจาก [prompt-templates.md](prompt-templates.md) พร้อมอ้างอิงไฟล์ `requirements/{Context}/{feature}.md`
3. **ให้ AI วางแผนก่อน (Plan First):**
   - สั่งให้ AI สรุปแผนการทำงาน (ไฟล์ที่จะสร้าง/แก้, Entity, กติกา Validation, Ledger, Permission) ให้ทีมตรวจทานก่อนลงมือเขียนโค้ดจริง
4. **ตรวจสอบ Diff และรัน Build / Test:**
   - ตรวจสอบโค้ดที่ AI สร้าง โดยเฉพาะจุดที่แตะงบประมาณอบรม การนำเข้าข้อมูลจาก CUPPEP/Sheet และสิทธิ์การเข้าถึงข้อมูลส่วนบุคคล (เช่น เลขบัญชีธนาคาร)
   - รัน Build และ Test ยืนยันผลทุกครั้ง
5. **อัปเดตสถานะในไฟล์ Requirement และ `roadmap.md`:**
   - ปรับสถานะในไฟล์ Requirement (`ร่าง` → `พร้อมพัฒนา` → `กำลังพัฒนา` → `เสร็จสิ้น`)

---

## ส่วนที่ 3 — คำสั่งที่ใช้บ่อย (เมื่อเริ่ม Scaffold โค้ดโปรเจกต์)

```bash
# 1. สร้างฐานข้อมูลบน PostgreSQL local
# CREATE DATABASE cupharm;
# CREATE DATABASE "cupharm-test";

# 2. รัน Backend API
dotnet run --project src/API

# 3. รัน Frontend Dev Server
cd src/vuewebui && pnpm dev

# 4. รัน Build & Automated Tests
dotnet build chula.sln --nologo
dotnet test chula.sln
cd src/vuewebui && pnpm run lint && pnpm run build

# 5. จัดการ Database Migration
dotnet ef migrations add InitialCreate --project src/Infrastructure --startup-project src/API -o Data/Migrations
dotnet ef database update --project src/Infrastructure --startup-project src/API
```
