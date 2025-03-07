using AutoMapper;
using TaskManager.Models;
using TaskManager.Models.Dtos;

namespace TaskManager.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateTaskDto -> Task
            CreateMap<CreateTaskDto, MyTasks>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id is auto-generated
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => false)) // Default to false
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now)); // Set on server

            // EditTaskDto -> Task
            CreateMap<EditTaskDto, MyTasks>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            //
            CreateMap<MyTasks, EditTaskDto>();
        }
    }
}
