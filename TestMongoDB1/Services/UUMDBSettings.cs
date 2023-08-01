using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMongoDB.Services
{
    public class UUMDBSettings
    {
        public string? ConnectionString {get; set; }

        public string? DatabaseName {get; set; }
        public string? DepartmentCollectionName {get; set; }
    }
}