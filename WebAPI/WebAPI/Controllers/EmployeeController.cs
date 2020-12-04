using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select EmployeeId, EmployeeName, Department,
                    convert(varchar(10),DateOfJoining,120) as DateOfJoining
                    ,PhotoFileName
                    from dbo.Employee";
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
        public JsonResult Post(Employee emp)
        {
            string query = @"
                    Insert into Employee 
                    (EmployeeName, Department,DateOfJoining, PhotoFileName)
                    values 
                    (
                        '" + emp.EmployeeName + @"'
                        ,'" + emp.Department + @"'
                        ,'" + emp.DateOfJoining + @"'
                        ,'" + emp.PhotoFileName + @"'
                    )";
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
        public JsonResult Put(Employee emp)
        {
            string query = @"
                    update dbo.Employee set 
                    EmployeeName = '" + emp.EmployeeName + @"',
                    Department = '" + emp.Department + @"',
                    DateOfJoining = '" + emp.DateOfJoining + @"',
                    PhotoFileName = '" + emp.PhotoFileName + @"'
                    where EmployeeId = " + emp.EmployeeId + @"
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

            return new JsonResult("Employee successfully update");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.Employee
                    where EmployeeId = " + id + @"
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

            return new JsonResult("Delete Employee");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile() 
        {
            try
            {
                var httpRequest = Request.Form;
                var postFile = httpRequest.Files[0];
                string fileName = postFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using (var stream = new FileStream(physicalPath, FileMode.Create)) 
                {
                    postFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch (Exception e) 
            {
                return new JsonResult("fileName");
            }
        }

        [Route("GetAllDepartmentNames")]
        [HttpGet]
        public JsonResult GetAllDepartmentNames() 
        {
            string query = @"
                    select DepartmentName from dbo.Department ";
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
    }
}