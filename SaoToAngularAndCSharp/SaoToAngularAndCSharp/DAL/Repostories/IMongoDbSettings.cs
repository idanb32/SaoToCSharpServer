namespace SaoToAngularAndCSharp.DAL.Repostories
{
    public interface IMongoDbSettings
    {
        string CollectionName { get; set; }
        string CollectionName2 { get; set; }
        string ConnectionURL { get; set; }
        string DatabaseName { get; set; }
    }
}