using System;
using System.Collections.Generic;

namespace CompanyManagerMVC.Entities
{
    public partial class File
    {
        public File()
        {
            Posts = new HashSet<Post>();
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Location { get; set; } = null!;

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
