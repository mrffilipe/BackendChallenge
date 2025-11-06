using BackendChallenge.API.Interfaces;
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
        public async Task AdminRegisterMotorcycle([FromBody] RegisterMotorcycleDto dto)
        {
            await _adminRegisterMotorcycle.ExecuteAsync(dto);
        }

        [HttpGet]
        public async Task<IEnumerable<MotorcycleDto>> AdminSearchesForMotorcycleByPlate([FromQuery] string placa)
        {
            return await _adminSearchesForMotorcycleByPlate.ExecuteAsync(placa);
        }

        [HttpPut("{id}")]
        public async Task AdminUpdatesMotorcyclePlate([FromRoute] string id, [FromBody] UpdateMotorcyclePlateDto dto)
        {
            await _adminUpdatesMotorcyclePlate.ExecuteAsync(id, dto);
        }

        [HttpGet("{id}")]
        public async Task<MotorcycleDto> AdminSearchesForMotorcycleById([FromRoute] string id)
        {
            return await _adminSearchesForMotorcycleById.ExecuteAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task AdminRemovesMotorcycleById([FromRoute] string id)
        {
            await _adminRemovesMotorcycleById.ExecuteAsync(id);
        }
    }
}
