using System;
using System.Collections.Generic;

namespace AutoServiceWPF.Entities;

public partial class Tagofclient
{
    public int Tagid { get; set; }

    public int Clientid { get; set; }

    public virtual Client Client { get; set; } = null!;
}
