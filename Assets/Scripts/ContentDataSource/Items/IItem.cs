namespace Company.Project.Features.Items
{
    public interface IItem
    {
        int Id { get; }
        ItemInfo Info { get; }
    }
}