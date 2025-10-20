using FluentValidation;
using Models;
using System.Text.RegularExpressions;

namespace WebApi.Validators
{
    public class SystemsValidator : AbstractValidator<SystemsDTO>
    {
        private readonly string[] _costCenters = ["ABC112", "ABC306", "ABC909", "ABC444"];
        private readonly string[] _status = ["Ativo", "Inativo", "Bloqueada"];
        private readonly string[] _databases = ["SQL Server", "Oracle", "MySQL"];
        private readonly string _emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public SystemsValidator()
        {
            RuleFor(x => x.CostCenter)
                .Must(x => _costCenters.Contains(x))
                .WithMessage($"Please provide a valid Cost Center ('{string.Join("', '", _costCenters)}')");

            RuleFor(x => x.Status)
                .Must(x => _status.Contains(x))
                .WithMessage($"Please provide a valid Status ('{string.Join("', '", _status)}')");
            
            RuleFor(x => x.Database)
                .Must(x => _databases.Contains(x))
                .WithMessage($"Please provide a valid Database ('{string.Join("', '", _databases)}')");

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
