﻿using System;
using System.Collections.Generic;

namespace ERP.UserManagement.Api.Entities;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = [];
}
