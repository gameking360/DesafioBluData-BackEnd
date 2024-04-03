using BackEnd.Data;
using BackEnd.DTOs;
using BackEnd.Mapper;
using BackEnd.Model;
using BackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Services
{
    public class FornecedorService : IFornecedor
    {

        private DataContext context;

        public FornecedorService(DataContext context)
        {
            this.context = context;
        }

        public async Task<string> CreateFornecedor(PessoaFisicaModel modelo)
        {
            if (modelo.CPF.IsNullOrEmpty()) throw new Exception("Campo CPF não pode ser nulo");
            if (modelo.RG.IsNullOrEmpty()) throw new Exception("Campo RG não pode ser nulo");
            if (modelo.Name.IsNullOrEmpty()) throw new Exception("Campo nome não pode ser nulo");

            modelo.RG = modelo.RG.Replace(".", "").Replace("-", "");
            modelo.RG = modelo.Criptografa(modelo.RG);
            modelo.CPF = modelo.CPF.Replace(".", "").Replace("-", "");
            modelo.CPF = modelo.Criptografa(modelo.CPF);

        
            EmpresaModel empresa = await context.Empresa_Model.FindAsync(modelo.IdEmpresa) ?? throw new Exception("Empresa não cadastrada");
            if (empresa.UF == "PR" && !modelo.VerificaIdade()) throw new Exception("Para empresa nesse estado, o fornecedor deve ter mais de 18 anos");

            if (modelo.Name.IsNullOrEmpty()) throw new Exception("Campo nome não pode ser nulo");

            var fornecedor = await context.Pessoa_Fisica.AddAsync(modelo);
            await context.SaveChangesAsync();

            modelo.Telefones.ForEach(t =>
            {
                t.FornecedorId = fornecedor.Entity.Id;
                t.Numero = t.Numero.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
                Console.WriteLine("Numero: " + t.Numero);
                context.Telefones.Add(t);
            });

            await context.SaveChangesAsync();

            return "Sucesso ao adicionar";

        }

        public async Task<string> CreateFornecedor(PessoaJuridicaModel modelo)
        {
            if (modelo.CNPJ.IsNullOrEmpty()) throw new Exception("Campo CNPJ não pode ser nulo");
            if (modelo.Name.IsNullOrEmpty()) throw new Exception("Campo nome não pode ser nulo");
            modelo.CNPJ = modelo.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "");
            modelo.CNPJ = modelo.Criptografa(modelo.CNPJ);

           

            var fornecedor = await context.Pessoa_Juridica.AddAsync(modelo);
            await context.SaveChangesAsync();

            modelo.Telefones.ForEach(t =>
            {
                t.FornecedorId = fornecedor.Entity.Id;
                t.Numero = t.Numero.Replace("(", "").Replace(")", "").Replace(" ","").Replace("-","");
                context.Telefones.Add(t);
            });

            await context.SaveChangesAsync();

            return "Sucesso ao adicionar";
        }

        public async Task<List<FornecedorDTO>> GetFornecedorByEmpresa(string empresa)
        {
            FornecedorMapper mapper = new FornecedorMapper(context);

            var companys =  context.Empresa_Model.Where(e => e.NomeFantasia.Contains(empresa)).ToList();


            List<FornecedorDTO> fornecedores = new List<FornecedorDTO>();

            foreach (var company in companys)
            {
                var fornecedoresEmpresa = await context.Pessoa_Fisica.Where(p => p.IdEmpresa == company.Id).Select(p => mapper.ModelToDTO(p)).ToListAsync();

                var fornecedoresJuridico = await context.Pessoa_Juridica.Where(p => p.IdEmpresa == company.Id).Select(p => mapper.ModelToDTO(p)).ToListAsync();
                                            
                if(fornecedoresEmpresa != null)
                {
                    fornecedores.AddRange(fornecedoresEmpresa);
                }
                if (fornecedoresJuridico != null) fornecedores.AddRange(fornecedoresJuridico);
               

            }

            return fornecedores;
        }

        public async Task<List<FornecedorDTO>> GetFornecedores()
        {
            FornecedorMapper mapper = new FornecedorMapper(context);

            var pf = await context.Pessoa_Fisica.ToListAsync();
            var pj = await context.Pessoa_Juridica.ToListAsync();

            List<FornecedorDTO> resultado = new List<FornecedorDTO>();

            pj.ForEach(pj =>
            {
                resultado.Add(mapper.ModelToDTO(pj));
            });
            pf.ForEach(pf => resultado.Add(mapper.ModelToDTO(pf)));

            return resultado;
        }

        public async Task<List<PessoaFisicaModel>> GetPessoaByCPF(string cpf)
        {
            return await context.Pessoa_Fisica.Where(p => p.Descriptografa(p.CPF) == cpf).ToListAsync();
        }

        public async Task<List<FornecedorDTO>> GetPessoaByData(DateTime cadastro)
        {
            FornecedorMapper mapper = new FornecedorMapper(context);


            var pessoaFisica = await context.Pessoa_Fisica.Where(p => p.HoraCadastro.Date == cadastro.Date).Select(p => mapper.ModelToDTO(p)).ToListAsync();

            var pessoaJuridica = await context.Pessoa_Juridica.Where(p => p.HoraCadastro.Date == cadastro.Date).Select(p => mapper.ModelToDTO(p)).ToListAsync();

            var resultado = pessoaFisica.Concat(pessoaJuridica).ToList();

            return resultado;
        }

        public async Task<List<FornecedorDTO>> GetPessoaByNome(string nome)
        {
            FornecedorMapper mapper = new FornecedorMapper(context);


            var pessoaFisica = await context.Pessoa_Fisica.Where(p => p.Name.ToLower().Contains(nome.ToLower()))
                .Select(p => mapper.ModelToDTO(p)).ToListAsync();

            var pessoaJuridica = await context.Pessoa_Juridica.Where(p => p.Name.ToLower().Contains(nome.ToLower()))
                .Select(p => mapper.ModelToDTO(p)).ToListAsync();

            var resultado = pessoaFisica.Union(pessoaJuridica).ToList();

            return resultado;
        }

        public async Task<PessoaJuridicaModel> GetPessoaJuridicaByCNPJ(string cnpj)
        {
            var resultado =  await context.Pessoa_Juridica.FirstAsync(p => p.CNPJ == cnpj) ?? throw new Exception("Erro ao realizar a consulta");

            return resultado;
        }

        

    }
}
