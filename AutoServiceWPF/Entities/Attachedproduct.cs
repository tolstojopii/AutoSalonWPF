namespace AutoServiceWPF.Entities;

public partial class Attachedproduct
{
    public int Attachedproductid { get; set; }

    public int Mainproductid { get; set; }

    public virtual Product Mainproduct { get; set; } = null!;
}