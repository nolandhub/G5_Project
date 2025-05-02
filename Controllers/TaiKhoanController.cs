using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Dtos;
using MyWebApi.Dtos.TaiKhoan;
using MyWebApi.Interfaces.IRepositories;
using MyWebApi.Interfaces.IServices;
using MyWebApi.Model;
using webAPI.Data;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaiKhoanController : ControllerBase
    {

        private readonly ITaiKhoanRepo _taikhoanrepo;


        public TaiKhoanController(ITaiKhoanRepo taikhoanrepo)
        {
            _taikhoanrepo = taikhoanrepo;
        }

        [Authorize(Roles = "1")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taikhoanrepo.GetAll();
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "1")]
        [HttpGet("GetByDisplayName")]
        public async Task<IActionResult> GetByDisplayName(string TenHienThi)
        {

            var result = await _taikhoanrepo.GetByDisplayName(TenHienThi);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "1")]
        [HttpGet("GetById")]

        public async Task<IActionResult> GetById(int Id)
        {
            var result = await _taikhoanrepo.GetById(Id);
            return StatusCode(result.StatusCode, result);
        }



        [Authorize(Roles = "1")]
        [HttpGet("GetByLoginName")]
        public async Task<IActionResult> GetByLoginName(string TenTK)
        {
            var result = await _taikhoanrepo.GetByLoginName(TenTK);
            return StatusCode(result.StatusCode, result);

        }


        [Authorize(Roles = "1,2,3")]
        [HttpPut("UpdateUserById")]
        public async Task<IActionResult> UpdateById([FromBody] TaiKhoanUpdateDTO dto)
        {

            string maTk = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(maTk))
                return Unauthorized("Người dùng không xác định.");

            int id = int.Parse(maTk);
            var result = await _taikhoanrepo.UpdateUserById(id, dto);
            return StatusCode(result.StatusCode, result);


        }

        [Authorize(Roles = "1,2,3")]
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePasswordByID([FromBody] UpdatePasswordDTO dto)
        {
            string maTk = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(maTk))
                return Unauthorized("Người dùng không xác định.");

            int id = int.Parse(maTk);
            var result = await _taikhoanrepo.UpdatePasswordById(id, dto);
            return StatusCode(result.StatusCode, result);
        }



        [Authorize(Roles = "1")]  // 1: Admin
        [HttpPatch("UpdateRoleById")]
        public async Task<IActionResult> UpdateRoleById(int userId, int idNewRole)
        {
            var result = await _taikhoanrepo.UpdateRoleById(userId, idNewRole);

            return StatusCode(result.StatusCode, result);
        }


        [Authorize(Roles = "1")]  // 1: Admin
        [HttpPost("CreateNewUser")]
        public async Task<IActionResult> CreateNewUser(CreateFullDTO input)
        {
            var result = await _taikhoanrepo.CreateNewUser(input);

            return StatusCode(result.StatusCode, result);
        }


        [Authorize(Roles = "1")]  // 1: Admin
        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteUserById(int Id)
        {
            var result = await _taikhoanrepo.DeleteById(Id);

            return StatusCode(result.StatusCode, result);
        }




    }
}