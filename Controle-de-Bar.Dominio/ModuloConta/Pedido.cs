using Controle_de_Bar.Dominio.ModuloProduto;

namespace Controle_de_Bar.Dominio.ModuloConta;
public class Pedido
{
    public Guid Id { get; set; }
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }

    public Pedido() { }

    public Pedido(Produto produto, int quantidade) : this()
    {
        Id = Guid.NewGuid();
        Produto = produto;
        Quantidade = quantidade;
    }

    public decimal CalcularTotal()
    {
        return Produto.Preco * Quantidade;
    }
    
    public override string ToString()
    {
        return $"{Quantidade}X {Produto}";
    }
}
