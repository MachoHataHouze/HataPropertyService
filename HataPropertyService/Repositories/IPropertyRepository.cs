using System.Collections;
using HataPropertyService.Models;
using HataPropertyService.Models.DTOs;

namespace HataPropertyService.Repositories;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<Property> GetByIdAsync(Guid id);
    Task AddAsync(Property property);
    Task UpdateAsync(Property property);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Property>> GetAllPropertiesAsync();
}