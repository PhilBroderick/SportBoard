using SportBoard.Data.DAL;
using SportBoard.Data.DAL.DTOs;
using SportBoard.Data.DAL.Respositories;
using System.IO;
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

        public ImageDTO SavePhotoLocally(HttpRequestBase request)
        {
            if (request.Files.Count == 0)
            {
                return null;
            }

            var file = request.Files[0];
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/images/"), fileName);
            file.SaveAs(filePath);

            return new ImageDTO
            {
                FilePath = filePath,
                FileNameWithoutExtenstion = fileNameWithoutExtension
            };
        }
    }
}