using ParkingSystem;
using System.ComponentModel.DataAnnotations;

public class ParkingSpaceManager
{
    [Key]
    public int id { get; set; }
    public string status { get; set; }
    public string pagesa {  get; set; }
    public string kontakti {  get; set; }

    //lidhja
    public List<ParkingSpace> ParkingSpaces { get; set; }

}