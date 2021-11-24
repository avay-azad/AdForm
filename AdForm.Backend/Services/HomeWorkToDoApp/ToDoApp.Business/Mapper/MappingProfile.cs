using AdForm.DBService;
using AutoMapper;

namespace ToDoApp.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, LoginRequestDto>().ReverseMap();

            CreateMap<ToDoItems, ToDoItemRequestDto>()
              .ForMember(dest => dest.ItemName, src => src.MapFrom(x => x.Name)).ReverseMap();

            CreateMap<ToDoItems, UpdateToDoItemRequestDto>()
              .ForMember(dest => dest.ItemName, src => src.MapFrom(x => x.Name)).ReverseMap();
 
            CreateMap<ToDoItems, GetItemRequestDto>().ReverseMap();


            CreateMap<LabelToDoItem, LabelToDoItemResponseDto>().ReverseMap();

            CreateMap<ToDoItems, ToDoItemResponseDto>()
                 .ForMember(dest => dest.ToDoLabelIds, src => src.MapFrom(x => x.LabelToDoItems)).ReverseMap();


            CreateMap<ToDoLists, ToDoListRequestDto>()
            .ForMember(dest => dest.ListName, src => src.MapFrom(x => x.Name)).ReverseMap();

            CreateMap<ToDoLists, UpdateToDoListRequestDto>()
              .ForMember(dest => dest.ListName, src => src.MapFrom(x => x.Name))
              .ForMember(dest => dest.ListId, src => src.MapFrom(x => x.ToDoListId)).ReverseMap();

            CreateMap<ToDoLists, GetListRequestDto>().ReverseMap();

            CreateMap<LabelToDoList, LabelToDoListResponseDto>().ReverseMap();

            CreateMap<ToDoLists, ToDoListResponseDto>()
                .ForMember(dest=> dest.LabelToDoLists, src=> src.MapFrom(x=>x.LabelToDoLists)).ReverseMap();

            //Labels

            CreateMap<Labels, LabelRequestDto>().ReverseMap();
            CreateMap<Labels, LabelResponseDto>().ReverseMap();
        }
    }
}
