using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Dtos;
using MyWebApi.Dtos.TaiKhoan;
using MyWebApi.Interfaces.IRepositories;

namespace MyWebApi.Repositories
{
    public class TaiKhoanRepo : ITaiKhoanRepo
    {
        public Task<ResultDTO> DeleteById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO> GetByDisplayName(string TenHienThi)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO> GetByLoginName(string TenTK)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO> UpdateUserById(UpdateDTO Edit)
        {
            throw new NotImplementedException();
        }
    }
}