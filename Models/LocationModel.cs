using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApiCore.Models;

public class LocationModel
{
    public int Id { get; set; }
    public string address { get; set; }
    public string city { get; set; }
    public string state { get; set; }   
    public int postalCode { get; set; }
}

public class HoursModel
{
    public int Id { get; set; }
    [ForeignKey("Location")]
    public int LocationId { get; set; }

    public LocationModel Location { get; set; }

    public string Sunday { get; set; } 
    public string Monday { get; set; }
    public string Tuesday { get; set; }
    public string Wednesday { get; set; }   
    public string Thursday { get; set; }
    public string Friday { get; set; }
    public string Saturday { get; set; }
}

public class UnitedStates
{
    public int Id { get; set; }
    public string stateCode { get; set; }
    public string stateName { get; set;}
}