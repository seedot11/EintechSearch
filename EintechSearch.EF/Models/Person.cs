using System;
using System.Collections.Generic;

namespace EintechSearch.Entity.Models
{
    public partial class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset Created { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
