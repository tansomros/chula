# Chula — Faculty of Pharmaceutical Sciences Monorepo

Repository กลางสำหรับรวบรวมระบบสารสนเทศของ **คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย** (Faculty of Pharmaceutical Sciences, Chulalongkorn University) จำนวน **3 โปรเจ็ค** ไว้ในที่เดียวกัน เพื่อความสะดวกในการบริหารจัดการซอร์สโค้ดและการเชื่อมต่อข้อมูลระหว่างระบบ

---

## 1. ภาพรวมทั้ง 3 โปรเจ็ค (Projects Overview)

| โฟลเดอร์ | ชื่อระบบ | บทบาทและขอบเขตงาน | Tech Stack | ฐานข้อมูล |
| --- | --- | --- | --- | --- |
| [`CU-PFIS/`](CU-PFIS/) | **Chula Pharmacy Faculty Information System**<br>(ระบบสารสนเทศคณะเภสัชศาสตร์) | **ระบบหลัก (Master System)** ที่พัฒนาขึ้นใหม่ สำหรับบริหารข้อมูลอาจารย์ (CV), การอบรมและงบประมาณ, ข้อมูลนิสิต/ศิษย์เก่า, ผลงาน/รางวัล, การได้งานทำ, ผู้ใช้บัณฑิต, ผลสอบใบประกอบวิชาชีพ, โครงงานนิสิต และรายงานย้อนหลัง 10 ปี | **.NET 10** (Clean Architecture, CQRS/MediatR, EF Core) + **Vue 3 / Vuetify 3** (TH/EN) | PostgreSQL (`cupharm`) |
| [`CU-PPEP/`](CU-PPEP/) | **Chula Pharmacy Professional Experience Practice**<br>(ระบบฝึกปฏิบัติงานวิชาชีพเภสัชกรรม) | **`Chula-PPS/`**: เว็บเดิมที่ใช้งานอยู่จริง (`chulappep.com`) สำหรับบริหารการฝึกปฏิบัติงานวิชาชีพ และเป็นหน้าให้นิสิตกรอกข้อมูลพื้นฐาน/ผลงาน/ผลสอบ/การได้งานทำรอบแรก *(ไม่มี WebAPI แยก — `CU-PFIS API` ดึงข้อมูลแบบ Read-Only จากฐานข้อมูล `chula_pps` โดยตรงผ่าน `SqlCuppepDataSource`)* | **ASP.NET Web Forms** (VB.NET, .NET Framework 4.8, DevExpress) | MS SQL Server (`chula_pps`) |
| [`CU-BCP/`](CU-BCP/) | **Chula BCP**<br>(ระบบเว็บไซต์และฐานข้อมูลหลักสูตรเภสัชกรประจำบ้าน) | ระบบเว็บไซต์ประชาสัมพันธ์ CMS และฐานข้อมูลหลักสูตรเภสัชกรประจำบ้านเดิม *(เก็บไว้รวมใน repo นี้เพื่อการจัดการโค้ดเท่านั้น — **อยู่นอกขอบเขตงาน CU-PFIS ห้ามแก้ไขโดยไม่ได้รับคำสั่ง**)* | **ASP.NET Web Forms** (VB.NET, .NET Framework 4.5, DevExpress) | MS SQL Server |

---

## 2. โครงสร้างโฟลเดอร์ (Repository Structure)

```text
Chula/
├── README.md                          # เอกสารสรุปภาพรวมและโครงสร้าง Monorepo (ไฟล์นี้)
├── .gitignore                         # กฎ ignore กลางครอบคลุม .NET, Vue/Vite, IDE, packages, temp files
│
├── CU-PFIS/                           # [Master System] ระบบสารสนเทศคณะเภสัชศาสตร์ (พัฒนาใหม่)
│   ├── README.md                      # คู่มือรายละเอียดโปรเจ็ค CU-PFIS
│   ├── AGENTS.md                      # จุดเริ่มต้นบริบทสำหรับ AI Agent
│   ├── chula.sln                      # .NET 10 Solution
│   ├── .doc/                          # เอกสาร Requirement ต้นฉบับจากลูกค้า (req071169.pdf ฯลฯ)
│   ├── .ai/                           # บริบทสถาปัตยกรรม, Domain Glossary, Coding Rules, Roadmap (D01–D12, Q01–Q18 / A01–A18)
│   ├── requirements/                  # สเปกฟีเจอร์ภาษาไทยแยกตาม Bounded Context
│   ├── src/
│   │   ├── Domain/                    # Entities, Enums, Value Objects, Domain Events (Dual-key: Id + ExternalId)
│   │   ├── Application/               # CQRS Commands/Queries (MediatR), Validators, Behaviours, Interfaces (ICuppepDataSource)
│   │   ├── Infrastructure/            # ChulaDbContext (PostgreSQL), Migrations, Interceptors, SqlCuppepDataSource (Read-Only Dapper)
│   │   ├── API/                       # ASP.NET Core Web API (Thin Controllers + Swagger/OpenAPI)
│   │   └── vuewebui/                  # Frontend (Vue 3 + Vuetify 3 + Pinia + vue-i18n TH/EN + CASL)
│   └── tests/
│       ├── Domain.UnitTests/          # Unit Tests สำหรับ Domain
│       └── Application.FunctionalTests/ # Functional / Integration Tests (PostgreSQL cupharm-test)
│
├── CU-PPEP/                           # ระบบฝึกปฏิบัติงานวิชาชีพเภสัชกรรม (Legacy Web Application)
│   ├── README.md                      # เอกสารอธิบายสถาปัตยกรรมและโมดูลของ Chula-PPS
│   ├── Chula-PPEP.sln                 # Solution ของเว็บเดิม (Web Forms)
│   └── Chula-PPS/                     # [Legacy Web] ASP.NET Web Forms (VB.NET / .NET Framework 4.8)
│       ├── Controllers/               # Data Access Layer (SqlHelper / Stored Procedures)
│       ├── Models/                    # DTOs และ Typed DataSets
│       ├── Reports/                   # รายงาน RDLC / SSRS
│       └── *.aspx / *.aspx.vb         # หน้าจอ Web Forms (130+ หน้า)
│
└── CU-BCP/                            # [Out of Scope / Reference Only] ระบบหลักสูตรเภสัชกรประจำบ้าน
    ├── README.md                      # ขอบเขตความสามารถของระบบ CU-BCP แบ่งตามเฟส (Phase 1–3)
    ├── ChulaBCP.sln                   # Solution (ASP.NET Web Forms)
    └── ChulaBCP/                      # ซอร์สโค้ด ASP.NET Web Forms (VB.NET / .NET Framework 4.5)
```

---

## 3. สถาปัตยกรรมการเชื่อมต่อระหว่างระบบ (Integration Architecture)

ระบบถูกออกแบบตามแนวทาง **Single-API Direct Read-Only Integration Architecture (`Option 1B` — Same Server / 2 Domains)** โดยไม่มีการสร้าง WebAPI แยกใน `CU-PPEP` เพื่อให้ดูแลรักษาง่าย ขณะที่ยังคงแยกขาดฐานข้อมูลเดิม (`chula_pps`) ออกจาก EF Core ของ `CU-PFIS` และป้องกันไม่ให้ข้อมูลที่เจ้าหน้าที่ตรวจสอบรับรองแล้วถูกเขียนทับโดยอัตโนมัติ

```mermaid
flowchart LR
    subgraph CUPPEP["CU-PPEP (Domain 1 — ระบบฝึกปฏิบัติงานวิชาชีพ)"]
        LEGACY["Chula-PPS WebForms\n(.NET Framework 4.8 / VB.NET)\nนิสิตกรอกข้อมูล & ฝึกงาน"]
        MSSQL[("MS SQL Server\n(chula_pps)\nLegacy Tables + Extension Tables\n+ Views")]
        LEGACY -->|Connection String\nWrite / Read| MSSQL
    end

    subgraph EXTERNAL["แหล่งข้อมูลภายนอกอื่น ๆ"]
        GSHEET["Google Form / Sheet\n(แบบประเมินผู้ใช้บัณฑิต)"]
        CHULA["ข้อมูลชุด Final จุฬาฯ\n(การได้งานทำ)"]
        SCOPUS["Scopus / Excel Import\n(ผลงานตีพิมพ์ & ประวัติอบรม)"]
    end

    subgraph CUPFIS["CU-PFIS (Domain 2 — Master System / Single API)"]
        PFISAPI["CU-PFIS API\n(.NET 10 Clean Architecture)\nSqlCuppepDataSource (SqlClient + Dapper)"]
        STAGING["ImportBatch → StagingRecord\n→ DataConflictQueue\n(Non-Destructive Sync)"]
        PG[("PostgreSQL\n(cupharm)")]
        VUE["Vue 3 + Vuetify 3\n(Admin / Staff / Faculty)\nBilingual TH/EN"]
        PFISAPI --> STAGING
        STAGING --> PG
        VUE <-->|REST / OpenAPI| PFISAPI
    end

    MSSQL -->|Read-Only Connection String\nOne-Way Incremental Pull| PFISAPI
    GSHEET -->|Import| PFISAPI
    CHULA -->|Recheck & Confirm| PFISAPI
    SCOPUS -->|Import| PFISAPI
```

### หลักการสำคัญของการแบ่งหน้าที่และเชื่อมต่อข้อมูล
1. **การแบ่งหน้าที่ระหว่าง `CU-PPEP` กับ `CU-PFIS`:**
   - **นิสิต** ใช้งานและกรอกข้อมูลตนเอง (ข้อมูลพื้นฐาน, รอบรับเข้า, ผลงาน/รางวัล, ผลสอบใบประกอบวิชาชีพ, การได้งานทำรอบแรก) ผ่าน **`CU-PPEP`** (`Chula-PPS`) เพื่อลดความซ้ำซ้อนและไม่ให้เกิดการเขียนข้อมูลชนกัน
   - **เจ้าหน้าที่ อาจารย์ และผู้ดูแลระบบ** ใช้งาน **`CU-PFIS`** เป็นระบบหลัก (Master System) สำหรับตรวจรับรองข้อมูล บริหารงบอบรม จัดการโครงงานนิสิต และออกรายงาน/Dashboard
2. **ความปลอดภัยต่อฐานข้อมูลเดิม (`chula_pps`):**
   - `CU-PPEP` **ไม่มี WebAPI แยก** แต่ `CU-PFIS API` ทำหน้าที่เป็น Single API บนเซิร์ฟเวอร์เดียวกัน โดยดึงข้อมูลแบบ **Read-Only** จาก `chula_pps` ผ่าน Adapter เฉพาะทางในชั้น Infrastructure (`SqlCuppepDataSource : ICuppepDataSource` ด้วย `Microsoft.Data.SqlClient` + Dapper และ Connection String `ConnectionStrings:CuppepSqlServer`)
   - ชั้น `Domain` และ `Application` ไม่แตะ SQL Server โดยตรง และ `ChulaDbContext` (EF Core) ยังคงเชื่อมต่อเฉพาะ **PostgreSQL (`cupharm`)** เพียงฐานข้อมูลเดียว จึงไม่มีความเสี่ยงที่ Migration จะไปแก้ไขหรือลบตารางเดิมใน `chula_pps`
   - **กฎการตั้งชื่อตารางใหม่ใน `chula_pps`:** ตารางที่สร้างเพิ่มใหม่ในฐานข้อมูล `chula_pps` ให้ตั้งชื่อแบบ `PascalCase` ตามโดเมนตรง ๆ ให้สอดคล้องกับตารางเดิม (เช่น `dbo.StudentAchievements`, `dbo.StudentProfileExt`, `dbo.StudentLicenseResults`, `dbo.StudentEmploymentRound1`) **โดยไม่ต้องใส่ prefix `PFIS_`**
3. **Non-Destructive Sync (`ImportBatch` → `StagingRecord` → `DataConflictQueue`):**
   - ข้อมูลที่ดึงมาจาก `CU-PPEP` หรือแหล่งภายนอกจะเก็บแหล่งที่มา (`SourceSystem`, `SourceRecordId`, `ImportedAt`) เสมอ
   - หากรายการใดถูกเจ้าหน้าที่ตรวจสอบหรือแก้ไขรับรองใน `CU-PFIS` ไปแล้ว การดึงข้อมูลรอบถัดไปจะ **ไม่เขียนทับอัตโนมัติ** แต่จะส่งเข้าคิว `DataConflictQueue` ให้เจ้าหน้าที่พิจารณาตัดสินใจ
4. **ขอบเขตของ `CU-BCP`:**
   - `CU-BCP` เป็นระบบเดิมที่แยกอิสระ (หลักสูตรเภสัชกรประจำบ้าน) และไม่มีการเชื่อมต่อข้อมูลกับ `CU-PFIS` ในเฟสปัจจุบัน — **ไม่ควรแก้ไขไฟล์ในโฟลเดอร์ `CU-BCP/` เมื่อทำงานในขอบเขต `CU-PFIS` / `CU-PPEP`**

---

## 4. รายละเอียดแต่ละระบบโดยย่อ

### 4.1 `CU-PFIS` — ระบบสารสนเทศคณะเภสัชศาสตร์ (Master System)
- **โมดูลหลัก (Bounded Contexts):**
  - `ReferenceData`: ปีการศึกษา (ย้อนหลัง 10+ ปี), ภาควิชา/หน่วยงาน, สาขาวิชา, ตำแหน่งทางวิชาการ, รอบรับเข้า, แม่แบบเอกสาร
  - `Faculty`: ประวัติอาจารย์ (CV อาจารย์), วุฒิการศึกษา, ผลงานตีพิมพ์ (รองรับการนำเข้า Scopus), ส่งออก PDF
  - `Training`: การอบรม 4 ประเภท + ระบบสมุดคุมงบประมาณรายปีงบประมาณแบบ Append-only (`TrainingBudgetLedger`) รองรับการยืมโควต้าปีงบประมาณถัดไป และแยกขั้นตอน "เจ้าหน้าที่ตรวจสอบ" ออกจาก "อนุมัติหลังคณบดีลงนาม"
  - `Students`: CV นิสิต, การตรวจรับรองผลงาน/รางวัลจาก CUPPEP, การเทียบและยืนยันข้อมูลการได้งานทำกับชุด Final จุฬาฯ, ข้อมูลผู้ใช้บัณฑิตจาก Google Form/Sheet
  - `ProfessionalLicense`: ผลสอบใบประกอบวิชาชีพ (`CC1`, `CC2`, `IP1`, `IP2`, `PC1`, `PC2`) และสรุปร้อยละผู้สอบผ่านตามรุ่น
  - `SeniorProjects`: โครงงานนิสิต, จัดเก็บไฟล์ PDF ฉบับจริง, การรวมเล่ม Abstract รายปีอัตโนมัติ, แจ้งเตือนติดตามการใช้ประโยชน์ปีที่ 1–3
  - `Integration`, `Reporting`, `Security`: ระบบซิงก์ข้อมูลและจัดการความขัดแย้ง, Dashboard/รายงาน 2 ภาษา (TH/EN), สิทธิ์ระดับภาควิชา และการยืนยัน PDPA Consent ครั้งแรก
- **เอกสารอ้างอิงเพิ่มเติม:** [`CU-PFIS/README.md`](CU-PFIS/README.md), [`CU-PFIS/.ai/architecture.md`](CU-PFIS/.ai/architecture.md), [`CU-PFIS/.ai/roadmap.md`](CU-PFIS/.ai/roadmap.md), [`CU-PFIS/.ai/domain.md`](CU-PFIS/.ai/domain.md)

### 4.2 `CU-PPEP` — ระบบฝึกปฏิบัติงานวิชาชีพเภสัชกรรม
- **`Chula-PPS/` (Legacy Web Application):** ระบบ Web Forms (VB.NET / .NET Framework 4.8) จัดการการลงทะเบียนนิสิต แหล่งฝึก การจัดสรรแหล่งฝึก (PC2/Random/Manual) อาจารย์นิเทศก์ แหล่งฝึก การประเมินผล เกรด และใบรับรอง รวมถึงเป็นช่องทางให้นิสิตกรอกข้อมูลส่วนขยายลง `chula_pps` เพื่อให้ `CU-PFIS` ดึงไปตรวจสอบแบบ Read-Only — ดูรายละเอียดที่ [`CU-PPEP/README.md`](CU-PPEP/README.md)

### 4.3 `CU-BCP` — ระบบหลักสูตรเภสัชกรประจำบ้าน
- เว็บไซต์ประชาสัมพันธ์ (CMS), ฐานข้อมูลเภสัชกรประจำบ้าน/อาจารย์/แหล่งฝึก/คำร้อง และการจัดการเอกสารของหลักสูตร — ดูรายละเอียดขอบเขตเดิมที่ [`CU-BCP/README.md`](CU-BCP/README.md)

---

## 5. คำสั่งเริ่มต้นใช้งาน (Quick Start)

### รัน `CU-PFIS` (Backend API & Frontend)
```powershell
# Backend (.NET 10 Web API)
dotnet build CU-PFIS/chula.sln
dotnet run --project CU-PFIS/src/API
dotnet test CU-PFIS/chula.sln

# Frontend (Vue 3 + Vuetify 3)
cd CU-PFIS/src/vuewebui
npm install
npm run dev
```

### โปรเจ็ค Legacy (`CU-PPEP/Chula-PPS` และ `CU-BCP/ChulaBCP`)
- เปิดไฟล์ Solution [`CU-PPEP/Chula-PPEP.sln`](CU-PPEP/Chula-PPEP.sln) หรือ [`CU-BCP/ChulaBCP.sln`](CU-BCP/ChulaBCP.sln) ด้วย **Visual Studio** (รองรับ .NET Framework 4.5 / 4.8 และ DevExpress v17.2) และรันผ่าน IIS Express
