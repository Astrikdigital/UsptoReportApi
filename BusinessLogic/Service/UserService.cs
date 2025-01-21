using BusinessLogicLayer.Interfaces; 
using Microsoft.Extensions.Configuration; 
using PuppeteerSharp; 
using DataAccessLayer.Interface;
using Converge.Shared.Helper;
using DinkToPdf; 
using ErrorLog;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLogicLayer.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly string? _secretKey;
        private readonly string? _issuer;
        private readonly string? _audience;
        private  string? _UsptoDataApi;
        private readonly int _durationInMinutes;
        private readonly IConfiguration _configuration;


        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _secretKey = configuration["Jwt:SecretKey"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            _durationInMinutes = int.Parse(configuration["Jwt:DurationInMinutes"]);
            _UsptoDataApi = configuration["UsptoDataApi:UsptoDataApi"];
            _configuration = configuration;

        }
        public async Task<dynamic> GetSearchReportLead(string Mark, int BrandId)
        {
            try
            {
                var res = await _userRepository.GetSearchReportLead(Mark, BrandId);
                return res;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
      

        public async Task<string> GenerateSearchReport(int BrandId, string Mark, string CustomerName, DateTime? Date)
        {
            try
            {
                //  return "";
                var pdfOptions = new PdfOptions()
                {
                    Format = PuppeteerSharp.Media.PaperFormat.A4,
                    PrintBackground = true,
                };

                try
                {
                    var res = await _userRepository.GetUsptoBrand(BrandId);
                    if (res != null)
                    {  
                        var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                        {
                            Headless = true,
                            ExecutablePath = @"C:\chrome-headless-shell-win64\chrome-headless-shell.exe"
                        });
                        Date = Date != null ? Date : DateTime.UtcNow;
                       
                        var PageLink = _UsptoDataApi+"Mark=" + Mark + "&Name=" + CustomerName + "&BrandId=" + BrandId + "&Date=" + Date?.ToString("MM/dd/yyyy");

                        using (var page = await browser.NewPageAsync())
                        {
                            await page.GoToAsync(PageLink, WaitUntilNavigation.Networkidle0);
                            var wwwRootPath = Directory.GetCurrentDirectory();
                            var searchReportsFolderPath = Path.Combine(wwwRootPath, "wwwroot", "SearchReports");

                            if (!Directory.Exists(searchReportsFolderPath))
                            {
                                Directory.CreateDirectory(searchReportsFolderPath);
                            }
                            var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
                            var pdfFileName = Mark.Length > 25  ? Mark.Substring(0, 25): Mark + $"_{timestamp}.pdf";

                            var pdfFilePath = Path.Combine(searchReportsFolderPath, pdfFileName);
                            //page.GoToAsync(PageLink, WaitUntilNavigation.Networkidle0);
                            await page.PdfAsync(pdfFilePath, pdfOptions);
                            await browser.CloseAsync();
                            if (searchReportsFolderPath != null && searchReportsFolderPath.Length > 0)
                            {
                                pdfFilePath = null;//await Helper.AttachFileToS3Async(pdfFilePath, _configuration, pdfFileName, res.BrandCode, res.BrandId);
                            }
                            if (File.Exists(Path.Combine(searchReportsFolderPath, pdfFileName)))
                            {
                                File.Delete(Path.Combine(searchReportsFolderPath, pdfFileName));
                            }
                            return pdfFilePath;
                        }

                    }
                    else return null;

                }
                catch (Exception ex)
                {
                    string request = ErrorLogger.LogMethodParameters(nameof(GenerateSearchReport), ErrorLogger.ConvertObjectToDictionary(BrandId + " "+Mark + " " + CustomerName));
                    ErrorLogger.LogError("UserService", "GenerateSearchReport", request, ex);
                    return "";
                }
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(GenerateSearchReport), ErrorLogger.ConvertObjectToDictionary(BrandId + " " + Mark + " " + CustomerName));
                ErrorLogger.LogError("UserService", "GenerateSearchReport", request, ex);
                return "";
            }
        }
        //public async Task<string> GenerateSearchReportV2(int BrandId, string Mark, string CustomerName, DateTime? Date)
        //{
        //    try
        //    {

        //        var res = await _userRepository.GetUsptoBrand(BrandId);

        //        var wwwRootPath = Directory.GetCurrentDirectory();
        //            var searchReportsFolderPath = Path.Combine(wwwRootPath, "wwwroot");
        //            string htmlTemplatePath = Path.Combine(searchReportsFolderPath, "index.html");
        //            string htmlContent = File.ReadAllText(htmlTemplatePath);
        //            htmlContent = htmlContent.Replace("{{HeaderFooter}}", res.ImgSource);
        //            htmlContent = htmlContent.Replace("[VAR_BRAND_NAME]", res.BrandName);
        //            htmlContent = htmlContent.Replace("[VAR_DATE]", Date != null ? Date?.ToString("MM/dd/yyyy") : DateTime.Now.ToString("MM/dd/yyyy"));
        //        htmlContent = htmlContent.Replace("[VAR_CLIENT_NAME]", CustomerName);
        //        htmlContent = htmlContent.Replace("[VAR_COUNT]", CustomerName); 

        //        //htmlContent = htmlContent.Replace("{{InvoiceDate}}", "");

        //        var pdfDoc = new HtmlToPdfDocument()
        //        {
        //            GlobalSettings = new GlobalSettings
        //            {
        //                ColorMode = ColorMode.Color, 
        //                PaperSize = PaperKind.A4
        //            },
        //            Objects = { new ObjectSettings { HtmlContent = htmlContent } }
        //        };
        //        var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
        //        var pdfFileName = Mark.Length > 25 ? Mark.Substring(0, 25) : Mark + $"_{timestamp}.pdf";

        //        if (!Directory.Exists(searchReportsFolderPath))
        //        {
        //            Directory.CreateDirectory(searchReportsFolderPath);
        //        }
        //        SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
        //        var pdfFilePath = Path.Combine(searchReportsFolderPath, pdfFileName);
        //        SelectPdf.PdfDocument pdf = converter.ConvertHtmlString(htmlContent);
        //        pdf.Save(pdfFilePath);
        //        pdf.Close();
        //        //byte[] pdf = converter.Convert(pdfDoc);


        //        //File.WriteAllBytes(pdfFilePath, pdf);
        //        return ""; 
 
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //    }
        //}

    }
} 