﻿using System;
using System.Collections.Generic;

namespace ERP.UserManagement.Api.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = [];

    public virtual ICollection<UserRole> UserRoles { get; set; } = [];
}
