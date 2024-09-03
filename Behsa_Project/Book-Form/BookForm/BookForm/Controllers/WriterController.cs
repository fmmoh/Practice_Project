using AutoMapper;
using Azure;
using BookForm.Data;
using BookForm.Entities;
using BookForm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace BookForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriterController : ControllerBase
    {
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public WriterController(BookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int pageIndex = 0, int pageSize = 10)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                var writerCount = _context.Writer.Count();
                var writerList = _mapper.Map<List<WriterViewModel>> (_context.Writer.Skip(pageIndex * pageSize).Take(pageSize).ToList());

                response.Status = true;
                response.Message = "Success";
                response.Data = new { Writer = writerList, Count = writerCount };

                return Ok(response);

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "something went wrong";

                return BadRequest(response);
            }

        }
        [HttpGet("{id}")]
        public IActionResult GetWriterById(int id)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {

                var writer = _context.Writer.Where(x => x.Id == id).FirstOrDefault();

                if (writer == null)
                {
                    response.Status = false;
                    response.Message = "Record Not Exist";

                    return BadRequest(response);
                }

                var writerDate = new WriterDetailsViewModel
                {
                    Id = writer.Id,
                    DateOfBirth = writer.DateOfBirth,
                    Name = writer.Name,
                    Books = _context.Book.Where(x => x.Writers.Contains(writer)).Select(x => x.Title).ToArray()
                };

                response.Status = true;
                response.Message = "Success";
                response.Data = writerDate;

                return Ok(response);
            }
            catch (Exception ex)
            {
                // for logging
                response.Status = false;
                response.Message = "something went wrong";

                return BadRequest(response);
            }


        }

        [HttpGet]
        [Route("search/{searchText}")]

        public IActionResult Get(string searchText)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                var searchedWriter = _context.Writer.Where(x => x.Name.Contains(searchText)).Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToList();

                response.Status = true;
                response.Message = "Success";
                response.Data = searchedWriter;

                return Ok(response);
            }
            catch(Exception)
            {
                response.Status = false;
                response.Message = "something went wrong";

                return BadRequest(response);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] WriterViewModel model)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                if (ModelState.IsValid)
                {


                    var postedModel = new Writer()
                    {
                        Name = model.Name,
                        DateOfBirth = model.DateOfBirth,
                    };

                    _context.Writer.Add(postedModel);
                    _context.SaveChanges();

                    model.Id = postedModel.Id;

                    response.Status = true;
                    response.Message = "Created Successfully";
                    response.Data = model;

                    return Ok(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Validation Failed.";
                    response.Data = ModelState;

                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest(response);
            }
        }

        [HttpPut]
        public IActionResult put(WriterViewModel model)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                if (ModelState.IsValid)
                {
                    var postModel = _mapper.Map<Writer>(model);

                    if (model.Id <= 0)
                    {
                        response.Status = false;
                        response.Message = "Invalid Writer Record.";

                        return BadRequest(response);
                    }

                    var writerDetails = _context.Writer.Where(x => x.Id == model.Id).AsNoTracking().FirstOrDefault();
                    if (writerDetails == null)
                    {
                        response.Status = false;
                        response.Message = "Invalid Writer Record.";

                        return BadRequest(response);
                    }

                    _context.Writer.Update(postModel);
                    _context.SaveChanges();

                    response.Status = true;
                    response.Message = "Updated Successfully";
                    response.Data = postModel;

                    return Ok(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Validation Failed.";
                    response.Data = ModelState;

                    return BadRequest(response);
                }
            }
            catch (Exception)
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest(response);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                var writer = _context.Writer.Where(x => x.Id == id).FirstOrDefault();
                if (writer == null)
                {
                    response.Status = false;
                    response.Message = "Invalid Writer record.";

                    return BadRequest(response);
                }

                _context.Writer.Remove(writer);
                _context.SaveChanges();

                response.Status = true;
                response.Message = "Deleted successfully.";

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest(response);
            }
        }
    }
}
