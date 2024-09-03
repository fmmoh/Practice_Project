using AutoMapper;
using BookForm.Entities;
using BookForm.Models;

namespace BookForm
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookListViewModel>();
            CreateMap<Book, BookDetailsViewModel>();
            CreateMap<BookListViewModel, Book>();
            CreateMap<CreateBookViewModel, Book>().ForMember(x => x.Writers, y => y.Ignore());
            
            
            CreateMap<Writer, WriterViewModel>();
            CreateMap<Writer, WriterDetailsViewModel>();
            CreateMap<WriterViewModel, Writer>();




        }
    }
}
