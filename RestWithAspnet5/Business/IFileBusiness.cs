using Microsoft.AspNetCore.Http;
using RestWithAspnet5.Data.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithAspnet5.Business
{
    public interface IFileBusiness
    {
        byte[] GetFile(string filename);

        Task<FileDetailVO> SaveFileToDisk(IFormFile file);

        Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files);
    }
}