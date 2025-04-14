using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Dtos.TaiKhoan;


namespace MyWebApi.Interfaces.IRepositories
{
    public interface ITaiKhoanRepo
    {

        Task<ICollection<TaiKhoanDTO>> GetAll();

        Task<ICollection<TaiKhoanDTO>> GetById();

        Task<ICollection<TaiKhoanDTO>> GetByName();

        Task<ICollection<TaiKhoanDTO>> GetByLoginName();


        Task<RegisterDTO> AddUser(RegisterDTO regis);

        Task<UpdateDTO> UpdateUserBy( UpdateDTO edit);

        Task<TaiKhoanDTO> DeleteById(RegisterDTO regis);

    }
}