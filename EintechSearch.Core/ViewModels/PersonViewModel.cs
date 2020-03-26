using System;

namespace EintechSearch.Core.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset Created { get; set; }
        public string GroupName { get; set; }
    }
}
