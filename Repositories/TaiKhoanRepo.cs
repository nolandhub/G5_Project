using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Firebase.Auth;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Dtos;
using MyWebApi.Dtos.TaiKhoan;
using MyWebApi.Helpers;
using MyWebApi.Interfaces.IRepositories;
using MyWebApi.Model;
using webAPI.Data;

namespace MyWebApi.Repositories
{
    public class TaiKhoanRepo : ITaiKhoanRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;



        public TaiKhoanRepo(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResultDTO> DeleteById(int Id)
        {
            try
            {

                var currentUser = _httpContextAccessor.HttpContext.User;
                if (!currentUser.IsInRole("1"))
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Không có quyền ",
                        StatusCode = 403   // Not Found 


                    };


                }


                var user = await _context.TaiKhoans.SingleOrDefaultAsync(t => t.MaTK == Id);

                if (user == null)
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Người dùng không tồn tại",
                        StatusCode = 404  // Not Found 
                    };
                }

                user.Xoa = true;
                await _context.SaveChangesAsync();

                return new ResultDTO
                {
                    Success = true,
                    Message = "Đã xóa người dùng ra khỏi hệ thống.",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi xóa người dùng: {ex.Message}",
                    StatusCode = 500  // Internal Server Error
                };
            }
        }

        public async Task<ResultDTO> GetAll()
        {
            try
            {
                var users = await _context.TaiKhoans.Where(t => t.Xoa == false).ToListAsync();

                //users là list<TaiKhoan> --> Map sang List<TaiKhoanDTO>(users) .
                //Bản chất chuyển list<TaiKhoan> --> List<TaiKhoanDTO> 
                var userDtos = _mapper.Map<List<TaiKhoanDTO>>(users);

                return new ResultDTO
                {
                    Success = true,
                    Message = $"Lấy thông tin hoàn tất, có {userDtos.Count} người dùng trong hệ thống.",
                    StatusCode = 200,
                    Data = userDtos
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi lấy danh sách người dùng: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<ResultDTO> GetByDisplayName(string TenHienThi)
        {
            try
            {
                var users = await _context.TaiKhoans.Where(t => t.TenHienThi == TenHienThi && t.Xoa == false).ToListAsync();

                if (!users.Any())
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = $"Không tìm thấy người dùng nào có tên {TenHienThi}",
                        StatusCode = 404
                    };
                }

                var TaiKhoanDTO = _mapper.Map<TaiKhoanDTO>(users);

                return new ResultDTO
                {
                    Success = true,
                    Message = $"Đã tìm thấy {users.Count} người dùng có tên {TenHienThi}",
                    StatusCode = 200,
                    Data = TaiKhoanDTO
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi tìm người dùng theo tên hiển thị: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<ResultDTO> GetById(int Id)
        {
            try
            {
                var user = await _context.TaiKhoans.SingleOrDefaultAsync(t => t.MaTK == Id);

                if (user == null)
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = $"Không tìm thấy người dùng với ID = {Id}",
                        StatusCode = 404,
                        Data = null
                    };
                }

                var TaiKhoanDTO = _mapper.Map<TaiKhoanDTO>(user);



                return new ResultDTO
                {
                    Success = true,
                    Message = $"Đã tìm thấy người dùng với ID = {Id}",
                    StatusCode = 200,
                    Data = TaiKhoanDTO
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi tìm người dùng theo ID: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<ResultDTO> GetByLoginName(string TenTK)
        {
            try
            {
                var user = await _context.TaiKhoans.SingleOrDefaultAsync(t => t.TenTK == TenTK && t.Xoa == false);

                if (user == null)
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = $"Không tìm thấy người dùng nào có tên {TenTK}",
                        StatusCode = 404
                    };
                }

                var TaiKhoanDTO = _mapper.Map<TaiKhoanDTO>(user);



                return new ResultDTO
                {
                    Success = true,
                    Message = $"Đã tìm thấy người dùng có tên tài khoản {TenTK}",
                    StatusCode = 200,
                    Data = TaiKhoanDTO
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi tìm người dùng theo tên đăng nhập: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<ResultDTO> UpdateUserById(int Id, TaiKhoanUpdateDTO Edit)
        {
            try
            {
                var user = await _context.TaiKhoans.SingleOrDefaultAsync(t => t.MaTK == Id);
                if (user == null)
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = $"Người dùng với mã là {Id} không tồn tại.",
                        StatusCode = 404
                    };
                }

                _mapper.Map(Edit, user);
                await _context.SaveChangesAsync();

                return new ResultDTO
                {
                    Success = true,
                    Message = "Thông tin người dùng đã được cập nhật.",
                    StatusCode = 200,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = "Lỗi hệ thống khi cập nhật: " + ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<ResultDTO> UpdatePasswordById(int id, UpdatePasswordDTO dto)
        {
            try
            {
                // Chuẩn hóa đầu vào
                dto.oldPassword = dto.oldPassword?.Trim();
                dto.newPassword = dto.newPassword?.Trim();
                dto.rePassword = dto.rePassword?.Trim();

                var user = await _context.TaiKhoans.SingleOrDefaultAsync(t => t.MaTK == id);

                if (user == null)
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Tài khoản không tồn tại.",
                        StatusCode = 404
                    };
                }

                // Kiểm tra mật khẩu cũ
                if (!IsPasswordMatch(user.MatKhau, dto.oldPassword))
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Mật khẩu cũ không chính xác, vui lòng nhập lại.",
                        StatusCode = 400
                    };
                }

                // điều kiện thỏa mãn khi IsNewPasswordValid return false;
                if (!IsNewPasswordValid(dto.newPassword, dto.rePassword))
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Mật khẩu xác nhận không khớp với mật khẩu mới.",
                        StatusCode = 400
                    };
                }

                user.MatKhau = PasswordHasher.Hash(dto.newPassword);
                await _context.SaveChangesAsync();

                return new ResultDTO
                {
                    Success = true,
                    Message = "Cập nhật mật khẩu thành công.",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Đã xảy ra lỗi hệ thống khi cập nhật mật khẩu: {ex.Message}",
                    StatusCode = 500
                };
            }
        }



        public async Task<ResultDTO> UpdateRoleById(int userId, int idNewRole)
        {
            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;

                if (!currentUser.IsInRole("1"))
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Bạn không có quyền thực hiện thao tác này.",
                        StatusCode = 403,
                    };

                }


                var user = await _context.TaiKhoans.SingleOrDefaultAsync(t => t.MaTK == userId);

                if (user == null)
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Không tìm thấy người dùng.",
                        StatusCode = 404
                    };
                }

                user.LoaiTK = idNewRole;
                await _context.SaveChangesAsync();

                return new ResultDTO
                {
                    Success = true,
                    Message = "Thay đổi vai trò thành công.",
                    StatusCode = 200,
                    Data = user.LoaiTK
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi cập nhật vai trò người dùng: {ex.Message}",
                    StatusCode = 500
                };
            }
        }


        public async Task<ResultDTO> CreateNewUser(CreateFullDTO input)
        {

            try
            {
                var currentUser = _httpContextAccessor.HttpContext.User;

                if (!currentUser.IsInRole("1"))
                {


                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Bạn không có quyền thực hiện thao tác này.",
                        StatusCode = 403

                    };
                }

                var isExist = await _context.TaiKhoans.AnyAsync(t => t.TenTK == input.TenTK);
                if (isExist)
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Tên tài khoản đã tồn tại.",
                        StatusCode = 400
                    };

                }

                var user = _mapper.Map<TaiKhoan>(input);
                user.IsVerified = false;
                user.Xoa = false;
                user.CreateAt = DateTime.Now;

                await _context.TaiKhoans.AddAsync(user);
                await _context.SaveChangesAsync();

                var shortInfo = _mapper.Map<TaiKhoanShortInfo>(user);

                return new ResultDTO
                {
                    Success = true,
                    Message = "Tạo người dùng thành công .",
                    Data = shortInfo,
                    StatusCode = 200
                };

            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi tạo mới người dùng: {ex.Message}",
                    StatusCode = 500
                };

            }

        }


        private bool IsPasswordMatch(string hashedPassword, string inputPassword)
        {
            return PasswordHasher.Verify(hashedPassword, inputPassword);
        }

        private bool IsNewPasswordValid(string newPassword, string rePassword)
        {
            return !string.IsNullOrWhiteSpace(newPassword) && newPassword == rePassword;
        }
    }
}