using System;
using System.Collections.Generic;

namespace FinancialSystem.Models.DB.DBModels;

public partial class UserRet
{
    public int UserId { get; set; }
    public string userName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public virtual ICollection<RoleRet> Roles { get; set; } = new List<RoleRet>();
}
