using FluentValidation;

namespace MedicalRecords.Domain.Requests.RiskFactor.Validators
{
    public class AddRiskFactorRequestValidator : AbstractValidator<AddRiskFactorRequest>
    {        
        public AddRiskFactorRequestValidator()
        {
            RuleFor(r => r.Factor).NotEmpty()
                .WithMessage("Factor name is required.");
        }
        
    }
}
