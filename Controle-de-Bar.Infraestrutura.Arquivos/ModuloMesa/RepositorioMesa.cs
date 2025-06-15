using Controle_de_Bar.Dominio.ModuloMesa;
using Controle_de_Bar.Infraestrutura.Arquivos.Compartilhado;

namespace Controle_de_Bar.Infraestrutura.Arquivos.ModuloMesa;
public class RepositorioMesaEmArquivo : RepositorioBaseEmArquivo<Mesa>, IRepositorioMesa
{
    public RepositorioMesaEmArquivo(ContextoDados contexto) : base(contexto) { }

    protected override List<Mesa> ObterRegistros()
    {
        return Contexto.Mesas;
    }
}

