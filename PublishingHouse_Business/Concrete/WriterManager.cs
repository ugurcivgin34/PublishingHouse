using AutoMapper;
using PublishingHouse_Business.Abstract;
using PublishingHouse_DataAccess.Repositories.Abstract;
using PublishingHouse_DataTransferObjects.Request.Writer;
using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Business.Concrete
{
    public class WriterManager : IWriterService
    {
        private readonly IWriterRepository _writerRepository;
        private readonly IMapper mapper;
        private List<Writer> categories;

        public WriterManager(IWriterRepository WriterRepository, IMapper mapper)
        {
            _writerRepository = WriterRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddWriter(AddWriterRequest request)
        {
            var writer = mapper.Map<Writer>(request);
            await _writerRepository.Add(writer);
            return writer.Id;
        }

        public async Task DeleteWriter(int id)
        {
            await _writerRepository.Delete(id);
        }

        public async Task<AddWriterRequest> GetWriter(int id)
        {

            var writer = await _writerRepository.GetById(id);
            var writerDisplayResponse = mapper.Map<AddWriterRequest>(writer);
            return writerDisplayResponse;
        }

        public async Task<IList<AddWriterRequest>> GetWriters()
        {
            var writers = await _writerRepository.GetAll();
            var result = mapper.Map<IList<AddWriterRequest>>(writers);
            return result;
        }



        public async Task<bool> IsWriterExists(int id)
        {
            return await _writerRepository.IsExists(id);

        }

        public async Task UpdateWriter(UpdateWriterRequest request)
        {
            var writer = mapper.Map<Writer>(request);
            await _writerRepository.Update(writer);
        }
    }
}
