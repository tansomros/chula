# Chula-PPS (Chula-PPEP)

## Overview

**Chula-PPS** (branded as **Chula-PPEP** — *Pharmacy Professional Experience Practice*) is a web-based management system for the **Office of Professional Experience Program, Faculty of Pharmacy, Chulalongkorn University** (หน่วยฝึกปฏิบัติงานวิชาชีพ คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย).

It manages the entire lifecycle of pharmacy student field practice placements — from student registration and location assignment, through assessments and grading, to certificate generation.

---

## Technology Stack

| Layer | Technology |
|---|---|
| **Framework** | ASP.NET Web Forms (.NET Framework 4.8) |
| **Language** | VB.NET |
| **Database** | Microsoft SQL Server (via stored procedures + inline SQL) |
| **Data Access** | `Microsoft.ApplicationBlocks.Data` (`SqlHelper`) — no ORM |
| **UI Framework** | AdminLTE + Bootstrap 3.3.x |
| **UI Components** | DevExpress v17.2, AjaxControlToolkit |
| **Reports** | Microsoft ReportViewer (RDLC/SSRS) + Crystal Reports (partially commented out) + DevExpress XtraReports |
| **JavaScript** | jQuery, jQuery UI, DataTables, Select2, FullCalendar, iCheck, Bootstrap Datepicker (Thai Buddhist calendar) |
| **Encryption** | Custom `CryptographyEngine` class |
| **Hosting** | IIS (IIS Express for development) |
| **Production URL** | `http://www.chulappep.com/` |

---

## Architecture

```mermaid
graph TD
    A["ASPX Pages<br/>(Web Forms UI)"] --> B["Code-Behind Files<br/>(.aspx.vb)"]
    B --> C["Controllers<br/>(Data Access Layer)"]
    C --> D["ApplicationBaseClass<br/>(DB Connection + Helpers)"]
    D --> E["SqlHelper<br/>(Microsoft.ApplicationBlocks.Data)"]
    E --> F["SQL Server<br/>(Stored Procedures + Inline SQL)"]
    
    A --> G["Master Pages<br/>(Site.Master, SiteSimple.Master, etc.)"]
    G --> H["User Controls<br/>(ucMenu.ascx, ucFooter.ascx)"]
    
    B --> I["modGlobals<br/>(Global Utilities Module)"]
    B --> J["Models<br/>(Data Transfer Objects)"]
    
    style A fill:#4CAF50,color:white
    style C fill:#2196F3,color:white
    style F fill:#FF9800,color:white
```

### Key Architectural Patterns

1. **No MVC** — This is a traditional ASP.NET Web Forms project (Page → Code-behind → Controller → DB)
2. **Controller = DAL** — The "Controllers" are not MVC controllers. They are data access classes that wrap stored procedure calls
3. **Cookie-based Auth** — User session data (UserID, roles, etc.) is stored in HTTP cookies, not ASP.NET Membership
4. **`BasePage`** — A base page class that checks for a `Username` cookie and redirects to login if missing
5. **Shared `ConnectionString`** — Built dynamically from `Web.config` appSettings in `ApplicationBaseClass`

---

## Project Structure

```
Chula-PPS/
├── Controllers/              # Data Access Layer (29 files)
│   ├── ApplicationBaseClass.vb   # Base DB connection class
│   ├── modGlobals.vb             # Global utility module (1132 lines)
│   ├── UserController.vb         # User CRUD + authentication
│   ├── StudentController.vb      # Student management
│   ├── LocationController.vb     # Practice location management
│   ├── AssessmentController.vb   # Assessment engine (largest controller!)
│   ├── CourseController.vb       # Course management
│   ├── CryptographyEngine.vb     # Password encryption
│   ├── SupervisorController.vb   # Supervisor management
│   ├── PC2Controller.vb          # "Phase Choice 2" selection system
│   ├── ... (15+ more controllers)
│   └── ReportServerCredentials.vb
│
├── Models/                   # Data models (DTOs)
│   ├── PersonInfo.vb
│   ├── StudentInfo.vb
│   ├── LocationInfo.vb
│   ├── TeacherInfo.vb
│   ├── SubjectInfo.vb
│   ├── RoleInfo.vb
│   ├── PreceptorInfo.vb
│   ├── TimePhaseInfo.vb
│   └── dsStudentBio.*        # Typed DataSet for Student Bio reports
│
├── *.aspx / *.aspx.vb       # Web Forms pages (~130+ pages)
├── Site.Master               # Main master page (AdminLTE sidebar layout)
├── SiteSimple.Master         # Simplified master page
├── SiteMain.Master           # Main public-facing master page
├── SiteNoAjax.Master         # Master without AJAX ToolkitManager
│
├── ucMenu.ascx               # Main sidebar menu (Thai) — 70KB!
├── ucMenuEn.ascx             # English menu
├── ucFooter.ascx             # Footer user control
├── BasePage.vb               # Auth guard base class
│
├── Reports/                  # RDLC report files
├── css/                      # Stylesheets (AdminLTE, custom)
├── js/                       # JavaScript files
├── components/               # Frontend packages (Bootstrap, jQuery, etc.) — see *Local development*
├── plugins/                  # iCheck and other plugins
├── images/                   # Application images
├── imgStudent/               # Student photos
├── imgPerson/                # Person photos
├── imgNews/                  # News images
├── tmpUploads/               # Uploaded documents
├── Documents/                # Document storage
│
├── Web.config                # Configuration (DB credentials, DevExpress, etc.)
├── Global.asax.vb            # Application lifecycle events
└── Chula-PPS.vbproj          # VB.NET project file
```

---

## Functional Modules

### 1. 🔐 Authentication & Users
| Pages | Purpose |
|---|---|
| `Default.aspx` | Login page + public news display |
| `Home.aspx` | Dashboard after login |
| `Users.aspx`, `UserModify.aspx` | User management |
| `UserStudent.aspx`, `UserPreceptor.aspx`, `UserAdvisor.aspx` | Role-specific user management |
| `ChangePassword.aspx`, `ChangeUsername.aspx` | Account settings |
| `ForgotPassword.aspx` | Password recovery via email |
| `Roles.aspx` | Role management |
| `PrivacyPolicy.aspx` | PDPA compliance |
| `LogfilesByUser.aspx` | Audit trail |

**User Roles:** Student (`ROLE_STD`), Teacher (`ROLE_TCH`), Advisor (`ROLE_ADV`), Preceptor (`ROLE_PCT`), Admin (`ROLE_ADM`), Super Admin (`ROLE_SPA`), Coordinator (`ROLE_CO1`)

### 2. 👨‍🎓 Student Management
| Pages | Purpose |
|---|---|
| `Student.aspx`, `StudentData.aspx` | Student list & data management |
| `Student_Reg.aspx`, `Student_Bio.aspx` | Student registration & biography |
| `StudentRegister.aspx`, `StudentNoRegister.aspx` | Registration status tracking |
| `StudentLocation.aspx`, `StudentLocation_Manage.aspx` | Student-location assignments |
| `StudentPrintBio.aspx`, `StudentPrintCert.aspx` | Print bios & certificates |
| `StudentCert.aspx` | Certificate management |
| `StudentAsmStatus.aspx` | Assessment status overview |
| `COVIDScreening.aspx`, `COVIDList.aspx` | COVID-19 screening (pandemic feature) |

### 3. 🏥 Practice Location Management
| Pages | Purpose |
|---|---|
| `Location.aspx`, `LocationDetail.aspx` | Location CRUD (largest page: 46KB!) |
| `LocationGroup.aspx`, `LocationZone.aspx` | Location grouping/zoning |
| `LocationWork.aspx`, `LocationOffice.aspx` | Work place & office details |
| `LocationList.aspx`, `LocationMaps.aspx` | Location listings & maps |

### 4. 📋 Assessment & Evaluation
| Pages | Purpose |
|---|---|
| `AssessmentStudent.aspx`, `AssessmentAdmin.aspx` | Assessment execution |
| `AssessmentResult.aspx`, `AssessmentResultPreceptor.aspx` | Result viewing |
| `AssesseeList.aspx`, `AssesseeEvaluationGroup.aspx` | Assessment assignments |
| `EvaluationForm.aspx`, `EvaluationGroup.aspx`, `EvaluationSubject.aspx`, `EvaluationTopic.aspx` | Evaluation form builder |
| `Grade.aspx`, `GradePreceptor.aspx`, `GradeResult.aspx` | Grading system |
| `AssesseeComment.aspx`, `AssessmentStudentComment.aspx` | Comment/feedback system |

### 5. 📅 Time Phase & Course Management
| Pages | Purpose |
|---|---|
| `TimePhase.aspx`, `TurnPhase.aspx` | Practice period scheduling |
| `Courses.aspx`, `CourseStudent.aspx`, `CourseLocation.aspx`, `CourseCoordinator.aspx` | Course management |
| `Subjects.aspx` | Subject management |

### 6. 🎯 Student Assignment (3 modes)
| Pages | Purpose |
|---|---|
| `AssignRandom.aspx` | Random assignment |
| `AssignManual.aspx` | Manual assignment |
| `AssignSpecify.aspx` | Specified assignment |
| `AssignBypass.aspx` | Bypass assignment |
| `PracticeAssign.aspx` | Practice assignment overview |

### 7. 📊 PC2 Module (Phase Choice Round 2)
| Pages | Purpose |
|---|---|
| `PC2_Student.aspx`, `PC2_Subject.aspx` | PC2 student/subject management |
| `PC2_Quota.aspx` | Quota management |
| `PC2_StudentSelection.aspx`, `PC2_StudentResult.aspx` | Selection & results |
| `PC2_AssignManual.aspx`, `PC2_AssignRandom.aspx` | PC2 assignment |
| `PC2_Report.aspx` | PC2 reporting |

### 8. 👨‍🏫 Supervisor & Preceptor
| Pages | Purpose |
|---|---|
| `SupervisorManage.aspx`, `SupervisorSelect.aspx` | Supervisor assignments |
| `SupervisorLocation.aspx`, `SupervisionLocation.aspx` | Supervision at locations |
| `SupervisionPlan.aspx` | Supervision planning |
| `SupervisorSend.aspx` | Send student lists to supervisors |
| `PreceptorData.aspx` | Preceptor management |
| `TeacherData.aspx`, `Teacher_Reg.aspx` | Teacher management |

### 9. 📰 News & Events
| Pages | Purpose |
|---|---|
| `News.aspx`, `NewsAll.aspx`, `News_List.aspx`, `News_Manage.aspx` | News CMS |
| `Events.aspx`, `EventModify.aspx` | Event management |

### 10. 📈 Reports
| Pages | Purpose |
|---|---|
| `ReportViewer.aspx`, `ReportServerViewer.aspx`, `ReportCRViewer.aspx` | Report viewers |
| `ReportCondition*.aspx` | Report filters (by subject, location, year, course, etc.) |
| `ReportStudentLocation.aspx`, `ReportStudentBioView.aspx` | Student reports |

### 11. ⚙️ System Configuration
| Pages | Purpose |
|---|---|
| `DataConfig.aspx`, `DataConfigLevel.aspx` | System configuration |
| `TimelineSetting.aspx`, `AlertSetting.aspx` | Timeline & alert settings |
| `PaymentConfig.aspx` | Payment configuration |
| `Department.aspx`, `Positions.aspx`, `Prefix.aspx`, `Provinces.aspx` | Master data |

---

## Data Access Pattern

All controllers inherit from `ApplicationBaseClass` which:

1. Reads DB credentials from `Web.config` appSettings (`ServerPath`, `DatabaseName`, `Username`, `Password`)
2. Builds a SQL Server connection string dynamically
3. Uses `Microsoft.ApplicationBlocks.Data.SqlHelper` for all DB operations

**Two data access styles are used:**

```vb
' Style 1: Stored Procedures (preferred)
ds = SqlHelper.ExecuteDataset(ConnectionString, "StoredProcName", param1, param2)

' Style 2: Inline SQL (legacy — SQL injection vulnerable!)
SQL = "select * from Students where Student_Code='" & id & "'"
ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
```

> [!WARNING]
> Several controllers contain **inline SQL with string concatenation**, which is vulnerable to SQL injection (e.g., `StudentController.GetStudentBio_ByID`, `UserController.GetUsers_ByUsername`, `UserController.User_GetEmail`).

---

## Authentication Flow

```mermaid
sequenceDiagram
    participant U as User
    participant D as Default.aspx
    participant UC as UserController
    participant DB as SQL Server
    
    U->>D: Enter username + password
    D->>UC: User_CheckLogin(username, encrypted_password)
    UC->>DB: Execute "User_Login" stored proc
    DB-->>UC: User record
    UC-->>D: DataTable result
    
    alt Valid Credentials
        D->>D: Create HTTP Cookies (UserID, Username, Roles, etc.)
        D->>D: Check UserRoles via UserRoleController
        D->>D: Set role cookies (ROLE_STD, ROLE_TCH, etc.)
        D->>D: Log the login event
        
        alt Privacy Policy Accepted
            D->>U: Redirect to Home.aspx
        else Not Yet Accepted
            D->>U: Redirect to PrivacyPolicy.aspx
        end
    else Invalid
        D->>U: Show error modal
    end
```

---

## Key Configuration (`Web.config`)

| Setting | Value | Description |
|---|---|---|
| `ServerPath` | `45.154.25.254` | SQL Server IP |
| `DatabaseName` | `chula_pps` | Database name |
| `ProjectPath` | `http://www.chulappep.com/` | Production URL |
| `PassPhase` | `CU@THEDEV` | Encryption passphrase |
| `ReportServerURL` | `http://45.154.25.254/ReportServer` | SSRS endpoint |
| `globalization` | `th-TH` | Thai locale |
| `targetFramework` | `4.8` | .NET Framework version |

---

## Local development

### สรุปภาษาไทย — รันในเครื่องและแก้หน้าเว็บไม่สวย (CSS เพี้ยน)

**รันโปรเจกต์**

1. เปิด `Chula-PPS/Chula-PPS.vbproj` ใน Visual Studio (.NET Framework 4.8)
2. ตั้งค่า DB ใน `Web.config` (`ServerPath`, `DatabaseName`, `Username`, `Password`)
3. กด F5 (IIS Express) — URL ดูใน `Chula-PPS.vbproj.user` เช่น `http://localhost:52396/`
4. แอปต้องรันโดยมี root เป็นโฟลเดอร์ `Chula-PPS` เพื่อให้ path แบบ `components/...` ถูกต้อง

**ทำไมหน้าตาไม่เหมือน production**

- หน้าเว็บอ้าง CSS/JS จากโฟลเดอร์ **`components/.../dist/`** (Bootstrap, jQuery, Select2, FullCalendar)
- ใน git มักมีแค่ **source ของ Bower** (เช่น `less/`, `src/`) **ไม่มีโฟลเดอร์ `dist`** ที่ build แล้ว
- เซิร์ฟเวอร์จริงมีครบ แต่เครื่อง dev ที่ clone มาอาจขาด → browser โหลดไม่ได้ (404) → layout AdminLTE พัง ไม่มี sidebar/ไอคอน
- โฟลเดอร์ `css/AdminLTE.min.css`, `plugins/`, `font-awesome` มักมีอยู่แล้ว ส่วนที่ขาดบ่อยคือ **`bootstrap`**, **`jquery`**, **`select2`**, **`fullcalendar`** ใต้ `dist/`
- หน้า login บางหน้าโหลด Bootstrap จาก CDN ด้วย จึงอาจดู “ไม่พังทั้งหมด” แต่หลัง login ยังเพี้ยนได้ถ้า `dist/` หาย

**วิธีเอาไฟล์กลับมา** (เลือกทางใดทางหนึ่ง)

| ทาง | ทำอย่างไร |
|---|---|
| A | แตก `CHULAPPEP.zip` แล้วคัดลอก `components/{bootstrap,jquery,select2,fullcalendar}/dist/` มาวางใน `Chula-PPS/components/` |
| B | ใช้ `robocopy` จากโฟลเดอร์สำรองที่มีครบ (เช่น `xChula\CHULAPPEP\Chula-PPS`) — ดูสคริปต์ PowerShell ในหัวข้อด้านล่าง |
| C | คัดลอกโฟลเดอร์ `dist` เดียวกันจาก IIS บน server production (`chulappep.com`) |

หลังคัดลอกแล้วกด **Ctrl+F5** หรือล้าง cache ก่อนเทียบกับ production

**ตรวจสอบ**

- เปิด DevTools → **Network** → กรอง CSS/JS → เปิด `Default.aspx` หรือ `Home.aspx`
- ต้องไม่มี **404** ใต้ `components/.../dist/`

**Master page กับ path AdminLTE**

- `Site.Master` ใช้ `css/AdminLTE.min.css` และ `css/skins/_all-skins.min.css` (ถูกต้อง)
- `SiteSimple.Master`, `Site000.Master` ต้องใช้ path เดียวกันในโฟลเดอร์ **`css/`** ไม่ใช่ `dist/css/...`
- ถ้าเฉพาะบางหน้าเพี้ยน ให้ดูว่าใช้ master ไหนและ path ใน `<link>` ตรงกับไฟล์จริงหรือไม่

**สำหรับทีม:** ถือว่า `components/{bootstrap,jquery,select2,fullcalendar}/dist` เป็น **ไฟล์ที่ต้องมีตอนรัน** เทียบเท่า `css/` และ `plugins/` — ควร commit ใน git หรือบังคับขั้นตอน restore หลัง clone ทุกครั้ง

---

### Run the application

1. Open `Chula-PPS/Chula-PPS.vbproj` in Visual Studio (target **.NET Framework 4.8**).
2. Set `Web.config` `appSettings` (`ServerPath`, `DatabaseName`, `Username`, `Password`) for a database you can reach.
3. Press **F5** (IIS Express). The URL is stored in `Chula-PPS.vbproj.user` (e.g. `http://localhost:52396/`).
4. The site root must be the `Chula-PPS` folder so relative paths such as `components/bootstrap/dist/...` resolve correctly.

### Frontend static assets (`components/*/dist/`)

Master pages and many `.aspx` files load CSS/JS from **built vendor paths**, for example:

| Referenced in UI | Typical path |
|---|---|
| Bootstrap 3 | `components/bootstrap/dist/css/bootstrap.min.css`, `.../js/bootstrap.min.js` |
| jQuery | `components/jquery/dist/jquery.min.js` |
| Select2 | `components/select2/dist/css/select2.min.css`, `.../js/select2.full.min.js` |
| FullCalendar | `components/fullcalendar/dist/fullcalendar.min.css` |
| AdminLTE theme | `css/AdminLTE.min.css`, `css/skins/_all-skins.min.css` (not under `components/`) |

The repository often contains **Bower source trees** under `components/` (`less/`, `src/`, etc.) **without** the corresponding `dist/` folders. Those `dist/` files exist on the production deploy but are easy to omit when copying or cloning the project. If they are missing, the app still runs but the browser returns **404** for Bootstrap/jQuery and the layout looks unstyled (broken AdminLTE sidebar, no icons, wrong login layout).

`css/AdminLTE.min.css` and most files under `css/`, `plugins/`, and `components/font-awesome/` are usually present; the gaps are most often **`bootstrap`**, **`jquery`**, **`select2`**, and **`fullcalendar`** `dist/` trees.

### Restore missing `dist/` files

Use one of the following (pick whichever you have locally):

**Option A — Full deployment archive**

If you have `CHULAPPEP.zip` (deploy snapshot), extract only the vendor build outputs, for example:

- `CHULAPPEP/Chula-PPS/components/bootstrap/dist/`
- `CHULAPPEP/Chula-PPS/components/jquery/dist/`
- `CHULAPPEP/Chula-PPS/components/select2/dist/`
- `CHULAPPEP/Chula-PPS/components/fullcalendar/dist/`

Copy those folders into the matching paths under `Chula-PPS/components/`.

**Option B — Sync from an existing tree (PowerShell on Windows)**

When another copy of the site on disk already has `dist/` (e.g. `xChula\CHULAPPEP\Chula-PPS`):

```powershell
$srcRoot = "D:\path\to\CHULAPPEP\Chula-PPS\components"
$dstRoot = "D:\Projects\Chula\CU-PPEP\Chula-PPS\components"

foreach ($name in @("bootstrap", "jquery", "select2", "fullcalendar")) {
  $from = Join-Path $srcRoot "$name\dist"
  $to   = Join-Path $dstRoot "$name\dist"
  if (Test-Path $from) {
    robocopy $from $to /E
  }
}
```

(`robocopy` exit codes **0–7** mean the copy succeeded; **8+** indicates errors.)

**Option C — Production server**

Copy the same `components/*/dist/` folders from the IIS site directory on the server that hosts `chulappep.com`.

After restoring, hard-refresh the browser (**Ctrl+F5**) or clear cache before comparing layout to production.

### Verify in the browser

1. Open **Developer Tools → Network**, filter by **CSS** and **JS**.
2. Load `Default.aspx` or `Home.aspx` after login.
3. Confirm there are **no 404** responses for paths under `components/.../dist/`.

Some public/login pages also load **Bootstrap or fonts from CDN** (`Default.aspx`). A page can look partly styled while logged-in AdminLTE pages still break if local `dist/` files are missing.

### Master pages and AdminLTE paths

| Master | AdminLTE / skins |
|---|---|
| `Site.Master` (main app) | `css/AdminLTE.min.css`, `css/skins/_all-skins.min.css` |
| `SiteSimple.Master`, `Site000.Master` | Same `css/` paths (not `dist/css/...`) |

If only some pages look wrong, check which master page they use and that its `<link>` / `<script>` paths match files that exist on disk.

> [!TIP]
> For team onboarding, treat `components/bootstrap/dist`, `components/jquery/dist`, `components/select2/dist`, and `components/fullcalendar/dist` as **required runtime assets** alongside `css/` and `plugins/`. Either commit them in git or document a mandatory restore step after clone.

---

## Notable Characteristics

1. **Massive codebase** — 130+ ASPX pages, 29 controllers, 533+ files
2. **Thai-centric** — Buddhist Era date handling (พ.ศ.), Thai month names, Thai number conversion
3. **Email notifications** — SMTP via Gmail for alerts to preceptors
4. **Multi-language support** — Thai (`ucMenu.ascx`) and English (`ucMenuEn.ascx`) menus, toggled by `LANG` cookie
5. **Legacy patterns** — Copy-paste files exist (e.g., `Users - Copy.aspx`, `Default - Copy1.aspx`)
6. **COVID-19 features** — COVID screening forms added during the pandemic
7. **DevExpress integration** — Rich UI components for grids, reports, HTML editor
8. **Version 3.0.0** — Currently at v3.0.0 (footer says v2.0.0 but login displays 3.0.0)
