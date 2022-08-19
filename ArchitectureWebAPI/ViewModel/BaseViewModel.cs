namespace ArchitectureWebAPI.ViewModel
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public int Status { get; set; } = 1;
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
    }
}
