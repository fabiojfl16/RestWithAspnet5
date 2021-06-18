﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithAspnet5.Business;
using RestWithAspnet5.Data.VO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RestWithAspnet5.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : ControllerBase
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType(200, Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            byte[] buffer = _fileBusiness.GetFile(fileName);

            if (buffer != null)
            {
                HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName)}".Replace(".", "");
                HttpContext.Response.Headers.Add("content-lenght", buffer.Length.ToString());
                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }

            return new ContentResult();
        }

        [HttpPost("uploadFile")]
        [ProducesResponseType(200, Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            var detail = await _fileBusiness.SaveFileToDisk(file);
            return new OkObjectResult(detail);
        }

        [HttpPost("uploadMultipleFiles")]
        [ProducesResponseType(200, Type = typeof(List<FileDetailVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> uploadMultipleFiles([FromForm] List<IFormFile> files)
        {
            var details = await _fileBusiness.SaveFilesToDisk(files);
            return new OkObjectResult(details);
        }
    }
}