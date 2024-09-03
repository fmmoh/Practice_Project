using AutoMapper;
using BookForm.Data;
using BookForm.Entities;
using BookForm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Dmf;
using System.Linq;
using System.Net.Http.Headers;

namespace BookForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookDbContext _context;
        private object responses;
        private readonly IMapper _mapper;

        public BookController(BookDbContext context, IMapper mapper)
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
                var bookCount = _context.Book.Count();
                var bookList = _mapper.Map<List<BookListViewModel>>(_context.Book.Include(x => x.Writers).Skip(pageIndex * pageSize).Take(pageSize).ToList());

                response.Status = true;
                response.Message = "Success";
                response.Data = new { Books = bookList, Count = bookCount };

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
        public IActionResult GetBookById(int id)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {

                var book = _context.Book.Include(x => x.Writers).Where(x => x.Id == id).FirstOrDefault();

                if (book == null)
                {
                    response.Status = false;
                    response.Message = "Record Not Exist";

                    return BadRequest(response);
                }

                var bookData = _mapper.Map<BookDetailsViewModel>(book);

                response.Status = true;
                response.Message = "Success";
                response.Data = bookData;

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

        [HttpPost]
        public IActionResult Post([FromBody] CreateBookViewModel model)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                if (!ModelState.IsValid)
                {
                    response.Status = false;
                    response.Message = "Validation Failed";
                    response.Data = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(response);
                }

                var writers = _context.Writer.Where(x => model.Writers.Contains(x.Id)).ToList();
                if (writers.Count != model.Writers.Count)
                {
                    response.Status = false;
                    response.Message = "Invalid Writer Assigned";
                    return BadRequest(response);
                }

                var postedModel = _mapper.Map<Book>(model);
                postedModel.Writers = writers;

                _context.Book.Add(postedModel);
                _context.SaveChanges();

                var responseData = _mapper.Map<BookDetailsViewModel>(postedModel);

                response.Status = true;
                response.Message = "Created Successfully";
                response.Data = responseData;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Something went wrong: {ex.Message}";
                return BadRequest(response);
            }
        }

        [HttpPut]
        public IActionResult Put(CreateBookViewModel model)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id <= 0)
                    {
                        response.Status = false;
                        response.Message = "Invalid Book Record.";

                        return BadRequest(response);
                    }

                    var writers = _context.Writer.Where(x => model.Writers.Contains(x.Id)).ToList();

                    if (writers.Count != model.Writers.Count)
                    {
                        response.Status = false;
                        response.Message = "Invalid Writer assigned.";

                        return BadRequest(response);
                    }

                    var BookDetails = _context.Book.Include(x => x.Writers).Where(x => x.Id == model.Id).FirstOrDefault();

                    if (BookDetails == null)
                    {
                        response.Status = false;
                        response.Message = "Invalid Book Record.";

                        return BadRequest(response);
                    }

                    BookDetails.CoverImage = model.CoverImage;
                    BookDetails.Description = model.Description;
                    BookDetails.Language = model.Language;
                    BookDetails.ReleaseImage = model.ReleaseImage;
                    BookDetails.Title = model.Title;


                    // find removed writer

                    var removedWriters = BookDetails.Writers.Where(x => !model.Writers.Contains(x.Id)).ToList();

                    foreach (var writer in removedWriters)
                    {
                        BookDetails.Writers.Remove(writer);
                    }

                    // find added writers
                    var addedWriters = writers.Except(BookDetails.Writers).ToList();
                    foreach (var writer in addedWriters)
                    {
                        BookDetails.Writers.Add(writer);
                    }

                    _context.SaveChanges();

                    var responseData = new BookDetailsViewModel
                    {
                        Id = BookDetails.Id,
                        Title = BookDetails.Title,
                        Writers = BookDetails.Writers.Select(y => new WriterViewModel
                        {
                            Id = y.Id,
                            Name = y.Name,
                            DateOfBirth = y.DateOfBirth
                        }).ToList(),
                        CoverImage = BookDetails.CoverImage,
                        Language = BookDetails.Language,
                        ReleaseImage = BookDetails.ReleaseImage,
                        Description = BookDetails.Description
                    };


                    response.Status = true;
                    response.Message = "Updated Successfully.";
                    response.Data = responseData;

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
                response.Message = "something went wrong";

                return BadRequest(response);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                var book = _context.Book.Where(x => x.Id == id).FirstOrDefault();
                if (book == null)
                {
                    response.Status = false;
                    response.Message = "Invalid Book record.";

                    return BadRequest(response);
                }

                _context.Book.Remove(book);
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

        [HttpPost]
        [Route("upload-book-cover")]
        public async Task<IActionResult> UploadBookCover(IFormFile imageFile)
        {
            try
            {
                var filename = ContentDispositionHeaderValue.Parse(imageFile.ContentDisposition).FileName.TrimStart('\"').TrimEnd('\"');
                string newPath = @"C:\Users\missm\Documents\Behsa_Project\Book-Form\BookForm\BookForm\Delete";

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                string[] allowedImageExtentions = new string[] { ".jpg", ".jpeg", ".png" };

                if (!allowedImageExtentions.Contains(Path.GetExtension(filename)))
                {
                    return BadRequest(new BaseResponseModel
                    {
                        Status = false,
                        Message = "only .jpg, .jpeg and .png type files are allowed."
                    });
                }

                string newFileName = Guid.NewGuid() + Path.GetExtension(filename);
                string fullFilePath = Path.Combine(newPath, newFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return Ok(new { ProfileImage = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/staticFiles/{newFileName}" });
            }
            catch
            {
                return BadRequest(new BaseResponseModel
                {
                    Status = false,
                    Message = "Error Occured."
                });
            }
        }
    }
}
