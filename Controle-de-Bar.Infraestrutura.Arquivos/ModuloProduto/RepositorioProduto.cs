using Controle_de_Bar.Dominio.ModuloProduto;
using Controle_de_Bar.Infraestrutura.Arquivos.Compartilhado;

namespace Controle_de_Bar.Infraestrutura.Arquivos.ModuloProduto;
public class RepositorioProduto : RepositorioBaseEmArquivo<Produto>, IRepositorioProduto
{
    public RepositorioProduto(ContextoDados contexto) : base(contexto) { }
    protected override List<Produto> ObterRegistros()
    {
        return Contexto.Produtos;
    }
}
