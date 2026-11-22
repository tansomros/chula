namespace Chula.Application.Common.Security;

public static class PermissionConstants
{
    public static class ReferenceData
    {
        public const string View = "reference-data.view";
        public const string Manage = "reference-data.manage";
    }

    public static class FacultyCv
    {
        public const string ViewOwn = "faculty-cv.view-own";
        public const string EditOwn = "faculty-cv.edit-own";
        public const string ExportOwn = "faculty-cv.export-own";
        public const string ViewAll = "faculty-cv.view-all";
        public const string EditAll = "faculty-cv.edit-all";
        public const string ExportAll = "faculty-cv.export-all";
        public const string NotifyUpdate = "faculty-cv.notify-update";
    }

    public static class TrainingRequests
    {
        public const string ViewOwn = "training-requests.view-own";
        public const string CreateOwn = "training-requests.create-own";
        public const string UpdateOwn = "training-requests.update-own";
        public const string ViewAll = "training-requests.view-all";
        public const string Verify = "training-requests.verify";
        public const string Approve = "training-requests.approve";
        public const string ViewBankAccount = "training-requests.view-bank-account";
        public const string Export = "training-requests.export";
    }

    public static class StudentsCv
    {
        public const string View = "students-cv.view";
        public const string Edit = "students-cv.edit";
        public const string Export = "students-cv.export";
    }

    public static class EmployerEvaluations
    {
        public const string View = "employer-evaluations.view";
    }

    public static class SeniorProjects
    {
        public const string ViewAll = "senior-projects.view-all";
        public const string Manage = "senior-projects.manage";
        public const string UploadPdf = "senior-projects.upload-pdf";
        public const string DownloadPdf = "senior-projects.download-pdf";
        public const string Publish = "senior-projects.publish";
        public const string CompileAbstract = "senior-projects.compile-abstract";
        public const string ViewAdvised = "senior-projects.view-advised";
        public const string SubmitUtilization = "senior-projects.submit-utilization";
    }
}
