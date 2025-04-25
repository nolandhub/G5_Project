using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWebApi.Dtos.TaiKhoan;
using MyWebApi.Model;

namespace MyWebApi.Mapping
{
    public class TaiKhoanProfile : Profile
    {
        public TaiKhoanProfile()
        {
            CreateMap<TaiKhoanUpdateDTO, TaiKhoan>();


            CreateMap<TaiKhoan, TaiKhoanDTO>();

            CreateMap<TaiKhoan, TaiKhoanShortInfo>();


            CreateMap<CreateFullDTO, TaiKhoan>();


        }
    }
}