using BackEnd.Model;

namespace BackEnd.Services.Interfaces
{
    public interface IEmpresa
    {
        Task<string> CreateEmpresa(EmpresaModel model);
        Task<List<EmpresaModel>> GetAllEmpresa();
        Task<string> DeleteEmpresa(int id);
        Task<EmpresaModel> GetEmpresa(int id);
        Task<EmpresaModel> GetEmpresa(string name);

        
    }
}
