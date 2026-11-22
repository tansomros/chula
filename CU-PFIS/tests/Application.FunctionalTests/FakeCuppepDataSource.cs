using Chula.Application.Common.Interfaces;

namespace Chula.Application.FunctionalTests;

public sealed class FakeCuppepDataSource : ICuppepDataSource
{
    public List<CuppepStudentSnapshot> NextStudents { get; } = new();

    public CuppepHealthStatus NextHealth { get; set; } = new(true, "https://fake-cuppep.local", "Healthy");

    public Exception? ExceptionToThrow { get; set; }

    public Task<CuppepHealthStatus> CheckHealthAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(NextHealth);
    }

    public Task<IReadOnlyList<CuppepStudentSnapshot>> PullStudentsAsync(
        DateTimeOffset? updatedSince,
        int? maxRecords = null,
        CancellationToken cancellationToken = default)
    {
        if (ExceptionToThrow is not null)
        {
            throw ExceptionToThrow;
        }

        IEnumerable<CuppepStudentSnapshot> query = NextStudents;
        if (updatedSince.HasValue)
        {
            query = query.Where(x => x.UpdatedAt >= updatedSince.Value);
        }

        if (maxRecords.HasValue)
        {
            query = query.Take(maxRecords.Value);
        }

        return Task.FromResult<IReadOnlyList<CuppepStudentSnapshot>>(query.ToList());
    }

    public void Reset()
    {
        NextStudents.Clear();
        ExceptionToThrow = null;
        NextHealth = new CuppepHealthStatus(true, "https://fake-cuppep.local", "Healthy");
    }
}
