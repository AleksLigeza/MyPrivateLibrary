using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPrivateLibraryAPI.DbModels;
using MyPrivateLibraryAPI.Models;

namespace MyPrivateLibraryAPI.Mapping
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<BookRequest, Book>();
            CreateMap<Book, BookResponse>();
        }
    }
}
