//MODELS UNTUK CONNECTION STRING

namespace TestMongoDB.Services
{
    public class UUMDBsetting
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string DepartmentCollectionName { get; set; } = null!;
    }
}