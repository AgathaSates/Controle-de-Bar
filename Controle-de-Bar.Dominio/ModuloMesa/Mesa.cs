using Controle_de_Bar.Dominio.Compartilhado;

namespace Controle_de_Bar.Dominio.ModuloMesa;
public class Mesa : EntidadeBase<Mesa>
{
    public int Numero { get; set; }
    public int Lugares { get; set; }
    public bool EstaOcupada { get; set; }

    public Mesa() { }

    public Mesa(int numero, int lugares) : this()
    {
        Numero = numero;
        Lugares = lugares;
        EstaOcupada = false;
    }

    public override void Atualizar(Mesa registroEditado)
    {
        Numero = registroEditado.Numero;
        Lugares = registroEditado.Lugares;
    }

    public void Ocupar()
    {
        EstaOcupada = true;
    }

    public void Desocupar()
    {
        EstaOcupada = false;
    }
}