using FreeCourse.Shared.Dtos;
using FreeCourseServices.Catalog.Dtos;
using FreeCourseServices.Catalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourseServices.Catalog.Services.Abstract
{
    public interface ICourseService
    {

        Task<Response<List<CourseDto>>> GetAllAsycn();
        Task<Response<CourseDto>> GetByIdAsycn(string id);
        Task<Response<List<CourseDto>>> GetByUserIdAsycn(string userId);
        Task<Response<CourseDto>> CreateAsycn(CourseCreateDto courseCreateDto);
        Task<Response<NoContent>> UpdateAsycn(CourseUpdateDto courseUpdateDto);
        Task<Response<NoContent>> DeleteAsycn(string id);




    }
}
