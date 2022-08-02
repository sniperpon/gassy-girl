using System.ComponentModel.DataAnnotations;

public class Car {
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Make { get; set; } = "Make";

    [Required]
    public string Model { get; set; } = "Model";

    public string? Trim { get; set; }

    [Required]
    public int Year { get; set; } = System.DateTime.Now.Year;
}
