using System;
using System.Collections.Generic;

namespace CompanyManagerMVC.DatabaseModels
{
    public partial class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
            RefreshTokens = new HashSet<RefreshToken>();
            TaskCreators = new HashSet<Task>();
            TaskExecutors = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateOnly RegistrationDate { get; set; }
        public int DepartmentId { get; set; }
        public string Phone { get; set; } = null!;
        public int RoleId { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual UserRole Role { get; set; } = null!;
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<Task> TaskCreators { get; set; }
        public virtual ICollection<Task> TaskExecutors { get; set; }
    }
}
