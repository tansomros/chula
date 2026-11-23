# Technical Notes & Implementation Guides — CU-PFIS

บันทึกแนวทางเชิงเทคนิค ตัวอย่างโค้ดต้นแบบ และข้อควรระวังสำหรับฟีเจอร์เชิงเทคนิคเฉพาะทางในระบบ **CU-PFIS** เพื่อให้ทีมและผู้พัฒนาสามารถเข้ามาศึกษาและทดลองทำตามได้ภายหลัง

---

## 1. [`A11`] การตรวจจับหน้า `"Abstract"` อัตโนมัติ (หน้า 3–5) และการรวมเล่ม Abstract ประจำปี

### 1.1 สรุปข้อกำหนดจากลูกค้า (`A11`)
- หน้า Abstract ของไฟล์ PDF โครงงานนิสิตแต่ละเล่มอาจอยู่ไม่ตรงกันเสมอไป แต่โดยทั่วไปจะอยู่ระหว่าง **หน้า 3–5**
- จุดสังเกตที่ตรงกันคือ มีคำว่า **`"Abstract"`** อยู่ที่ **บรรทัดแรก (ด้านบนสุดของหน้า)** และ **จัดกึ่งกลางหน้า (Centered)**
- รูปเล่มรวม Abstract ประจำปีการศึกษา ประกอบด้วย **หน้าปก (หน้า 1) เพียงหน้าเดียว** แล้วตามด้วย **หน้า Abstract ของแต่ละโครงการเรียงต่อกันไปเรื่อย ๆ** (ไม่ต้องมีสารบัญ)

---

### 1.2 แนะนำไลบรารีฝั่ง .NET (ฟรีและเหมาะกับเชิงพาณิชย์)

| หน้าที่ | ไลบรารีที่แนะนำ | License | เหตุผล |
| --- | --- | --- | --- |
| **อ่านข้อความ + พิกัด (X, Y) ในหน้า PDF** | `UglyToad.PdfPig` | Apache-2.0 | อ่านพิกัด Bounding Box ของทุกตัวอักษร/คำได้แม่นยำ เบา และไม่ติดลิขสิทธิ์ AGPL |
| **ตัดหน้า PDF และรวมหลายไฟล์เป็นเล่มเดียว** | `PDFsharp` (`PdfSharpCore` / `PDFsharp 6`) | MIT | ดึงหน้าเฉพาะ (`AddPage`) จากไฟล์ PDF ต้นฉบับมารวมกันได้โดยคงฟอนต์และ Layout เดิม 100% |
| **สร้างหน้าปก (Cover Page) ประจำปี** | `QuestPDF` (Community) หรือใช้ Template PDF | MIT (ตรวจเกณฑ์รายได้) / PDF Template | สร้างหน้าปกแบบ Dynamic (ปีการศึกษา, โลโก้คณะ) หรือใช้ไฟล์ PDF ปกที่เจ้าหน้าที่อัปโหลดใน `DocumentTemplate` |

> **ข้อควรระวัง:** หลีกเลี่ยง `iText7` (เวอร์ชันฟรีเป็น AGPL ซึ่งบังคับเปิด Source Code หากไม่ซื้อ Commercial License)

---

### 1.3 อัลกอริทึมตรวจจับหน้า `"Abstract"` ช่วงหน้า 3–5

ในระบบพิกัดของ PDF จุดเริ่มต้น `(0, 0)` จะอยู่ที่ **มุมซ้ายล่าง (Bottom-Left)** ดังนั้น:
- **บรรทัดแรก (บนสุดของหน้า)** = ข้อความที่มีค่า `Y` สูงที่สุด (ใกล้ `page.Height` มากที่สุด)
- **จัดกึ่งกลางหน้า (Centered)** = จุดกึ่งกลางแนวนอนของคำ `(word.Left + word.Right) / 2` อยู่ใกล้ `page.Width / 2` ภายในช่วงเผื่อความคลาดเคลื่อน (Tolerance) เช่น `±15%` ของความกว้างหน้า

#### ขั้นตอนการทำงาน (ตอนเจ้าหน้าที่อัปโหลด PDF โครงงาน)
1. ตรวจสอบไฟล์เบื้องต้น: ต้องเป็น PDF Header (`%PDF-`), ไม่เสียหาย, ไม่ติดรหัสผ่านเข้ารหัส (`IsEncrypted`) และเก็บ `PageCount`
2. วนตรวจเฉพาะ **หน้า 3 ถึง `Math.Min(5, pageCount)`**
3. ในแต่ละหน้า ดึงรายการคำด้วย `page.GetWords()`:
   - หากหน้ามี `0` คำ → บันทึก flag ว่าเป็น **ไฟล์สแกนภาพ (Scanned / No Text Layer)** เพื่อให้เจ้าหน้าที่เลือกหน้าเอง
   - หาคำที่อยู่ **โซนบนสุด** (ข้ามเลขหน้า/Header เล็ก ๆ ที่อาจอยู่มุมขวาบน เช่น ตรวจคำที่อยู่ในกลุ่มบรรทัดแรกหรือ Top 25% ของหน้า)
   - ตรวจว่าข้อความตรงกับ `"abstract"` (ไม่สนใจตัวพิมพ์เล็ก-ใหญ่ `StringComparison.OrdinalIgnoreCase`)
   - ตรวจการจัดกึ่งกลางหน้า: `Math.Abs(wordCenterX - (page.Width / 2.0)) <= page.Width * 0.15`
4. หากพบ → บันทึก `AbstractPageNumber` อัตโนมัติ + ตั้งสถานะ `AutoDetected`
5. หากไม่พบ หรือพบหลายหน้า → ตั้งสถานะ `NeedsManualReview` และให้เจ้าหน้าที่กดพรีวิว/ระบุเลขหน้าเองในหน้า UI

#### ตัวอย่างโค้ดต้นแบบ C# (`UglyToad.PdfPig`)

```csharp
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

public sealed record AbstractDetectionResult(
    int? DetectedPageNumber,
    bool IsScannedOrNoText,
    string StatusMessage);

public static class AbstractPageDetector
{
    public static AbstractDetectionResult DetectAbstractPage(Stream pdfStream)
    {
        using var document = PdfDocument.Open(pdfStream);
        if (document.NumberOfPages < 3)
        {
            return new AbstractDetectionResult(null, false, "PDF has fewer than 3 pages.");
        }

        int endPage = Math.Min(5, document.NumberOfPages);
        bool anyTextFound = false;

        for (int pageNumber = 3; pageNumber <= endPage; pageNumber++)
        {
            Page page = document.GetPage(pageNumber);
            var words = page.GetWords().ToList();
            if (words.Count == 0)
            {
                continue;
            }

            anyTextFound = true;

            // จัดกลุ่มคำตามบรรทัดโดยอิงค่า Y (ระยะห่างแนวตั้งไม่เกิน 4 pt ถือเป็นบรรทัดเดียวกัน)
            var lines = words
                .OrderByDescending(w => w.BoundingBox.Bottom)
                .Aggregate(new List<List<Word>>(), (acc, word) =>
                {
                    var currentLine = acc.FirstOrDefault(l =>
                        Math.Abs(l[0].BoundingBox.Bottom - word.BoundingBox.Bottom) <= 4.0);
                    if (currentLine is null)
                    {
                        acc.Add(new List<Word> { word });
                    }
                    else
                    {
                        currentLine.Add(word);
                    }

                    return acc;
                });

            // ตรวจเฉพาะ 2 บรรทัดบนสุด (เผื่อกรณีบรรทัดบนสุดเป็นเลขหน้า เช่น "iii" หรือ "3")
            foreach (var line in lines.Take(2))
            {
                var orderedWords = line.OrderBy(w => w.BoundingBox.Left).ToList();
                string lineText = string.Join(" ", orderedWords.Select(w => w.Text)).Trim();

                if (!lineText.Equals("Abstract", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                double lineLeft = orderedWords.First().BoundingBox.Left;
                double lineRight = orderedWords.Last().BoundingBox.Right;
                double lineCenterX = (lineLeft + lineRight) / 2.0;
                double pageCenterX = page.Width / 2.0;
                double tolerance = page.Width * 0.15; // เผื่อคลาดเคลื่อน ±15% (รองรับระยะเย็บเล่ม/margin ซ้ายขวาไม่เท่ากัน)

                if (Math.Abs(lineCenterX - pageCenterX) <= tolerance)
                {
                    return new AbstractDetectionResult(pageNumber, false, $"Detected centered 'Abstract' on page {pageNumber}.");
                }
            }
        }

        return !anyTextFound
            ? new AbstractDetectionResult(null, true, "No extractable text in pages 3–5 (likely scanned PDF). Manual page selection required.")
            : new AbstractDetectionResult(null, false, "Centered 'Abstract' header not found in pages 3–5. Manual page selection required.");
    }
}
```

---

### 1.4 ขั้นตอนการรวมเล่ม Abstract ประจำปี (Cover Page + Abstract Pages)

1. รับพารามิเตอร์ `AcademicYearId` (และเลือกเฉพาะโครงงานในปีนั้นที่มีไฟล์ PDF + `AbstractPageNumber` พร้อมแล้ว)
2. สร้างไฟล์ผลลัพธ์ `PdfDocument outputDoc = new()`:
   - **หน้า 1:** ดึงหน้าปกจาก `DocumentTemplate` (หรือ Generate หน้าปกปีการศึกษา) ใส่เป็นหน้าแรก
   - **หน้า 2 เป็นต้นไป:** วนตามรายการโครงงาน (เรียงตามรหัสโครงงาน/ชื่อโครงงาน) เปิดไฟล์ PDF ต้นฉบับในโหมด `PdfDocumentOpenMode.Import` แล้วคัดลอกหน้าที่ `AbstractPageNumber` (1-based → 0-based index `AbstractPageNumber - 1`) ด้วย `outputDoc.AddPage(sourceDoc.Pages[index])`
3. คืนผลลัพธ์เป็น **รายงานการรวมเล่ม** (จำนวนไฟล์ที่รวมสำเร็จ + รายชื่อโครงการที่ถูกข้ามเพราะไฟล์มีปัญหา/ยังไม่ระบุหน้า Abstract) เพื่อไม่ให้ไฟล์เสียเพียง 1 ไฟล์ทำให้การรวมเล่มทั้งปีล่ม

### 1.5 เคสพิเศษที่ควรทดสอบกับไฟล์ PDF จริงของคณะ
- **ไฟล์สแกนภาพ (Scanned PDF):** ไม่มี text layer → ต้องแจ้งเตือนให้เจ้าหน้าที่กดพรีวิวและกรอกเลขหน้าเองได้
- **ระยะขอบซ้ายสำหรับเย็บเล่ม (Binding Margin):** วิทยานิพนธ์จุฬาฯ มักตั้งขอบซ้ายกว้างกว่าขอบขวา (เช่น ซ้าย 1.5 นิ้ว, ขวา 1.0 นิ้ว) ทำให้คำว่า `"Abstract"` เยื้องขวาเล็กน้อยจากกึ่งกลางกระดาษ → ค่า `tolerance = 15%` ช่วยรองรับจุดนี้ได้
- **เลขหน้าบนบรรทัดแรก:** บางเล่มมีเลขหน้าโรมัน (เช่น `iv`, `ค`) อยู่มุมขวาบนสูงกว่าคำว่า `"Abstract"` → การตรวจ `lines.Take(2)` ช่วยไม่ให้พลาด
- **Abstract ยาว 2 หน้า:** ในอนาคตหากพบบางเล่มที่ Abstract ยาวเกิน 1 หน้า สามารถขยายฟิลด์เป็น `AbstractStartPage` และ `AbstractEndPage` (ค่าเริ่มต้นเท่ากันคือ 1 หน้า) ได้ทันที

---

## 2. [`A16`] แนวทางการเชื่อมต่อข้อมูลผลงานตีพิมพ์จาก Scopus

### 2.1 ทำไมจึงไม่ควรทำ Web Scraping จากหน้าเว็บ Scopus
- หน้าเว็บ Scopus มีระบบป้องกันบอท (Cloudflare / Captcha / Login wall) และโครงสร้าง HTML เปลี่ยนแปลงได้ตลอดเวลา
- ผิดเงื่อนไขการใช้งาน (Terms of Service) ของ Elsevier ซึ่งอาจส่งผลให้ IP ของเซิร์ฟเวอร์หรือมหาวิทยาลัยถูกบล็อก

---

### 2.2 เปรียบเทียบ 3 แนวทางที่ถูกต้องและใช้งานได้จริง

| แนวทาง | ความอัตโนมัติ | เงื่อนไข / สิทธิ์ที่ต้องขอ | จุดเด่น | ข้อจำกัด |
| --- | --- | --- | --- | --- |
| **1. Elsevier Scopus API** *(แนะนำหลัก)* | อัตโนมัติ 100% (กด Sync หรือตั้งรอบ Sync) | ต้องมี `API Key` จาก `dev.elsevier.com` + `InstToken` (หรือรันผ่าน IP เครือข่ายจุฬาฯ) | ข้อมูลตรงจาก Scopus ครบถ้วน (Title, Journal, Year, DOI, EID, Citation count, Author Profile) | ต้องประสานงานสำนักวิทยทรัพยากร/ฝ่าย IT จุฬาฯ เพื่อขอ `InstToken` หรือสิทธิ์ Institutional |
| **2. ORCID Public API** *(ทางเลือกร่วม/ฟรี)* | อัตโนมัติ (ดึงผ่าน `ORCID iD`) | ฟรี (ลงทะเบียน Public API Client ฟรีที่ `orcid.org`) | ไม่ติดข้อจำกัด IP มหาวิทยาลัย และอาจารย์สามารถกดเชื่อม Scopus → ORCID ได้เองในเว็บ Scopus | อาจารย์ต้องมีบัญชี ORCID และกดซิงก์กับ Scopus ไว้แล้ว |
| **3. Import ไฟล์ CSV / BibTeX / RIS จาก Scopus** *(Fallback ทำได้ทันที)* | กึ่งอัตโนมัติ (Export จาก Scopus แล้วอัปโหลดเข้า CU-PFIS) | **ไม่ต้องขอ API Key ใด ๆ** | เริ่มใช้ได้ทันทีใน Phase 1, ไม่ติดเรื่อง Firewall/API Quota, ข้อมูลแม่นยำ 100% | เจ้าหน้าที่หรืออาจารย์ต้องกดปุ่ม Export จากหน้า Scopus แล้วมาอัปโหลด |

---

### 2.3 รายละเอียดเชิงเทคนิคของแต่ละแนวทาง (สำหรับทดลองทำ)

#### แนวทางที่ 1: Elsevier Scopus API (`dev.elsevier.com`)
1. **ขั้นตอนการขอสิทธิ์ทดลอง:**
   - เข้า [https://dev.elsevier.com/](https://dev.elsevier.com/) → สมัครบัญชีนักพัฒนา (แนะนำใช้อีเมล `@chula.ac.th` หรือ `@pharm.chula.ac.th`) → กด **Create API Key**
   - หากเรียก API จากเซิร์ฟเวอร์ภายนอกเครือข่ายจุฬาฯ ให้ส่งคำขอ **`InstToken`** (Institutional Token) ผ่าน Elsevier Support โดยอ้างอิงว่าพัฒนาให้คณะเภสัชศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย (ซึ่งเป็นสมาชิก Scopus อยู่แล้ว)
2. **ข้อมูลที่ต้องเก็บเพิ่มใน `FacultyMember`:**
   - `ScopusAuthorId` (เช่น `57191234567` — ดูได้จาก URL หน้าโปรไฟล์ Scopus ของอาจารย์ `authorId=...`)
   - `OrcidId` (ไม่บังคับ เช่น `0000-0002-1825-0097`)
3. **API Endpoint สำคัญ:**
   - **ดึงรายการผลงานตีพิมพ์ทั้งหมดของอาจารย์ (Scopus Search API):**
     ```http
     GET https://api.elsevier.com/content/search/scopus?query=AU-ID(57191234567)&count=25&start=0&sort=-coverDate
     Accept: application/json
     X-ELS-APIKey: {YOUR_API_KEY}
     X-ELS-Insttoken: {YOUR_INST_TOKEN}
     ```
   - **ดึงข้อมูลโปรไฟล์อาจารย์ / ตัวชี้วัด (Author Retrieval API):**
     ```http
     GET https://api.elsevier.com/content/author/author_id/57191234567?view=ENHANCED
     Accept: application/json
     X-ELS-APIKey: {YOUR_API_KEY}
     X-ELS-Insttoken: {YOUR_INST_TOKEN}
     ```
4. **การแมปฟิลด์ JSON เข้าสู่ `FacultyPublication`:**
   - `entry["eid"]` → `ScopusId` (คีย์อ้างอิงเฉพาะของ Scopus ใช้ป้องกันข้อมูลซ้ำได้ดีที่สุด เช่น `2-s2.0-85123456789`)
   - `entry["prism:doi"]` → `Doi`
   - `entry["dc:title"]` → `Title`
   - `entry["prism:publicationName"]` → `JournalOrVenue`
   - `entry["prism:coverDate"]` → `PublishedYear` / `PublishedDate`
   - `entry["citedby-count"]` → `CitationCount` (ถ้าต้องการเก็บ)
5. **ข้อควรระวังเรื่อง Rate Limit & Non-Destructive Sync:**
   - เก็บ `X-ELS-APIKey` และ `X-ELS-Insttoken` ใน User Secrets / Environment Variables เท่านั้น **ห้าม commit ลง Git**
   - ใช้วิธี Cache / Sync ตามคำสั่ง (กดปุ่ม "ซิงก์ผลงานจาก Scopus" หรือ Scheduled Background Job รายสัปดาห์) **ห้ามยิง Scopus API ทุกครั้งที่เปิดหน้า CV**
   - เช็กรายการซ้ำด้วย `ScopusId` (`eid`) หรือ `Doi` และไม่เขียนทับข้อมูลที่อาจารย์/เจ้าหน้าที่แก้ไขเองใน `CU-PFIS`

#### แนวทางที่ 2: ORCID Public API (ฟรี ทดลองได้ทันที)
```http
GET https://pub.orcid.org/v3.0/{ORCID_ID}/works
Accept: application/json
```
- คืนรายการผลงานตีพิมพ์ในรูปแบบ JSON (`group[].work-summary[]`) พร้อมชื่อเรื่อง, ปีที่ตีพิมพ์, ชื่อวารสาร และ `external-ids` (`doi`, `eid`)

#### แนวทางที่ 3: Scopus CSV / BibTeX Import (Fallback ใน Phase 1)
1. บนหน้าเว็บ Scopus ของอาจารย์ เลือกผลงานทั้งหมด → กด **Export** → เลือกรูปแบบ **CSV** (ติ๊ก `Citation information`: Authors, Title, Year, EID, Source title, DOI)
2. อัปโหลดไฟล์ `.csv` ในหน้า อาจารย์ของ `CU-PFIS`
3. ฝั่ง .NET ใช้ไลบรารี `CsvHelper` อ่านคอลัมน์ `EID`, `DOI`, `Title`, `Source title`, `Year` แล้ว Upsert เข้า `FacultyPublication` โดยข้ามรายการที่มี `EID` หรือ `DOI` ซ้ำอยู่แล้ว

---

### 2.4 แผนการพัฒนาที่แนะนำสำหรับ `A16` (ทำทีละขั้นแบบไม่สะดุด)
1. **Phase 1 (ทำทันที):** เพิ่มฟิลด์ `ScopusAuthorId` และ `OrcidId` ใน `FacultyMember` + สร้าง Interface `IPublicationImportService` และทำตัว **Import ไฟล์ CSV จาก Scopus** ให้ใช้งานได้จริงก่อนทันที
2. **Phase 1.5 / เมื่อได้ API Key จากจุฬาฯ:** เพิ่ม Implementation `ElsevierScopusApiClient : IPublicationImportService` เสียบเข้า DI Container ได้เลยโดยไม่ต้องแก้ Domain หรือหน้า UI หลัก
