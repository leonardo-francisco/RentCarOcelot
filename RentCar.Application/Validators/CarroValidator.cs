using FluentValidation;
using RentCar.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Validators
{
    public class CarroValidator : AbstractValidator<CarroDto>
    {
        public CarroValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Campo nome não pode ser vazio ou nulo");
            RuleFor(x => x.Modelo)
                .NotEmpty().WithMessage("Campo modelo não pode ser vazio ou nulo");
            RuleFor(x => x.Marca)
                .NotEmpty().WithMessage("Campo marca não pode ser vazio ou nulo");
            RuleFor(x => x.Ano)
                .NotEmpty().WithMessage("Campo ano não pode ser vazio ou nulo");
            RuleFor(x => x.Cor)
                .NotEmpty().WithMessage("Campo cor não pode ser vazio ou nulo");
            RuleFor(x => x.PrecoDiaria)
                .NotEmpty().WithMessage("Campo preco diaria não pode ser vazio ou nulo");
            RuleFor(x => x.Disponivel)
                .Must(x => x == false || x == true);
            RuleFor(x => x.Avariado)
                .Must(x => x == false || x == true);
               
        }
    }
}
