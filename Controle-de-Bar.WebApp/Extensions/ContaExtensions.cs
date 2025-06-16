using Controle_de_Bar.Dominio.ModuloConta;
using Controle_de_Bar.Dominio.ModuloGarcom;
using Controle_de_Bar.Dominio.ModuloMesa;
using Controle_de_Bar.WebApp.Models;

namespace Controle_de_Bar.WebApp.Extensions;

public static class ContaExtensions
{
    public static Conta ParaEntidade(this AbrirContaViewModel abrirVM, List<Mesa> mesas, List<Garcom> garcons)
    {
        Mesa? mesaSelecionada = null;

        foreach (var mesa in mesas)
        {
            if (mesa.Id == abrirVM.MesaId)
                mesaSelecionada = mesa;
        }

        Garcom? garcomSelecionado = null;

        foreach (var garcom in garcons)
        {
            if (garcom.Id == abrirVM.GarcomId)
                garcomSelecionado = garcom;
        }

        return new Conta(abrirVM.Titular, mesaSelecionada, garcomSelecionado);
    }

    public static DetalhesContaViewModel DetalhesVM(this Conta conta)
    {
        return new DetalhesContaViewModel(
                conta.Id,
                conta.Titular,
                conta.Mesa.Numero,
                conta.Garcom.Nome,
                conta.EstaAberta,
                conta.CalcularTotal(),
                conta.Pedidos
        );
    }
}
