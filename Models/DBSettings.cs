using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Models
{
    public class DBSettings: IDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public class Settings 
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }

    public interface IDBSettings 
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
