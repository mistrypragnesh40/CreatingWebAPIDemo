using CRUDOperationUsingWEBAPI.Models;
using CRUDOperationUsingWEBAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUDOperationUsingWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetAllStudent")]
        public async Task<IActionResult> GetAllStudent()
        {
            try
            {
                var response = await _studentService.GetAllStudent();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetStudentByStudentID/{StudentID}")]
        public async Task<IActionResult> GetStudentByStudentID(int StudentID)
        {
            try
            {
                var response = await _studentService.GetStudentByStudentID(StudentID);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] StudentDTO studentInfo)
        {
            try
            {
                var response = await _studentService.AddStudent(studentInfo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentDTO studentInfo)
        {
            try
            {
                if (studentInfo.StudentID > 0)
                {
                    var response = await _studentService.UpdateStudent(studentInfo);
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Please pass student ID");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent([FromBody] DeleteStudentDTO studentInfo)
        {
            try
            {
                if (studentInfo.StudentID > 0)
                {
                    var response = await _studentService.DeleteStudent(studentInfo);
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Please pass student ID");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
