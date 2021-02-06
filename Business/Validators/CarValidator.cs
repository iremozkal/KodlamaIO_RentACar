using FluentValidation;
using FluentValidation.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(x => x.Description).MinimumLength(2).WithMessage("The car name must be at least 2 characters.");
            RuleFor(x => x.DailyPrice).GreaterThan(0).WithMessage("Daily price of the car must be greater than 0");
        } 
    }
}
