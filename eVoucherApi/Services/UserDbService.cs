using eVoucherApi.Models;
using Microsoft.EntityFrameworkCore;

namespace eVoucherApi.Services
{
    public class UserDbService
    {
        private readonly DBContext _db;

        public UserDbService(DBContext db)
        {
            _db = db;
        }

        public async Task<int> Register(UserModel model)
        {
            try
            {
                _db.Users.Add(model);
                return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserModel> Login(UserModel model)
        {
            try
            {
                var dataResult = await _db.Users.Where(x => x.UserName == model.UserName && x.Password == model.Password).FirstOrDefaultAsync();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
