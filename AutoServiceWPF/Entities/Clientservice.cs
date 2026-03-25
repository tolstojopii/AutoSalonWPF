using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Media;

namespace AutoServiceWPF.Entities;

public partial class Clientservice
{
    public int Id { get; set; }

    public int Clientid { get; set; }

    public int Serviceid { get; set; }

    public DateTime Starttime { get; set; }

    public string? Comment { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Documentbyservice> Documentbyservices { get; set; } = new List<Documentbyservice>();

    public virtual ICollection<Productsale> Productsales { get; set; } = new List<Productsale>();

    public virtual Service Service { get; set; } = null!;

    public string TotalHour
    {
        get
        {
            var timeLeft = Starttime - DateTime.Now;
            int hours = 0;
            if (timeLeft.Hours > 0)
                hours = timeLeft.Hours;
            else
                hours = 0;
            if (timeLeft.Days != 0)
                hours += 24;
            if (hours != 0)
                return $"{hours} ч.";
            else
                return "";
        }
    }

    public Brush ColorOfTime
    {
        get
        {
            var timeLeft = Starttime - DateTime.Now;
            if(timeLeft.TotalMinutes <= 60)  //Если до услуги меньше часа
                return Brushes.Red;
            else
                return Brushes.Black;
        }
    }

    public string TotalMinutes
    {
        get
        {
            var timeLeft = Starttime - DateTime.Now;
            if (timeLeft.Minutes > 0)
                return $"{timeLeft.Minutes + 1} м.";
            else if (timeLeft.Seconds > 0 && timeLeft.Seconds < 60)
                return $"1 м.";
            else
                return "";
        }
    }

    public string TotalTime
    {
        get
        {
            var results = $"{TotalHour} {TotalMinutes}";
            return results.Trim() ;
        }
    }
}
