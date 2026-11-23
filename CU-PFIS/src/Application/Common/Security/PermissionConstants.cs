namespace Chula.Application.Common.Security;

public static class PermissionConstants
{
    public static class ReferenceData
    {
        public const string View = "reference-data.view";
        public const string Manage = "reference-data.manage";
    }

    public static class Faculty
    {
        public const string ViewOwn = "faculty.view-own";
        public const string EditOwn = "faculty.edit-own";
        public const string ExportOwn = "faculty.export-own";
        public const string ViewAll = "faculty.view-all";
        public const string EditAll = "faculty.edit-all";
        public const string ExportAll = "faculty.export-all";
        public const string NotifyUpdate = "faculty.notify-update";
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

    public static class TrainingBudgetPolicies
    {
        public const string Manage = "training-budget-policies.manage";
    }

    public static class Students
    {
        public const string View = "students.view";
        public const string Edit = "students.edit";
        public const string Export = "students.export";
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
