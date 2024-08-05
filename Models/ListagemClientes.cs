
namespace LHPet.Models
{
    public class ListagemClientes
    {
        public const string CaminhoArquivoClientes = "clientes.txt";

        public List<Cliente> LerClientesDoArquivo() {
            var clientes = new List<Cliente>();

            if (System.IO.File.Exists(CaminhoArquivoClientes)) {

                var linhas = System.IO.File.ReadAllLines(CaminhoArquivoClientes);

                foreach (var linha in linhas) {

                    var parte = linha.Split(',');

                    if( parte.Length == 5) {
                        clientes.Add(new Cliente {
                            Id = int.Parse(parte[0]),
                            Nome = parte[1],
                            Cpf = parte[2],
                            Email = parte[3],
                            Paciente = parte[4],
                        });
                    }
                }
            }

            return clientes;
        }

        public void EscreverClientesNoArquivo(List<Cliente> clientes) {

            using (var escrever = new StreamWriter(CaminhoArquivoClientes)) {

                foreach (var cliente in clientes) {
                    escrever.WriteLine($"{cliente.Id},{cliente.Nome},{cliente.Cpf},{cliente.Email},{cliente.Paciente}");
                }
            }
        }
    }
}
