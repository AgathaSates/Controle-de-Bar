using Controle_de_Bar.Dominio.ModuloConta;
using Controle_de_Bar.Infraestrutura.Arquivos.Compartilhado;
using Microsoft.Win32;

namespace Controle_de_Bar.Infraestrutura.Arquivos.ModuloConta;
public class RepositorioConta : IRepositorioConta
{
    private readonly ContextoDados Contexto;
    protected readonly List<Conta> Registros;

    public RepositorioConta(ContextoDados contexto)
    {
        Contexto = contexto;
        Registros = contexto.Contas;
    }

    public void Cadastrar(Conta novaConta)
    {
        Registros.Add(novaConta);

        Contexto.Salvar();
    }

    public List<Conta> SelecionarContas()
    {
        return Registros;
    }

    public List<Conta> SelecionarContasAbertas()
    {
        var contasAbertas = new List<Conta>();

        foreach (var item in Registros)
        {
            if (item.EstaAberta)
                contasAbertas.Add(item);
        }

        return contasAbertas;
    }

    public List<Conta> SelecionarContasFechadas()
    {
        var contasFechadas = new List<Conta>();

        foreach (var item in Registros)
        {
            if (!item.EstaAberta)
                contasFechadas.Add(item);
        }

        return contasFechadas;
    }

    public List<Conta> SelecionarContasPorPeriodo(DateTime data)
    {
        var contasDoPeriodo = new List<Conta>();

        foreach (var item in Registros)
        {
            if (item.Fechamento.Date == data.Date)
                contasDoPeriodo.Add(item);
        }

        return contasDoPeriodo;
    }

    public Conta SelecionarPorId(Guid idRegistro)
    {
        foreach (var item in Registros)
        {
            if (item.Id == idRegistro)
                return item;
        }

        return null;
    }
}
