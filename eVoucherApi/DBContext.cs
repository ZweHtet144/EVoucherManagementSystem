using eVoucherApi.Models;
using Microsoft.EntityFrameworkCore;

namespace eVoucherApi
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<EvoucherModel> EVoucher { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
