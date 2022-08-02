using System.ComponentModel.DataAnnotations;

public class MileageRecord {
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime Date { get; set; } = System.DateTime.Now;

    [Required]
    public string CarModel { get; set; } = "Select a car";

    [Required]
    public Double TripOdometer { get; set; }

    [Required]
    public Double Gallons { get; set; }

    [Required]
    public Double PricePerGallon { get; set; }

    [Required]
    public int Odometer { get; set; }
}
