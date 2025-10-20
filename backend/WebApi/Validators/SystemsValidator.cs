using FluentValidation;
using System.Text.RegularExpressions;
using WebApi.Models;

namespace WebApi.Validators
{
    public class SystemsValidator : AbstractValidator<SystemsDTO>
    {
        private enum CostCenterEnum
        {
            ABC112,
            ABC306,
            ABC909,
            ABC444
        }

        private enum StatusEnum
        {
            Ativo,
            Inativo,
            Bloqueada
        }

        private enum DatabasesEnum
        {
            SQLServer,
            Oracle,
            MySQL
        }

        private readonly string _emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public SystemsValidator()
        {
            RuleFor(x => x.CostCenter)
                .Must(x => Enum.GetNames(typeof(CostCenterEnum)).Contains(x))
                .WithMessage($"Please provide a valid Cost Center ('{string.Join("', '", Enum.GetNames(typeof(CostCenterEnum)))}')");

            RuleFor(x => x.Status)
                .Must(x => Enum.GetNames(typeof(StatusEnum)).Contains(x))
                .WithMessage($"Please provide a valid Status ('{string.Join("', '", Enum.GetNames(typeof(StatusEnum)))}')");
            
            RuleFor(x => x.Database)
                .Must(x => Enum.GetNames(typeof(DatabasesEnum)).Contains(x))
                .WithMessage($"Please provide a valid Database ('{string.Join("', '", Enum.GetNames(typeof(DatabasesEnum)))}')");

            RuleFor(x => x.EmailSupport)
                .Must(x => x.All(y => Regex.IsMatch(y, _emailRegex)))
                .WithMessage("Please provide a valid list of email addresses");

            RuleFor(x => x.ApplicationName)
                .NotEmpty()
                .WithMessage("Application Name is required");

            RuleFor(x => x.ApplicationCode)
                .NotEmpty()
                .WithMessage("Application Code is required");
            
            RuleFor(x => x.InstallationLocation)
                .NotEmpty()
                .WithMessage("Installation Location is required");
        }
    }
}
