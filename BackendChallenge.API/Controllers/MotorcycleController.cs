using BackendChallenge.API.Interfaces;
using BackendChallenge.Application.Common;
using BackendChallenge.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BackendChallenge.API.Controllers
{
    [Route("motos")]
    public class MotorcycleController : BaseController
    {
        private readonly IAdminRegisterMotorcycle _adminRegisterMotorcycle;
        private readonly IAdminSearchesForMotorcycleByPlate _adminSearchesForMotorcycleByPlate;
        private readonly IAdminUpdatesMotorcyclePlate _adminUpdatesMotorcyclePlate;
        private readonly IAdminSearchesForMotorcycleById _adminSearchesForMotorcycleById;
        private readonly IAdminRemovesMotorcycleById _adminRemovesMotorcycleById;

        public MotorcycleController(
            IAdminRegisterMotorcycle adminRegisterMotorcycle,
            IAdminSearchesForMotorcycleByPlate adminSearchesForMotorcycleByPlate,
            IAdminUpdatesMotorcyclePlate adminUpdatesMotorcyclePlate,
            IAdminSearchesForMotorcycleById adminSearchesForMotorcycleById,
            IAdminRemovesMotorcycleById adminRemovesMotorcycleById)
        {
            _adminRegisterMotorcycle = adminRegisterMotorcycle;
            _adminSearchesForMotorcycleByPlate = adminSearchesForMotorcycleByPlate;
            _adminUpdatesMotorcyclePlate = adminUpdatesMotorcyclePlate;
            _adminSearchesForMotorcycleById = adminSearchesForMotorcycleById;
            _adminRemovesMotorcycleById = adminRemovesMotorcycleById;
        }

        [HttpPost]
        public async Task<IActionResult> AdminRegisterMotorcycle([FromBody] RegisterMotorcycleDto dto)
        {
            await _adminRegisterMotorcycle.ExecuteAsync(dto);
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotorcycleDto>>> AdminSearchesForMotorcycleByPlate([FromQuery] string placa)
        {
            var result = await _adminSearchesForMotorcycleByPlate.ExecuteAsync(placa);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseToTheRequest>> AdminUpdatesMotorcyclePlate([FromRoute] string id, [FromBody] UpdateMotorcyclePlateDto dto)
        {
            var result = await _adminUpdatesMotorcyclePlate.ExecuteAsync(id, dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<MotorcycleDto>>> AdminSearchesForMotorcycleById([FromRoute] string id)
        {
            var result = await _adminSearchesForMotorcycleById.ExecuteAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> AdminRemovesMotorcycleById([FromRoute] string id)
        {
            await _adminRemovesMotorcycleById.ExecuteAsync(id);
            return Ok();
        }
    }
}
