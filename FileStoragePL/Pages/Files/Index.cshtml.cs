using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.Files
{
    [RequireAdmin]
    public class IndexModel : PageModel
    {
        private readonly FileFacade _fileFacade;

        public IndexModel(FileFacade fileFacade)
        {
            _fileFacade = fileFacade;
        }

        public List<File> FileModel { get;set; }

        public async Task OnGetAsync()
        {
            FileModel = await _fileFacade.GetAllFilesAsync();
        }
    }
}
