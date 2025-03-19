using BUDGET.MANAGER.Data;
using BUDGET.MANAGER.Models;
using BUDGET.MANAGER.Models.UserManager;
using BUDGET.MANAGER.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BUDGET.MANAGER.Services.UserManager.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<UserModel>>> GetAllUsers()
        {
            var _response = new ResponseModel<List<UserModel>>();

            try
            {
                var users = await _context.Users.ToListAsync();

                if (users == null || users.Count == 0)
                {
                    _response.Status = 0;
                    _response.Message = "No users found.";
                }
                else
                {
                    _response.Status = 1;
                    _response.Message = "Successfully fetched users.";
                    _response.Data = users;
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = $"An error occured: {ex.Message}";
            }

            return _response;
        }

        public async Task<ResponseModel<UserModel>> GetUserById(int userId)
        {
            var _response = new ResponseModel<UserModel>();

            try
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    _response.Status = 0;
                    _response.Message = "User not found.";
                }
                else
                {
                    _response.Status = 1;
                    _response.Message = "Successfully fetched user.";
                    _response.Data = user;
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = $"An error occured: {ex.Message}";
            }

            return _response;
        }

        public async Task<ResponseModel<UserModel>> AddUser(UserModel user)
        {
            var _response = new ResponseModel<UserModel>();

            try
            {
                if (await _context.Users.AnyAsync(u => (u.Firstname == user.Firstname && u.Lastname == user.Lastname) || u.Username == user.Username))
                {
                    _response.Status = 0;
                    _response.Message = "User already exists.";
                }
                else
                {
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();

                    _response.Status = 1;
                    _response.Message = "User added successfully.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = $"An error occured: {ex.Message}";
            }

            return _response; 
        }

        public async Task<ResponseModel<UserModel>> UpdateUser(UserModel user)
        {
            var _response = new ResponseModel<UserModel>();

            try
            {
                var getUser = await _context.Users.FindAsync(user.UserId);

                if (getUser == null)
                {
                    _response.Status = 0;
                    _response.Message = "User not found.";
                }
                else
                {
                    getUser.Firstname = user.Firstname;
                    getUser.Lastname = user.Lastname;
                    getUser.Username = user.Username;
                    getUser.Gender = user.Gender;
                    getUser.IsActive = user.IsActive;

                    _context.Users.Update(getUser);
                    await _context.SaveChangesAsync();

                    _response.Status = 1;
                    _response.Message = "User updated successfully.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = $"An error occured: {ex.Message}";
            }

            return _response;
        }

        public async Task<ResponseModel<UserModel>> DeleteUser(int userId)
        {
            var _response = new ResponseModel<UserModel>();

            try
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    _response.Status = 0;
                    _response.Message = "User not found.";
                }
                else
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();

                    _response.Status = 1;
                    _response.Message = "User deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = $"An error occured: {ex.Message}";
            }

            return _response;
        }
    }
}
