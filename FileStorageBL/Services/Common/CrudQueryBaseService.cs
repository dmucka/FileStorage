using AutoMapper;
using FileStorageBL.DTOs;
using FileStorageDAL.Models;
using FileStorageDAL.Repository;
using System.Threading.Tasks;

namespace FileStorageBL.Services
{
    public abstract class CrudQueryBaseService<TModel, TCreateDto, TShowDto, TUpdateDto> : BaseService
        where TModel : BaseModel
        where TCreateDto : BaseDto
        where TShowDto : BaseDto
        where TUpdateDto : BaseDto
    {
        protected readonly Repository<TModel> Repository;

        protected CrudQueryBaseService(IMapper mapper, Repository<TModel> repository) : base(mapper)
        {
            Repository = repository;
        }

        public async Task<TModel> Create(TCreateDto modelDto)
        {
            var model = _mapper.Map<TModel>(modelDto);
            await Repository.Add(model);
            return model;
        }

        public async Task Update(TUpdateDto updateDto)
        {
            var model = await Repository.Get(updateDto.Id);
            _mapper.Map(updateDto, model);
            Repository.Update(model);
        }

        public void Delete(int entityId)
        {
            //var model = _mapper.Map<TModel>(deleteDto);
            Repository.Delete(entityId);
        }
    }
}
