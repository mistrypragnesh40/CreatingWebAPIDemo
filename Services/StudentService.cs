using CRUDOperationUsingWEBAPI.Context;
using CRUDOperationUsingWEBAPI.Data;
using CRUDOperationUsingWEBAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDOperationUsingWEBAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentDBContext _context;
        public StudentService(StudentDBContext context)
        {
            _context = context;
        }

        public async Task<MainResponse> AddStudent(StudentDTO studentDTO)
        {
            var response = new MainResponse();
            try
            {
                if (_context.Students.Any(f => f.Email.ToLower() == studentDTO.Email.ToLower()))
                {
                    response.ErrorMessage = "Student is already exist with this email";
                    response.IsSuccess = false;
                }
                else
                {
                    await _context.AddAsync(new Student
                    {
                        Email = studentDTO.Email,
                        Address = studentDTO.Address,
                        FirstName = studentDTO.FirstName,
                        Gender = studentDTO.Gender,
                        LastName = studentDTO.LastName,
                    });
                    await _context.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Content = "Student Added";
                }


            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }

        public async Task<MainResponse> DeleteStudent(DeleteStudentDTO studentDTO)
        {
            var response = new MainResponse();
            try
            {
                var existingStudent = _context.Students.Where(f => f.StudentID == studentDTO.StudentID).FirstOrDefault();

                if (existingStudent != null)
                {
                    _context.Remove(existingStudent);
                    await _context.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Content = "Student Info Deleted";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Content = "Student not found with specify student ID";
                }

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }

        public async Task<MainResponse> GetAllStudent()
        {
            var response = new MainResponse();
            try
            {
                response.Content = await _context.Students.ToListAsync();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<MainResponse> GetStudentByStudentID(int studentID)
        {
            var response = new MainResponse();
            try
            {
                response.Content =
                    await _context.Students.Where(f => f.StudentID == studentID).FirstOrDefaultAsync();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<MainResponse> UpdateStudent(UpdateStudentDTO studentDTO)
        {
            var response = new MainResponse();
            try
            {
                var existingStudent = _context.Students.Where(f => f.StudentID == studentDTO.StudentID).FirstOrDefault();

                if (existingStudent != null)
                {
                    existingStudent.Address = studentDTO.Address;
                    existingStudent.FirstName = studentDTO.FirstName;
                    existingStudent.LastName = studentDTO.LastName;
                    existingStudent.Email = studentDTO.Email;
                    existingStudent.Gender = studentDTO.Gender;
                    await _context.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Content = "Record Updated";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Content = "Student not found with specify student ID";
                }

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }
    }
}
