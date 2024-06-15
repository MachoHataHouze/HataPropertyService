namespace HataPropertyService.Models.DTOs;

public class PropertyDto
{
    public string Address { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public IFormFile Photo { get; set; }
    public int Rooms { get; set; }
    public string PropertyType { get; set; }
    public bool HasInternet { get; set; }
    public bool HasFurniture { get; set; }
    public int Bathrooms { get; set; }
    public double Area { get; set; }
}
