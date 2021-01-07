using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace APImages.Pages
{
    public class ImageModel : PageModel
    {
        private readonly ILogger<ImageModel> _logger;

        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

        private Random _random = new Random();


        public ImageModel(ILogger<ImageModel> logger, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public void OnGet()
        {
            // Build base path where the images are stored
            string basePath = Path.Combine(_env.WebRootPath, @"img");

            // Allowed file formats
            string[] extensions = { ".png", ".jpg" };

            // Get the files
            IEnumerable<string> files = Directory.EnumerateFiles(basePath, "*", SearchOption.AllDirectories).Where(x => extensions.Any(y => y == Path.GetExtension(x)));

            // Random :)
            string imagePath = files.ElementAt(_random.Next(0, files.Count()));
            imagePath = imagePath.Substring(basePath.Length + 1, imagePath.Length - basePath.Length -1);
            
            Response.Redirect($"img/{imagePath}");
        }
    }
}
