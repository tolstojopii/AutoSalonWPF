using System;
using System.Collections.Generic;

namespace AutoServiceWPF.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Roleid { get; set; }

    public virtual Role Role { get; set; } = null!;
}
