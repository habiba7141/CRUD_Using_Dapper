using CRUDUsingDapper.Models;

namespace CRUDUsingDapper.Repo
{
    public interface IEmployeeRepo
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetByCode(int code);
        Task<string> Delete(int code);
        Task<string> Update(Employee employee,int code);
        Task<string> Create(Employee employee);       
        Task<Employee> GetByIdProcedure(int id);
    }
}
