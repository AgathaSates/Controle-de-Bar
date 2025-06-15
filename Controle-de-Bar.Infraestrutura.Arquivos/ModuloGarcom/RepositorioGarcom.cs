using Controle_de_Bar.Dominio.ModuloGarcom;
using Controle_de_Bar.Infraestrutura.Arquivos.Compartilhado;

namespace Controle_de_Bar.Infraestrutura.Arquivos.ModuloGarcom;
public class RepositorioGarcom : RepositorioBaseEmArquivo<Garcom>, IRepositorioGarcom
{
    public RepositorioGarcom(ContextoDados contexto) : base(contexto) { }

    protected override List<Garcom> ObterRegistros()
    {
        return Contexto.Garcons;
    }
}

