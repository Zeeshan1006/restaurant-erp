using System;
using System.Collections.Generic;

namespace ERP.UserManagement.Api.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool? IsActive { get; set; }

    public bool? TwoFactorEnabled { get; set; }

    public string? TwoFactorSecret { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<LoginHistory> LoginHistories { get; set; } = [];

    public virtual ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = [];

    public virtual ICollection<UserRole> UserRoles { get; set; } = [];
}
