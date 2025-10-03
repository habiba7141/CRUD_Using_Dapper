using CRUDUsingDapper.Models;
using CRUDUsingDapper.Models.Data;
using Dapper;
using System.Data;

namespace CRUDUsingDapper.Repo
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly DapperDbContext context;
        public EmployeeRepo(DapperDbContext context)
        {
            this.context = context;
        }

        public  async Task<string> Create(Employee employee)
        {
            string response = string.Empty;
            string query = "INSERT INTO Employees (Name, Email, Phone, Designation) VALUES (@Name, @Email, @Phone, @Designation)";
            var parameters = new DynamicParameters();
            parameters.Add("name", employee.name, DbType.String);
            parameters.Add("email", employee.email, DbType.String);
            parameters.Add("phone", employee.phone, DbType.String);
            parameters.Add("designation", employee.designation, DbType.String);
            using (var connection = this.context.CreateConnection())
            {
                await connection.ExecuteAsync(query,parameters);
                response = "created";
            }
            return response;
        }

        public async Task<string> Delete(int code)
        {
            string response=string.Empty;
            string query = "Delete From Employees Where Code=@code";
            using (var connection = this.context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { code });
                response = "Deleted";
            }
            return response;
        }

        public async Task<List<Employee>> GetAll()
        {
            string query = "Select * From Employees";
            using (var connection = this.context.CreateConnection())
            {
                var emplist=await connection.QueryAsync<Employee>(query);
                return emplist.ToList();
            }
        }

        public async Task<Employee> GetByCode(int code)
        {
            string query = "Select  * From Employees Where Code=@code";
            using (var connection = this.context.CreateConnection())
            {
                var emp = await connection.QueryFirstOrDefaultAsync<Employee>(query, new {code});
                return emp;
            }
        }

        public async Task<string> Update(Employee employee, int code)
        {
            string response = string.Empty;

            string query = @"UPDATE Employees
                     SET Name = @name,
                         Email = @email,
                         Phone = @phone,
                         Designation = @designation
                     WHERE Code = @code";

            var parameters = new DynamicParameters();
            parameters.Add("name", employee.name, DbType.String);
            parameters.Add("email", employee.email, DbType.String);
            parameters.Add("phone", employee.phone, DbType.String);
            parameters.Add("designation", employee.designation, DbType.String);
            parameters.Add("code", code, DbType.Int32);

            using (var connection = this.context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                response = "updated";
            }

            return response;
        }

    }
}
