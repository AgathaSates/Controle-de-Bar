using Controle_de_Bar.Dominio.ModuloGarcom;
using Controle_de_Bar.WebApp.Models;

namespace Controle_de_Bar.WebApp.Extensions;

public static class GarcomExtensions
{
    public static Garcom ParaEntidade(this FormularioGarcomViewModel formularioVM)
    {
        return new Garcom(formularioVM.Nome, formularioVM.Cpf);
    }

    public static DetalhesGarcomViewModel DetalhesVM(this Garcom garcom)
    {
        return new DetalhesGarcomViewModel(
                garcom.Id,
                garcom.Nome,
                garcom.Cpf
        );
    }
}
