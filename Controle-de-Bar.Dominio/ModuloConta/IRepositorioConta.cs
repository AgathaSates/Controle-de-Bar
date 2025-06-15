namespace Controle_de_Bar.Dominio.ModuloConta;
public interface IRepositorioConta
{
    void Cadastrar(Conta conta);
    Conta SelecionarPorId(Guid idRegistro);
    List<Conta> SelecionarContas();
    List<Conta> SelecionarContasAbertas();
    List<Conta> SelecionarContasFechadas();
    List<Conta> SelecionarContasPorPeriodo(DateTime data);
}
