using System;
using System.Collections.Generic;

namespace CompanyManagerMVC.DatabaseModels
{
    public partial class Task
    {
        public Task()
        {
            Files = new HashSet<File>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int StatusId { get; set; }
        public int CreatorId { get; set; }
        public int ExecutorId { get; set; }
        public int DepartmentId { get; set; }

        public virtual User Creator { get; set; } = null!;
        public virtual Department Department { get; set; } = null!;
        public virtual User Executor { get; set; } = null!;
        public virtual TaskStatus Status { get; set; } = null!;

        public virtual ICollection<File> Files { get; set; }
    }
}
