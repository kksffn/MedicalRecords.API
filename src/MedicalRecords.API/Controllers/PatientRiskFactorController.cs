using MedicalRecords.Domain.Requests.PatientRiskFactor;
using MedicalRecords.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRecords.API.Controllers
{
    [Route("api/patientRiskFactors")]
    [ApiController]
    public class PatientRiskFactorController : ControllerBase
    {
        private readonly IPatientRiskFactorService _patientRiskFactorService;

        public PatientRiskFactorController(IPatientRiskFactorService patientRiskFactorService)
        {
            _patientRiskFactorService = patientRiskFactorService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddPatientRiskFactorRequest request)
        {
            return Ok(await _patientRiskFactorService.AddPatientRiskFactor(request));            
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeletePatientRiskFactorRequest request)
        {
            //return Ok(await _patientRiskFactorService.DeletePatientRiskFactor(request));
            await _patientRiskFactorService.DeletePatientRiskFactor(request);
            return NoContent();
        }
    }
}
