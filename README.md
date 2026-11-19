# CU-PFIS : ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย (Chula Pharmacy Faculty Information System)

เว็บแอปพลิเคชันหลัก (Master System) สำหรับบริหารจัดการข้อมูลประวัติอาจารย์ (CV), การอบรมและงบประมาณ, ข้อมูลนิสิตและศิษย์เก่า, ผลงาน/รางวัล, การได้งานทำ, ข้อมูลผู้ใช้บัณฑิต, ผลสอบใบประกอบวิชาชีพ และโครงงานนิสิต (Senior Project) ของคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย

---

## 1. ภาพรวมโปรเจกต์ (Project Overview)

- **ชื่อระบบภาษาไทย:** ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย
- **ชื่อระบบภาษาอังกฤษ:** Chula Pharmacy Faculty Information System
- **ชื่อย่อ / ชื่อเรียกสั้น:** **CU-PFIS**
- **เจ้าของระบบ:** คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย
- **รูปแบบระบบ:** พัฒนา **Web Application ใหม่ (CU-PFIS)** แยกจากระบบ **CUPPEP** (ระบบฝึกปฏิบัติงานวิชาชีพเภสัชกรรมที่ใช้งานอยู่เดิม)
- **บทบาทของระบบ CU-PFIS:** เป็น **ระบบหลักในการดูแลข้อมูล (Master Data & Core Workflow)** การตรวจสอบความถูกต้อง การอนุมัติ การจัดการสิทธิ์ การจัดเก็บไฟล์โครงงานจริง และการออกรายงาน/Dashboard ย้อนหลังอย่างน้อย 10 ปี แยกตามปีการศึกษา
- **การเชื่อมต่อข้อมูลภายนอก:**
  - **CUPPEP:** ดึงข้อมูลพื้นฐานนิสิต อาจารย์ที่ปรึกษา ประวัติการศึกษา และการฝึกงาน รวมถึงรับข้อมูลที่นิสิตกรอกเพิ่มเติม (เช่น ผลงาน/รางวัล, การได้งานทำรอบแรก, ผลสอบใบประกอบวิชาชีพ) มาให้เจ้าหน้าที่ตรวจสอบในระบบ **CU-PFIS**
  - **Google Form / Google Sheet / Google Drive:** รับข้อมูลแบบประเมินผู้ใช้บัณฑิต และเป็นทางเลือกในการรับข้อมูลการอบรม
  - **ข้อมูลการได้งานทำชุด Final (จุฬาฯ):** นำมาเทียบและยึดเป็นข้อมูลหลักควบคู่กับการตรวจสอบของเจ้าหน้าที่
  - **Scopus:** ทางเลือกในการดึงข้อมูลผลงานตีพิมพ์ของอาจารย์
- **สถานะปัจจุบัน:** อยู่ระหว่างขั้นตอนการวิเคราะห์และยืนยัน Requirement (Pre-Meeting / Requirement Gathering)

---

## 2. โมดูลหลักของระบบ (Core Modules)

### 2.1 CV อาจารย์ (`Faculty`)
- จัดเก็บข้อมูลส่วนบุคคล ตำแหน่งทางวิชาการ วุฒิการศึกษา ผลงานตีพิมพ์ และประวัติการอบรม
- ค้นหาและเรียงลำดับข้อมูล ส่งออกไฟล์ PDF รายบุคคลหรือทั้งหมดตามแม่แบบ (Template)
- ระบบแจ้งเตือนอาจารย์ให้ปรับปรุงข้อมูลให้เป็นปัจจุบัน และทางเลือกการดึงผลงานจาก Scopus

### 2.2 การอบรมและงบประมาณ (`Training`)
- รองรับการอบรม 4 ประเภท:
  1. ใช้เงินสนับสนุนทางวิชาการ
  2. ไม่ใช้เงินสนับสนุนทางวิชาการ
  3. คณะจัด
  4. อบรมระยะสั้น / ต่างประเทศทางวิชาการ
- บริหารวงเงินงบประมาณรายบุคคลต่อปี (เช่น ค่าตั้งต้น 10,000 บาท/คน/ปี สามารถตั้งค่ารายปีได้) คำนวณยอดใช้ไปและยอดคงเหลือผ่านระบบ Ledger
- แยกขั้นตอน **"เจ้าหน้าที่ตรวจข้อมูลถูกต้อง"** ออกจาก **"การแจ้งอนุมัติหลังคณบดีลงนาม"** พร้อมสร้างเอกสาร PDF และส่งอีเมลแจ้งตามขั้นตอน

### 2.3 ข้อมูลนิสิต ผลงาน การได้งานทำ และผู้ใช้บัณฑิต (`Students`)
- **CV นิสิต:** รวบรวมข้อมูลทั่วไป อาจารย์ที่ปรึกษา ประวัติการศึกษา การฝึกงาน รอบรับเข้า โครงการ ผลงาน/รางวัล ใบประกอบวิชาชีพ การได้งานทำ และข้อมูลผู้ใช้บัณฑิต พร้อมส่งออก PDF
- **ผลงาน/รางวัลนิสิต:** นิสิตกรอกผ่าน CUPPEP → เจ้าหน้าที่ตรวจสอบในระบบ CU-PFIS (ขอแก้ไข / รับรอง) → แสดงผลใน CV นิสิตเมื่อได้รับการรับรอง
- **การได้งานทำ:** รับข้อมูลรอบแรกของคณะผ่าน CUPPEP นำมาตรวจเทียบกับข้อมูลชุด Final จากจุฬาฯ (ยึดข้อมูลจุฬาฯ เป็นหลัก) โดยเจ้าหน้าที่ตรวจสอบและปรับปรุงโดยไม่ถูกเขียนทับอัตโนมัติเมื่อนำเข้าซ้ำ
- **ข้อมูลผู้ใช้บัณฑิต:** รับผลประเมินจากผู้บังคับบัญชาผ่าน Google Form/Sheet นำมาจับคู่กับบัณฑิต ค้นหา เรียงลำดับ และส่งออกรายงาน

### 2.4 ใบประกอบวิชาชีพ (`ProfessionalLicense`)
- รับข้อมูลผลสอบจาก CUPPEP แยกตามประเภทการสอบ (`CC1`, `CC2`, `IP1`, `IP2` หรือ `PC1`, `PC2`)
- แสดงรายชื่อผู้สอบผ่าน สรุปจำนวนและคำนวณร้อยละแยกตามรุ่น/ปีการศึกษา

### 2.5 โครงงานนิสิตและการใช้ประโยชน์ (`SeniorProjects`)
- จัดเก็บข้อมูลโครงงานรายบุคคล/รายกลุ่ม เชื่อมโยงกับ CV นิสิต และอาจารย์ที่ปรึกษา
- อัปโหลดและจัดเก็บไฟล์ **PDF โครงงานฉบับจริง** บนระบบ พร้อมกำหนดสถานะการเผยแพร่ (`Publish`)
- **การรวมเล่ม Abstract รายปี:** ดึงหน้าบทคัดย่อ (ตามข้อกำหนดเบื้องต้นคือหน้าที่ 3 ของไฟล์ PDF) มารวมเป็นเล่มรายปีการศึกษา
- **การติดตามการใช้ประโยชน์:** แจ้งเตือนอาจารย์ที่ปรึกษาให้กรอกข้อมูลการใช้ประโยชน์จากโครงงาน เริ่มแจ้งหลังผ่านไป 1 ปี (แจ้งปีละครั้ง) และหยุดแจ้งเมื่อกรอกข้อมูลแล้วหรือครบ 3 ปี

### 2.6 ความต้องการร่วมและรายงาน (`Reporting` & Cross-cutting)
- **สองภาษา:** รองรับภาษาไทยและภาษาอังกฤษ (TH/EN)
- **กลุ่มผู้ใช้งานระบบ CU-PFIS:** Admin, เจ้าหน้าที่, อาจารย์ (ส่วนนิสิตเน้นกรอกข้อมูลผ่าน CUPPEP ตามกระบวนการที่ตกลง)
- **Dashboard & Export:** แสดงตัวชี้วัดสำคัญ และส่งออกรายงานตามแม่แบบในรูปแบบ PDF, Word, Excel ย้อนหลังอย่างน้อย 10 ปี แยกตามปีการศึกษา

---

## 3. เครื่องมือและสถาปัตยกรรมในการพัฒนา (Tech Stack & Architecture)

### 3.1 เครื่องมือในการพัฒนา
- **C#** / **.NET 10**
- **ASP.NET Core** Web API
- **Swagger / OpenAPI**
- **Entity Framework Core**
- **PostgreSQL**
- **Vue 3 + Vuetify 3** (Frontend)
- **Visual Studio 2026** หรือ **VS Code / Cursor / Antigravity IDE **

### 3.2 หลักการและรูปแบบที่นำมาใช้
- **Clean Architecture**
- **CQRS** (Command-Query Responsibility Segregation)
- **SOLID Principles**
- **Mediator Pattern** (MediatR)

---

## 4. การ Build, Run, Scaffold และการรันทดสอบ

### 4.1 ขั้นตอนการรันใน Visual Studio
- ใน Solution มีหลาย Project ให้เลือก Startup Project ที่ต้องการรัน (เช่น `API`)

### 4.2 ขั้นตอนการ Build และ Run บน Terminal

- **Build Solution:**
  ```powershell
  dotnet build -tl
  ```
- **รัน Project:**
  ```powershell
  dotnet run --project src/API
  ```
- **รัน Project แบบ Auto Reload (`dotnet watch`):**
  ```powershell
  dotnet watch run --project src/API
  ```
- **รันการทดสอบ (Automated Tests):**
  ```powershell
  dotnet test
  ```

### 4.3 เครื่องมือสร้างโค้ด (Code Scaffolding)

- **สร้าง Command:**
  ```powershell
  cd src\Application
  dotnet new ca-usecase --name CreateTodoList --feature-name TodoLists --usecase-type command --return-type int
  ```
- **สร้าง Query:**
  ```powershell
  cd src\Application
  dotnet new ca-usecase -n GetTodos -fn TodoLists -ut query -rt TodosVm
  ```
- **กรณีพบปัญหาไม่พบ Template (`ca-usecase`):**
  หากพบข้อความแจ้งเตือน *"No templates or subcommands found matching: 'ca-usecase'."* ให้ติดตั้ง Template ก่อนแล้วลองใหม่อีกครั้ง:
  ```powershell
  dotnet new install Clean.Architecture.Solution.Template::9.0.10
  ```

---

## 5. การทำ Database Migration (Code First)

### 5.1 ผ่าน Package Manager Console (Visual Studio)

1. ไปที่เมนู **Tools** → **NuGet Package Manager** → **Package Manager Console**
2. ในแท็บ Package Manager Console เลือก **Default project** เป็น `Infrastructure`
3. เลือก Startup Project ที่จะรันเป็น `API`
4. รันคำสั่งต่อไปนี้เพื่อสร้าง Migration Class:
   ```powershell
   PM> Add-Migration InitialCreate -Context ChulaDbContext -o Data/Migrations
   ```
5. รันคำสั่งต่อไปนี้เพื่อนำ Migration Class ไปอัปเดตบนฐานข้อมูล:
   ```powershell
   PM> Update-Database -Context ChulaDbContext
   ```

#### กรณีเปลี่ยนแปลง Domain Entity และต้องการรีเซ็ต Migration ในเครื่องพัฒนา
> **คำเตือน:** คำสั่ง `Drop-Database` จะลบฐานข้อมูลทิ้งทันทีและกู้คืนไม่ได้ โปรดตรวจสอบ Connection String ให้แน่ใจก่อนรันทุกครั้งว่าเป็นฐานข้อมูลสำหรับพัฒนาในเครื่องตนเอง

1. รันคำสั่งเพื่อ Drop Database:
   ```powershell
   PM> Drop-Database -Context ChulaDbContext
   ```
2. รันคำสั่งเพื่อลบ Migration Class ล่าสุดออก:
   ```powershell
   PM> Remove-Migration -Context ChulaDbContext
   ```

### 5.2 ผ่าน `dotnet-ef` CLI บน Terminal

เริ่มต้นด้วยการเข้าไปที่โฟลเดอร์ `Infrastructure`:
```powershell
cd src/Infrastructure
```

- **สร้าง Migration (`add`):**
  ```powershell
  dotnet ef migrations add InitialCreate --context ChulaDbContext --output-dir Data/Migrations --startup-project ../API/API.csproj --verbose
  ```
- **อัปเดตฐานข้อมูล (`update`):**
  ```powershell
  dotnet ef database update --context ChulaDbContext --startup-project ../API/API.csproj --verbose
  ```
- **ลบ Migration ล่าสุด (`remove`):**
  ```powershell
  dotnet ef migrations remove --context ChulaDbContext --startup-project ../API/API.csproj --verbose
  ```
- **ลบฐานข้อมูล (`drop`):**
  ```powershell
  dotnet ef database drop --context ChulaDbContext --startup-project ../API/API.csproj --verbose --force
  ```

---

## 6. มาตรฐานการร่วมพัฒนาและขั้นตอน Git Workflow

### 6.1 มาตรฐานการเขียนโค้ด (โปรดปฏิบัติตามอย่างเคร่งครัด)
1. **Interface:** ให้ขึ้นต้นชื่อไฟล์และชื่อ Interface ด้วยตัว `I` (ไอพิมพ์ใหญ่) เช่น `IChulaDbContext`
2. **การตั้งชื่อ:** ให้ตั้งชื่อให้สื่อความหมายและอธิบายตัวเองได้ดี ไม่ว่าจะเป็นชื่อคลาส ตัวแปร และเมธอด หากไม่แน่ใจให้ปรึกษาทีม (ดูคำศัพท์โดเมนได้ที่ [`.ai/domain.md`](.ai/domain.md))
3. **Class & Method:** ใช้ `PascalCase` ในการตั้งชื่อคลาสและเมธอด เช่น คลาส `ChulaDbContext` หรือเมธอด `SaveChangesAsync()`
4. **Constants:** ใช้ `PascalCase` ในการตั้งชื่อค่าคงที่ทั้งที่เป็น local constants และ fields เช่น `ConnectionString = "Database"`
5. **Variables & Parameters:** ใช้ `camelCase` ในการตั้งชื่อ method arguments, local variables และ parameters เช่น `ChulaDatabase(string connectionString)`
6. **Private Fields:** ถ้าเป็น private instance field ให้ใช้ `_` (underscore) นำหน้า เช่น `_context`
7. **Method Parameters:** พารามิเตอร์ในฟังก์ชันหรือเมธอด หากมีไม่เกิน 3 ตัว ให้เขียนเรียงต่อกันในบรรทัดเดียว แต่ถ้าเกิน 3 ตัว ให้ขึ้นบรรทัดใหม่ทุกตัว

### 6.2 ขั้นตอนการเข้าร่วมพัฒนา
1. Clone source code จาก Git Repository ของโปรเจกต์ผ่าน SourceTree หรือ Git CLI
2. สร้าง branch ใหม่ที่แตกออกจาก branch หลัก (เช่น `main` หรือ branch ที่กำหนด) โดยตั้งชื่อ branch ให้สื่อความหมาย เช่น `FeatureNameDevelopment` หรือภาษาไทย เช่น `พัฒนาฟังก์ชันการทำงานของระบบอบรม`
3. พัฒนาคุณสมบัติในส่วนที่เกี่ยวข้อง
4. Commit การเปลี่ยนแปลง โดยหากเกี่ยวข้องกับหลายไฟล์สามารถรวมกลุ่มเป็น commit เดียวกันได้ แต่ควรแยกส่วนให้ย่อยและชัดเจนที่สุดเท่าที่ทำได้
5. ตั้งข้อความ commit ให้สื่อความหมายถึงสิ่งที่เปลี่ยนแปลงอย่างชัดเจนและเข้าใจง่าย (ภาษาไทยหรือภาษาอังกฤษ)
6. Push เข้า branch ของตนเองบน remote server เท่านั้น **ห้าม push เข้า branch อื่นโดยตรงเด็ดขาด**

### 6.3 ข้อกำหนดก่อนรวม Source Code เข้ากับ Branch หลัก
1. ต้องเขียน **Unit Tests** สำหรับ Domain Entity ก่อนเสมอ อย่างน้อยต้องครอบคลุม constructor และ guard clauses สำหรับฟิลด์ที่บังคับ (required fields)
2. สำหรับ Command หรือ Query ต้องเขียน **Integration / Functional Tests** อย่างน้อยครอบคลุม:
   - กรณีทำงานสำเร็จ (Happy path)
   - กรณีไม่พบข้อมูล (NotFound)
   - กรณีข้อมูลไม่ผ่าน Validation Rules หรือกติกาธุรกิจที่สำคัญ/วิกฤต
3. การไม่เขียน tests สำหรับโค้ดที่ตนเองพัฒนา ส่งผลกระทบต่อคุณภาพของระบบและผู้ใช้งานโดยตรง และสร้างความเสี่ยงสูงเมื่อนำขึ้น Production
4. แม้การเขียน tests จะไม่ได้การันตีว่า Production จะไม่มี Bug 100% แต่ช่วยตรวจจับและป้องกันข้อผิดพลาดสำคัญได้ตั้งแต่ระหว่างพัฒนา

### 6.4 ขั้นตอนการรวม Source Code (Pull Request / Merge Request)
1. Commit สิ่งที่เปลี่ยนแปลงและจัดเก็บไฟล์ที่แก้ไข (modified / unstaged) ให้เรียบร้อย
2. สลับไปยัง branch หลักที่ต้องการรวม แล้วทำ `fetch` → `pull` เพื่อดึงโค้ดล่าสุดลงมาก่อนเสมอ
3. สลับกลับมายัง branch ของตนเอง แล้วคลิกขวาที่ branch หลักเลือก **Rebase**
4. หากเกิด conflict ให้ทำการ resolve conflict (เช่น ใช้ DiffMerge / IDE Merge Tool) และปรึกษาทีมหากไม่แน่ใจ
5. เมื่อ resolve conflict เรียบร้อยแล้ว ให้ Push ขึ้น branch ของตนเอง
6. เปิด **Pull Request / Merge Request** บนระบบ Git Server ของโปรเจกต์
7. เลือก branch ต้นทาง (ของตนเอง) และ branch ปลายทางให้ถูกต้อง
8. กำหนดผู้รับผิดชอบ (Assignee / Reviewer) และเขียนคำอธิบายสั้นกระชับเข้าใจง่ายว่าเปลี่ยนแปลงอะไร
9. บันทึกการขอ Merge (หากเป็น branch ส่วนตัวที่ตัวเองดูแลสามารถกด merge เองได้ แต่หากเป็น branch หลักต้องรอผู้รับผิดชอบตรวจรับ)
10. ผู้ที่ได้รับมอบหมายจะทำการ pull โค้ดลงมา review และรัน test ก่อนกด Accept หรือ Reject Merge Request
11. เมื่อรวมโค้ดเสร็จสมบูรณ์ ให้ pull เวอร์ชันใหม่ลงมา แล้วจึงแตก branch ใหม่เพื่อพัฒนาฟีเจอร์ถัดไปตามขั้นตอนข้างต้น

---

## 7. โครงสร้างเอกสารและแนวทางการทำงานร่วมกับ AI

โปรเจกต์ **CU-PFIS** ใช้แนวทาง **Spec-driven & AI-assisted Development** โดยมีโครงสร้างโฟลเดอร์สำคัญดังนี้:

| โฟลเดอร์ / ไฟล์ | คำอธิบาย |
| --- | --- |
| [`.doc/`](.doc/) | เอกสารต้นฉบับจากลูกค้า (`req071169.pdf`) และบันทึกการตั้งชื่อระบบ (`CuProjectName.md`) |
| [`.ai/`](.ai/) | บริบทหลักสำหรับทีมพัฒนาและ AI Agent (สถาปัตยกรรม, คำศัพท์โดเมน, กฎการเขียนโค้ด, ขั้นตอนการทำงาน, Prompt สำเร็จรูป และสถานะโครงการ) |
| [`requirements/`](requirements/) | เอกสาร Requirement รายฟีเจอร์ที่เขียนตามโครงสร้างมาตรฐาน ([`_template.md`](requirements/_template.md)) เพื่อใช้เป็นสเปกกลางให้ทั้งคนในทีมและ AI |

### เอกสารสำคัญใน `.ai/`

1. [`architecture.md`](.ai/architecture.md) — สถาปัตยกรรมระบบ แผนผัง Bounded Context และหลักการออกแบบข้อมูล
2. [`domain.md`](.ai/domain.md) — พจนานุกรมคำศัพท์โดเมน (Domain Glossary) และผังกระบวนการทำงานหลักของคณะเภสัชศาสตร์
3. [`coding-rules.md`](.ai/coding-rules.md) — กฎเหล็กในการเขียนโค้ด (Ledger สำหรับงบอบรม, การป้องกันข้อมูลทับซ้อนจาก CUPPEP/Sheet, Concurrency, Audit Trail, PDPA)
4. [`ai-agent-guide.md`](.ai/ai-agent-guide.md) — คู่มือสำหรับ AI Agent ว่าไฟล์แต่ละประเภทต้องวางที่ไหน และข้อห้ามสำคัญ (MUST / MUST NOT)
5. [`dev-workflow.md`](.ai/dev-workflow.md) — คู่มือการติดตั้งเครื่องมือ การรันระบบ การทดสอบ และขั้นตอนทำงานร่วมกับ AI
6. [`prompt-templates.md`](.ai/prompt-templates.md) — Prompt สำเร็จรูปสำหรับสั่ง AI พัฒนาแต่ละส่วนโดยอ้างอิงไฟล์จาก `requirements/`
7. [`roadmap.md`](.ai/roadmap.md) — แผนงานแต่ละเฟส กฎ Non-negotiable และรายการคำถามที่รอสรุปจากลูกค้า (Q01–Q18)
8. [`known-issues.md`](.ai/known-issues.md) — ทะเบียนความเสี่ยงทางเทคนิค ข้อจำกัด และประเด็นค้างที่ต้องระวังก่อนพัฒนา
