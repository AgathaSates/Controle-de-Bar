using Controle_de_Bar.Dominio.ModuloMesa;
using Controle_de_Bar.Infraestrutura.Arquivos.Compartilhado;
using Controle_de_Bar.Infraestrutura.Arquivos.ModuloMesa;
using Controle_de_Bar.WebApp.Extensions;
using Controle_de_Bar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Bar.WebApp.Controllers;

[Route("mesas")]
public class MesaController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioMesa repositorioMesa;

    public MesaController()
    {
        contextoDados = new ContextoDados(true);
        repositorioMesa = new RepositorioMesa(contextoDados);
    }

    [HttpGet]
    public IActionResult Index()
    {
        var registros = repositorioMesa.ObterTodos();

        var visualizarVM = new VisualizarMesasViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarMesaViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public ActionResult Cadastrar(CadastrarMesaViewModel cadastrarVM)
    {
        var registros = repositorioMesa.ObterTodos();

        foreach (var item in registros)
        {
            if (item.Numero.Equals(cadastrarVM.Numero))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe uma mesa registrada com este número.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var entidade = cadastrarVM.ParaEntidade();

        repositorioMesa.Cadastrar(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public ActionResult Editar(Guid id)
    {
        var registroSelecionado = repositorioMesa.ObterPorId(id);

        var editarVM = new EditarMesaViewModel(
            id,
            registroSelecionado.Numero,
            registroSelecionado.Lugares
        );

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(Guid id, EditarMesaViewModel editarVM)
    {
        var registros = repositorioMesa.ObterTodos();

        foreach (var item in registros)
        {
            if (!item.Id.Equals(id) && item.Numero.Equals(editarVM.Numero))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe uma mesa registrada com este número.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(editarVM);

        var entidadeEditada = editarVM.ParaEntidade();

        repositorioMesa.Editar(id, entidadeEditada);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public ActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioMesa.ObterPorId(id);

        var excluirVM = new ExcluirMesaViewModel(registroSelecionado.Id, registroSelecionado.Numero);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public ActionResult ExcluirConfirmado(Guid id)
    {
        repositorioMesa.Excluir(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("detalhes/{id:guid}")]
    public ActionResult Detalhes(Guid id)
    {
        var registroSelecionado = repositorioMesa.ObterPorId(id);

        var detalhesVM = new DetalhesMesaViewModel(
            id,
            registroSelecionado.Numero,
            registroSelecionado.Lugares
        );

        return View(detalhesVM);
    }
}