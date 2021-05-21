using MedicalRecords.API.Exceptions;
using MedicalRecords.Domain.Requests.Patient;
using MedicalRecords.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace MedicalRecords.API.Filters
{
    public class PatientExistsAttribute : TypeFilterAttribute
    {
        public PatientExistsAttribute() : base(typeof(PatientExistsFilterImpl))
        {
        }

        public class PatientExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IPatientService _PatientService;

            public PatientExistsFilterImpl(IPatientService PatientService)
            {
                _PatientService = PatientService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context,
                ActionExecutionDelegate next)
            {
                if (!(context.ActionArguments["id"] is int id))
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                var result = await _PatientService.GetPatientAsync(new GetPatientRequest { Id = id });

                if (result == null)
                {
                    context.Result = new NotFoundObjectResult(
                        new JsonErrorPayload
                        {
                            DetailedMessage =
                        $"Patient with id {id} doesn\'t exist."
                        });
                    
                return;
                }

                await next();
            }
        }

    }
}
