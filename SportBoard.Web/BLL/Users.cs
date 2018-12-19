using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class Users
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Users(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public void UpdatePreferences(UserPreferences userPreferences)
        {
            _unitOfWork.UserPreferences.Add(userPreferences);
            _unitOfWork.Complete();
        }
    }
}