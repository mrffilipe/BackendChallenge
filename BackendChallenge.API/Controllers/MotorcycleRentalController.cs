using BackendChallenge.API.Interfaces;
using BackendChallenge.Application.Common;
using BackendChallenge.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BackendChallenge.API.Controllers
{
    [Route("locacao")]
    public class MotorcycleRentalController : BaseController
    {
        private readonly IRegisterMotorcycleRental _registerMotorcycleRental;
        private readonly ISearcheForMotorcycleRentalById _searcheForMotorcycleRentalById;
        private readonly IUpdateReturnDate _updateReturnDate;

        public MotorcycleRentalController(
            IRegisterMotorcycleRental registerMotorcycleRental,
            ISearcheForMotorcycleRentalById searcheForMotorcycleRentalById,
            IUpdateReturnDate updateReturnDate)
        {
            _registerMotorcycleRental = registerMotorcycleRental;
            _searcheForMotorcycleRentalById = searcheForMotorcycleRentalById;
            _updateReturnDate = updateReturnDate;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterMotorcycleRental([FromBody] RegisterMotorcycleRentalDto dto)
        {
            await _registerMotorcycleRental.ExecuteAsync(dto);
            return Created();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MotorcycleRentalDto>> SearcheForMotorcycleRentalById([FromRoute] string id)
        {
            var result = await _searcheForMotorcycleRentalById.ExecuteAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}/devolucao")]
        public async Task<ActionResult<ResponseToTheRequest>> UpdateReturnDate([FromRoute] string id, [FromBody] UpdateReturnDateDto dto)
        {
            var result = await _updateReturnDate.ExecuteAsync(id, dto);
            return Ok(result);
        }
    }
}
