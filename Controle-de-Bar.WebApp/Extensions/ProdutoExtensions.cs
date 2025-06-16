using Controle_de_Bar.Dominio.ModuloProduto;
using Controle_de_Bar.WebApp.Models;

namespace Controle_de_Bar.WebApp.Extensions;

public static class ProdutoExtensions
{
    public static Produto ParaEntidade(this FormularioProdutoViewModel formularioVM)
    {
        return new Produto(formularioVM.Nome, formularioVM.Preco);
    }

    public static DetalhesProdutoViewModel DetalhesVM(this Produto produto)
    {
        return new DetalhesProdutoViewModel(
                produto.Id,
                produto.Nome,
                produto.Preco
        );
    }
}
