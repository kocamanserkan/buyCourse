using FreeCourse.Shared.Dtos;
using FreeCourseServices.Catalog.Dtos;
using FreeCourseServices.Catalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourseServices.Catalog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsycn();
        Task<Response<CategoryDto>> CreateAsycn(CategoryDto categoryDto);

        Task<Response<CategoryDto>> GetByIdAsycn(string id);

    }
}
