using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _config;

        public DepartmentController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public JsonResult Get() 
        {
            string query = @"
                    select DepartmentId, DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            string sqlDataSource = _config.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) 
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) 
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
                return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Department dep) 
        {
            string query = @"
                    Insert into Department values ('"+dep.DepartmentName+@"')";
            DataTable table = new DataTable();
            string sqlDataSource = _config.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Department dep) 
        {
            string query = @"
                    update dbo.Department set 
                    DepartmentName = '"+ dep.DepartmentName+ @"'
                    where DepartmentId = "+dep.DepartmentId + @"
                    ";
            DataTable table = new DataTable();
            string SqlConnection = _config.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(SqlConnection)) 
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) 
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Department successfully update");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.Department
                    where DepartmentId = " + id + @"
                    ";
            DataTable table = new DataTable();
            string SqlConnection = _config.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(SqlConnection))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Delete department");
        }
    }
}