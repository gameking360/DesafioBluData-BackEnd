namespace BackEnd.Model
{
    public class PessoaFisicaModel : FornecedorModel
    {
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime DataNascimento { get; set; }

        public bool VerificaIdade()
        {
            if (DataNascimento.Year - DateTime.Now.Year < 18) return false;
            return true;
        }
    }
}
