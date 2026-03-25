using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Windows;

namespace AutoServiceWPF.Entities;

public partial class Service
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal Cost { get; set; }

    public int Durationinservice { get; set; }

    public string? Description { get; set; }

    public double Discount { get; set; }

    public byte[]? Mainimage { get; set; }

    public BitArray? Isactual { get; set; }

    public virtual ICollection<Clientservice> Clientservices { get; set; } = new List<Clientservice>();

    public string CostWithDiscount
    {
        get
        {
            if (Discount != 0)
                return string.Format("{0:NO}", (Convert.ToDouble(Cost) / 100 * Discount));
            else
                return "";
        }
    }

    public string AmountOfCost
    {
        get
        {
            if (Discount != 0)
                return $"* скидка{Discount}%";
            else
                return "";
        }
    }

    public string FullCost
    {
        get
        {
            if(Discount == 0)
                return $"{string.Format("{0:NO", Cost)} рублей за {Durationinservice / 60} минут";
            else
                return $"{CostWithDiscount} рублей за {Durationinservice / 60} минут";
        }
    }

    public Visibility IsHaveDiscount
    {
        get
        {
            if(Discount == 0)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }
    }

    public string BackColor
    {
        get
        {
            if (Discount == 0)
                return "White";
            else
                return "#FFBAFFB4";
        }
    }

    public string NameAndDuration
    {
        get
        {
            return $"{Title} ({string.Format("{0:N2}", Durationinservice / 60)}мин)";
        }
    }
}
