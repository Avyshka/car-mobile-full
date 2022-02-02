namespace Company.Project.Features.Shed
{
    public interface IUpgradeHandler
    {
        IUpgradable Upgrade(IUpgradable upgradable);
    }
}