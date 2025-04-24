﻿using System;
using System.Collections.Generic;

namespace ERP.UserManagement.Api.Entities;

public partial class PasswordResetToken
{
    public int TokenId { get; set; }

    public int? UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Expiry { get; set; }

    public bool? IsUsed { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
