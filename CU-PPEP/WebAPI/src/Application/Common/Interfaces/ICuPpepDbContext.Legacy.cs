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

namespace CUPPEP.Application.Common.Interfaces;

public partial interface ICuPpepDbContext
{
    DbSet<Assessment> Assessments { get; }
    DbSet<AssessmentMailAlert> AssessmentMailAlerts { get; }
    DbSet<AssessmentSubject> AssessmentSubjects { get; }
    DbSet<Certification> Certifications { get; }
    DbSet<Coordinator> Coordinators { get; }
    DbSet<Course> Courses { get; }
    DbSet<CourseCoordinator> CourseCoordinators { get; }
    DbSet<CourseLocation> CourseLocations { get; }
    DbSet<CourseStudent> CourseStudents { get; }
    DbSet<Department> Departments { get; }
    DbSet<Document> Documents { get; }
    DbSet<DocumentPrint> DocumentPrints { get; }
    DbSet<Education> Educations { get; }
    DbSet<EvaluationForm> EvaluationForms { get; }
    DbSet<EvaluationGroup> EvaluationGroups { get; }
    DbSet<EvaluationTopic> EvaluationTopics { get; }
    DbSet<EventCalendar> EventCalendars { get; }
    DbSet<Grade> Grades { get; }
    DbSet<GradePreceptor> GradePreceptors { get; }
    DbSet<Graduate> Graduates { get; }
    DbSet<Location> Locations { get; }
    DbSet<LocationCredit> LocationCredits { get; }
    DbSet<LocationDepartment> LocationDepartments { get; }
    DbSet<LocationGroup> LocationGroups { get; }
    DbSet<LocationOffice> LocationOffices { get; }
    DbSet<LocationRecommend> LocationRecommends { get; }
    DbSet<LocationSubject> LocationSubjects { get; }
    DbSet<LocationType> LocationTypes { get; }
    DbSet<LocationWork> LocationWorks { get; }
    DbSet<LocationWorkDetail> LocationWorkDetails { get; }
    DbSet<LocationZone> LocationZones { get; }
    DbSet<Major> Majors { get; }
    DbSet<Minor> Minors { get; }
    DbSet<News> News { get; }
    DbSet<PaymentConfig> PaymentConfigs { get; }
    DbSet<Payor> Payors { get; }
    DbSet<Pc2Assessment> Pc2Assessments { get; }
    DbSet<Pc2Quotum> Pc2Quota { get; }
    DbSet<Pc2Student> Pc2Students { get; }
    DbSet<Pc2StudentRegister> Pc2StudentRegisters { get; }
    DbSet<Pc2Subject> Pc2Subjects { get; }
    DbSet<Perceptor> Perceptors { get; }
    DbSet<Person> Persons { get; }
    DbSet<Position> Positions { get; }
    DbSet<PracticeHistory> PracticeHistories { get; }
    DbSet<PreceptorLocation> PreceptorLocations { get; }
    DbSet<Prefix> Prefixs { get; }
    DbSet<Prominent> Prominents { get; }
    DbSet<Province> Provinces { get; }
    DbSet<ReportGroup> ReportGroups { get; }
    DbSet<ReportGroupLink> ReportGroupLinks { get; }
    DbSet<Requirement> Requirements { get; }
    DbSet<RequirementEstimate> RequirementEstimates { get; }
    DbSet<Requirements2> Requirements2s { get; }
    DbSet<Role> Roles { get; }
    DbSet<RoomRecommend> RoomRecommends { get; }
    DbSet<RunningConfig> RunningConfigs { get; }
    DbSet<RunningNumber> RunningNumbers { get; }
    DbSet<SendMailAlert> SendMailAlerts { get; }
    DbSet<StatusDefinition> StatusDefinitions { get; }
    DbSet<Student> Students { get; }
    DbSet<StudentAssessment> StudentAssessments { get; }
    DbSet<StudentAssessmentComment> StudentAssessmentComments { get; }
    DbSet<StudentAssessmentCommentText> StudentAssessmentCommentTexts { get; }
    DbSet<StudentAssessmentGrade> StudentAssessmentGrades { get; }
    DbSet<StudentAssessmentGradeModify> StudentAssessmentGradeModifies { get; }
    DbSet<StudentAssessmentReason> StudentAssessmentReasons { get; }
    DbSet<StudentAssessmentScore> StudentAssessmentScores { get; }
    DbSet<StudentEvaluation> StudentEvaluations { get; }
    DbSet<StudentLevel> StudentLevels { get; }
    DbSet<StudentRegister> StudentRegisters { get; }
    DbSet<StudentStatus> StudentStatuses { get; }
    DbSet<SubMinor> SubMinors { get; }
    DbSet<Subject> Subjects { get; }
    DbSet<SubjectGroup> SubjectGroups { get; }
    DbSet<Subjects2> Subjects2s { get; }
    DbSet<SupervisionPlan> SupervisionPlans { get; }
    DbSet<SupervisionPlanDetail> SupervisionPlanDetails { get; }
    DbSet<Supervisor> Supervisors { get; }
    DbSet<SystemConfig> SystemConfigs { get; }
    DbSet<TimePhase> TimePhases { get; }
    DbSet<TmStudentRegisterSummary> TmStudentRegisterSummaries { get; }
    DbSet<TmpDocumentPrint> TmpDocumentPrints { get; }
    DbSet<TmpReport> TmpReports { get; }
    DbSet<TmpStudent> TmpStudents { get; }
    DbSet<TmpStudentBio> TmpStudentBios { get; }
    DbSet<TmpStudentCert> TmpStudentCerts { get; }
    DbSet<TurnPhase> TurnPhases { get; }
    DbSet<User> Users { get; }
    DbSet<UserLogFile> UserLogFiles { get; }
    DbSet<UserProfile> UserProfiles { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<VisitLocation> VisitLocations { get; }
    DbSet<Vpnaccount> Vpnaccounts { get; }
    DbSet<XDocumentExtension> XDocumentExtensions { get; }
    DbSet<XDocumentTemplate> XDocumentTemplates { get; }
}
