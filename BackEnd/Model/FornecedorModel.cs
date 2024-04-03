using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Model
{
    public class FornecedorModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int IdEmpresa { get; set; }
        
        public DateTime HoraCadastro { get; set; }
        [NotMapped]
        public List<TelefoneModel> Telefones { get; set; } = new List<TelefoneModel>();


        public string Criptografa(string dado)
        {
            Random random = new Random();

            byte chave = (byte) random.Next(48, 57);
            Console.WriteLine(chave.ToString());

            ASCIIEncoding encoding = new ASCIIEncoding();


            byte[] inChar = encoding.GetBytes(dado);

            
            
            for (int index = 0; index < inChar.Length; index++)
            {
                inChar[index] += chave;
                
            }

            



            return encoding.GetString(inChar) + chave.ToString();
        }

        public string Descriptografa(string dado)
        {
            byte chave = byte.Parse($"{dado[dado.Length - 2]}{dado[dado.Length - 1]}");
            Console.WriteLine(chave.ToString());

            ASCIIEncoding encoding = new ASCIIEncoding();


            byte[] palava = encoding.GetBytes(dado.Remove(dado.Length - 2));


            for (int index = 0; index < palava.Length; index++)
            {
                palava[index] -= chave;
                Console.WriteLine(palava[index].ToString());
            }

            return encoding.GetString(palava);

        }
    }
}
