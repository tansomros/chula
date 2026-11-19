# CLAUDE.md

**CU-PFIS** (**ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย** / **Chula Pharmacy Faculty Information System**) — ระบบสารสนเทศหลักสำหรับบริหารจัดการข้อมูลอาจารย์ (CV), การอบรมและงบประมาณ, ข้อมูลนิสิต/บัณฑิต, ผลงาน, การได้งานทำ, ผู้ใช้บัณฑิต, ใบประกอบวิชาชีพ และโครงงานนิสิต (Senior Project) ของคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย

- **ชื่อระบบภาษาไทย:** ระบบสารสนเทศคณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย
- **ชื่อภาษาอังกฤษ:** Chula Pharmacy Faculty Information System
- **ตัวย่อ / ชื่อเรียกสั้น:** `CU-PFIS`

This file is intentionally short. Detailed project context, rules, and specifications live in `.ai/`, `requirements/`, and `.doc/` — read them before making changes, not this file alone.

## Read first

- **[.ai/known-issues.md](.ai/known-issues.md)** — open requirement risks, external integration caveats (CUPPEP / Google Sheet / Chula Final dataset), and technical constraints. Check before you start.
- **[.ai/ai-agent-guide.md](.ai/ai-agent-guide.md)** — where things go, MUST/MUST NOT rules, and how to structure new features. Read this before adding or changing code.
- **[.ai/roadmap.md](.ai/roadmap.md)** — current phase, confirmed decisions (`D01–D04`), open stakeholder questions (`Q01–Q18`), and non-negotiable rules. Update it when you finish a unit of work.
- **[requirements/README.md](requirements/README.md)** — how feature specs (`requirements/{Context}/{feature}.md`) are organized and used as the primary source of truth for implementation.

## Then, as needed

- **[.ai/architecture.md](.ai/architecture.md)** — Clean Architecture layering, stack, bounded-context folder map (`ReferenceData`, `Faculty`, `Training`, `Students`, `ProfessionalLicense`, `SeniorProjects`, `Integration`, `Reporting`, `Security`), and data/sync conventions.
- **[.ai/domain.md](.ai/domain.md)** — bilingual domain glossary and core workflows (Faculty CV, 4 Training categories & budget ledger, Student CV & achievements, Graduate employment reconciliation, License exams `CC1/CC2/IP1/IP2/PC1/PC2`, Senior Project PDF & page-3 Abstract compilation).
- **[.ai/coding-rules.md](.ai/coding-rules.md)** — naming conventions and binding technical rules (append-only `TrainingBudgetLedger`, non-destructive external sync, optimistic concurrency `xmin`, `AuditLog`, permissions, PDPA protection).
- **[.ai/dev-workflow.md](.ai/dev-workflow.md)** — install/run/test/build/migrate commands and hybrid AI workflow.
- **[.ai/prompt-templates.md](.ai/prompt-templates.md)** — copy-paste AI session starters for each stage of feature work (entity → EF config → CQRS feature → unit test → functional test → frontend → e2e).
- **[.doc/](.doc/)** — original client scope (`req071169.pdf`) and official project naming notes (`CuProjectName.md`).

## Rules that override convenience

**CU-PFIS** is the **Master System (ระบบหลักในการดูแลข้อมูล)** for faculty profiles, academic training budgets, student achievements, graduate employment, license pass rates, and senior project publications. **Data accuracy, provenance, and privacy come before speed of delivery:**

1. **Never mutate training budgets in place** — always derive balances from the append-only `TrainingBudgetLedger`, and keep annual limits (e.g. `10,000 THB/person/year`) configurable via `TrainingBudgetPolicy`.
2. **Never silently overwrite staff-verified data on re-import** — data synced from CUPPEP, Google Sheets, or Chula's Final employment dataset must track provenance (`SourceSystem`, `SourceRecordId`, `ImportedAt`) and route conflicts with staff-edited records to a review queue.
3. **Keep Verification and Approval distinct** — "Staff verifies data accuracy" and "Dean approval recorded after signature" are separate states and must be logged with an append-only history trail.
4. **Protect sensitive personal data (PDPA)** — restrict bank account numbers and employer evaluations to authorized roles only, and never write PII to plaintext logs.

## Housekeeping

Keep `.ai/known-issues.md`, `.ai/roadmap.md`, and the status field in `requirements/{Context}/{feature}.md` in sync with reality. For build/run/test commands, see `.ai/dev-workflow.md`. For project overview, see `README.md`.
