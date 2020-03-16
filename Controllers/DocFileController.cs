using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noche.Models;
using noche.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace noche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocFileController : ControllerBase
    {
        private DBX dbx { get; set; } = new DBX();
        private ResponseModel rm { get; set; } = new ResponseModel();

        private readonly IDocFile _repository;

        public DocFileController(IDocFile ie) { _repository = ie; }

        async Task Upload(DropboxClient dbx, string folder, string file, string content)
        {
            using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                var updated = await dbx.Files.UploadAsync(
                    folder + "/" + file,
                    WriteMode.Overwrite.Instance,
                    body: mem);
                Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, updated.Rev);
            }
        }

        [HttpPost]
        public async Task<ResponseModel> Post(ICollection<IFormFile> files)
        {

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var postedFile = file;
                    string tmpname = Util.ConvertToTimestamp();
                    tmpname = tmpname + Path.GetExtension(postedFile.FileName);
                    dbx.fileName = tmpname;
                    dbx.hpf = file.OpenReadStream();

                    var tmp = Task.Run((Func<Task>)_dbxRun);
                    tmp.Wait();
                }
            }
            return rm;
        }

        async Task _dbxRun()
        {
            var dbxsettings = await _repository.Get();
            dbx.folderDBX = dbxsettings.foldername;
            dbx.tokenDBX = dbxsettings.credentials;

            try
            {
                using (dbx.dbx = new DropboxClient(dbx.tokenDBX))
                {
                    string remotePath = string.Concat("/", dbx.folderDBX, "/", dbx.fileName);
                    // var full = await _dbx.Users.GetCurrentAccountAsync();
                    var list = await dbx.dbx.Files.ListFolderAsync(string.Empty);
                    var checkFolderExist = list.Entries.Where(x => x.Name == "products").FirstOrDefault() == null;
                    if (checkFolderExist)
                        await dbx.dbx.Files.CreateFolderAsync("/" + dbx.folderDBX, false);

                    var _tmp = await dbx.dbx.Files.UploadAsync(remotePath, body: dbx.hpf);

                    var result = await dbx.dbx.Sharing.CreateSharedLinkWithSettingsAsync(remotePath);
                    var url = result.Url;
                    url = url.Replace("www", "dl");
                    url = url.Replace("?dl=0", "");
                    dbx.linkDBX = url;
                    dbx.flag = true;
                    rm.href = result.Url;
                    rm.response = true;
                    rm.result = dbx.linkDBX;
                    rm.SetResponse(rm.response);
                }
            }
            catch (Exception ex)
            {
                rm.message = ex.Message;
                rm.response = false;
            }
        }
    }


    public class DBX
    {
        public DropboxClient dbx { get; set; }
        public byte[] bytes { get; set; }
        public Stream hpf { get; set; }

        public string fileName { get; set; }
        public bool flag { get; set; }
        public string folderDBX { get; set; }
        public string tokenDBX { get; set; }
        public string linkDBX { get; set; }
        public string errorDBX { get; set; }
    }
}