using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DAL.Entities;

namespace Schedule.DAL.Interfaces
{
    public interface IClientManager:IDisposable
    {
        void Create(ClientProfile item);
    }
}
