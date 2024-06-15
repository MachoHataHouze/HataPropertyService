using HataPropertyService.Models;
using HataPropertyService.Models.DTOs;
using HataPropertyService.Repositories;

namespace HataPropertyService.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await _propertyRepository.GetAllAsync();
    }

    public async Task<Property> GetByIdAsync(Guid id)
    {
        return await _propertyRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Property property)
    {
        await _propertyRepository.AddAsync(property);
    }

    public async Task UpdateAsync(Property property)
    {
        await _propertyRepository.UpdateAsync(property);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _propertyRepository.DeleteAsync(id);
    }

public async Task<IEnumerable<Property>> SearchAsync(SearchParametersDto searchParameters)
{
    var properties = await _propertyRepository.GetAllPropertiesAsync();

    // Фильтрация
    if (!string.IsNullOrEmpty(searchParameters.Keywords))
    {
        properties = properties.Where(p => p.Address.Contains(searchParameters.Keywords) ||
                                           p.Description.Contains(searchParameters.Keywords));
    }

    if (searchParameters.MinPrice.HasValue && searchParameters.MinPrice.Value > 0)
    {
        properties = properties.Where(p => p.Price >= searchParameters.MinPrice.Value);
    }

    if (searchParameters.MaxPrice.HasValue && searchParameters.MaxPrice.Value > 0)
    {
        properties = properties.Where(p => p.Price <= searchParameters.MaxPrice.Value);
    }

    if (searchParameters.MinRooms.HasValue && searchParameters.MinRooms.Value > 0)
    {
        properties = properties.Where(p => p.Rooms >= searchParameters.MinRooms.Value);
    }

    if (searchParameters.MaxRooms.HasValue && searchParameters.MaxRooms.Value > 0)
    {
        properties = properties.Where(p => p.Rooms <= searchParameters.MaxRooms.Value);
    }

    if (!string.IsNullOrEmpty(searchParameters.PropertyType))
    {
        properties = properties.Where(p => p.PropertyType == searchParameters.PropertyType);
    }

    if (searchParameters.HasInternet.HasValue && searchParameters.HasInternet.Value)
    {
        properties = properties.Where(p => p.HasInternet);
    }

    if (searchParameters.HasFurniture.HasValue && searchParameters.HasFurniture.Value)
    {
        properties = properties.Where(p => p.HasFurniture);
    }

    // Сортировка
    switch (searchParameters.SortBy)
    {
        case "price":
            properties = properties.OrderBy(p => p.Price);
            break;
        case "rating":
            // Assuming there's a Rating property
            properties = properties.OrderBy(p => p.Rating);
            break;
        case "date":
            properties = properties.OrderBy(p => p.DateAdded);
            break;
        default:
            properties = properties.OrderBy(p => p.DateAdded);
            break;
    }

    // Постраничный вывод
    properties = properties
        .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
        .Take(searchParameters.PageSize);

    return properties;
}
}