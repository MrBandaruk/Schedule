using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DAL.EF;
using Schedule.DAL.Entities;
using Schedule.DAL.Interfaces;

namespace Schedule.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager (ApplicationContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {           
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
