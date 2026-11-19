namespace Chula.Domain.Common;

public interface IHasActiveFlag
{
    bool IsActive { get; }

    void Activate();

    void Deactivate();
}
