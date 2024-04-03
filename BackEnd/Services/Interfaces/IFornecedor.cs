using BackEnd.DTOs;
using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
    public interface IFornecedor
    {
          Task<string> CreateFornecedor(PessoaFisicaModel modelo);
          Task<string> CreateFornecedor(PessoaJuridicaModel modelo);
          Task<PessoaJuridicaModel> GetPessoaJuridicaByCNPJ(string cnpj);
          Task<List<FornecedorDTO>> GetPessoaByNome(string nome);
          Task<List<FornecedorDTO>> GetFornecedores();
          Task<List<FornecedorDTO>> GetFornecedorByEmpresa(string empresa);
          Task<List<PessoaFisicaModel>> GetPessoaByCPF(string cpf);
          Task<List<FornecedorDTO>> GetPessoaByData(DateTime cadastro);


    }
}
