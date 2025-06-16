using System.ComponentModel.DataAnnotations;
using Controle_de_Bar.Dominio.ModuloMesa;
using Controle_de_Bar.WebApp.Extensions;

namespace Controle_de_Bar.WebApp.Models;

public abstract class FormularioMesaViewModel
{
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [Range(1, 100, ErrorMessage = "O campo \"Nome\" precisa conter um valor entre 1 e 100.")]
    public int Numero { get; set; }

    [Required(ErrorMessage = "O campo \"Capacidade\" é obrigatório.")]
    [Range(1, 100, ErrorMessage = "O campo \"Capacidade\" precisa conter um valor entre 1 e 100.")]
    public int Lugares { get; set; }
}

public class CadastrarMesaViewModel : FormularioMesaViewModel
{
    public CadastrarMesaViewModel() { }

    public CadastrarMesaViewModel(int numero, int lugares) : this()
    {
        Numero = numero;
        Lugares = lugares;
    }
}
public class EditarMesaViewModel : FormularioMesaViewModel
{
    public Guid Id { get; set; }

    public EditarMesaViewModel() { }

    public EditarMesaViewModel(Guid id, int numero, int lugares) : this()
    {
        Id = id;
        Numero = numero;
        Lugares = lugares;
    }
}

public class ExcluirMesaViewModel
{
    public Guid Id { get; set; }
    public int Numero { get; set; }

    public ExcluirMesaViewModel() { }

    public ExcluirMesaViewModel(Guid id, int numero) : this()
    {
        Id = id;
        Numero = numero;
    }
}

public class VisualizarMesasViewModel
{
    public List<DetalhesMesaViewModel> Registros { get; }

    public VisualizarMesasViewModel(List<Mesa> mesas)
    {
        Registros = [];

        foreach (var m in mesas)
        {
            var detalhesVM = m.DetalhesVM();

            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesMesaViewModel
{
    public Guid Id { get; set; }
    public int Numero { get; set; }
    public int Lugares { get; set; }

    public DetalhesMesaViewModel(Guid id, int numero, int lugares)
    {
        Id = id;
        Numero = numero;
        Lugares = lugares;
    }
}

public class SelecionarMesaViewModel
{
    public Guid Id { get; set; }
    public int Numero { get; set; }

    public SelecionarMesaViewModel(Guid id, int numero)
    {
        Id = id;
        Numero = numero;
    }
}