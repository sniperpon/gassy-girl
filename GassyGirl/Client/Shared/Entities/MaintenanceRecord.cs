using System.ComponentModel.DataAnnotations;

public class MaintenanceRecord {
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime Date { get; set; } = System.DateTime.Now;

    [Required]
    public string CarModel { get; set; } = "Select a car";

    [Required]
    public string Item { get; set; } = "Oil change";

    [Required]
    public int Odometer { get; set; }

    public string? Notes { get; set; }
}
