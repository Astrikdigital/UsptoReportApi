using BusinessObjectsLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IUserRepository
    {
        Task<dynamic> GetUsptoBrand(int? BrandId);
        Task<dynamic> GetSearchReportLead(string Mark, int BrandId);


    }
}



