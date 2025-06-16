using Controle_de_Bar.Dominio.ModuloConta;
using Controle_de_Bar.Dominio.ModuloGarcom;
using Controle_de_Bar.Dominio.ModuloMesa;
using Controle_de_Bar.Dominio.ModuloProduto;
using Controle_de_Bar.Infraestrutura.Arquivos.Compartilhado;
using Controle_de_Bar.Infraestrutura.Arquivos.ModuloConta;
using Controle_de_Bar.Infraestrutura.Arquivos.ModuloGarcom;
using Controle_de_Bar.Infraestrutura.Arquivos.ModuloMesa;
using Controle_de_Bar.Infraestrutura.Arquivos.ModuloProduto;
using Controle_de_Bar.WebApp.Extensions;
using Controle_de_Bar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Bar.WebApp.Controllers;

[Route("contas")]
public class ContaController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioConta repositorioConta;
    private readonly IRepositorioMesa repositorioMesa;
    private readonly IRepositorioGarcom repositorioGarcom;
    private readonly IRepositorioProduto repositorioProduto;

    public ContaController()
    {
        contextoDados = new ContextoDados(true);
        repositorioConta = new RepositorioConta(contextoDados);
        repositorioMesa = new RepositorioMesa(contextoDados);
        repositorioGarcom = new RepositorioGarcom(contextoDados);
        repositorioProduto = new RepositorioProduto(contextoDados);
    }

    [HttpGet]
    public IActionResult Index(string status)
    {
        List<Conta> registros;

        switch (status)
        {
            case "abertas": registros = repositorioConta.SelecionarContasAbertas(); break;
            case "fechadas": registros = repositorioConta.SelecionarContasFechadas(); break;
            default: registros = repositorioConta.SelecionarContas(); break;
        }

        var visualizarVM = new VisualizarContasViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("abrir")]
    public IActionResult Abrir()
    {
        var mesas = repositorioMesa.ObterTodos();
        var garcons = repositorioGarcom.ObterTodos();

        var abrirVM = new AbrirContaViewModel(mesas, garcons);

        return View(abrirVM);
    }

    [HttpPost("abrir")]
    [ValidateAntiForgeryToken]
    public IActionResult Abrir(AbrirContaViewModel abrirVM)
    {
        var registros = repositorioConta.SelecionarContas();

        foreach (var conta in registros)
        {
            if (conta.Titular.Equals(abrirVM.Titular) && conta.EstaAberta)
            {
                ModelState.AddModelError("CadastroUnico", "Já existe uma conta aberta para este titular.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(abrirVM);

        var mesas = repositorioMesa.ObterTodos();
        var garcons = repositorioGarcom.ObterTodos();

        var entidade = abrirVM.ParaEntidade(mesas, garcons);

        repositorioConta.Cadastrar(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet, Route("/contas/{id:guid}/fechar")]
    public IActionResult Fechar(Guid id)
    {
        var registro = repositorioConta.SelecionarPorId(id);

        var fecharContaVM = new FecharContaViewModel(
            registro.Id,
            registro.Titular,
            registro.Mesa.Numero,
            registro.Garcom.Nome,
            registro.CalcularTotal()
        );

        return View(fecharContaVM);
    }

    [HttpPost, Route("/contas/{id:guid}/fechar")]
    public IActionResult FecharConfirmado(Guid id)
    {
        var registroSelecionado = repositorioConta.SelecionarPorId(id);

        registroSelecionado.Fechar();

        contextoDados.Salvar();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet, Route("/contas/{id:guid}/gerenciar-pedidos")]
    public IActionResult GerenciarPedidos(Guid id)
    {
        var contaSelecionada = repositorioConta.SelecionarPorId(id);
        var produtos = repositorioProduto.ObterTodos();

        var gerenciarPedidosVm = new GerenciarPedidosViewModel(contaSelecionada, produtos);

        return View(gerenciarPedidosVm);
    }

    [HttpPost, Route("/contas/{id:guid}/adicionar-pedido")]
    public IActionResult AdicionarPedido(Guid id, AdicionarPedidoViewModel adicionarPedidoVm)
    {
        var contaSelecionada = repositorioConta.SelecionarPorId(id);
        var produtoSelecionado = repositorioProduto.ObterPorId(adicionarPedidoVm.IdProduto);

        contaSelecionada.AdicionarPedido(
            produtoSelecionado,
            adicionarPedidoVm.QuantidadeSolicitada
        );

        contextoDados.Salvar();

        var produtos = repositorioProduto.ObterTodos();

        var gerenciarPedidosVm = new GerenciarPedidosViewModel(contaSelecionada, produtos);

        return View("GerenciarPedidos", gerenciarPedidosVm);
    }

    [HttpPost, Route("/contas/{id:guid}/remover-pedido/{idPedido:guid}")]
    public IActionResult RemoverPedido(Guid id, Guid idPedido)
    {
        var contaSelecionada = repositorioConta.SelecionarPorId(id);

        var pedidoRemovido = contaSelecionada.RemoverPedido(idPedido);

        contextoDados.Salvar();

        var produtos = repositorioProduto.ObterTodos();

        var gerenciarPedidosVm = new GerenciarPedidosViewModel(contaSelecionada, produtos);

        return View("GerenciarPedidos", gerenciarPedidosVm);
    }

    [HttpGet("faturamento")]
    public IActionResult Faturamento(DateTime? data)
    {
        if (!data.HasValue)
            return View();

        var registros = repositorioConta.SelecionarContasPorPeriodo(data.GetValueOrDefault());

        var faturamentoVM = new FaturamentoViewModel(registros);

        return View(faturamentoVM);
    }
}
