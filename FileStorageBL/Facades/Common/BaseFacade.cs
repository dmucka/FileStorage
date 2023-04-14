using FileStorageDAL;

namespace FileStorageBL.Facades
{
    public abstract class BaseFacade
    {
        protected readonly UnitOfWork _unitOfWork;

        public BaseFacade(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
