using System;
using System.Collections.Generic;

namespace AutoServiceWPF.Entities;

public partial class Servicephoto
{
    public int Id { get; set; }

    public int Serviceid { get; set; }

    public string Photopath { get; set; } = null!;
}
