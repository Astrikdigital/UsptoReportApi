using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Response; 
using Microsoft.AspNetCore.Mvc; 

namespace ConvergeApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController] 
    public class AdminController : ControllerBase
    {
        #region Depedenices
        private readonly IConfiguration _configuration; 
        private readonly IWebHostEnvironment _environment; 
        private static IHttpContextAccessor? _httpContextAccessor;
        private readonly IUserService _userService;
        #endregion

        public AdminController(IUserService userService)
        { 
            _userService = userService;

        }
        [HttpGet("Generate-Search-Report")]
        public async Task<IActionResult> GenerateSearchReport(int BrandId, string Mark, string CustomerName, DateTime? Date)
        {
            try
            {
                //var result = await _userService.GenerateSearchReport(Mark, OwnerName,CompletedDate);
                var result = await _userService.GenerateSearchReport(BrandId, Mark, CustomerName, Date);
                if (!string.IsNullOrEmpty(result))
                {
                    return Ok(ResponseHelper.GetSuccessResponse(result));
                }
                else
                {
                    return Ok(ResponseHelper.GetFailureResponse());
                }
            }
            catch (Exception)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }
        //[HttpGet("Generate-Search-ReportV2")]
        //public async Task<IActionResult> GenerateSearchReportV2(int BrandId, string Mark, string CustomerName, DateTime? Date)
        //{
        //    try
        //    {
        //        //var result = await _userService.GenerateSearchReport(Mark, OwnerName,CompletedDate);
        //        var result = await _userService.GenerateSearchReportV2(BrandId, Mark, CustomerName, Date);
        //        if (result != null)
        //        {
        //            return Ok(ResponseHelper.GetSuccessResponse(result));
        //        }
        //        else
        //        {
        //            return Ok(ResponseHelper.GetFailureResponse());
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return Ok(ResponseHelper.GetFailureResponse());
        //    }
        //}
        [HttpGet("get-search-report-Lead")]
        public async Task<IActionResult> GetSearchReportLead(string Mark, int BrandId)
        {
            try
            {
                var result = await _userService.GetSearchReportLead(Mark, BrandId);
                if (result != null)
                {
                    return Ok(ResponseHelper.GetSuccessResponse(result));
                }
                else
                {
                    return Ok(ResponseHelper.GetFailureResponse());
                }
            }
            catch (Exception)
            {
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }
    }
}
