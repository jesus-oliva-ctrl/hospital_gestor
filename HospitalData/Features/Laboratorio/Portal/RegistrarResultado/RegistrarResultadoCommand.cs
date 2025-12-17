using MediatR;
using HospitalData.Models; 

namespace HospitalData.Features.Laboratorio.Portal.RegistrarResultado
{
    public class RegistrarResultadoCommand : IRequest<Unit>
    {
        public LabResult Resultado { get; set; }

        public RegistrarResultadoCommand(LabResult resultado)
        {
            Resultado = resultado;
        }
    }
}