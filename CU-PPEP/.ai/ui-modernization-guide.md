# CU-PPEP UI Modernization Guide

มาตรฐานและแนวทางการปรับปรุงหน้า UI ของระบบ **CU-PPEP (`Chula-PPS`)** ให้สวยงาม ทันสมัย และเป็นแนวทางเดียวกันทั้งระบบ

---

## 1. ธีมสีและ Design Tokens

ใช้ร่วมกันผ่านไฟล์ [student-achievement.css](../Chula-PPS/css/student-achievement.css) (ตั้งชื่อคลาสแบบ Scoped ตามโมดูล เช่น `.ach-*`, `.bio-*`, `.reg-*`, `.prh-*` เพื่อไม่ให้กระทบหน้า AdminLTE เดิมที่ยังไม่ได้ปรับ)

| Token | ค่าสี | การใช้งาน |
|---|---|---|
| **Primary Green (Chula-PPEP)** | `#008749` (dark `#006B3A`, hover `#007640` → `#005930`) | ปุ่มหลัก (`.ach-btn-primary`), ไอคอน `fa`, ไอคอน Hero, แท็บ/ปุ่มกรองที่เลือกอยู่ |
| **Green Soft Surface** | `#EBFAF2` / border `#BBE8D3` | พื้นหลังไอคอนรอง, Empty State icon, แถบยืนยัน, แถว Hover |
| **Info Blue (Badge)** | bg `#EFF6FF`, text `#1D4ED8`, icon `#2563EB`, border `#BFDBFE` | Badge รหัสนิสิต (`.ach-meta-chip-code`), จำนวนรายการ (`.ach-count-pill`), ปีการศึกษา (`.ach-year-pill`) |
| **Page Container Width** | `.ach-page { width: 100%; max-width: 100%; padding: 20px 28px 32px 28px; }` | เต็มพื้นที่สมดุลซ้าย-ขวา (Responsive ลด padding ในหน้าจอเล็ก) |

> ทุกครั้งที่แก้ไข `css/student-achievement.css` ให้ขยับ query version (`?v=N`) ในหน้า `.aspx` เพื่อป้องกัน browser cache

---

## 2. โครงสร้าง UI หลักที่ใช้งานร่วมกัน

1. **Hero Header (`.ach-hero`)**: กล่องไอคอนไล่สีเขียว `#008749` + ชื่อหน้า + คำอธิบายสั้น + ปุ่ม Action ด้านขวา (`.ach-btn-primary`, `.ach-btn-secondary`)
2. **Student Profile Strip (`.ach-profile-strip`)**: แสดงชื่อ-นามสกุล, รหัสนิสิต (Badge น้ำเงิน), สาขาวิชา และชั้นปี (รองรับทั้งโหมดนิสิตและ `?stdcode=...`)
3. **KPI Summary Cards (`.ach-stats-row` / `.ach-stat-card`)**: การ์ดสรุปตัวเลข 3 คอลัมน์ (`col-md-4`)
4. **Card Container (`.ach-card`)**: แบ่งเป็น `.ach-card-header` (มี `.ach-card-title` + `.ach-count-pill`), `.ach-card-body` และ `.ach-card-footer` (`.ach-info-banner`)
5. **Empty State (`.ach-empty-state`)**: แสดงเมื่อไม่มีข้อมูลแทนตารางว่าง

---

## 3. กฎสำหรับตาราง DataTables.net

- เปลี่ยนตารางเดิม (`GridView` หรือ `<table>` ธรรมดา) เป็น HTML `<table>` (`Repeater` หรือ loop) ครอบด้วย `<div class="ach-dt-wrap">` เพื่อให้ได้สไตล์เหมือนหน้า `students/list` ของ CU-PFIS
- กล่องแสดงจำนวนรายการชิดซ้าย + กล่องค้นหาชิดขวา และส่วน Footer (ข้อความสรุปชิดซ้าย + Pagination ชิดขวา) ถูกจัดไว้แล้วใน `.ach-dt-wrap` (ปิด Bootstrap 3 `.row::before/::after` ไม่ให้ดัน flex item)
- คอลัมน์ลำดับ `#` (index `0`) ให้ auto-renumber ด้วย event `order.dt search.dt draw.dt`
- **ข้อควรระวังสำคัญ (ห้ามใช้ ID ซ้ำกับ `Site.Master`)**:
  ใน `Site.Master` (บรรทัด 526–568) มีการเรียก `.DataTable()` แบบ Global บน ID ต่อไปนี้อยู่แล้ว:
  `#tbdata`, `#tbdata2`, `#grdData2`, `#tbuser`, `#tbeval`, `#tbpreceptor`, `#tbmail`, `#tbpractice`
  ดังนั้นเมื่อเขียนสคริปต์ DataTables ประจำหน้า **ต้องตั้ง ID ตารางใหม่ไม่ให้ซ้ำ** (เช่น `#tbAchievement`, `#tbPracticeHistory`) มิฉะนั้นจะเกิดแจ้งเตือน `DataTables warning: tn/3 Cannot reinitialise DataTable`

---

## 4. กฎสำหรับหน้าฟอร์มที่มีข้อมูลเยอะ (Tabbed Form)

- จัดกลุ่มช่องกรอกเป็น Bootstrap Tabs ภายใน `.reg-tabs-card` + `.reg-tabs-nav` + `.reg-tab-content`
- ใส่ปุ่มนำทางด้านล่าง (`.reg-action-bar`) ที่มีปุ่ม **ก่อนหน้า / ถัดไป** คู่กับปุ่มบันทึก/ล้างข้อมูล
- เพิ่ม JS ตรวจ ASP.NET Validators (`RequiredFieldValidator` / `RegularExpressionValidator`) ข้ามแท็บ: แสดงจุดแดง `.reg-tab-error-dot` บนแท็บที่กรอกไม่ครบ และสลับไปยังแท็บแรกที่มี error อัตโนมัติ
- Re-init Select2 (`width: '100%'`) เมื่อ `shown.bs.tab` และหลัง `UpdatePanel` (`Sys.WebForms.PageRequestManager` `endRequest`) และจำแท็บล่าสุดข้าม PostBack ด้วย `sessionStorage`
- คง Control IDs และ Validators เดิมทั้งหมดเพื่อไม่ให้กระทบ Code-Behind (`.aspx.vb`) และ Designer
- สำหรับช่องวันที่ (`.input-group` + `.input-group-addon`): ใช้สไตล์ใน `.ach-page` (`display: flex` + `min-width: 42px; flex-shrink: 0`) เพื่อไม่ให้ขอบทับไอคอนปฏิทิน

---

## 5. กฎ Encoding และวิธีตรวจสอบ Build (สำคัญมาก!)

หน้าเดิมใน `Chula-PPS` จำนวนหนึ่งเข้ารหัสเป็น `Windows-874` (`TIS-620`) จึงไม่สามารถเปิด global `fileEncoding="utf-8"` ใน `Web.config` ได้ (หากไฟล์ UTF-8 ไม่มี BOM ตัวอักษรไทย เช่น `ด` จะเพี้ยนและทำให้ VB compiler error `BC30455`)

**ทุกหน้า `.aspx` ที่ปรับปรุงใหม่ต้องทำ 2 อย่างเสมอ:**
1. เพิ่ม `<location path="PageName.aspx">` ใน `Chula-PPS/Web.config`:
   ```xml
   <location path="PageName.aspx">
       <system.web>
           <globalization culture="th-TH" uiCulture="th-TH" fileEncoding="utf-8" requestEncoding="utf-8" responseEncoding="utf-8" />
       </system.web>
   </location>
   ```
2. บันทึกไฟล์ `.aspx`, `.aspx.vb` และ `.css` ที่แก้ไขด้วย **UTF-8 BOM (`EF BB BF`)**

**คำสั่งตรวจสอบหลังแก้ไข (PowerShell 5.1):**
```powershell
# 1. Build Solution
& 'C:\Program Files\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin\MSBuild.exe' 'd:\Projects\Chula\CU-PPEP\Chula-PPEP.sln' /t:Build /p:Configuration=Debug /v:q /nologo

# 2. Compile ตรวจเฉพาะหน้า ASPX (ไม่ใช้ aspnet_compiler ทั้งเว็บเพราะหน้า legacy บางหน้าติด EO.Web)
Add-Type -AssemblyName System.Web
$cbm = New-Object System.Web.Compilation.ClientBuildManager('/', 'd:\Projects\Chula\CU-PPEP\Chula-PPS')
$cbm.CompileFile('/PageName.aspx')
```

---

## 6. รายการหน้าที่ปรับปรุงแล้ว

| หน้า | คลาสเฉพาะ | สรุปการปรับปรุง |
|---|---|---|
| `StudentAchievement.aspx` | `.ach-*` | Hero + Profile Strip + KPI + Filter + DataTables.net (`#tbAchievement`) |
| `StudentAchievementModify.aspx` | `.ach-*` | ฟอร์มแบ่ง Section + Datepicker fix + การ์ดยืนยันข้อมูล |
| `Student_Bio.aspx` | `.bio-*` | โปรไฟล์ 2 คอลัมน์ + การ์ดรูปถ่าย/ฉุกเฉิน + ตารางประวัติการศึกษา |
| `Student_Reg.aspx` | `.reg-*` | ฟอร์ม 6 แท็บ + ตรวจ Validator ข้ามแท็บ + ปุ่มก่อนหน้า/ถัดไป + Select2/UpdatePanel fix |
| `PracticeHistory.aspx` | `.prh-*` | Hero + Profile Strip + KPI + DataTables.net (`#tbPracticeHistory`) + ปุ่มกรองปีการศึกษา |
