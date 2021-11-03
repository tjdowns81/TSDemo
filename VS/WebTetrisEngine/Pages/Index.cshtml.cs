using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebTetrisEngine.Classes;

namespace WebTetrisEngine.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHostEnvironment _environment;

        [BindProperty]
        public IFormFile TestFile { get; set; }
        [BindProperty]
        public List<string> TestFileOutput { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
            TestFileOutput = new List<string>();
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            try
            {
                if (TestFile == null || TestFile.Length == 0)
                {
                    return;
                }

                _logger.LogInformation($"Uploading {TestFile.FileName}.");
                string targetFileName = $"{_environment.ContentRootPath}/{TestFile.FileName}";

                var solver = new Solver();
                using (var stream = TestFile.OpenReadStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        TestFileOutput = await solver.SolveTestFile(reader);
                    }
                }
            }
            catch (Exception e)
            {
                if (e.StackTrace != null) TestFileOutput.Add("ERROR: " + e.StackTrace.ToString());
            }
        }
    }
}
