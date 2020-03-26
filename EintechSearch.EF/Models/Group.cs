using System;
using System.Collections.Generic;

namespace EintechSearch.Entity.Models
{
    public partial class Group
    {
        public Group()
        {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
