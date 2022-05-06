using System;
using System.Collections.Generic;

namespace CompanyManagerMVC.Entities
{
    public partial class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public DateOnly CreationDate { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public string CreationIp { get; set; } = null!;
        public string RevokeIp { get; set; } = null!;
        public bool? IsActive { get; set; }
        public int OwnerId { get; set; }

        public virtual User Owner { get; set; } = null!;
    }
}
