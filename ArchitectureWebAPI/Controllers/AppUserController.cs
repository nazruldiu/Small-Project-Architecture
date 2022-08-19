using Architecture.DataService.IConfiguration;
using Architecture.Entities.DbEntities;
using ArchitectureWebAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public AppUserController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var userList = await _unitOfWork.appUser.GetAll();
            return Ok(userList);
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> UserRegistration([FromForm] AppUserViewModel appUser)
        {
            if(appUser.File != null)
            {
                appUser.FileUrl = UploadedFile(appUser.File);
            }
            var appUserEntity = new AppUser
            {
                Username = appUser.Username,
                Password = appUser.Password,
                Email = appUser.Email,
                MobileNo = appUser.MobileNo,
                FileName = appUser.FileName,
                FileUrl = appUser.FileUrl
            };
            await _unitOfWork.appUser.Add(appUserEntity);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] AppUserViewModel appUser)
        {
            if (appUser.File != null)
            {
                DeleteFile(appUser.FileUrl);
                appUser.FileUrl = UploadedFile(appUser.File);
            }
            var appUserEntity = new AppUser
            {
                Id = appUser.Id,
                Username = appUser.Username,
                Password = appUser.Password,
                Email = appUser.Email,
                MobileNo = appUser.MobileNo,
                FileName = appUser.FileName,
                FileUrl = appUser.FileUrl,
                UpdateDate = DateTime.Now
            };
            _unitOfWork.appUser.Update(appUserEntity);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _unitOfWork.appUser.Delete(id);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        private string UploadedFile(IFormFile formFile)
        {
            string uniqueFileName = String.Empty;

            if (formFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                formFile.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        private void DeleteFile(string? fileUrl)
        {
            if (fileUrl != null)
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileUrl);
                System.IO.File.Delete(filePath);
            }
        }
    }
}
