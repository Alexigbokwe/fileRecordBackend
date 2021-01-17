using System;
using AutoMapper;
using fileRecord.DTO;
using fileRecord.Models;

namespace fileRecord.Profiles.File
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            //mapping from model to ReadDto
            CreateMap<FileModel, FileReadDto>();
            //mapping from CreateDto to model
            CreateMap<FileCreateDto, FileModel>();
            //mapping from UpdateDto to model
            CreateMap<FileUpdateDto, FileModel>();
            //mapping from model to UpdateDto
            CreateMap<FileModel, FileUpdateDto>();
        }
    }
}
