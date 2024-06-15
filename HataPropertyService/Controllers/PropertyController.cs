using System.Security.Claims;
using HataPropertyService.Models;
using HataPropertyService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using HataPropertyService.Models.DTOs;

namespace HataPropertyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    /// <summary>
    /// Этот класс выполняет определённые действия.
    /// </summary>
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await _propertyService.GetAllAsync();
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProperty(Guid id)
        {
            var property = await _propertyService.GetByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            return Ok(property);
        }

        /// <summary>
        /// Этот метод выполняет вычисления.
        /// </summary>
        /// <param name="value">Входное значение для вычислений.</param>
        /// <returns>Результат вычислений.</returns>
        [HttpPost]
        public async Task<IActionResult> AddProperty([FromForm] PropertyDto propertyDto)
        {
            var ownerIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ??
                               User.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            if (ownerIdClaim == null)
            {
                return Unauthorized(new { Message = "nameid claim not found" });
            }

            if (!int.TryParse(ownerIdClaim, out var ownerId))
            {
                return Unauthorized(new { Message = "Invalid nameid value" });
            }

            var property = new Property
            {
                OwnerId = ownerId,
                Address = propertyDto.Address,
                Description = propertyDto.Description,
                Price = propertyDto.Price,
                Rooms = propertyDto.Rooms,
                PropertyType = propertyDto.PropertyType,
                HasInternet = propertyDto.HasInternet,
                HasFurniture = propertyDto.HasFurniture,
                Bathrooms = propertyDto.Bathrooms,
                Area = propertyDto.Area,
                DateAdded = DateTime.UtcNow
            };

            if (propertyDto.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await propertyDto.Photo.CopyToAsync(memoryStream);
                    property.Photo = memoryStream.ToArray();
                }
            }

            await _propertyService.AddAsync(property);
            return CreatedAtAction(nameof(GetProperty), new { id = property.Id }, property);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(Guid id, [FromForm] PropertyDto propertyDto)
        {
            var existingProperty = await _propertyService.GetByIdAsync(id);
            if (existingProperty == null)
            {
                return NotFound();
            }

            existingProperty.Address = propertyDto.Address;
            existingProperty.Description = propertyDto.Description;
            existingProperty.Price = propertyDto.Price;
            existingProperty.Rooms = propertyDto.Rooms;
            existingProperty.PropertyType = propertyDto.PropertyType;
            existingProperty.HasInternet = propertyDto.HasInternet;
            existingProperty.HasFurniture = propertyDto.HasFurniture;
            existingProperty.Bathrooms = propertyDto.Bathrooms;
            existingProperty.Area = propertyDto.Area;

            if (propertyDto.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await propertyDto.Photo.CopyToAsync(memoryStream);
                    existingProperty.Photo = memoryStream.ToArray();
                }
            }

            await _propertyService.UpdateAsync(existingProperty);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            var existingProperty = await _propertyService.GetByIdAsync(id);
            if (existingProperty == null)
            {
                return NotFound();
            }

            await _propertyService.DeleteAsync(id);
            return NoContent();
        }
        
        [HttpPost("search")]
        public async Task<IActionResult> SearchProperties([FromBody] SearchParametersDto searchParameters)
        {
            var properties = await _propertyService.SearchAsync(searchParameters);
            return Ok(properties);
        }






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
    }
}
