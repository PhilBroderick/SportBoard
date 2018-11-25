using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class CreateImage
    {
        private readonly IImageRepository _imageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateImage(IImageRepository imageRepository, IUnitOfWork unitOfWork)
        {
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateNewImage(Image image)
        {
            _unitOfWork.Images.Add(image);
            _unitOfWork.Complete();
        }
    }
}