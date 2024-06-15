using HataPropertyService.Data;
using HataPropertyService.Models;
using HataPropertyService.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HataPropertyService.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly PropertyContext _context;

    public PropertyRepository(PropertyContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await _context.Properties.ToListAsync();
    }

    public async Task<Property> GetByIdAsync(Guid id)
    {
        return await _context.Properties.FindAsync(id);
    }

    public async Task AddAsync(Property property)
    {
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Property property)
    {
        _context.Entry(property).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property != null)
        {
            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
    {
        return await _context.Properties.ToListAsync();
    }





}