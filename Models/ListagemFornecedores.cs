
namespace LHPet.Models
{
    public class ListagemFornecedores
    {
        public const string CaminhoArquivoFornecedores = "fornecedor.txt";

        public List<Fornecedor> LerFornecedoresDoArquivo() {

            var fornecedores = new List<Fornecedor>();

            if(System.IO.File.Exists(CaminhoArquivoFornecedores)) {

                var linhas = System.IO.File.ReadAllLines(CaminhoArquivoFornecedores);

                foreach (var linha in linhas) {

                    var parte = linha.Split(',');
                    if (parte.Length == 4) {

                        fornecedores.Add(new Fornecedor {
                            
                            Id = int.Parse(parte[0]),
                            Nome = parte[1],
                            Cnpj = parte[2],
                            Email = parte[3]
                        });
                    }
                }
            }

            return fornecedores;
        }

        public void EscreverFornecedoresNoArquivo(List<Fornecedor> fornecedores) {

            using (var escrever = new StreamWriter(CaminhoArquivoFornecedores)) {

                foreach (var fornecedor in fornecedores) {
                    
                    escrever.WriteLine($"{fornecedor.Id},{fornecedor.Nome},{fornecedor.Cnpj},{fornecedor.Email}");
                }
            }
        }
    }
}