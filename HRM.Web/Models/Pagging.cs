namespace HRM.Web.Models
{
    public class Pagging
    {
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;
        public Guid? ManagerId { get; set; }

    }
}
