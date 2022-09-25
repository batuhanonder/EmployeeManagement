namespace EmployeeManagement.Application.Validations;
public class UpdateEmployeeCommandValidator :  AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(e => e.Id).NotNull().NotEmpty();
        RuleFor(e => e.Name).MaximumLength(50).NotNull().WithMessage("The Name field cannot exceed 50 characters or entry surname.");
        RuleFor(e => e.Email).EmailAddress().NotNull();
        RuleFor(e => e.Phone).Matches(@"^((?:[0-9]\-?){6,14}[0-9])|((?:[0-9]\x20?){6,14}[0-9])$");
        RuleFor(e => e.ImageUrl).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.ImageUrl));
    }
}