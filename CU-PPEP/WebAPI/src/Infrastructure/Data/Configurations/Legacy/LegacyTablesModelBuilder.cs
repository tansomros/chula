using CUPPEP.Domain.Common;
using CUPPEP.Domain.Entities.Assessments;
using CUPPEP.Domain.Entities.Courses;
using CUPPEP.Domain.Entities.Documents;
using CUPPEP.Domain.Entities.Locations;
using CUPPEP.Domain.Entities.Pc2;
using CUPPEP.Domain.Entities.ReferenceData;
using CUPPEP.Domain.Entities.Reporting;
using CUPPEP.Domain.Entities.Requirements;
using CUPPEP.Domain.Entities.Security;
using CUPPEP.Domain.Entities.Students;
using CUPPEP.Domain.Entities.Teachers;
using Microsoft.EntityFrameworkCore;

namespace CUPPEP.Infrastructure.Data.Configurations.Legacy;

/// <summary>
/// Fluent API mappings for all 98 existing tables in legacy chula_pps database.
/// Marked ExcludeFromMigrations so Code-First migrations only alter new * tables
/// without recreating existing chula_pps tables.
/// </summary>
public static class LegacyTablesModelBuilder
{
    public static void Apply(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assessment>(entity =>
        {
            entity.HasKey(e => new { e.SubjectCode, e.StudentCode });

            entity.ToTable("Assessment");

            entity.Property(e => e.SubjectCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Student_Code");
            entity.Property(e => e.AssessmentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("AssessmentID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Grade)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())", "DF_Assessment_LastUpdate")
                .HasColumnType("datetime");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.PreceptorScore).HasColumnName("Preceptor_Score");
            entity.Property(e => e.Pyear).HasColumnName("PYear");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.TeacherScore).HasColumnName("Teacher_Score");
            entity.Property(e => e.TimePhaseId).HasColumnName("TimePhaseID");
            entity.Property(e => e.UpdBy)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AssessmentMailAlert>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AssessmentMailAlert");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lang)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhaseId).HasColumnName("PhaseID");
            entity.Property(e => e.SendDttm).HasColumnType("datetime");
            entity.Property(e => e.SendStatus)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TimePhaseId).HasColumnName("TimePhaseID");
            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AssessmentSubject>(entity =>
        {
            entity.HasKey(e => new { e.EduYear, e.SubjectId, e.EvaluationGroupUid });

            entity.ToTable("AssessmentSubject");

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.EvaluationGroupUid).HasColumnName("EvaluationGroupUID");
            entity.Property(e => e.Cuser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUser");
            entity.Property(e => e.Cwhen)
                .HasDefaultValueSql("(getdate())", "DF_AssessmentSubject_CWhen")
                .HasColumnType("datetime")
                .HasColumnName("CWhen");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Uid)
                .ValueGeneratedOnAdd()
                .HasColumnName("UID");
        });

        modelBuilder.Entity<Certification>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Certification");
        });

        modelBuilder.Entity<Coordinator>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("Coordinator");

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsDefault).HasColumnName("isDefault");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PositionName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remark)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A", "DF_Coordinator_StatusFlag");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.Tel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WorkId).HasColumnName("WorkID");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => new { e.Cyear, e.SubjectCode });

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("AUTO_COURSEID");
                    tb.HasTrigger("DelCourse");
                    tb.HasTrigger("UpdateSubjectCode");
                });

            entity.Property(e => e.Cyear).HasColumnName("CYear");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())", "DF_Courses_LastUpdate")
                .HasColumnType("datetime");
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A", "DF_Courses_StatusFlag");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.UpdBy).HasMaxLength(50);
        });

        modelBuilder.Entity<CourseCoordinator>(entity =>
        {
            entity.HasKey(e => new { e.Cyear, e.SubjectCode, e.PersonId });

            entity.ToTable("CourseCoordinator");

            entity.Property(e => e.Cyear).HasColumnName("CYear");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.ItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("itemID");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())", "DF_CourseCoordinator_LastUpdate")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdBy)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CourseLocation>(entity =>
        {
            entity.HasKey(e => new { e.CourseId, e.LocationId });

            entity.ToTable("CourseLocation");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.IsForceSupervision)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N", "DF_CourseLocation_isForceSupervision")
                .HasColumnName("isForceSupervision");
            entity.Property(e => e.IsSupervision)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isSupervision");
            entity.Property(e => e.IsTrain)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isTrain");
            entity.Property(e => e.ItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("itemID");
            entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            entity.Property(e => e.UpdBy)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CourseStudent>(entity =>
        {
            entity.HasKey(e => new { e.CourseId, e.StudentCode });

            entity.ToTable("CourseStudent");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Student_Code");
            entity.Property(e => e.ItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("itemID");
            entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue(0, "DF_CourseStudent_Status");
            entity.Property(e => e.UpdBy)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Department");

            entity.Property(e => e.DepartmentName).HasMaxLength(50);
            entity.Property(e => e.DepartmentUid)
                .ValueGeneratedOnAdd()
                .HasColumnName("DepartmentUID");
            entity.Property(e => e.StatusFlag).HasMaxLength(50);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.DocumentCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DocumentName).HasMaxLength(500);
            entity.Property(e => e.DocumentPath)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FileDataName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MailMergeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A", "DF_Documents_StatusFlag");
            entity.Property(e => e.SubjectTitle).HasMaxLength(500);
        });

        modelBuilder.Entity<DocumentPrint>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("DocumentPrint");

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.Cuser).HasColumnName("CUser");
            entity.Property(e => e.Cwhen)
                .HasDefaultValueSql("(getdate())", "DF_DocumentPrint_CWhen")
                .HasColumnType("datetime")
                .HasColumnName("CWhen");
            entity.Property(e => e.DocumentDate).HasMaxLength(50);
            entity.Property(e => e.DocumentUid).HasColumnName("DocumentUID");
            entity.Property(e => e.EduYear).HasMaxLength(4);
            entity.Property(e => e.LetterTo).HasMaxLength(100);
            entity.Property(e => e.LevelClass).HasMaxLength(50);
            entity.Property(e => e.LocationId)
                .HasMaxLength(5)
                .HasColumnName("LocationID");
            entity.Property(e => e.LocationName).HasMaxLength(100);
            entity.Property(e => e.PersonName).HasMaxLength(4000);
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("Education");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.CourseName).HasMaxLength(200);
            entity.Property(e => e.EduYear)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Institute).HasMaxLength(200);
            entity.Property(e => e.ProvinceName).HasMaxLength(50);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
        });

        modelBuilder.Entity<EvaluationForm>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EvaluationForm");

            entity.Property(e => e.Cuser).HasColumnName("CUser");
            entity.Property(e => e.Cwhen)
                .HasDefaultValueSql("(getdate())", "DF_EvaluationForm_CWhen")
                .HasColumnType("datetime")
                .HasColumnName("CWhen");
            entity.Property(e => e.EvaluationGroupUid).HasColumnName("EvaluationGroupUID");
            entity.Property(e => e.EvaluationTopicUid).HasColumnName("EvaluationTopicUID");
            entity.Property(e => e.Muser).HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.Remark).HasMaxLength(4000);
            entity.Property(e => e.Seqno).HasColumnName("SEQNO");
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Uid).HasColumnName("UID");
        });

        modelBuilder.Entity<EvaluationGroup>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK_TopicGroup");

            entity.ToTable("EvaluationGroup");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.AliasName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsAdvisor)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isAdvisor");
            entity.Property(e => e.IsCompare)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N", "DF_EvaluationGroup_isCompare")
                .HasColumnName("isCompare");
            entity.Property(e => e.IsNoScore)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N", "DF_EvaluationGroup_isNoScore")
                .HasColumnName("isNoScore");
            entity.Property(e => e.IsPreceptor)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isPreceptor");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Remark).IsUnicode(false);
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A", "DF_TopicGroup_StatusFlag");
        });

        modelBuilder.Entity<EvaluationTopic>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("EvaluationTopic");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Cuser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUser");
            entity.Property(e => e.Cwhen)
                .HasDefaultValueSql("(getdate())", "DF_EvaluationTopic_CWhen")
                .HasColumnType("datetime")
                .HasColumnName("CWhen");
            entity.Property(e => e.Muser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.Remark).HasMaxLength(4000);
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A", "DF_EvaluationTopic_StatusFlag");
            entity.Property(e => e.TopicName).HasMaxLength(500);
            entity.Property(e => e.Weight).HasDefaultValue(1.0, "DF_EvaluationTopic_Weight");
            entity.Property(e => e.XEvaluationGroupUid).HasColumnName("xEvaluationGroupUID");
        });

        modelBuilder.Entity<EventCalendar>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("EventCalendar");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Barcolor)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.Details).HasColumnType("ntext");
            entity.Property(e => e.Enddate)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Muser).HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasDefaultValueSql("(getdate())", "DF_EventCalendar_MWhen")
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.Startdate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(1000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => new { e.EduYear, e.SubjectCode, e.Grade1 });

            entity.ToTable("Grade");

            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Grade1)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("Grade");
            entity.Property(e => e.Uid)
                .ValueGeneratedOnAdd()
                .HasColumnName("UID");
        });

        modelBuilder.Entity<GradePreceptor>(entity =>
        {
            entity.HasKey(e => new { e.EduYear, e.SubjectCode, e.Grade });

            entity.ToTable("GradePreceptor");

            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Grade)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Uid)
                .ValueGeneratedOnAdd()
                .HasColumnName("UID");
        });

        modelBuilder.Entity<Graduate>(entity =>
        {
            entity.HasKey(e => e.StudentCode);

            entity.ToTable("Graduate");

            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Student_Code");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.AddressLive).HasMaxLength(1000);
            entity.Property(e => e.AdvisorId).HasColumnName("AdvisorID");
            entity.Property(e => e.BirthDate).HasMaxLength(10);
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.Characteristic).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CongenitalDisease).HasMaxLength(255);
            entity.Property(e => e.ContactAddress).HasMaxLength(1000);
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactRelation).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Cvlink)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("CVLink");
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.DoctorName).HasMaxLength(50);
            entity.Property(e => e.EduExperience).HasMaxLength(4000);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Expectations).HasMaxLength(1000);
            entity.Property(e => e.FatherCareer)
                .HasMaxLength(50)
                .HasColumnName("Father_Career");
            entity.Property(e => e.FatherFirstName)
                .HasMaxLength(50)
                .HasColumnName("Father_FirstName");
            entity.Property(e => e.FatherLastName)
                .HasMaxLength(50)
                .HasColumnName("Father_LastName");
            entity.Property(e => e.FatherTel)
                .HasMaxLength(50)
                .HasColumnName("Father_Tel");
            entity.Property(e => e.FavoriteSubject).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FirstNameEn)
                .HasMaxLength(50)
                .HasColumnName("FirstNameEN");
            entity.Property(e => e.Gender).HasMaxLength(1);
            entity.Property(e => e.Gpax).HasColumnName("GPAX");
            entity.Property(e => e.HighProvince).HasMaxLength(50);
            entity.Property(e => e.HighSchool).HasMaxLength(255);
            entity.Property(e => e.Hobby).HasMaxLength(255);
            entity.Property(e => e.HospitalName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LastNameEn)
                .HasMaxLength(50)
                .HasColumnName("LastNameEN");
            entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            entity.Property(e => e.MajorId).HasColumnName("MajorID");
            entity.Property(e => e.MedicalHistory).HasMaxLength(255);
            entity.Property(e => e.MedicalRecommend).HasMaxLength(4000);
            entity.Property(e => e.MedicineUsually).HasMaxLength(255);
            entity.Property(e => e.MinorId).HasColumnName("MinorID");
            entity.Property(e => e.MobilePhone).HasMaxLength(50);
            entity.Property(e => e.MotherCareer)
                .HasMaxLength(50)
                .HasColumnName("Mother_Career");
            entity.Property(e => e.MotherFirstName)
                .HasMaxLength(50)
                .HasColumnName("Mother_FirstName");
            entity.Property(e => e.MotherLastName)
                .HasMaxLength(50)
                .HasColumnName("Mother_LastName");
            entity.Property(e => e.MotherTel)
                .HasMaxLength(50)
                .HasColumnName("Mother_Tel");
            entity.Property(e => e.NickName).HasMaxLength(50);
            entity.Property(e => e.PayorDetail).HasMaxLength(50);
            entity.Property(e => e.PayorId).HasColumnName("PayorID");
            entity.Property(e => e.PicturePath).HasMaxLength(50);
            entity.Property(e => e.Prefix).HasMaxLength(20);
            entity.Property(e => e.PrimaryProvince).HasMaxLength(50);
            entity.Property(e => e.PrimarySchool).HasMaxLength(255);
            entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");
            entity.Property(e => e.ProvinceName).HasMaxLength(50);
            entity.Property(e => e.Religion).HasMaxLength(50);
            entity.Property(e => e.SecondaryProvince).HasMaxLength(50);
            entity.Property(e => e.SecondarySchool).HasMaxLength(255);
            entity.Property(e => e.StatusFlag).HasMaxLength(2);
            entity.Property(e => e.StudentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Student_ID");
            entity.Property(e => e.StudentStatus).HasMaxLength(2);
            entity.Property(e => e.SubMinorId).HasColumnName("SubMinorID");
            entity.Property(e => e.Talent).HasMaxLength(1000);
            entity.Property(e => e.TelLive).HasMaxLength(50);
            entity.Property(e => e.Telephone).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(5);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK_Locations_1");

            entity.Property(e => e.LocationId)
                .ValueGeneratedNever()
                .HasColumnName("LocationID");
            entity.Property(e => e.Address).HasMaxLength(1000);
            entity.Property(e => e.ApartmentAddr1).HasMaxLength(255);
            entity.Property(e => e.ApartmentAddr2).HasMaxLength(255);
            entity.Property(e => e.ApartmentAddr3).HasMaxLength(255);
            entity.Property(e => e.ApartmentDistance1).HasMaxLength(50);
            entity.Property(e => e.ApartmentDistance2).HasMaxLength(50);
            entity.Property(e => e.ApartmentDistance3).HasMaxLength(50);
            entity.Property(e => e.ApartmentName1).HasMaxLength(50);
            entity.Property(e => e.ApartmentName2).HasMaxLength(50);
            entity.Property(e => e.ApartmentName3).HasMaxLength(50);
            entity.Property(e => e.ApartmentTel1).HasMaxLength(50);
            entity.Property(e => e.ApartmentTel2).HasMaxLength(50);
            entity.Property(e => e.ApartmentTel3).HasMaxLength(50);
            entity.Property(e => e.ApartmentTravel1).HasMaxLength(50);
            entity.Property(e => e.ApartmentTravel2).HasMaxLength(50);
            entity.Property(e => e.ApartmentTravel3).HasMaxLength(50);
            entity.Property(e => e.BillName)
                .HasMaxLength(100)
                .HasColumnName("Bill_Name");
            entity.Property(e => e.BranchRemark).HasMaxLength(255);
            entity.Property(e => e.CoMail)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Co_Mail");
            entity.Property(e => e.CoName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Co_Name");
            entity.Property(e => e.CoPosition)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Co_Position");
            entity.Property(e => e.CoTel)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Co_Tel");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ConfirmHold).HasMaxLength(50);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasDefaultValue("ประเทศไทย", "DF_Locations_Country");
            entity.Property(e => e.CuCoordinator).HasColumnName("CU_Coordinator");
            entity.Property(e => e.CuMail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CU_Mail");
            entity.Property(e => e.CuName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CU_Name");
            entity.Property(e => e.CuPosition)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CU_Position");
            entity.Property(e => e.CuTel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CU_Tel");
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Facebook)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FieldCount).HasColumnName("Field_Count");
            entity.Property(e => e.InfoDate).HasMaxLength(50);
            entity.Property(e => e.InfoPosition).HasMaxLength(50);
            entity.Property(e => e.Informant).HasMaxLength(50);
            entity.Property(e => e.IsBranch).HasColumnName("isBranch");
            entity.Property(e => e.IsForce).HasColumnName("isForce");
            entity.Property(e => e.IsPay)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isPay");
            entity.Property(e => e.IsPublic)
                .HasDefaultValue(1, "DF_Locations_isPublic")
                .HasColumnName("isPublic");
            entity.Property(e => e.IsReceive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isReceive");
            entity.Property(e => e.IsSameGender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isSameGender");
            entity.Property(e => e.Lang)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValue("TH", "DF_Locations_Lang");
            entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            entity.Property(e => e.Lat)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LetterTo).HasMaxLength(100);
            entity.Property(e => e.LevelId).HasColumnName("LevelID");
            entity.Property(e => e.Lng)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LocationGroupId).HasColumnName("LocationGroupID");
            entity.Property(e => e.LocationIdOld).HasColumnName("LocationID_Old");
            entity.Property(e => e.LocationName).HasMaxLength(255);
            entity.Property(e => e.LocationNameCert1).HasMaxLength(50);
            entity.Property(e => e.LocationNameCert2).HasMaxLength(50);
            entity.Property(e => e.LocationTypeId).HasColumnName("LocationTypeID");
            entity.Property(e => e.OfficeFax)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Office_Fax");
            entity.Property(e => e.OfficeHours)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Office_hours");
            entity.Property(e => e.OfficeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("OfficeID");
            entity.Property(e => e.OfficeTel)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Office_Tel");
            entity.Property(e => e.OfficerCount).HasColumnName("Officer_Count");
            entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");
            entity.Property(e => e.ProvinceName).HasMaxLength(50);
            entity.Property(e => e.Remark).HasColumnType("text");
            entity.Property(e => e.UpdBy).HasMaxLength(50);
            entity.Property(e => e.Website)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.WorkDayDesc).HasMaxLength(200);
            entity.Property(e => e.WorkDayId).HasColumnName("WorkDayID");
            entity.Property(e => e.WorkList).HasMaxLength(50);
            entity.Property(e => e.WorkSpec).HasMaxLength(255);
            entity.Property(e => e.WorkTimeDesc).HasMaxLength(200);
            entity.Property(e => e.WorkTimeId).HasColumnName("WorkTimeID");
            entity.Property(e => e.WorkTop).HasMaxLength(1000);
            entity.Property(e => e.ZipCode).HasMaxLength(5);
            entity.Property(e => e.ZoneId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ZoneID");
        });

        modelBuilder.Entity<LocationCredit>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("LocationCredit");

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.Byear).HasColumnName("BYear");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Muser).HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasDefaultValueSql("(getdate())", "DF_LocationCredit_MWhen")
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LocationDepartment>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("LocationDepartment");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<LocationGroup>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("LocationGroup");

            entity.Property(e => e.Code).ValueGeneratedNever();
            entity.Property(e => e.Descriptions).HasMaxLength(255);
            entity.Property(e => e.IsPublic).HasDefaultValue(1, "DF_LocationGroup_IsPublic");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<LocationOffice>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("LocationOffice");

            entity.Property(e => e.OfficeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("OfficeID");
            entity.Property(e => e.OfficeName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LocationRecommend>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("LocationRecommend");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.ApproveStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("W", "DF_LocationRecommend_ApproveStatus");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Mwhen)
                .HasDefaultValueSql("(getdate())", "DF_LocationRecommend_MWhen")
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.Recommend).IsUnicode(false);
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A", "DF_LocationRecommend_StatusFlag");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LocationSubject>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("LocationSubject");

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
        });

        modelBuilder.Entity<LocationType>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK_Location_A25C5AA6751945F8");

            entity.ToTable("LocationType");

            entity.Property(e => e.Code).ValueGeneratedNever();
            entity.Property(e => e.Descriptions).HasMaxLength(255);
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<LocationWork>(entity =>
        {
            entity.HasKey(e => e.WorkId);

            entity.ToTable("LocationWork");

            entity.Property(e => e.WorkId)
                .ValueGeneratedNever()
                .HasColumnName("WorkID");
            entity.Property(e => e.Cwhen)
                .HasDefaultValueSql("(getdate())", "DF_LocationWork_CWhen")
                .HasColumnType("datetime")
                .HasColumnName("CWhen");
            entity.Property(e => e.LocationGroup)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Mwhen)
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A", "DF_LocationWork_StatusFlag");
            entity.Property(e => e.WorkName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LocationWorkDetail>(entity =>
        {
            entity.HasKey(e => new { e.WorkId, e.LocationId });

            entity.ToTable("LocationWorkDetail");

            entity.Property(e => e.WorkId).HasColumnName("WorkID");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Cwhen)
                .HasColumnType("datetime")
                .HasColumnName("CWhen");
            entity.Property(e => e.Mwhen)
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LocationZone>(entity =>
        {
            entity.HasKey(e => e.ZoneId).HasName("PK_Zone");

            entity.ToTable("LocationZone");

            entity.Property(e => e.ZoneId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ZoneID");
            entity.Property(e => e.ZoneName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.ToTable("Major");

            entity.Property(e => e.MajorId)
                .ValueGeneratedNever()
                .HasColumnName("MajorID");
            entity.Property(e => e.AliasName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MajorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MajorNameEn)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Minor>(entity =>
        {
            entity.ToTable("Minor");

            entity.Property(e => e.MinorId)
                .ValueGeneratedNever()
                .HasColumnName("MinorID");
            entity.Property(e => e.MajorId).HasColumnName("MajorID");
            entity.Property(e => e.MinorName).HasMaxLength(50);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.Property(e => e.NewsId).HasColumnName("NewsID");
            entity.Property(e => e.ContentNews).HasColumnType("ntext");
            entity.Property(e => e.FirstPage).HasDefaultValue(0, "DF_News_FistPage");
            entity.Property(e => e.IsPublic)
                .HasDefaultValue(1, "DF_News_isPublic")
                .HasColumnName("isPublic");
            entity.Property(e => e.LinkPath).HasMaxLength(1000);
            entity.Property(e => e.NewsDate)
                .HasDefaultValueSql("(getdate())", "DF_News_NewsDate")
                .HasColumnType("datetime");
            entity.Property(e => e.NewsOrder).HasDefaultValue(99, "DF_News_NewsOrder");
            entity.Property(e => e.NewsType).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(1000);
        });

        modelBuilder.Entity<PaymentConfig>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("PaymentConfig");

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.StatusFlag).HasMaxLength(1);
            entity.Property(e => e.SubjectCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Payor>(entity =>
        {
            entity.ToTable("Payor");

            entity.Property(e => e.PayorId)
                .ValueGeneratedNever()
                .HasColumnName("PayorID");
            entity.Property(e => e.PayorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A", "DF_Payor_StatusFlag");
        });

        modelBuilder.Entity<Pc2Assessment>(entity =>
        {
            entity.HasKey(e => e.AssessmentId);

            entity.ToTable("PC2_Assessment");

            entity.Property(e => e.AssessmentId).HasColumnName("AssessmentID");
            entity.Property(e => e.Muser).HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasDefaultValueSql("(getdate())", "DF_PC2_Assessment_MWhen")
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubjectUid).HasColumnName("SubjectUID");
        });

        modelBuilder.Entity<Pc2Quotum>(entity =>
        {
            entity.HasKey(e => new { e.EduYear, e.SubjectUid });

            entity.ToTable("PC2_Quota");

            entity.Property(e => e.SubjectUid).HasColumnName("SubjectUID");
            entity.Property(e => e.Muser).HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasDefaultValueSql("(getdate())", "DF_PC2_Quota_MWhen")
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.Qty).HasColumnName("QTY");
            entity.Property(e => e.Reqid).HasColumnName("REQID");
        });

        modelBuilder.Entity<Pc2Student>(entity =>
        {
            entity.HasKey(e => new { e.EduYear, e.StudentCode });

            entity.ToTable("PC2_Student");

            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Uid)
                .ValueGeneratedOnAdd()
                .HasColumnName("UID");
        });

        modelBuilder.Entity<Pc2StudentRegister>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("PC2_StudentRegister");

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.Muser)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SubjectUid).HasColumnName("SubjectUID");
        });

        modelBuilder.Entity<Pc2Subject>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("PC2_Subject");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.NameEn)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("NameEN");
            entity.Property(e => e.NameTh)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("NameTH");
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Perceptor>(entity =>
        {
            entity.Property(e => e.PerceptorId)
                .ValueGeneratedNever()
                .HasColumnName("Perceptor_ID");
            entity.Property(e => e.Address).HasMaxLength(1000);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.DeptName).HasMaxLength(50);
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(1);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.MobilePhone).HasMaxLength(50);
            entity.Property(e => e.NickName).HasMaxLength(50);
            entity.Property(e => e.PicturePath).HasMaxLength(50);
            entity.Property(e => e.PositionName).HasMaxLength(50);
            entity.Property(e => e.Prefix).HasMaxLength(50);
            entity.Property(e => e.Prefix1).HasMaxLength(50);
            entity.Property(e => e.Prefix2).HasMaxLength(50);
            entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");
            entity.Property(e => e.ProvinceName).HasMaxLength(50);
            entity.Property(e => e.Telephone).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(5);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("Person_UpdatePrefixName"));

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Address)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ConfirmStatus)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Cvpath)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("CVPath");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DepartmentUid).HasColumnName("DepartmentUID");
            entity.Property(e => e.District)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsMain)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N", "DF_Persons_IsMain");
            entity.Property(e => e.IsTraining)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            entity.Property(e => e.LicenseNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.MobilePhone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NickName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PersonType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PicturePath)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.PositionName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Prefix1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prefix2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PrefixId).HasColumnName("PrefixID");
            entity.Property(e => e.PrefixName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A", "DF_Persons_StatusFlag");
            entity.Property(e => e.Telephone)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionName);

            entity.Property(e => e.PositionName).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(1, "DF_Positions_isActive")
                .HasColumnName("isActive");
            entity.Property(e => e.OrderSort).HasDefaultValue(0, "DF_Positions_OrderSort");
            entity.Property(e => e.PositionId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PositionID");
        });

        modelBuilder.Entity<PracticeHistory>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK_Practice_History");

            entity.ToTable("PracticeHistory");

            entity.Property(e => e.ItemId).HasColumnName("itemID");
            entity.Property(e => e.Grade).HasMaxLength(5);
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())", "DF_Practice_History_LastUpdate")
                .HasColumnType("datetime");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.PhaseId)
                .HasMaxLength(50)
                .HasColumnName("PhaseID");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(50)
                .HasColumnName("Student_Code");
            entity.Property(e => e.SubjectCode).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
        });

        modelBuilder.Entity<PreceptorLocation>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("PreceptorLocation");

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<Prefix>(entity =>
        {
            entity.Property(e => e.PrefixId).HasColumnName("PrefixID");
            entity.Property(e => e.Cuser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUser");
            entity.Property(e => e.Cwhen)
                .HasDefaultValueSql("(getdate())", "DF_Prefixs_CWhen")
                .HasColumnType("datetime")
                .HasColumnName("CWhen");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(1, "DF_Prefixs_isActive")
                .HasColumnName("isActive");
            entity.Property(e => e.IsPreceptor)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isPreceptor");
            entity.Property(e => e.IsStudent)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isStudent");
            entity.Property(e => e.IsTeacher)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isTeacher");
            entity.Property(e => e.Muser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasDefaultValueSql("(getdate())", "DF_Prefixs_MWhen")
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.PrefixName).HasMaxLength(100);
            entity.Property(e => e.Remark).HasMaxLength(50);
        });

        modelBuilder.Entity<Prominent>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("Prominent");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Descriptions)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ItemType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A", "DF_Table_1_StatusFlage");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.ToTable("Province");

            entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");
            entity.Property(e => e.ProvinceName).HasMaxLength(255);
        });

        modelBuilder.Entity<ReportGroup>(entity =>
        {
            entity.HasKey(e => e.Rptgrpuid);

            entity.ToTable("ReportGroup");

            entity.Property(e => e.Rptgrpuid)
                .ValueGeneratedNever()
                .HasColumnName("RPTGRPUID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A", "DF_ReportGroup_StatusFlag");
        });

        modelBuilder.Entity<ReportGroupLink>(entity =>
        {
            entity.HasKey(e => new { e.Rptgrpuid, e.SubjectCode });

            entity.ToTable("ReportGroupLink");

            entity.Property(e => e.Rptgrpuid).HasColumnName("RPTGRPUID");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Requirement>(entity =>
        {
            entity.HasKey(e => new { e.Reqyear, e.LocationId, e.TimePhaseId }).HasName("PK_Requirement_Account");

            entity.Property(e => e.Reqyear).HasColumnName("REQYEAR");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.TimePhaseId).HasColumnName("TimePhaseID");
            entity.Property(e => e.IsSameGender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isSameGender");
            entity.Property(e => e.Man).HasDefaultValue(0, "DF_Requirement_Account_Man");
            entity.Property(e => e.NoSpec).HasDefaultValue(0, "DF_Requirement_Account_NoSpec");
            entity.Property(e => e.Reqid)
                .ValueGeneratedOnAdd()
                .HasColumnName("REQID");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Women).HasDefaultValue(0, "DF_Requirement_Account_Women");
        });

        modelBuilder.Entity<RequirementEstimate>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("RequirementEstimate");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Cuser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUser");
            entity.Property(e => e.Cwhen)
                .HasDefaultValueSql("(getdate())", "DF_RequirementEstimate_CWhen")
                .HasColumnType("datetime")
                .HasColumnName("CWhen");
            entity.Property(e => e.IsSameGender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isSameGender");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Muser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.Qty1).HasColumnName("QTY1");
            entity.Property(e => e.Qty2).HasColumnName("QTY2");
            entity.Property(e => e.ReqOption)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.WorkId).HasColumnName("WorkID");
        });

        modelBuilder.Entity<Requirements2>(entity =>
        {
            entity.HasKey(e => new { e.Reqyear, e.LocationId, e.SubjectCode }).HasName("PK_Requirements");

            entity.ToTable("Requirements2");

            entity.Property(e => e.Reqyear).HasColumnName("REQYEAR");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.SubjectCode).HasMaxLength(50);
            entity.Property(e => e.ApartmentAddr).HasMaxLength(255);
            entity.Property(e => e.ApartmentDistance).HasMaxLength(50);
            entity.Property(e => e.ApartmentName).HasMaxLength(50);
            entity.Property(e => e.ApartmentTel).HasMaxLength(50);
            entity.Property(e => e.ApartmentTravel).HasMaxLength(50);
            entity.Property(e => e.BranchRemark).HasMaxLength(255);
            entity.Property(e => e.ConfirmHold).HasMaxLength(50);
            entity.Property(e => e.InfoDate).HasMaxLength(50);
            entity.Property(e => e.InfoPosition).HasMaxLength(50);
            entity.Property(e => e.Informant).HasMaxLength(50);
            entity.Property(e => e.IsBranch).HasColumnName("isBranch");
            entity.Property(e => e.IsForce).HasColumnName("isForce");
            entity.Property(e => e.IsPublic).HasDefaultValue(1, "DF_Requirements_IsPublic");
            entity.Property(e => e.IsSameGender)
                .HasMaxLength(1)
                .HasColumnName("isSameGender");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())", "DF_Requirements_LastUpdate")
                .HasColumnType("datetime");
            entity.Property(e => e.LocationGroupId).HasColumnName("LocationGroupID");
            entity.Property(e => e.Remark).HasColumnType("text");
            entity.Property(e => e.Reqid)
                .ValueGeneratedOnAdd()
                .HasColumnName("REQID");
            entity.Property(e => e.UpdBy).HasMaxLength(50);
            entity.Property(e => e.WorkDayDesc).HasMaxLength(50);
            entity.Property(e => e.WorkDayId).HasColumnName("WorkDayID");
            entity.Property(e => e.WorkList).HasMaxLength(50);
            entity.Property(e => e.WorkSpec).HasMaxLength(255);
            entity.Property(e => e.WorkTimeDesc).HasMaxLength(50);
            entity.Property(e => e.WorkTimeId).HasColumnName("WorkTimeID");
            entity.Property(e => e.WorkTop).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.Descriptions).HasMaxLength(100);
            entity.Property(e => e.IsPublic).HasDefaultValue(1, "DF_Roles_IsPublic");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<RoomRecommend>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("RoomRecommend");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.ApproveStatus)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Mwhen)
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.Recommend).IsUnicode(false);
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RunningConfig>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RunningConfig");

            entity.Property(e => e.Code)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Descriptions)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsCode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isCode");
            entity.Property(e => e.IsYear)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isYear");
            entity.Property(e => e.TemplateCode)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RunningNumber>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("RunningNumber");

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cyear).HasColumnName("CYear");
            entity.Property(e => e.RunningValue).HasDefaultValue(0, "DF_RunningNumber_Running");
        });

        modelBuilder.Entity<SendMailAlert>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("SendMailAlert");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.SendDttm).HasColumnType("datetime");
            entity.Property(e => e.SendStatus)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.TimePhaseId).HasColumnName("TimePhaseID");
        });

        modelBuilder.Entity<StatusDefinition>(entity =>
        {
            entity.HasKey(e => new { e.StatusGroup, e.StatusCode });

            entity.ToTable("StatusDefinition");

            entity.Property(e => e.StatusGroup)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentCode).HasName("PK_Students2");

            entity.ToTable("Students");

            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Student_Code");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.AddressLive).HasMaxLength(1000);
            entity.Property(e => e.AdvisorId).HasColumnName("AdvisorID");
            entity.Property(e => e.BirthDate).HasMaxLength(10);
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.Characteristic).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CongenitalDisease).HasMaxLength(255);
            entity.Property(e => e.ContactAddress).HasMaxLength(1000);
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactRelation).HasMaxLength(50);
            entity.Property(e => e.ContactTel).HasMaxLength(50);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())", "DF_Students2_CreateDate")
                .HasColumnType("datetime");
            entity.Property(e => e.Cvlink)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("CVLink");
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.DoctorName).HasMaxLength(50);
            entity.Property(e => e.EduExperience).HasMaxLength(4000);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Expectations).HasMaxLength(1000);
            entity.Property(e => e.FatherCareer)
                .HasMaxLength(50)
                .HasColumnName("Father_Career");
            entity.Property(e => e.FatherFirstName)
                .HasMaxLength(50)
                .HasColumnName("Father_FirstName");
            entity.Property(e => e.FatherLastName)
                .HasMaxLength(50)
                .HasColumnName("Father_LastName");
            entity.Property(e => e.FatherTel)
                .HasMaxLength(50)
                .HasColumnName("Father_Tel");
            entity.Property(e => e.FavoriteSubject).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FirstNameEn)
                .HasMaxLength(50)
                .HasColumnName("FirstNameEN");
            entity.Property(e => e.Gender).HasMaxLength(1);
            entity.Property(e => e.Gpax).HasColumnName("GPAX");
            entity.Property(e => e.HighProvince).HasMaxLength(50);
            entity.Property(e => e.HighSchool).HasMaxLength(255);
            entity.Property(e => e.Hobby).HasMaxLength(255);
            entity.Property(e => e.HospitalName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LastNameEn)
                .HasMaxLength(50)
                .HasColumnName("LastNameEN");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())", "DF_Students2_LastUpdate")
                .HasColumnType("datetime");
            entity.Property(e => e.LevelClass).HasDefaultValue(1, "DF_Students_LevelClass");
            entity.Property(e => e.MajorId).HasColumnName("MajorID");
            entity.Property(e => e.MedicalHistory).HasMaxLength(255);
            entity.Property(e => e.MedicalRecommend).HasMaxLength(4000);
            entity.Property(e => e.MedicineUsually).HasMaxLength(255);
            entity.Property(e => e.MinorId).HasColumnName("MinorID");
            entity.Property(e => e.MobilePhone).HasMaxLength(50);
            entity.Property(e => e.MotherCareer)
                .HasMaxLength(50)
                .HasColumnName("Mother_Career");
            entity.Property(e => e.MotherFirstName)
                .HasMaxLength(50)
                .HasColumnName("Mother_FirstName");
            entity.Property(e => e.MotherLastName)
                .HasMaxLength(50)
                .HasColumnName("Mother_LastName");
            entity.Property(e => e.MotherTel)
                .HasMaxLength(50)
                .HasColumnName("Mother_Tel");
            entity.Property(e => e.NickName).HasMaxLength(50);
            entity.Property(e => e.PayorDetail).HasMaxLength(50);
            entity.Property(e => e.PayorId).HasColumnName("PayorID");
            entity.Property(e => e.PicturePath).HasMaxLength(50);
            entity.Property(e => e.Prefix).HasMaxLength(20);
            entity.Property(e => e.PrimaryProvince).HasMaxLength(50);
            entity.Property(e => e.PrimarySchool).HasMaxLength(255);
            entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");
            entity.Property(e => e.ProvinceName).HasMaxLength(50);
            entity.Property(e => e.Religion).HasMaxLength(50);
            entity.Property(e => e.SecondaryProvince).HasMaxLength(50);
            entity.Property(e => e.SecondarySchool).HasMaxLength(255);
            entity.Property(e => e.SeniorProjectId).HasColumnName("SeniorProjectID");
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(2)
                .HasDefaultValue("A", "DF_Students_StatusFlag");
            entity.Property(e => e.StudentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Student_ID");
            entity.Property(e => e.StudentStatus).HasMaxLength(2);
            entity.Property(e => e.SubMinorId).HasColumnName("SubMinorID");
            entity.Property(e => e.Talent).HasMaxLength(1000);
            entity.Property(e => e.TelLive).HasMaxLength(50);
            entity.Property(e => e.Telephone).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(5);
        });

        modelBuilder.Entity<StudentAssessment>(entity =>
        {
            entity.HasKey(e => new { e.EduYear, e.EvaluationTopicUid, e.AssessorUid, e.SubjectCode, e.StudentCode }).HasName("PK_StudentAssessment_1");

            entity.ToTable("StudentAssessment");

            entity.Property(e => e.EvaluationTopicUid).HasColumnName("EvaluationTopicUID");
            entity.Property(e => e.AssessorUid).HasColumnName("AssessorUID");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.AssessorLocationId).HasColumnName("AssessorLocationID");
            entity.Property(e => e.AssessorRoleId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("AssessorRoleID");
            entity.Property(e => e.Cuser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CUser");
            entity.Property(e => e.Cwhen)
                .HasDefaultValueSql("(getdate())", "DF_StudentAssessment_CWhen")
                .HasColumnType("datetime")
                .HasColumnName("CWhen");
            entity.Property(e => e.EvaluationGroupUid).HasColumnName("EvaluationGroupUID");
            entity.Property(e => e.IsExclude)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N", "DF_StudentAssessment_isExclude")
                .HasColumnName("isExclude");
            entity.Property(e => e.Muser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasDefaultValueSql("(getdate())", "DF_StudentAssessment_MWhen")
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.Uid)
                .ValueGeneratedOnAdd()
                .HasColumnName("UID");
            entity.Property(e => e.XEvaluationFormUid).HasColumnName("xEvaluationFormUID");
        });

        modelBuilder.Entity<StudentAssessmentComment>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("StudentAssessmentComment");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Comment)
                .HasMaxLength(4000)
                .IsUnicode(false);
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Muser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.PhaseId).HasColumnName("PhaseID");
            entity.Property(e => e.ProsId).HasColumnName("ProsID");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TimePhaseId).HasColumnName("TimePhaseID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<StudentAssessmentCommentText>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("StudentAssessmentCommentText");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Comment1)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.Comment2)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.IsSure)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Muser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.PhaseId).HasColumnName("PhaseID");
            entity.Property(e => e.Reason)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.StandingGrade)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TimePhaseId).HasColumnName("TimePhaseID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.WorkDetail)
                .HasMaxLength(8000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StudentAssessmentGrade>(entity =>
        {
            entity.HasKey(e => new { e.EduYear, e.StudentCode, e.SubjectCode }).HasName("PK_StudentAssessmentGrade_1");

            entity.ToTable("StudentAssessmentGrade");

            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Cuser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUser");
            entity.Property(e => e.Cwhen)
                .HasDefaultValueSql("(getdate())", "DF_StudentAssessmentGrade_CWhen")
                .HasColumnType("datetime")
                .HasColumnName("CWhen");
            entity.Property(e => e.Grade)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Muser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasDefaultValueSql("(getdate())", "DF_StudentAssessmentGrade_MWhen")
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.Uid)
                .ValueGeneratedOnAdd()
                .HasColumnName("UID");
        });

        modelBuilder.Entity<StudentAssessmentGradeModify>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("StudentAssessmentGradeModify");

            entity.Property(e => e.GradeNew)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("Grade_New");
            entity.Property(e => e.GradeOld)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("Grade_Old");
            entity.Property(e => e.Muser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasDefaultValueSql("(getdate())", "DF_StudentAssessmentGradeModify_MWhen")
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.Reason)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.ScoreNew).HasColumnName("Score_New");
            entity.Property(e => e.ScoreOld).HasColumnName("Score_Old");
            entity.Property(e => e.StudentAssessmentGradeUid).HasColumnName("StudentAssessmentGradeUID");
            entity.Property(e => e.Uid)
                .ValueGeneratedOnAdd()
                .HasColumnName("UID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<StudentAssessmentReason>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("StudentAssessmentReason");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.EvaluationGroupUid).HasColumnName("EvaluationGroupUID");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Reason)
                .HasMaxLength(4000)
                .IsUnicode(false);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SubmitBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SubmitDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<StudentAssessmentScore>(entity =>
        {
            entity.HasKey(e => new { e.EduYear, e.EvaluationGroupUid, e.AssessorUid, e.StudentCode, e.SubjectCode });

            entity.ToTable("StudentAssessmentScore");

            entity.Property(e => e.EvaluationGroupUid).HasColumnName("EvaluationGroupUID");
            entity.Property(e => e.AssessorUid).HasColumnName("AssessorUID");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.AssessorLocationId).HasColumnName("AssessorLocationID");
            entity.Property(e => e.AssessorName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AssessorRoleId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("AssessorRoleID");
            entity.Property(e => e.Comments)
                .HasMaxLength(4000)
                .IsUnicode(false);
            entity.Property(e => e.EvaluationFormUid).HasColumnName("EvaluationFormUID");
            entity.Property(e => e.Muser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasDefaultValueSql("(getdate())", "DF_StudentAssessmentScore_MWhen")
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.Uid)
                .ValueGeneratedOnAdd()
                .HasColumnName("UID");
        });

        modelBuilder.Entity<StudentEvaluation>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("StudentEvaluation");

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.AssessorLocationId).HasColumnName("AssessorLocationID");
            entity.Property(e => e.AssessorName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.AssessorRoleId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("AssessorRoleID");
            entity.Property(e => e.AssessorUid).HasColumnName("AssessorUID");
            entity.Property(e => e.Comments)
                .HasMaxLength(4000)
                .IsUnicode(false);
            entity.Property(e => e.ConfirmDate).HasColumnType("datetime");
            entity.Property(e => e.ConfirmStatus)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.EstimateGrade)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Muser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TimePhaseId).HasColumnName("TimePhaseID");
        });

        modelBuilder.Entity<StudentLevel>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("StudentLevel");

            entity.Property(e => e.Code).ValueGeneratedNever();
            entity.Property(e => e.StudentStatus).HasMaxLength(20);
        });

        modelBuilder.Entity<StudentRegister>(entity =>
        {
            entity.HasKey(e => new { e.Regyear, e.DegreeNo, e.StudentCode, e.SubjectCode, e.LocationId });

            entity.ToTable("StudentRegister");

            entity.Property(e => e.Regyear).HasColumnName("REGYEAR");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Student_Code");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.ItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("itemID");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())", "DF_StudentRegister_LastUpdate")
                .HasColumnType("datetime");
            entity.Property(e => e.TimePhaseId)
                .HasDefaultValue(1, "DF_StudentRegister_PhaseNo")
                .HasColumnName("TimePhaseID");
            entity.Property(e => e.UpdBy)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StudentStatus>(entity =>
        {
            entity.HasKey(e => e.StatusCode);

            entity.ToTable("StudentStatus");

            entity.Property(e => e.StatusCode).HasMaxLength(5);
            entity.Property(e => e.StatusFlag).HasMaxLength(1);
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<SubMinor>(entity =>
        {
            entity.ToTable("SubMinor");

            entity.Property(e => e.SubMinorId)
                .ValueGeneratedNever()
                .HasColumnName("SubMinorID");
            entity.Property(e => e.MinorId).HasColumnName("MinorID");
            entity.Property(e => e.SubMinorName).HasMaxLength(50);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectCode);

            entity.ToTable(tb => tb.HasTrigger("AUTO_SUBJECTID"));

            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.AliasName).HasMaxLength(50);
            entity.Property(e => e.IsPublic).HasDefaultValue(1, "DF_Subjects_IsPublic");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())", "DF_Subjects_LastUpdate")
                .HasColumnType("datetime");
            entity.Property(e => e.MajorId)
                .HasComment("วิชาเลือก/บังคับ")
                .HasColumnName("MajorID");
            entity.Property(e => e.NameCert1En)
                .HasMaxLength(100)
                .HasColumnName("NameCert1EN");
            entity.Property(e => e.NameCert1Th)
                .HasMaxLength(100)
                .HasColumnName("NameCert1TH");
            entity.Property(e => e.NameCert2En)
                .HasMaxLength(100)
                .HasColumnName("NameCert2EN");
            entity.Property(e => e.NameCert2Th)
                .HasMaxLength(100)
                .HasColumnName("NameCert2TH");
            entity.Property(e => e.NameEn)
                .HasMaxLength(500)
                .HasColumnName("NameEN");
            entity.Property(e => e.NameTh)
                .HasMaxLength(500)
                .HasColumnName("NameTH");
            entity.Property(e => e.PharmCare).HasMaxLength(50);
            entity.Property(e => e.PharmSci).HasMaxLength(50);
            entity.Property(e => e.SubjectGroupId).HasColumnName("SubjectGroupID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.UpdBy)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubjectGroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SubjectGroup");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Subjects2>(entity =>
        {
            entity.HasKey(e => e.SubjectCode);

            entity.ToTable("Subjects2");

            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.AliasName).HasMaxLength(50);
            entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            entity.Property(e => e.MajorId).HasColumnName("MajorID");
            entity.Property(e => e.NameEn)
                .HasMaxLength(500)
                .HasColumnName("NameEN");
            entity.Property(e => e.NameTh)
                .HasMaxLength(500)
                .HasColumnName("NameTH");
            entity.Property(e => e.PharmCare).HasMaxLength(50);
            entity.Property(e => e.PharmSci).HasMaxLength(50);
            entity.Property(e => e.SubjectId)
                .ValueGeneratedOnAdd()
                .HasColumnName("SubjectID");
            entity.Property(e => e.UpdBy)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SupervisionPlan>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("SupervisionPlan");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ApprovalId).HasColumnName("ApprovalID");
            entity.Property(e => e.ApproveDate).HasColumnType("datetime");
            entity.Property(e => e.ApproveStatus)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Remark).HasMaxLength(1000);
            entity.Property(e => e.Seq).HasColumnName("SEQ");
            entity.Property(e => e.TravelBy).HasMaxLength(50);
            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.UpdBy).HasMaxLength(50);
            entity.Property(e => e.UpdDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<SupervisionPlanDetail>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("SupervisionPlanDetail");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Cuser)
                .HasMaxLength(20)
                .HasColumnName("CUser");
            entity.Property(e => e.Cwhen)
                .HasDefaultValueSql("(getdate())", "DF_SupervisionPlanDetail_CWhen")
                .HasColumnType("datetime")
                .HasColumnName("CWhen");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Muser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MUser");
            entity.Property(e => e.Mwhen)
                .HasDefaultValueSql("(getdate())", "DF_SupervisionPlanDetail_MWhen")
                .HasColumnType("datetime")
                .HasColumnName("MWhen");
            entity.Property(e => e.PlanUid).HasColumnName("PlanUID");
        });

        modelBuilder.Entity<Supervisor>(entity =>
        {
            entity.HasKey(e => new { e.Pyear, e.PersonId, e.WorkDate, e.WorkTime });

            entity.ToTable("Supervisor", tb =>
                {
                    tb.HasTrigger("AUTO_SupervisorUID");
                    tb.HasTrigger("Supervisor_UpdateSubjectCode");
                });

            entity.Property(e => e.Pyear).HasColumnName("PYear");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.IsSend)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N", "DF_Supervisor_isSend")
                .HasColumnName("isSend");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.SubjectCode).HasMaxLength(50);
            entity.Property(e => e.TimePhaseId).HasColumnName("TimePhaseID");
            entity.Property(e => e.TravelBy).HasMaxLength(50);
            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.UpdBy).HasMaxLength(50);
            entity.Property(e => e.UpdDate)
                .HasDefaultValueSql("(getdate())", "DF_Supervisor_UpdDate")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<SystemConfig>(entity =>
        {
            entity.HasKey(e => e.CodeConfig).HasName("PK_TRPT_SystemConfig");

            entity.ToTable("SystemConfig");

            entity.Property(e => e.CodeConfig).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.ItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("itemID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ValueConfig).HasMaxLength(255);
        });

        modelBuilder.Entity<TimePhase>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("TimePhase", tb => tb.HasTrigger("AUTO_TimePhaseID"));

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.PhaseId).HasColumnName("PhaseID");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TimePhaseId).HasColumnName("TimePhaseID");
        });

        modelBuilder.Entity<TmStudentRegisterSummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tm_StudentRegister_Summary");

            entity.Property(e => e.LocationGroup).HasMaxLength(255);
            entity.Property(e => e.LocationGroupId).HasColumnName("LocationGroupID");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.LocationName).HasMaxLength(255);
            entity.Property(e => e.PhaseName1).HasMaxLength(50);
            entity.Property(e => e.PhaseName2).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(1000);
            entity.Property(e => e.Reqyear).HasColumnName("REQYEAR");
        });

        modelBuilder.Entity<TmpDocumentPrint>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK_tempDocumentPrint");

            entity.ToTable("tmpDocumentPrint");

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.DocumentTemplateUid).HasColumnName("DocumentTemplateUID");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
        });

        modelBuilder.Entity<TmpReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tmpReports");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TmpStudent>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tmpStudents");

            entity.Property(e => e.AdvisorId).HasColumnName("AdvisorID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gpax).HasColumnName("GPAX");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MajorId).HasColumnName("MajorID");
            entity.Property(e => e.MinorId).HasColumnName("MinorID");
            entity.Property(e => e.Prefix).HasMaxLength(50);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(20)
                .HasColumnName("Student_Code");
            entity.Property(e => e.SubMinorId).HasColumnName("SubMinorID");
        });

        modelBuilder.Entity<TmpStudentBio>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tmpStudentBio");

            entity.Property(e => e.StudentId).HasColumnName("Student_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TmpStudentCert>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.Username });

            entity.ToTable("tmpStudentCert");

            entity.Property(e => e.StudentId).HasColumnName("Student_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DocumentDate).HasMaxLength(50);
        });

        modelBuilder.Entity<TurnPhase>(entity =>
        {
            entity.HasKey(e => e.PhaseId);

            entity.ToTable("TurnPhase");

            entity.Property(e => e.PhaseId)
                .ValueGeneratedNever()
                .HasColumnName("PhaseID");
            entity.Property(e => e.Descriptions).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK_Users2");

            entity.Property(e => e.Username).HasMaxLength(20);
            entity.Property(e => e.AcceptDttm).HasColumnType("datetime");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LastLogin)
                .HasDefaultValueSql("(getdate())", "DF_Users2_LastLogin")
                .HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(1000);
            entity.Property(e => e.PolicyAccept)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ProfileId)
                .HasMaxLength(50)
                .HasColumnName("ProfileID");
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A", "DF_Users_StatusFlag");
            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserID");
            entity.Property(e => e.UserProfileId).HasColumnName("UserProfileID");
            entity.Property(e => e.XEmail)
                .HasMaxLength(255)
                .HasColumnName("xEmail");
            entity.Property(e => e.XFirstName)
                .HasMaxLength(255)
                .HasColumnName("xFirstName");
            entity.Property(e => e.XLastName)
                .HasMaxLength(255)
                .HasColumnName("xLastName");
        });

        modelBuilder.Entity<UserLogFile>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("UserLogFile");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.ActType)
                .HasMaxLength(50)
                .HasColumnName("Act_Type");
            entity.Property(e => e.DbEffective)
                .HasMaxLength(50)
                .HasColumnName("DB_Effective");
            entity.Property(e => e.Descrp).HasMaxLength(255);
            entity.Property(e => e.Remark).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.WorkDate)
                .HasDefaultValueSql("(getdate())", "DF_UserLogFile_Work_Date")
                .HasColumnType("datetime")
                .HasColumnName("Work_Date");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.ToTable("UserProfile");

            entity.Property(e => e.UserProfileId).HasColumnName("UserProfileID");
            entity.Property(e => e.ProfileName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(1, "DF_UserRoles_isActive")
                .HasColumnName("isActive");
            entity.Property(e => e.UpdDate)
                .HasDefaultValueSql("(getdate())", "DF_UserRoles_UpdDate")
                .HasColumnType("datetime");
            entity.Property(e => e.UserRoleId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserRoleID");
        });

        modelBuilder.Entity<VisitLocation>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("VisitLocation");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Cuser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CUser");
            entity.Property(e => e.Cwhen)
                .HasDefaultValueSql("(getdate())", "DF_VisitLocation_CWhen")
                .HasColumnType("datetime")
                .HasColumnName("CWhen");
            entity.Property(e => e.IsForceVisit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isForceVisit");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
        });

        modelBuilder.Entity<Vpnaccount>(entity =>
        {
            entity.HasKey(e => e.LocationId);

            entity.ToTable("VPNAccount");

            entity.Property(e => e.LocationId)
                .ValueGeneratedNever()
                .HasColumnName("LocationID");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<XDocumentExtension>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK_DocumentExtension");

            entity.ToTable("xDocumentExtension");

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.Descriptions).HasMaxLength(200);
            entity.Property(e => e.DocumentTemplateUid).HasColumnName("DocumentTemplateUID");
            entity.Property(e => e.Qty)
                .HasMaxLength(50)
                .HasColumnName("QTY");
            entity.Property(e => e.Uom)
                .HasMaxLength(50)
                .HasColumnName("UOM");
        });

        modelBuilder.Entity<XDocumentTemplate>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK_DocumentTemplate");

            entity.ToTable("xDocumentTemplate");

            entity.Property(e => e.Uid).HasColumnName("UID");
            entity.Property(e => e.Content1).HasMaxLength(500);
            entity.Property(e => e.Content2).HasMaxLength(500);
            entity.Property(e => e.Cuser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUser");
            entity.Property(e => e.Cwhen)
                .HasColumnType("datetime")
                .HasColumnName("CWhen");
            entity.Property(e => e.DocumentDate).HasMaxLength(500);
            entity.Property(e => e.DocumentName).HasMaxLength(500);
            entity.Property(e => e.DocumentUid).HasColumnName("DocumentUID");
            entity.Property(e => e.EduYear).HasMaxLength(50);
            entity.Property(e => e.PhaseName).HasMaxLength(50);
            entity.Property(e => e.StdLevel).HasMaxLength(50);
            entity.Property(e => e.SubjectTitle).HasMaxLength(500);
        });


        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
            .Where(e => !typeof(EntityBase).IsAssignableFrom(e.ClrType) && e.GetViewName() is null))
        {
            entityType.SetIsTableExcludedFromMigrations(true);
        }
    }
}
