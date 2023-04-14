using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileStorageBL.Facades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace FileStoragePL.Pages.Files
{
    [RequireBasic]
    public class DownloadModel : PageModel
    {
        private readonly FileFacade _fileFacade;
        private readonly UserFacade _userFacade;
        private readonly IWebHostEnvironment _environment;

        public DownloadModel(IWebHostEnvironment environment, UserFacade userFacade, FileFacade fileFacade)
        {
            _fileFacade = fileFacade;
            _userFacade = userFacade;
            _environment = environment;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var file = await _fileFacade.GetFileByIdAsync(id);

            if (file == null)
                return NotFound();

            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);
            var fileOwner = await _fileFacade.GetOwner(file.Id);

            if (fileOwner.Id != owner.Id)
                return Unauthorized();

            if (!System.IO.File.Exists(file.Link))
                return NotFound();

            var path = System.IO.Path.Combine(_environment.ContentRootPath, file.Link);

            return PhysicalFile(path, "application/octet-stream", file.Name);
        }
    }
}
