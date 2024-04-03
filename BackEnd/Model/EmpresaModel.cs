using System.ComponentModel.DataAnnotations;

namespace BackEnd.Model
{
    public class EmpresaModel
    {
        public int Id { get;set; }
        public string UF { get;set; }
        public string NomeFantasia { get;set; }
        public string CNPJ { get;set; }
    }
}
