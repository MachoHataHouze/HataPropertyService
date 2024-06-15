namespace HataPropertyService.Models;

public class Property
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int OwnerId { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public byte[] Photo { get; set; }
    public string Status { get; set; } = "Available"; // Default status
    public int Rooms { get; set; } // Количество комнат
    public string PropertyType { get; set; } // Тип жилья (квартира, дом, студия и т.д.)
    public bool HasInternet { get; set; } // Наличие интернета
    public bool HasFurniture { get; set; } // Наличие мебели
    public DateTime DateAdded { get; set; } = DateTime.UtcNow; // Дата добавления объекта недвижимости
    public double Rating { get; set; } // Рейтинг объекта недвижимости
    public int Bathrooms { get; set; } // Количество ванных комнат
    public double Area { get; set; } // Площадь объекта недвижимости
}
