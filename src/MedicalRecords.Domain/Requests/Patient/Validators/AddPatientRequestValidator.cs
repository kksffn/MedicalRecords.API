using FluentValidation;
using System;

namespace MedicalRecords.Domain.Requests.Patient.Validators
{
    public class AddPatientRequestValidator : AbstractValidator<AddPatientRequest>
    {
        public AddPatientRequestValidator()
        {
            RuleFor(p => p.PatientName).NotEmpty()
                .WithMessage("Name is required.")
                .Length(2, 50)
                .WithMessage("Name must have at least 2 and less than 50 characters.");
            RuleFor(p => p.PatientSurname).NotEmpty()
                .WithMessage("Surname is required.")
                .Length(2, 50)
                .WithMessage("Surname must have at least 2 and less than 50 characters.");
            RuleFor(p => p.DateOfBirth).NotEmpty()
                .WithMessage("Date of birth is required.")
                .Must(BeValidDate)
                .WithMessage("Not a valid date")
                .LessThan(p => DateTime.Now)
                .WithMessage("You can\'t insert someone who hasn\'t been born yet.");
            RuleFor(p => p.PhoneNumber).Matches("[1-9][0-9]{8}")
                .WithMessage("Phonenumber has to have 9 digits.");
        }

        private bool BeValidDate(DateTime arg)
        {
            return !arg.Equals(default(DateTime));
        }
    }
}
