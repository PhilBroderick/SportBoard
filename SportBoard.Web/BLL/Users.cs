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
        private readonly IUserPreferenceRepository _userPreferenceRepository;

        public Users(IUserRepository userRepository, IUnitOfWork unitOfWork, IUserPreferenceRepository userPreferenceRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userPreferenceRepository = userPreferenceRepository;
        }

        public void UpdatePreferences(UserPreferences userPreferences)
        {
            _unitOfWork.UserPreferences.Add(userPreferences);
            _unitOfWork.Complete();
        }

        public void AddProfilePicture(AspNetUsers user)
        {
            _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();
        }
    }
}