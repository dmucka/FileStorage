using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileStorageDAL.Models;
using FileStorageBL.Facades;
using Microsoft.Extensions.Configuration;
using SampleApp.Utilities;
using FileStorageBL.DTOs;
using FileStoragePL.Pages.Models;

namespace FileStoragePL.Pages.Files
{
    [RequireBasic]
    public class UploadModel : PageModel
    {
        private readonly long _fileSizeLimit;
        private readonly string _targetFilePath;

        private readonly FileFacade _fileFacade;
        private readonly UserFacade _userFacade;
        private readonly FileVersionFacade _fileVersionFacade;
        private readonly FolderFacade _folderFacade;

        [BindProperty]
        public UploadedFile FileUpload { get; set; }
        public FileVersion FileVersionModel { get; set; }

        public UploadModel(IConfiguration config, UserFacade userFacade, FileFacade fileFacade, FileVersionFacade fileVersionFacade, FolderFacade folderFacade)
        {
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            _targetFilePath = config.GetValue<string>("StoredFilesPath");

            _fileVersionFacade = fileVersionFacade;
            _fileFacade = fileFacade;
            _userFacade = userFacade;
            _folderFacade = folderFacade;
        }

        public string Result { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            FileVersionModel = await _fileVersionFacade.GetFileVersionByIdAsync(id);

            if (FileVersionModel == null)
                return NotFound();

            var folder = await _folderFacade.GetFolderByIdAsync(FileVersionModel.VersionedFile.FolderId);
            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);

            if (folder.Owner.Id != owner.Id)
                return Unauthorized();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var fileVersion = await _fileVersionFacade.GetFileVersionByIdAsync(id);

            if (fileVersion == null)
                return NotFound();

            var folder = await _folderFacade.GetFolderByIdAsync(fileVersion.VersionedFile.FolderId);
            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);

            if (folder.Owner.Id != owner.Id)
                return Unauthorized();

            if (!ModelState.IsValid)
            {
                Result = "Please correct the form.";

                return Page();
            }

            var formFileContent =
                await FileHelpers.ProcessFormFile<File>(FileUpload.FormFile, ModelState, _fileSizeLimit);

            var trustedFileNameForFileStorage = System.IO.Path.GetRandomFileName();
            var filePath = System.IO.Path.Combine(
                _targetFilePath, trustedFileNameForFileStorage);

            using (var fileStream = System.IO.File.Create(filePath))
            {
                await fileStream.WriteAsync(formFileContent);
            }

            await _fileFacade.CreateFileAsync(new FileDto
            {
                Name = FileUpload.FormFile.FileName,
                Link = filePath,
                Size = (int)FileUpload.FormFile.Length,
                FileVersionId = fileVersion.Id
            });

            return RedirectToPage("/VersionedFiles/Open", new { id = fileVersion.VersionedFileId });
        }
    }
}
