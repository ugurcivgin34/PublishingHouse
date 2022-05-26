using PublishingHouse_DataTransferObjects.Request.Writer;
using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Business.Abstract
{
    public interface IWriterService
    {
        Task<IList<UpdateWriterRequest>> GetWriters();
        Task<UpdateWriterRequest> GetWriter(int id);
        Task<int> AddWriter(AddWriterRequest request);
        Task UpdateWriter(UpdateWriterRequest request);
        Task DeleteWriter(int id);
        Task<bool> IsWriterExists(int id);
        Writer Validate(string username, string password);

    }
}
