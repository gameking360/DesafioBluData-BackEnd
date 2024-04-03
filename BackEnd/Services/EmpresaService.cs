using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Services
{
    public class EmpresaService : IEmpresa
    {
        private DataContext _dataContext;
        private readonly string[] _ufs = { "AC","AL","AP","AM","BA","CE","DF","ES","GO","MA","MT","MS","MG","PA","PB","PR","PE","PI","RJ","RN","RS","RO","RR","SC","SP","SE","TO" };

        public EmpresaService(DataContext dataContext) { _dataContext = dataContext; }

        public async Task<string> CreateEmpresa(EmpresaModel model)
        {
            try
            {
                if (model.CNPJ == "" || model.CNPJ.IsNullOrEmpty()) throw new Exception("CNPJ can't null");
                if (model.NomeFantasia == "" || model.NomeFantasia.IsNullOrEmpty()) throw new Exception("Nome Fantasia can't null");
                if (model.UF.Length != 2 || model.UF.IsNullOrEmpty() || model.UF == "" || !_ufs.Contains(model.UF.ToUpper())) throw new Exception("Enter a valid UF");

               var t = await _dataContext.Empresa_Model.AddAsync(model);
                await _dataContext.SaveChangesAsync();
                Console.WriteLine(t.Entity.Id);
                return ("Company created");
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteEmpresa(int id)
        {
            EmpresaModel company = await _dataContext.Empresa_Model.FindAsync(id) ?? throw new Exception("Company not found");

            try
            {
                _dataContext.Empresa_Model.Remove(company);
                await _dataContext.SaveChangesAsync();
                return "Company deleted";
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<EmpresaModel>> GetAllEmpresa()
        {
            return await _dataContext.Empresa_Model.ToListAsync();
        }

        public async Task<EmpresaModel> GetEmpresa(int id)
        {
            EmpresaModel company = await _dataContext.Empresa_Model.FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception("Company not found");

            return company;
        }

        public async Task<EmpresaModel> GetEmpresa(string name)
        {
            EmpresaModel comapny = await _dataContext.Empresa_Model.FirstOrDefaultAsync(c => c.NomeFantasia.ToLower().Contains(name.ToLower())) ?? throw new Exception("Company not found");

            return comapny;
        }
    }
}
