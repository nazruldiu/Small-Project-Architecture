namespace ArchitectureWebAPI.ViewModel
{
    public class AppUserViewModel : BaseViewModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public IFormFile? File { get; set; }
        public string? FileName { get; set; }
        public string? FileUrl { get; set; }
    }
}
