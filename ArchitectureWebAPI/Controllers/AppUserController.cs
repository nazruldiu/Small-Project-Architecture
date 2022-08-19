using Architecture.DataService.IConfiguration;
using Architecture.Entities.DbEntities;
using ArchitectureWebAPI.Utilites;
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
        
        public AppUserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var userList = await _unitOfWork.appUser.GetAll();
            return Ok(userList);
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userList = await _unitOfWork.appUser.GetById(id);
            return Ok(userList);
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> UserRegistration([FromForm] AppUserViewModel appUser)
        {
            if(appUser.File != null)
            {
                appUser.FileUrl = FileUploadHelper.UploadedFile(appUser.File);
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
                FileUploadHelper.DeleteFile(appUser.FileUrl);
                appUser.FileUrl = FileUploadHelper.UploadedFile(appUser.File);
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
    }
}
