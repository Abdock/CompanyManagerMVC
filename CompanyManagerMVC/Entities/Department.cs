using System;
using System.Collections.Generic;

namespace CompanyManagerMVC.Entities
{
    public partial class Department
    {
        public Department()
        {
            Posts = new HashSet<Post>();
            Tasks = new HashSet<Task>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
