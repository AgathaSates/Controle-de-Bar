using System.ComponentModel.DataAnnotations;
using Controle_de_Bar.Dominio.ModuloProduto;
using Controle_de_Bar.WebApp.Extensions;

namespace Controle_de_Bar.WebApp.Models;

public class FormularioProdutoViewModel
{
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [MinLength(3, ErrorMessage = "O campo \"Nome\" precisa conter ao menos 3 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Nome\" precisa conter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo \"Preço\" é obrigatório.")]
    [DataType(DataType.Currency)]
    [Range(0, double.MaxValue,
        ErrorMessage = "O campo \"Preço\" precisa conter um valor positivo.")]
    public decimal Preco { get; set; }
}

public class CadastrarProdutoViewModel : FormularioProdutoViewModel
{
    public CadastrarProdutoViewModel() { }

    public CadastrarProdutoViewModel(string nome, decimal preco) : this()
    {
        Nome = nome;
        Preco = preco;
    }
}

public class EditarProdutoViewModel : FormularioProdutoViewModel
{
    public Guid Id { get; set; }

    public EditarProdutoViewModel() { }

    public EditarProdutoViewModel(Guid id, string nome, decimal preco) : this()
    {
        Id = id;
        Nome = nome;
        Preco = preco;
    }
}

public class ExcluirProdutoViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public ExcluirProdutoViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarProdutosViewModel
{
    public List<DetalhesProdutoViewModel> Registros { get; set; }

    public VisualizarProdutosViewModel(List<Produto> produtos)
    {
        Registros = new List<DetalhesProdutoViewModel>();

        foreach (var p in produtos)
            Registros.Add(p.DetalhesVM());
    }
}

public class DetalhesProdutoViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }

    public DetalhesProdutoViewModel(Guid id, string nome, decimal preco)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
    }
}

public class SelecionarProdutoViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public SelecionarProdutoViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}
