using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Ifarstructures;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Ifarstructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public bool CheckEmployeeCodeBeForeUpdate(Employee employee)
        {
            var procedure = $"Proc_Check{ClassName}BeforeUpdate";
            DynamicParameters dynamic = new DynamicParameters();
            dynamic.Add("@EmployeeId", employee.EmployeeId);
            dynamic.Add("@EmployeeCode", employee.EmployeeCode);
            var res = DbConnection.Query<bool>(procedure, dynamic, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return res;
        }

        public bool CheckEmployeeCodeExist(string EmployeeCode)
        {
            var procedure = $"Proc_Check{ClassName}CodeExist";
            DynamicParameters dynamic = new DynamicParameters();
            dynamic.Add($"@{ClassName}Code", EmployeeCode);
            var res = DbConnection.Query<bool>(procedure, dynamic, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return res;
        }

        public string GetMaxEmployeeCode()
        {
            var procedue = $"Proc_GetMax{ClassName}Code";
            var res = DbConnection.Query<string>(procedue, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return res;
        }
    }
}
