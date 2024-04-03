using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class TelefoneModel
    {
        public int FornecedorId { get; set; }
        [Key]
        public string Numero { get; set; }

        public bool Juridico { get; set; }
    }
}
