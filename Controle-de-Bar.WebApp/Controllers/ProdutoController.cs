using Controle_de_Bar.Dominio.ModuloProduto;
using Controle_de_Bar.Infraestrutura.Arquivos.Compartilhado;
using Controle_de_Bar.Infraestrutura.Arquivos.ModuloProduto;
using Controle_de_Bar.WebApp.Extensions;
using Controle_de_Bar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Bar.WebApp.Controllers;

[Route("produtos")]
public class ProdutoController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioProduto repositorioProduto;

    public ProdutoController()
    {
        contextoDados = new ContextoDados(true);
        repositorioProduto = new RepositorioProduto(contextoDados);
    }

    [HttpGet]
    public IActionResult Index()
    {
        var registros = repositorioProduto.ObterTodos();

        var visualizarVM = new VisualizarProdutosViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarProdutoViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarProdutoViewModel cadastrarVM)
    {
        var registros = repositorioProduto.ObterTodos();

        foreach (var item in registros)
        {
            if (item.Nome.Equals(cadastrarVM.Nome))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um produto registrado com este nome.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var entidade = cadastrarVM.ParaEntidade();

        repositorioProduto.Cadastrar(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        var registroSelecionado = repositorioProduto.ObterPorId(id);

        var editarVM = new EditarProdutoViewModel(
            id,
            registroSelecionado.Nome,
            registroSelecionado.Preco
        );

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarProdutoViewModel editarVM)
    {
        var registros = repositorioProduto.ObterTodos();

        foreach (var item in registros)
        {
            if (!item.Id.Equals(id) && item.Nome.Equals(editarVM.Nome))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um produto registrado com este nome.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(editarVM);

        var entidadeEditada = editarVM.ParaEntidade();

        repositorioProduto.Editar(id, entidadeEditada);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioProduto.ObterPorId(id);

        var excluirVM = new ExcluirProdutoViewModel(registroSelecionado.Id, registroSelecionado.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioProduto.Excluir(id);

        return RedirectToAction(nameof(Index));
    }
}
