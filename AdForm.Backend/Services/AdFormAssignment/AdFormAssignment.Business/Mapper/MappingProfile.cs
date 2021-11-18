using AdForm.DBService;
using AutoMapper;

namespace AdFormAssignment.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, LoginRequestDto>().ReverseMap();

            //ToDo Items
            CreateMap<ToDoItems, ToDoItemRequestDto>()
              .ForMember(dest => dest.ItemName, src => src.MapFrom(x => x.Name)).ReverseMap();

            CreateMap<ToDoItems, UpdateToDoItemRequestDto>()
              .ForMember(dest => dest.ItemName, src => src.MapFrom(x => x.Name))
              .ForMember(dest => dest.ItemId, src => src.MapFrom(x => x.Id)).ReverseMap();

            CreateMap<ToDoItems, GetItemRequestDto>().ReverseMap();

            CreateMap<ToDoItems, ToDoItemResponseDto>().ReverseMap();
           
            //ToDo List
            CreateMap<ToDoLists, ToDoListRequestDto>()
            .ForMember(dest => dest.ListName, src => src.MapFrom(x => x.Name)).ReverseMap();

            CreateMap<ToDoLists, UpdateToDoListRequestDto>()
              .ForMember(dest => dest.ListName, src => src.MapFrom(x => x.Name))
              .ForMember(dest => dest.ListId, src => src.MapFrom(x => x.Id)).ReverseMap();

            CreateMap<ToDoLists, GetListRequestDto>().ReverseMap();

            CreateMap<ToDoLists, ToDoListResponseDto>().ReverseMap();

            //Labels

            CreateMap<Labels, LabelRequestDto>().ReverseMap();
            CreateMap<Labels, LabelResponseDto>().ReverseMap();
        }
    }
}
