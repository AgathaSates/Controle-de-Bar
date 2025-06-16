using Controle_de_Bar.Dominio.ModuloMesa;
using Controle_de_Bar.Infraestrutura.Arquivos.Compartilhado;

namespace Controle_de_Bar.Infraestrutura.Arquivos.ModuloMesa;
public class RepositorioMesa : RepositorioBaseEmArquivo<Mesa>, IRepositorioMesa
{
    public RepositorioMesa(ContextoDados contexto) : base(contexto) { }

    protected override List<Mesa> ObterRegistros()
    {
        return Contexto.Mesas;
    }
}

