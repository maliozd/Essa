using Application.Common.DTOs.InstagramApiDtos;
using Application.InstaMedias.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;


namespace Application.InstaMedias.Mapper
{
    public class InstaMediaMapperConfig : Profile
    {
        public InstaMediaMapperConfig()
        {
            CreateMap<Node, InstaMedia>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.InstaId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MediaType, opt => opt.MapFrom(src => ConvertToMediaType(src.MediaType)))
                .ForMember(dest => dest.Caption, opt => opt.MapFrom(src => src.Caption))
                .ForMember(dest => dest.Shortcode, opt => opt.MapFrom(src => src.Shortcode))
                .ForMember(dest => dest.DisplayUrl, opt => opt.MapFrom(src => src.DisplayUrl));

            CreateMap<InstaMedia, MediaResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UrlShortCode, opt => opt.MapFrom(src => src.Shortcode))
                .ForMember(dest => dest.DisplayUrl, opt => opt.MapFrom(src => src.DisplayUrl))
                .ForMember(dest => dest.Caption, opt => opt.MapFrom(src => src.Caption));
        }
        static MediaType ConvertToMediaType(string mediaType)
        {
            switch (mediaType)
            {
                case "GraphImage":
                case "GraphSidecar":
                    return MediaType.Image;
                case "GraphVideo":
                    return MediaType.Video;
                default:
                    return MediaType.Image;
            }
        }
    }
}
