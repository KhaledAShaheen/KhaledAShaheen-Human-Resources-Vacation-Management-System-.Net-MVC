using HRM.Models;

namespace HRM.Web.Models
{
    public class ListModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public UserType Type { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public Guid ManagerId { get; set; }
    }
    public class JqueryDatatableParam
    {
        public string sEcho { get; set; }
        public string sSearch { get; set; }
        public int iDisplayLength { get; set; }
        public int iDisplayStart { get; set; }
        public int iColumns { get; set; }
        public int iSortCol_0 { get; set; }
        public string sSortDir_0 { get; set; }
        public int iSortingCols { get; set; }
        public string sColumns { get; set; }
    }
}
