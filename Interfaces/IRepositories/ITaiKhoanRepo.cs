using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Dtos;
using MyWebApi.Dtos.TaiKhoan;
using MyWebApi.Model;


namespace MyWebApi.Interfaces.IRepositories
{
    public interface ITaiKhoanRepo
    {

        Task<ResultDTO> GetAll();
        Task<ResultDTO> GetById(int Id);
        Task<ResultDTO> GetByDisplayName(string TenHienThi);
        Task<ResultDTO> GetByLoginName(string TenTK);
        Task<ResultDTO> UpdateUserById(int Id, TaiKhoanUpdateDTO Edit);
        Task<ResultDTO> UpdatePasswordById(int Id, UpdatePasswordDTO UpdatePass);
        Task<ResultDTO> UpdateRoleById(int Id, int MaLoai);

        Task<ResultDTO> CreateNewUser(CreateFullDTO input);
        Task<ResultDTO> DeleteById(int Id);

    }
}