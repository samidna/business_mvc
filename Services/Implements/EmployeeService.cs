using Business.Helpers;
using Business.Models;
using System.Data;
using Business.Services.Interfaces;

namespace Business.Services.Implements;

public class EmployeeService : IEmployeeService
{
    public async Task<int> AddAsync(Employee employee)
    {
        return await SqlHelper.ExecuteAsync($"INSERT INTO Employees VALUES ({employee.Name}, {employee.Surname},{employee.FatherName},{employee.PositionId},{employee.BranchId})");
    }

    public async Task<int> AddAsync(List<Employee> employees)
    {
        string query = "INSERT INTO Employees VALUES";
        foreach (Employee employee in employees)
        {
            query += $"{employee.Name}, {employee.Surname},{employee.FatherName},{employee.PositionId},{employee.BranchId}),";
        }
        return await SqlHelper.ExecuteAsync(query.Substring(0, query.Length - 1));
    }

    public async Task<int> Delete(int id)
    {
        await GetByIdAsync(id);
        return await SqlHelper.ExecuteAsync("DELETE Employee WHERE Id = " + id);
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        List<Employee> list = new List<Employee>();
        DataTable dt = await SqlHelper.SelectAsync("Select * from Employees");
        foreach (DataRow item in dt.Rows)
        {
            list.Add(new Employee
            {
                Id = (int)item["Id"],
                Name = (string)item["Name"],
                Surname = (string)(item["Surname"]),
                FatherName = (string)item["FatherName"],
                Salary = (int)item["Salary"],
                PositionId = (int)item["PositionId"],
                BranchId = (int)item["BranchId"]
            });
        }
        return list;
    }

    public async Task<Employee> GetByIdAsync(int id)
    {
        DataTable dt = await SqlHelper.SelectAsync("Select * from Employees where Id = " + id);
        if (dt.Rows.Count != 1) throw new Exception("Error");
        return new Employee
        {
            Id = (int)dt.Rows[0]["Id"],
            Name = (string)dt.Rows[0]["Name"],
            Surname = (string)dt.Rows[0]["Surname"],
            FatherName = (string)dt.Rows[0]["FatherName"],
            Salary = (int)dt.Rows[0]["Salary"],
            PositionId = (int)dt.Rows[0]["PositionId"],
            BranchId = (int)dt.Rows[0]["BranchId"]
        };
    }

    public Task<Employee> Update(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> Update(int id, Employee employee)
    {
        throw new NotImplementedException();
    }
}
