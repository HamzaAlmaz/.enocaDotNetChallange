using Core.Application.DTOs.carrierConfiguration;
using Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierConfigurationController : ControllerBase
    {
        private readonly ICarrierConfigurationService _carrierConfigurationService;

        public CarrierConfigurationController(ICarrierConfigurationService carrierConfigurationService)
        {
            _carrierConfigurationService = carrierConfigurationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _carrierConfigurationService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _carrierConfigurationService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarrierConfigurationDTO dto)
        {
            var result = await _carrierConfigurationService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCarrierConfigurationDTO dto)
        {
            var result = await _carrierConfigurationService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _carrierConfigurationService.DeleteAsync(id);
            return Ok(result);
        }
    }
}