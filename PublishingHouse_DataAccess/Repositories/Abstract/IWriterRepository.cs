using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_DataAccess.Repositories.Abstract
{
    public interface IWriterRepository : IRepository<Writer>
    {
        Writer Validate(string username,string password);
    }
}
