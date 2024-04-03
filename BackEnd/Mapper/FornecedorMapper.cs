using BackEnd.Data;
using BackEnd.DTOs;
using BackEnd.Model;

namespace BackEnd.Mapper
{
    public class FornecedorMapper
    {
        private readonly DataContext _dataContext;

        public FornecedorMapper(DataContext dataContext) { _dataContext = dataContext; }

        public FornecedorDTO ModelToDTO (FornecedorModel pessoa)
        {
            var telefones = _dataContext.Telefones.Where(t => t.FornecedorId == pessoa.Id).Select(t => t.Numero).ToList();

            FornecedorDTO fornecedorDTO = new FornecedorDTO {
                HoraCadastro = pessoa.HoraCadastro,
                Empresa = _dataContext.Empresa_Model.Find(pessoa.IdEmpresa).NomeFantasia,
                Name = pessoa.Name,
                Telefones = telefones,
                Tipo = pessoa is PessoaFisicaModel ? "Fisica" : "Juridica"
            };

            return fornecedorDTO;

        }



        
    }
}
