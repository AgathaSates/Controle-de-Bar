using Controle_de_Bar.Dominio.ModuloMesa;
using Controle_de_Bar.WebApp.Models;

namespace Controle_de_Bar.WebApp.Extensions;

public static class MesaExtensions
{
    public static Mesa ParaEntidade(this FormularioMesaViewModel formularioVM)
    {
        return new Mesa(formularioVM.Numero, formularioVM.Lugares);
    }

    public static DetalhesMesaViewModel DetalhesVM(this Mesa mesa)
    {
        return new DetalhesMesaViewModel(
                mesa.Id,
                mesa.Numero,
                mesa.Lugares
        );
    }
}
