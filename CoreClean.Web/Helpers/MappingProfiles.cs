﻿using AutoMapper;
using CoreClean.Domain.Models;
using CoreClean.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreClean.Web.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PhotoViewModel, Photo>().ReverseMap();
            CreateMap<CategoryViewModel, Category>().ReverseMap();

            }
        }
}