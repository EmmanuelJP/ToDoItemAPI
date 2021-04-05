using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoItem2.Model.Entities;
using ToDoItem2.Dtos;

namespace ToDoItem2.Mapper
{
    public class ToDoItemProfile : Profile
    {
        public ToDoItemProfile()
        {
            CreateMap<Item, ItemDto>()
                .ForMember(destination => destination.FullName, options => options.MapFrom(source => $"{source.Name} - {source.Description}"))
                .ReverseMap();
        }
    }
}
