using ParkingSystem;
using System.ComponentModel.DataAnnotations;

public class AvailabilityMonitor
{
    [Key]
    public int MonitorId { get; set; }          
    public string Status { get; set; }         
    public DateTime LastCheckedTime { get; set; }  
    public TimeSpan Uptime { get; set; }        
    public TimeSpan Downtime { get; set; }     
    public TimeSpan CheckInterval { get; set; }

    //lidhja
    public List<ParkingSpaceManager> ParkingSpaceManagers { get; set; }
    public List<ParkingSpace> ParkingSpaces { get; set; }
}
