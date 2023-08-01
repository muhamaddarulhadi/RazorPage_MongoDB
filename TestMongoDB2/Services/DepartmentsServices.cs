//BUAT NI UNTUK CONNECTION SERVICES MONGODB
//HAT NI GUNA HANYA SATU TABLE JA, KNA CARI CARA GUNA CONTEXT

using TestMongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TestMongoDB.Services
{
    public class DepartmentsServices
    {
        private readonly IMongoCollection<Department> _deptCollection;

        public DepartmentsServices(
            IOptions<UUMDBsetting> UUMDBsetting)
        {
            var mongoClient = new MongoClient(
                UUMDBsetting.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                UUMDBsetting.Value.DatabaseName);

            _deptCollection = mongoDatabase.GetCollection<Department>(
                UUMDBsetting.Value.DepartmentCollectionName);
        }

        //GET DATA BY LIST
        public async Task<List<Department>> GetAsync() =>
            await _deptCollection.Find(_ => true).ToListAsync();

        //GET ONE DATA
        public async Task<Department?> GetAsync(string id) =>
            await _deptCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        //CREATE DATA
        public async Task CreateAsync(Department newDepartment) =>
            await _deptCollection.InsertOneAsync(newDepartment);

        //UPDATE DATA
        public async Task UpdateAsync(string id, Department updatedDepartment) =>
            await _deptCollection.ReplaceOneAsync(x => x.Id == id, updatedDepartment);

        //DELETE DATA
        public async Task RemoveAsync(string id) =>
            await _deptCollection.DeleteOneAsync(x => x.Id == id);
        }
}