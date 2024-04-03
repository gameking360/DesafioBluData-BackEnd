using BackEnd.Model;

namespace BackEnd.DTOs
{
    public class FornecedorDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;

        public DateTime HoraCadastro { get; set; }
        public List<string> Telefones { get; set; } = new List<string>();
        public string Tipo { get; set; }
    }
}
