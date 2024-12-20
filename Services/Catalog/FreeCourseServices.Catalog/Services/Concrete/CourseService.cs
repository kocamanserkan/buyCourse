﻿using AutoMapper;
using FreeCourse.Shared.Dtos;
using FreeCourseServices.Catalog.Dtos;
using FreeCourseServices.Catalog.Models;
using FreeCourseServices.Catalog.Services.Abstract;
using FreeCourseServices.Catalog.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourseServices.Catalog.Services.Concrete
{
    public class CourseService:ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {

            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsycn()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any())
            {

                foreach (var course in courses)
                {

                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.Id).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }



            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);


        }
        public async Task<Response<CourseDto>> GetByIdAsycn(string id)
        {


            var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();



            if (course == null)
            {
                return Response<CourseDto>.Fail("Category does not found", 404);
            }


            var category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);


        }
        public async Task<Response<List<CourseDto>>> GetByUserIdAsycn(string userId)
        {
            var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {

                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.Id).FirstAsync();
                }

            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }
        public async Task<Response<CourseDto>> CreateAsycn(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            await _courseCollection.InsertOneAsync(newCourse);

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }
        public async Task<Response<NoContent>> UpdateAsycn(CourseUpdateDto courseUpdateDto)
        {
            var updCourse = _mapper.Map<Course>(courseUpdateDto);

            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updCourse);

            if (result == null)
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }


            return Response<NoContent>.Success(204);
        }
        public async Task<Response<NoContent>> DeleteAsycn(string id)
        {

            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount < 1)
            {
                return Response<NoContent>.Fail("Course not found", 404);

            }
            return Response<NoContent>.Success(204);

        }

    }
}
