using HataPropertyService.Models;
using HataPropertyService.Models.DTOs;

namespace HataPropertyService.Services;

public interface IPropertyService
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<Property> GetByIdAsync(Guid id);
    Task AddAsync(Property property);
    Task UpdateAsync(Property property);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Property>> SearchAsync(SearchParametersDto searchParameters);
}
