using eVoucherApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eVoucherApi.Services
{
    public class EVoucherDbServices
    {
        private readonly DBContext _db;

        public EVoucherDbServices(DBContext db)
        {
            _db = db;
        }

        public async Task<List<EvoucherModel>> GetEVoucherList(string phoneNumber)
        {
            try
            {
                var eVoucherList = await _db.EVoucher.Where(x => x.PhoneNumber == phoneNumber).ToListAsync();
                return eVoucherList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CreateEVoucher(EvoucherModel model)
        {
            try
            {
                _db.EVoucher.Add(model);
                return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EvoucherModel> EVoucherDetails(int Id)
        {
            try
            {
                var dataResult = await _db.EVoucher.Where(x => x.Id == Id).FirstOrDefaultAsync();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateEVoucher(EvoucherModel model)
        {
            try
            {
                _db.EVoucher.Update(model);
                return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteEVocuher(int Id)
        {
            try
            {
                EvoucherModel model = new EvoucherModel()
                {
                    Id = Id
                };
                _db.EVoucher.Remove(model);
                return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
