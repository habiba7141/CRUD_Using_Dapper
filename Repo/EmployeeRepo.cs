using Azure;
using CRUDUsingDapper.Models;
using CRUDUsingDapper.Models.Data;
using Dapper;
using System.ComponentModel.DataAnnotations;
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
        //procedure
        public async Task<Employee> GetByIdProcedure(int id)
        {
            var procedureName = "GetEmployeeById";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = this.context.CreateConnection())
            {
                var Emp= await connection.QueryFirstOrDefaultAsync<Employee>(procedureName, parameters,commandType:CommandType.StoredProcedure);
                return Emp;
            }
           
        }

        public async Task<Company> MultipleQuery(int id)
        {
            var query = "Select * From Companies Where Id=@Id;" +
                "Select * From Employees Where CompanyId=@Id;";

            using(var connection = this.context.CreateConnection())
                using(var multi=await connection.QueryMultipleAsync(query,new {id}))
            {
                var company = await multi.ReadSingleOrDefaultAsync<Company>();
                if(company is not null)
                    company.Employees=(await multi.ReadAsync<Employee>()).ToList();

                return company;
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
        public async Task<bool> AddMultipleEmployeesAsync(List<Employee> employees)
        {
            using (var connection =this.context.CreateConnection())
            {
               
                connection.Open();

              
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var query = @"INSERT INTO Employees (Name, Email, Phone, Designation, CompanyId)
                              VALUES (@Name, @Email, @Phone, @Designation, @CompanyId);";

                        foreach (var emp in employees)
                        {
                            await connection.ExecuteAsync(query, emp, transaction: transaction);
                        }

                      
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"❌ Transaction failed: {ex.Message}");
                        throw; // 👈 علشان نعرف الخطأ في Swagger أو Postman
                    }

                }
            }
        }


    }
}
