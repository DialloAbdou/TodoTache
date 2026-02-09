using FluentValidation;
using TodoList.Dtos;

namespace TodoList.Validations
{
    public class TacheValidation : AbstractValidator<TacheImput>
    {
        public TacheValidation()
        {
            RuleFor(t => t.Titre).NotEmpty();
        }

    }
}
