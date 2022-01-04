using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFMC.Service.Common.Constants;
using EFMC.Service.Interfaces;
using EFMC.Service.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFMC.API.Controllers
{
    [ApiController]
    [Route("api/v1/pharmacies")]
    public class PharmaciesController : ControllerBase
    {
        private readonly IPharmacyService pharmacyService;
        private readonly IConsignmentService consignmentService;
        public PharmaciesController(IPharmacyService pharmacyService, IConsignmentService consignmentService)
        {
            this.pharmacyService = pharmacyService;
            this.consignmentService = consignmentService;
        }
        [HttpPost]
        public IActionResult CreatePharmacy(PharmacyCreationModel pharmacyCreationModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(pharmacyCreationModel);

            var result = pharmacyService.CreatePharmacy(pharmacyCreationModel);
            if (result.Success == ResultConstant.SUCCESS)
                return StatusCode(201, result);
            else
            {
                if (result.Client == ResultConstant.CLIENT)
                    return BadRequest(result);
            }

            return StatusCode(500, result);
        }
        [HttpPut("{pharmacyId}")]
        public IActionResult UpdatePharmacy(int pharmacyId, PharmacyUpdateModel pharmacyUpdateModel)
        {
            var result = pharmacyService.UpdatePharmacy(pharmacyId, pharmacyUpdateModel);

            if (result.Success == ResultConstant.SUCCESS)
                return Ok(result);
            else
            {
                if (result.Client == ResultConstant.CLIENT)
                    return BadRequest(result);
            }

            return StatusCode(500, result);
        }
        [HttpGet("{ownerId}")]
#nullable enable
        public IActionResult GetPharmacies(int ownerId, [FromQuery] string? status)
        {
            var result = pharmacyService.GetPharmacies(ownerId, status);
            if (result.Success)
                return Ok(result);
            else
            {
                if (result.Client)
                    return BadRequest(result);
            }
            return StatusCode(500, result);
        }
        [HttpPost("/api/v1/pharmacies/{pharmacyId}/consignments")]
        public IActionResult ImportConsignment(int pharmacyId, ConsignmentImported consignmentImported)
        {
            var result = consignmentService.ImportConsignment(pharmacyId, consignmentImported);
            if (result.Success)
                return StatusCode(201, result);
            else
            {
                if (result.Client == ResultConstant.CLIENT)
                    return BadRequest(result);
            }
            return StatusCode(500, result);
        }
        [HttpGet("/api/v1/pharmacies/{pharmacyId}/consignments")]
        public IActionResult GetConsignments(
            int pharmacyId,
            [FromQuery] DateTime? specificDate,
            [FromQuery] int? year,
            [FromQuery] int? month,
            [FromQuery] int? day)
        {
            var result = consignmentService.GetConsignments(
                pharmacyId,
                specificDate,
                year,
                month,
                day);
            if (result.Success)
                return Ok(result);
            else
            {
                if (result.Client)
                {
                    return BadRequest(result);
                }
            }
            return StatusCode(500, result);
        }
        [HttpPost("/api/v1/pharmacies/{pharmacyId}/industries")]
        public IActionResult CreateIndustries(int pharmacyId, [FromBody] List<string> industries)
        {
            var result = pharmacyService.CreateIndustry(pharmacyId, industries);
            if (result.Success)
                return StatusCode(201, result);
            else
            {
                if (result.Client == ResultConstant.CLIENT)
                    return BadRequest(result);
            }
            return StatusCode(500, result);
        }
        [HttpGet("/api/v1/pharmacies/{pharmacyId}/industries")]
        public IActionResult GetIndustries(int pharmacyId)
        {
            var result = pharmacyService.GetIndustries(pharmacyId);
            if (result.Success)
                return Ok(result);
            else
            {
                if (result.Client == ResultConstant.CLIENT)
                    return BadRequest(result);
            }
            return StatusCode(500, result);
        }
    }
}
