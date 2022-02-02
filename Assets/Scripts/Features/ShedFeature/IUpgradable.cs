namespace Company.Project.Features.Shed
{
    public interface IUpgradable
    {
        void Restore();
        float Speed { get; set; }
    }
}