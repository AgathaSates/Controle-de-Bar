using Controle_de_Bar.Dominio.Compartilhado;
using Controle_de_Bar.Dominio.ModuloGarcom;
using Controle_de_Bar.Dominio.ModuloMesa;
using Controle_de_Bar.Dominio.ModuloProduto;

namespace Controle_de_Bar.Dominio.ModuloConta;
public class Conta : EntidadeBase<Conta>
{
    public string Titular { get; set; }
    public Mesa Mesa { get; set; }
    public Garcom Garcom { get; set; }
    public DateTime Abertura { get; set; }
    public DateTime Fechamento { get; set; }
    public bool EstaAberta { get; set; }
    public List<Pedido> Pedidos { get; set; }

    public Conta()
    {
        Pedidos = new List<Pedido>();
    }

    public Conta(string titular, Mesa mesa, Garcom garcom) : this()
    {
        Id = Guid.NewGuid();
        Titular = titular;
        Mesa = mesa;
        Garcom = garcom;

        Abrir();
    }

    public void Abrir()
    {
        EstaAberta = true;
        Abertura = DateTime.Now;

        Mesa.Ocupar();
    }

    public void Fechar()
    {
        EstaAberta = false;
        Fechamento = DateTime.Now;

        Mesa.Desocupar();
    }

    public Pedido AdicionarPedido(Produto produto, int quantidade)
    {
        var novoPedido = new Pedido(produto, quantidade);
        Pedidos.Add(novoPedido);
        return novoPedido;
    }

    public Pedido RemoverPedido(Pedido pedido)
    {
        Pedidos.Remove(pedido);
        return pedido;
    }

    public Pedido RemoverPedido(Guid idPedido)
    {
        Pedido pedidoSelecionado = null;

        foreach (var p in Pedidos)
        {
            if (p.Id == idPedido)
                pedidoSelecionado = p;
        }

        if (pedidoSelecionado == null)
            return null;

        Pedidos.Remove(pedidoSelecionado);

        return pedidoSelecionado;
    }

    public decimal CalcularTotal()
    {
        decimal valorTotal = 0;

        foreach (var p in Pedidos)
            valorTotal += p.CalcularTotal();

        return valorTotal;
    }

    public override void Atualizar(Conta registroAtualizado)
    {
        EstaAberta = registroAtualizado.EstaAberta;
        Fechamento = registroAtualizado.Fechamento;
    }
}
   

