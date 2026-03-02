using AutoMapper;
using Portfolio.Api.DTOs;
using Portfolio.Api.Models;

namespace Portfolio.Api.Mappings;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        // Profile mappings
        CreateMap<Models.Profile, ProfileDto>();
        CreateMap<ProfileUpdateDto, Models.Profile>();

        // Skill mappings
        CreateMap<Skill, SkillDto>();
        CreateMap<SkillCreateDto, Skill>();
        CreateMap<SkillUpdateDto, Skill>();

        // Project mappings
        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectCreateDto, Project>();
        CreateMap<ProjectUpdateDto, Project>();

        // ContactMessage mappings
        CreateMap<ContactMessage, ContactMessageDto>();
        CreateMap<ContactMessageCreateDto, ContactMessage>();

        // SocialLink mappings
        CreateMap<SocialLink, SocialLinkDto>();
    }
}
