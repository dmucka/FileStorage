using AutoMapper;
using FileStorageDAL;

namespace FileStorageBL.QueryObjects
{
    public class BaseQueryObject
    {
        protected readonly IMapper _mapper;
        protected readonly UnitOfWork _unitOfWork;

        public BaseQueryObject(UnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}
