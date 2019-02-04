using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using SportBoard.Web.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class DeletionRequests
    {
        private readonly IDeletionRequestRepository _deletionRequestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletionRequests(IDeletionRequestRepository deletionRequestRepository, IUnitOfWork unitOfWork)
        {
            _deletionRequestRepository = deletionRequestRepository;
            _unitOfWork = unitOfWork;
        }

        public bool TryCreateRequest(DeletionRequest deletionRequest)
        {
            if(deletionRequest != null)
            {
                CreateRequest(deletionRequest);
                return true;
            }
            return false;
        }

        private void CreateRequest(DeletionRequest deletionRequest)
        {
            _unitOfWork.DeletionRequests.Add(deletionRequest);
            _unitOfWork.Complete();
        }
    }
}