using Microsoft.Extensions.Options;
using noche.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Repository
{
    public class DocFileSettings
    {
        public string foldername { get; set; }
        public string credentials { get; set; }
    }

    public interface IDocFile
    {

        Task<DocFileSettings> Get();

    }
    public class DocFileRepository : IDocFile
    {
        private readonly IOptions<Nochesettings> _settings;


        public DocFileRepository(IOptions<Nochesettings> settings)
        {
            _settings = settings;
        }
        public async Task<DocFileSettings> Get()
        {
            DocFileSettings docFileSettings = new DocFileSettings()
            {
                foldername = _settings.Value.dbxfolder,
                credentials = _settings.Value.dbxcredentials
            };
            return docFileSettings;
        }
    }
}
