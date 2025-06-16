using Controle_de_Bar.Dominio.ModuloGarcom;
using Controle_de_Bar.Infraestrutura.Arquivos.Compartilhado;
using Controle_de_Bar.Infraestrutura.Arquivos.ModuloGarcom;
using Controle_de_Bar.WebApp.Extensions;
using Controle_de_Bar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Bar.WebApp.Controllers;

[Route("garcons")]
public class GarcomController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioGarcom repositorioGarcom;

    public GarcomController()
    {
        contextoDados = new ContextoDados(true);
        repositorioGarcom = new RepositorioGarcom(contextoDados);
    }

    public IActionResult Index()
    {
        var registros = repositorioGarcom.ObterTodos();

        var visualizarVM = new VisualizarGarconsViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarGarcomViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarGarcomViewModel cadastrarVM)
    {
        var registros = repositorioGarcom.ObterTodos();

        foreach (var item in registros)
        {
            if (item.Nome.Equals(cadastrarVM.Nome))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um garçom registrado com este nome.");
                break;
            }

            if (item.Cpf.Equals(cadastrarVM.Cpf))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um garçom registrado com este CPF.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var entidade = cadastrarVM.ParaEntidade();

        repositorioGarcom.Cadastrar(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public ActionResult Editar(Guid id)
    {
        var registroSelecionado = repositorioGarcom.ObterPorId(id);

        var editarVM = new EditarGarcomViewModel(
            id,
            registroSelecionado.Nome,
            registroSelecionado.Cpf
        );

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(Guid id, EditarGarcomViewModel editarVM)
    {
        var registros = repositorioGarcom.ObterTodos();

        foreach (var item in registros)
        {
            if (!item.Id.Equals(id) && item.Nome.Equals(editarVM.Nome))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um garçom registrado com este nome.");
                break;
            }

            if (!item.Id.Equals(id) && item.Cpf.Equals(editarVM.Cpf))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um garçom registrado com este CPF.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(editarVM);

        var entidadeEditada = editarVM.ParaEntidade();

        repositorioGarcom.Editar(id, entidadeEditada);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioGarcom.ObterPorId(id);

        var excluirVM = new ExcluirGarcomViewModel(registroSelecionado.Id, registroSelecionado.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioGarcom.Excluir(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("detalhes/{id:guid}")]
    public IActionResult Detalhes(Guid id)
    {
        var registroSelecionado = repositorioGarcom.ObterPorId(id);

        var detalhesVM = new DetalhesGarcomViewModel(
            id,
            registroSelecionado.Nome,
            registroSelecionado.Cpf
        );

        return View(detalhesVM);
    }
}
