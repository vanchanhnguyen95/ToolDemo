﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SpeedWebAPI.Common.Enum;
using SpeedWebAPI.Common.Models;
using SpeedWebAPI.Infrastructure;
using SpeedWebAPI.Models;
using SpeedWebAPI.Services.Base;
using SpeedWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWebAPI.Services
{
    public interface ISpeedUploadService : IBaseService<SpeedLimit>
    {
        string GetLinkFileUpLoad(IFormFile postedFile);
        Task<IResult<object>> UpdateListSpeedProvider(IFormFile postedFile);
    }

    public class SpeedUploadService : BaseService<SpeedLimit, ApplicationDbContext>, ISpeedUploadService
    {
        private readonly ISpeedLimitService _speedLimitService;
        [Obsolete]
        private IHostingEnvironment _environment;

        [Obsolete]
        public SpeedUploadService(ApplicationDbContext db, ISpeedLimitService speedLimitService, IHostingEnvironment environment) : base(db)
        {
            _speedLimitService = speedLimitService;
            _environment = environment;
        }

        public string GetLinkFileUpLoad(IFormFile postedFile)
        {
            try
            {
                //Create the Directory.
                string pathFile = Path.Combine(_environment.WebRootPath, "Uploads\\");
                if (!Directory.Exists(pathFile))
                {
                    Directory.CreateDirectory(pathFile);
                }

                //Fetch the File.
                //Microsoft.AspNetCore.Http.IFormFile fileInput = postedFile;

                //Fetch the File Name.
                //string fileName = Request.Form["fileName"] +Path.GetExtension(postedFile.FileName);
                string fileName = postedFile.FileName;

                //Save the File.
                using (FileStream stream = new FileStream(Path.Combine(pathFile, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);

                    return pathFile + fileName;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        public Task<IResult<object>> UpdateListSpeedProvider(IFormFile postedFile)
        {
            string linkTextFile = string.Empty;
            throw new NotImplementedException();
        }

        private List<SpeedProviderUpLoadVm> GetSpeedProviderFromUpload(string linkTextFile)
        {
            if (File.Exists(linkTextFile))
            {
                // Read file using StreamReader. Reads file line by line  
                using (StreamReader file = new StreamReader(linkTextFile))
                {
                    string ln;
                    int count = 0;
                    List<SpeedProviderUpLoadVm> listUpload = new List<SpeedProviderUpLoadVm>();
                    SpeedProviderUpLoadVm lineAdd;
                    while ((ln = file.ReadLine()) != null)
                    {
                        count++;
                        lineAdd = new SpeedProviderUpLoadVm();
                        List<string> linesUpload = ln.Split(',').ToList();

                        if (count < 2) continue; // Bỏ qua dòng header: Tên cột

                        // Lấy dữ liệu
                        lineAdd.SegmentID = Convert.ToInt64((linesUpload[(int)DataSpeedUpLoad.ColSegmentID]).ToString());
                        lineAdd.Lat = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad.ColLat]).ToString());
                        lineAdd.Lng = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad.ColLng]).ToString());
                        lineAdd.Note = (linesUpload[(int)DataSpeedUpLoad.ColNote]).ToString();
                        listUpload.Add(lineAdd);
                    }
                    file.Close();

                    return listUpload;
                }
            }
            return new List<SpeedProviderUpLoadVm>();
        }
    }
}
