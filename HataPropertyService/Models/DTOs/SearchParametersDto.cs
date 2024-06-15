namespace HataPropertyService.Models.DTOs;

public class SearchParametersDto
{
    public string Keywords { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinRooms { get; set; }
    public int? MaxRooms { get; set; }
    public string PropertyType { get; set; }
    public bool? HasInternet { get; set; }
    public bool? HasFurniture { get; set; }
    public string SortBy { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
