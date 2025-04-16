using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Dtos;
using MyWebApi.Dtos.TaiKhoan;


namespace MyWebApi.Interfaces.IRepositories
{
    public interface ITaiKhoanRepo
    {

        Task<ResultDTO> GetAll();
        Task<ResultDTO> GetById(int Id);
        Task<ResultDTO> GetByDisplayName(string TenHienThi);
        Task<ResultDTO> GetByLoginName(string TenTK);
        Task<ResultDTO> UpdateUserById(UpdateDTO Edit);
        Task<ResultDTO> DeleteById(int Id);

    }
}