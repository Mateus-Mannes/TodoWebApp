using AutoMapper;
using TodoApp.Domain;
using TodoApp.ViewModels;

namespace TodoApp
{
    public class TodoAppAutoMapperProfile : Profile
    {
        public TodoAppAutoMapperProfile()
        {
            CreateMap<Todo, TodoCreateViewModel>().ReverseMap();
        }
    }
}
