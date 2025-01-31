using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestaurantMVC.Models;

namespace RestaurantMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public AdminImagensController(IWebHostEnvironment hostingEnviroment,
            IOptions<ConfigurationImagens> myConfiguration)
        {
            _hostingEnviroment = hostingEnviroment;
            _myConfig = myConfiguration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if(files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: No file(s) selected";
                return View(ViewData);
            }
            if(files.Count > 10)
            {
                ViewData["Erro"] = "Error: File count exceeded the limit (10)";
                return View(ViewData);
            }

            long size = files.Sum(files => files.Length);

            var filePathsName = new List<string>();

            var filePath = Path.Combine(_hostingEnviroment.WebRootPath, _myConfig.NomePastaImagensProdutos);

            foreach(var formFile in files)
            {
                if(formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".gif")
                    || formFile.FileName.Contains(".png"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);
                    filePathsName.Add(fileNameWithPath);

                    using (var steam = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(steam);
                    }
                }
            }
            ViewData["Resultado"] = $"{files.Count} files have been sent to the server," + $" with total size of : {size} bytes";

            ViewBag.Arquivos = filePathsName;
            return View(ViewData);
        }
        public IActionResult GetImagens()
        {
            FileManagerModel model = new FileManagerModel();
            var userImagesPath = Path.Combine(_hostingEnviroment.WebRootPath,
                _myConfig.NomePastaImagensProdutos);

            DirectoryInfo dir = new DirectoryInfo(userImagesPath);

            FileInfo[] files = dir.GetFiles();

            model.PathImagesProduto = _myConfig.NomePastaImagensProdutos;

            if(files.Length == 0)
            {
                ViewData["Erro"] = $"No file found in {userImagesPath}";
            }
            model.Files = files;
            return View(model);
        }

        public IActionResult Deletefile(string fname)
        {
            string _imagemDeleta = Path.Combine(_hostingEnviroment.WebRootPath,
                _myConfig.NomePastaImagensProdutos + "\\", fname);

            if ((System.IO.File.Exists(_imagemDeleta)))
            {
                System.IO.File.Delete(_imagemDeleta);

                ViewData["Deletado"] = $"File(s) {_imagemDeleta} sucessfully deleted";
            }
            return View("index");
        }
    }
}
