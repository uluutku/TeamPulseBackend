using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
//using HRAppBackend.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAppBackend.Infrastructure.Services.Photo
{
    public class PhotoService : IPhotoService
    {
        public string UploadFile(IFormFile myFile, string id)
        {
            var filename = GenerateFileName(myFile.FileName, id);
            var fileUrl = "";
            BlobContainerClient container = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=kartalphoto;AccountKey=Y7AdIXX3JVOlhjuIeUpXDEGYiiKZ23yZDCHapuvDynNhSkLfmuFOYEF8Hq+6Rf20q0C9nWxfafyk+AStuUGiMg==;EndpointSuffix=core.windows.net", "images");
            try
            {
                BlobClient blobClient = container.GetBlobClient(filename);
                using (Stream stream = myFile.OpenReadStream())
                {
                    blobClient.Upload(stream);
                }
                fileUrl = blobClient.Uri.AbsoluteUri;
            }
            catch (Exception ex) { }
            var result = fileUrl;
            return result;
        }

        public string UploadCompanyLogo(IFormFile companyLogo, string companyName, string logoFileName)
        {
			
			var fileUrl = "";
			BlobContainerClient container = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=kartalphoto;AccountKey=Y7AdIXX3JVOlhjuIeUpXDEGYiiKZ23yZDCHapuvDynNhSkLfmuFOYEF8Hq+6Rf20q0C9nWxfafyk+AStuUGiMg==;EndpointSuffix=core.windows.net", "images");
			try
			{
				BlobClient blobClient = container.GetBlobClient(logoFileName);
				using (Stream stream = companyLogo.OpenReadStream())
				{
					blobClient.Upload(stream);
				}
				fileUrl = blobClient.Uri.AbsoluteUri;
			}
			catch (Exception ex) { }
			var result = fileUrl;
			return result;
		}


		public string GenerateLogoFileName(string fileName, string companyName)
		{
			try
			{
				string strFileName = string.Empty;
				string[] strName = fileName.Split('.');
				strFileName = companyName + DateTime.Now.ToString("yyyyMMdd\\THHmmssfff") + "." + strName[strName.Length - 1];
				return strFileName;
			}
			catch (Exception ex)
			{
				return fileName;
			}
		}


		public string GenerateFileName(string fileName, string EmployeeId)
        {
            try
            {
                string strFileName = string.Empty;
                string[] strName = fileName.Split('.');
                strFileName = EmployeeId + DateTime.Now.ToString("yyyyMMdd\\THHmmssfff") + "." + strName[strName.Length - 1];
                return strFileName;
            }
            catch (Exception ex)
            {
                return fileName;
            }
        }
    }
}
