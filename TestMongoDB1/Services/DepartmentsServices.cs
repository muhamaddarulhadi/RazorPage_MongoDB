using TestMongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TestMongoDB.Services
{
    public class DepartmentsServices
    {
       private readonly IMongoCollection<Department> _deptsCollection; 
       public DepartmentsServices(IOptions<UUMDBSettings> settings)
       {
           var client = new MongoClient(settings.Value.ConnectionString);
           var db = client.GetDatabase(settings.Value.DatabaseName);

           _deptsCollection = db.GetCollection<Department>(settings.Value.DepartmentCollectionName);


       }
       public async Task<List<Department>> GetAsync() => await _deptsCollection.Find(_=>true).ToListAsync();

          public async Task<Department?> GetAsync(string id) =>
            await _deptsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Department newDepartment) =>
            await _deptsCollection.InsertOneAsync(newDepartment);

        public async Task UpdateAsync(string id, Department updatedDepartment) =>
            await _deptsCollection.ReplaceOneAsync(x => x.Id == id, updatedDepartment); 

        public async Task RemoveAsync(string id) =>
            await _deptsCollection.DeleteOneAsync(x => x.Id == id);
    }
}