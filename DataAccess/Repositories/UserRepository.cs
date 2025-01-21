
using BusinessObjectsLayer.Entities; 
using Dapper;
using DataAccess.DbContext;
using DataAccessLayer.Interface; 
using Microsoft.AspNetCore.Http;
using System.Data; 

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        private readonly HttpContextAccessor _httpContextAccessor;
        public UserRepository(HttpContextAccessor httpContextAccessor,DapperContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<dynamic> GetUsptoBrand(int? BrandId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var user = await connection.QueryAsync<dynamic>("GetUsptoBrand @BrandId", new { BrandId = BrandId });
                if (user != null)
                {
                    return user.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task<dynamic> GetSearchReportLead(string Mark, int BrandId)
        {
            try
            {
                var param = new
                {
                    Mark = Mark,
                    BrandId = BrandId
                };
                using (IDbConnection con = _context.CreateConnection())
                {
                    using var multi = await con.QueryMultipleAsync("GetSearchReportLead", param: param, commandType: CommandType.StoredProcedure);
                    var Brand = (await multi.ReadAsync<dynamic>()).FirstOrDefault();
                    var Leads = (await multi.ReadAsync<dynamic>()).ToList();

                    return new { Brand, Leads };
                }

            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
