# CU-PPEP RESTful API (.NET 10 Clean Architecture + EF Core SQL Server)

Full RESTful **.NET 10 Clean Architecture** API (`GET` / `POST` / `PUT` / `DELETE`) for **CU-PPEP** (`chula_pps` MS SQL Server), following the **exact same architecture, EF Core Code-First Migration workflow, CQRS/MediatR pipeline, Dual-key (`Id` + `ExternalId` UUIDv7), Soft Delete, Audit Interceptor, and Swagger UI** pattern as **CU-PFIS**.

## Legacy Database Safety
- Legacy `chula_pps` objects (`View_Student_Bio`, `Students`, `Persons`, `RPT_Student_PracticeHistory`) are mapped strictly as **read-only keyless views** (`dbo.vw_pfis_students`, `dbo.vw_pfis_teachers`) — EF Core Migrations **never alters or drops** legacy tables.
- All new Code-First tables are prefixed **`*`** (`StudentProfileExts`, `StudentAchievements`, `StudentLicenseResults`, `StudentEmploymentRound1s`) and tracked in isolated history table `dbo.MigrationsHistory`.

## RESTful Endpoints (`/swagger`)
- **Legacy Read-Only Views (`chula_pps`):**
  - `GET /api/students?updatedSince=&page=1&pageSize=200`
  - `GET /api/students/{studentCode}` (includes `RPT_Student_PracticeHistory`)
  - `GET /api/teachers`
- **Full RESTful Extension Resources (`*`):**
  - `GET /api/student-profiles` | `GET /api/student-profiles/{studentCode}` | `PUT /api/student-profiles/{studentCode}`
  - `GET /api/student-achievements` | `GET /api/student-achievements/{externalId}` | `POST /api/student-achievements` | `PUT /api/student-achievements/{externalId}` | `DELETE /api/student-achievements/{externalId}`
  - `GET /api/student-license-results` | `GET /api/student-license-results/{studentCode}` | `PUT /api/student-license-results/{studentCode}`
  - `GET /api/student-employments` | `GET /api/student-employments/{studentCode}` | `PUT /api/student-employments/{studentCode}`

## EF Core Migrations (Code-First on MS SQL Server `chula_pps`)
```powershell
cd D:\Projects\Chula\CU-PPEP\IntegrationApi\src\Infrastructure

# Create a new migration:
dotnet ef migrations add <MigrationName> --context CuPpepDbContext --output-dir Data/Migrations --startup-project ../API/API.csproj

# Apply migrations (creates * tables + vw_pfis_students / vw_pfis_teachers views):
dotnet ef database update --context CuPpepDbContext --startup-project ../API/API.csproj
```

## Run & Swagger UI
```powershell
dotnet run --project D:\Projects\Chula\CU-PPEP\IntegrationApi\src\API --launch-profile https
# Opens https://localhost:7002/swagger
```
