using BackendChallenge.API.Interfaces;
using BackendChallenge.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BackendChallenge.API.Controllers
{
    [Route("entregadores")]
    public class DeliveryPersonController : BaseController
    {
        private readonly IRegisterDeliveryPerson _registerDeliveryPerson;
        private readonly IUpdateYourDriversLicensePhoto _updateYourDriversLicensePhoto;

        public DeliveryPersonController(IRegisterDeliveryPerson registerDeliveryPerson, IUpdateYourDriversLicensePhoto updateYourDriversLicensePhoto)
        {
            _registerDeliveryPerson = registerDeliveryPerson;
            _updateYourDriversLicensePhoto = updateYourDriversLicensePhoto;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDeliveryPerson([FromBody] RegisterDeliveryPersonDto dto)
        {
            await _registerDeliveryPerson.ExecuteAsync(dto);
            return Created();
        }

        [HttpPost("{id}/cnh")]
        public async Task<IActionResult> UpdateYourDriversLicensePhoto([FromRoute] string id, [FromBody] UpdateDriversLicensePhotoDto dto)
        {
            await _updateYourDriversLicensePhoto.ExecuteAsync(id, dto);
            return Created();
        }
    }
}
