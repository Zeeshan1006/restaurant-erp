using System;
using System.Collections.Generic;

namespace ERP.UserManagement.Api.Entities;

public partial class LoginHistory
{
    public int LoginId { get; set; }

    public int? UserId { get; set; }

    public DateTime? LoginTime { get; set; }

    public string? Ipaddress { get; set; }

    public bool? Success { get; set; }

    public virtual User? User { get; set; }
}
