using Controle_de_Bar.Dominio.Compartilhado;

namespace Controle_de_Bar.Infraestrutura.Arquivos.Compartilhado;
public abstract class RepositorioBaseEmArquivo<T> where T : EntidadeBase<T>
{
    protected ContextoDados Contexto;
    protected List<T> registros = new List<T>();

    protected RepositorioBaseEmArquivo(ContextoDados contexto)
    {
        Contexto = contexto;

        registros = ObterRegistros();
    }

    protected abstract List<T> ObterRegistros();

    public void Cadastrar(T novoRegistro)
    {
        registros.Add(novoRegistro);

        Contexto.Salvar();
    }

    public bool Editar(Guid idRegistro, T registroEditado)
    {
        foreach (T item in registros)
        {
            if (item.Id == idRegistro)
            {
                item.Atualizar(registroEditado);

                Contexto.Salvar();

                return true;
            }
        }

        return false;
    }

    public bool Excluir(Guid idRegistro)
    {
        T registroSelecionado = ObterPorId(idRegistro);

        if (registroSelecionado != null)
        {
            registros.Remove(registroSelecionado);

            Contexto.Salvar();

            return true;
        }

        return false;
    }

    public List<T> ObterTodos()
    {
        return registros;
    }

    public T ObterPorId(Guid idRegistro)
    {
        foreach (T item in registros)
        {
            if (item.Id == idRegistro)
                return item;
        }

        return null;
    }
}