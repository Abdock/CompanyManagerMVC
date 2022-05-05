using System;
using System.Collections.Generic;

namespace CompanyManagerMVC.DatabaseModels
{
    public partial class Post
    {
        public Post()
        {
            Files = new HashSet<File>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int CreatorId { get; set; }
        public DateOnly CreationDate { get; set; }
        public int DepartmentId { get; set; }

        public virtual User Creator { get; set; } = null!;
        public virtual Department Department { get; set; } = null!;

        public virtual ICollection<File> Files { get; set; }
    }
}
