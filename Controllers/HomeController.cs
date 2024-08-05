using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LHPet.Models;

namespace LHPet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    ListagemClientes listaClientes = new ListagemClientes();
    ListagemFornecedores listaFornecedores = new ListagemFornecedores();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        
        var clientes = listaClientes.LerClientesDoArquivo();
        var fornecedores = listaFornecedores.LerFornecedoresDoArquivo();

        ViewBag.listaClientes = clientes;
        ViewBag.listaFornecedores = fornecedores;

        return View();
    }

    [HttpPost]
    public IActionResult AdicionarCliente(Cliente cliente) {
        
        var clientes = listaClientes.LerClientesDoArquivo();
        cliente.Id = clientes.Any() ? clientes.Max( c => c.Id) + 1 : 1;
        clientes.Add(cliente);
        listaClientes.EscreverClientesNoArquivo(clientes);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ExcluirCliente(int id) {

        var clientes = listaClientes.LerClientesDoArquivo();
        var clienteParaRemover = clientes.FirstOrDefault(c => c.Id == id);

        if( clienteParaRemover != null ) {
            clientes.Remove(clienteParaRemover);
            listaClientes.EscreverClientesNoArquivo(clientes);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult AdicionarFornecedor(Fornecedor fornecedor) {

        var fornecedores = listaFornecedores.LerFornecedoresDoArquivo();
        fornecedor.Id = fornecedores.Any() ? fornecedores.Max(f => f.Id) + 1 : 1;
        fornecedores.Add(fornecedor);
        listaFornecedores.EscreverFornecedoresNoArquivo(fornecedores);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ExcluirFornecedor(int id) {

        var fornecedores = listaFornecedores.LerFornecedoresDoArquivo();
        var fornecedorParaRemover = fornecedores.FirstOrDefault(f => f.Id == id);

        if( fornecedorParaRemover != null) {

            fornecedores.Remove(fornecedorParaRemover);
            listaFornecedores.EscreverFornecedoresNoArquivo(fornecedores);
        }

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
