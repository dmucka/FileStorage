using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorageBL.Facades;
using FileStorageDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileStoragePL.Pages.Folders
{
    [RequireBasic]
    public class RemoveModel : PageModel
    {
        private readonly UserFacade _userFacade;
        private readonly FolderFacade _folderFacade;

        public RemoveModel(UserFacade userFacade, FolderFacade folderFacade)
        {
            _userFacade = userFacade;
            _folderFacade = folderFacade;
        }

        public User UserModel { get; set; }

        [BindProperty]
        public Folder FolderModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            FolderModel = await _folderFacade.GetFolderByIdAsync(id);

            if (FolderModel == null)
                return NotFound();

            // check that folder owner is correct
            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);
            if (FolderModel.Owner.Id != owner.Id)
                return Unauthorized();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var folder = await _folderFacade.GetFolderByIdAsync(id);

            if (folder == null)
                return NotFound();

            var returnFolder = folder.FolderId;

            // check that folder owner is correct
            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);
            if (folder.Owner.Id != owner.Id)
                return Unauthorized();


            // depth first traversal and recursive deletion
            var children = new List<Folder>();
            var stack = new Stack<Folder>();

            stack.Push(folder);

            while (stack.Count > 0)
            {
                var currentFolder = stack.Pop();

                if (currentFolder.Folders != null)
                    foreach (var subfolder in currentFolder.Folders)
                        stack.Push(await _folderFacade.GetFolderByIdAsync(subfolder.Id));

                children.Add(currentFolder);
            }

            foreach (var f in children)
                await _folderFacade.DeleteFolderAsync(f.Id);

            if (returnFolder == null)
            {
                return LocalRedirect("/");
            }
            else
            {
                return RedirectToPage("/Folders/Open", new { id });
            }
        }
    }
}
