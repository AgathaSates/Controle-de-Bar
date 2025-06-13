namespace Controle_de_Bar.Dominio.Compartilhado;
public abstract class EntidadeBase<T>
{
    public Guid Id { get; set; }

    public abstract void Atualizar(T registroEditado);
}