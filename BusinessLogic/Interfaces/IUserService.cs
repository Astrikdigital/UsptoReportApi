using BusinessObjectsLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        Task<string> GenerateSearchReport(int BrandId, string Mark, string CustomerName, DateTime? Date);
        Task<dynamic> GetSearchReportLead(string Mark, int BrandId);
        //Task<string> GenerateSearchReportV2(int BrandId, string Mark, string CustomerName, DateTime? Date);


    }
}
