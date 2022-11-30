﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SpeedWebAPI.Common.Constants;
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
    #region interface
    public interface ISpeedProviderFile3PointService : IBaseService<SpeedLimit3Point>
    {
        Task<IResultFile<object>> UpdateListSpeedProvider3Point(IFormFile postedFile);

        Task<IResultFile<object>> GetFileListSpeedFromFileUpd3Point(IFormFile postedFile);
    }
    #endregion

    public class SpeedProviderFile3PointService : BaseService<SpeedLimit3Point, ApplicationDbContext>, ISpeedProviderFile3PointService
    {
        private readonly ISpeedLimit3PointService _speedLimit3PointService;
        [Obsolete]
        private IHostingEnvironment _environment;

        [Obsolete]
        public SpeedProviderFile3PointService(ApplicationDbContext db, ISpeedLimit3PointService speedLimit3PointService, IHostingEnvironment environment) : base(db)
        {
            _speedLimit3PointService = speedLimit3PointService;
            _environment = environment;
        }

        public async Task<IResultFile<object>> UpdateListSpeedProvider3Point(IFormFile postedFile)
        {
            try
            {
                if (Path.GetExtension(postedFile.FileName) != ".txt")
                {
                    return ResultFile<object>.Error(string.Empty, ErrMessage.UPD_FILE_FORMAT_TXT);
                }

                string filePath = await GetLinkFileUpLoad(postedFile);

                if (string.IsNullOrEmpty(filePath))
                {
                    // Delete file temp
                    File.Delete(filePath);
                    return ResultFile<object>.Error(filePath, ErrMessage.NOT_FIND_UPD);
                }

                List<SpeedProviderUpLoadVm> listSpeed = await GetSpeedProviderFromUpload3Point(filePath);

                if (!listSpeed.Any() || listSpeed.Count() == 0)
                {
                    // Delete file temp
                    File.Delete(filePath);
                    return ResultFile<object>.Error(string.Empty, ErrMessage.GET_DATA_FILE_TXT);
                }

                await _speedLimit3PointService.UpdloadSpeedProvider3Point(listSpeed);

                // Delete file temp
                File.Delete(filePath);

                return ResultFile<object>.Success(null, filePath, Message.SUCCESS);
            }
            catch (Exception ex)
            {
                return ResultFile<object>.Error(ex.ToString());
            }
        }

        public async Task<IResultFile<object>> GetFileListSpeedFromFileUpd3Point(IFormFile postedFile)
        {
            try
            {
                if (Path.GetExtension(postedFile.FileName) != ".txt")
                {
                    return ResultFile<object>.Error(string.Empty, ErrMessage.UPD_FILE_FORMAT_TXT);
                }

                // Get link FileUpLoad
                string filePath = await GetLinkFileUpLoadOutput(postedFile);

                if (string.IsNullOrEmpty(filePath))
                {
                    // Delete file temp
                    File.Delete(filePath);
                    return ResultFile<object>.Error(filePath, ErrMessage.NOT_FIND_UPD);
                }

                // Get list SpeedProvider from File UpLoad
                List<SpeedProviderUpLoadVm> listSpeedUpload = await GetSpeedProviderFromUpload3Point(filePath);

                if (!listSpeedUpload.Any() || listSpeedUpload.Count() == 0)
                {
                    // Delete file temp
                    File.Delete(filePath);
                    return ResultFile<object>.Error(string.Empty, ErrMessage.GET_DATA_FILE_TXT);
                }

                // Get list SpeedProvider from Database
                List<SpeedProviderUpLoad3PointVm> listSpeedDb = await GetSpeedProviderFromDb3Point(listSpeedUpload);

                if (!listSpeedDb.Any() || listSpeedDb.Count() == 0)
                {
                    // Delete file temp
                    File.Delete(filePath);
                    return ResultFile<object>.Success(listSpeedDb, string.Empty, ErrMessage.NO_DATA_SPEED);
                }

                //await WriteFileFromListUpd3Point(listSpeedDb, filePath);
                await WriteFileFromListUpd3Point(listSpeedDb, filePath);
                return ResultFile<object>.Success(listSpeedDb, filePath, Message.GET_SPEED_SUCCESS);
            }
            catch (Exception ex)
            {
                return ResultFile<object>.Error(string.Empty, ex.ToString());
            }
        }

        #region private method

        private async Task<string> GetLinkFileUpLoad(IFormFile postedFile)
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
                    await postedFile.CopyToAsync(stream);

                    return pathFile + fileName;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        private async Task<string> GetLinkFileUpLoadOutput(IFormFile postedFile)
        {
            try
            {
                //Create the Directory.
                string pathFile = Path.Combine(_environment.WebRootPath, SpeedProviderCons.FOLDER_UPLOADS);
                if (!Directory.Exists(pathFile))
                {
                    Directory.CreateDirectory(pathFile);
                }

                //Fetch the File.
                //Microsoft.AspNetCore.Http.IFormFile fileInput = postedFile;

                //Fetch the File Name.
                //string fileName = Request.Form["fileName"] +Path.GetExtension(postedFile.FileName);

                var oldfileName = postedFile.FileName.Replace(Path.GetExtension(postedFile.FileName), "");

                string fileName = oldfileName + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()
                                + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()
                                + Path.GetExtension(postedFile.FileName);

                //Save the File.
                using (FileStream stream = new FileStream(Path.Combine(pathFile, fileName), FileMode.Create))
                {
                    await postedFile.CopyToAsync(stream);

                    return pathFile + fileName;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        private async Task<List<SpeedProviderUpLoad3PointVm>> GetSpeedProviderFromDb3Point(List<SpeedProviderUpLoadVm> listUpload)
        {
            // Lấy ra danh sách chỉ chứa các SegmentID

            // Duyệt danh sách chứa SegmentID
            // Tìm kiếm trong Db để lấy dữ liệu 3 điểm đầu, cuối giữa
            // Tạo line để export ra file txt

            List<SpeedProviderUpLoad3PointVm> listResult = new List<SpeedProviderUpLoad3PointVm>();

            if (!listUpload.Any())
            {
                return listResult;
            }

            SpeedProviderUpLoad3PointVm lineAdd;
            foreach (SpeedProviderUpLoadVm itemSpeed in listUpload)
            {
                // Lấy ra danh sách các điểm trong 1 line
                List<SpeedLimit3Point> lstFind = await Db.SpeedLimit3Points.Where(x => x.SegmentID == itemSpeed.SegmentID).ToListAsync();


                //var obj = await Db.SpeedLimit3Points
                //    .Where(x => x.Lat == itemSpeed.Lat && x.Lng == itemSpeed.Lng && x.DeleteFlag == 0).AsNoTracking().FirstOrDefaultAsync();

                //if (obj == null)
                //{
                //    continue;
                //}

                //SpeedProviderUpLoad3PointVm item = null;
                //if (listResult != null && listResult.Any())
                //{
                //    item = listResult.Where(x => x.SegmentID == itemSpeed.SegmentID).SingleOrDefault();
                //}    

                //if(item == null)
                //{
                //    lineAdd = new SpeedProviderUpLoad3PointVm();

                //    lineAdd.SegmentID = obj.SegmentID ?? 0;

                //    if (itemSpeed.Position == SpeedProviderCons.Position.START)
                //    {
                //        lineAdd.Lat1 = obj.Lat;
                //        lineAdd.Lng1 = obj.Lng;
                //        lineAdd.MinSpeed1 = obj.MinSpeed;
                //        lineAdd.MaxSpeed1 = obj.MaxSpeed;

                //    }
                //    if (itemSpeed.Position == SpeedProviderCons.Position.MID)
                //    {
                //        lineAdd.Lat2 = obj.Lat;
                //        lineAdd.Lng2 = obj.Lng;
                //        lineAdd.MinSpeed2 = obj.MinSpeed;
                //        lineAdd.MaxSpeed2 = obj.MaxSpeed;
                //    }
                //    if (itemSpeed.Position == SpeedProviderCons.Position.END)
                //    {
                //        lineAdd.Lat3 = obj.Lat;
                //        lineAdd.Lng3 = obj.Lng;
                //        lineAdd.MinSpeed3 = obj.MinSpeed;
                //        lineAdd.MaxSpeed3 = obj.MaxSpeed;
                //    }

                //    lineAdd.Position = obj.Position;
                //    listResult.Add(lineAdd);
                //}
                //else
                //{
                //    // Remove item old
                //    lineAdd = new SpeedProviderUpLoad3PointVm();

                //    lineAdd = item;
                //    listResult.Remove(item);
                //    lineAdd.SegmentID = obj.SegmentID ?? 0;
                //    if (itemSpeed.Position == SpeedProviderCons.Position.START)
                //    {
                //        lineAdd.Lat1 = obj.Lat;
                //        lineAdd.Lng1 = obj.Lng;
                //        lineAdd.MinSpeed1 = obj.MinSpeed;
                //        lineAdd.MaxSpeed1 = obj.MaxSpeed;

                //    }
                //    if (itemSpeed.Position == SpeedProviderCons.Position.MID)
                //    {
                //        lineAdd.Lat2 = obj.Lat;
                //        lineAdd.Lng2 = obj.Lng;
                //        lineAdd.MinSpeed2 = obj.MinSpeed;
                //        lineAdd.MaxSpeed2 = obj.MaxSpeed;
                //    }
                //    if (itemSpeed.Position == SpeedProviderCons.Position.END)
                //    {
                //        lineAdd.Lat3 = obj.Lat;
                //        lineAdd.Lng3 = obj.Lng;
                //        lineAdd.MinSpeed3 = obj.MinSpeed;
                //        lineAdd.MaxSpeed3 = obj.MaxSpeed;
                //    }

                //    lineAdd.Position = obj.Position;

                //    listResult.Add(lineAdd);
                //    //lineAdd = item;
                //}

               
            }

            return listResult;
        }


        private async Task<string> WriteFileFromListUpd3Point(List<SpeedProviderUpLoad3PointVm> listSpeedDb, string filePath)
        {
            if (!listSpeedDb.Any())
            {
                return string.Empty;
            }

            StreamWriter writerheader = new StreamWriter(filePath, false);
            //string header = SpeedProviderCons.HEADER_FILE_SPEED_LIMIT;
            string header = SpeedProviderCons.HEADER_FILE_UPD_3_POINT;
            writerheader.Write(header);
            writerheader.Close();

            foreach (SpeedProviderUpLoad3PointVm item in listSpeedDb)
            {
                // Read old data
                StreamReader reader = new StreamReader(filePath);
                string readedData = await reader.ReadToEndAsync();
                reader.Close();

                // Write  data
                StreamWriter writer = new StreamWriter(filePath, false);
                // write old data
                await writer.WriteLineAsync(readedData);

                // write new data
                string line = item.SegmentID.ToString() + "," + item.Lng1.ToString() + "," + item.Lat1.ToString()
                + "," + item.Lng2.ToString() + "," + item.Lat2.ToString()
                + "," + item.Lng3.ToString() + "," + item.Lat3.ToString()
                + "," + item.MinSpeed1.ToString() + "," + item.MaxSpeed1.ToString()
                + "," + item.MinSpeed2.ToString() + "," + item.MaxSpeed2.ToString()
                + "," + item.MinSpeed3.ToString() + "," + item.MaxSpeed3.ToString(); 

                await writer.WriteAsync(line);
                writer.Close();
            }

            return filePath;

        }

        private async Task<List<SpeedProviderUpLoadVm>> GetSpeedProviderFromUpload3Point(string filePath)
        {
            if (File.Exists(filePath))
            {
                // Read file using StreamReader. Reads file line by line  
                using (StreamReader file = new StreamReader(filePath))
                {
                    string ln;
                    int count = 0;
                    List<SpeedProviderUpLoadVm> listUpload = new List<SpeedProviderUpLoadVm>();
                    SpeedProviderUpLoadVm lineS;
                    SpeedProviderUpLoadVm lineM;
                    SpeedProviderUpLoadVm lineE;
                    while ((ln = await file.ReadLineAsync()) != null)
                    {
                        count++;
                        lineS = new SpeedProviderUpLoadVm();
                        lineM = new SpeedProviderUpLoadVm();
                        lineE = new SpeedProviderUpLoadVm();
                        List<string> linesUpload = ln.Split(',').ToList();

                        if (count < 2) continue; // Bỏ qua dòng header: Tên cột

                        // Lấy dữ liệu
                        lineS.SegmentID = Convert.ToInt64((linesUpload[(int)DataSpeedUpLoad3Point.ColSegmentID]).ToString());
                        lineS.Lat = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad3Point.ColLat1]).ToString());
                        lineS.Lng = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad3Point.ColLng1]).ToString());
                        lineS.Position = SpeedProviderCons.Position.START;

                        lineM.SegmentID = Convert.ToInt64((linesUpload[(int)DataSpeedUpLoad3Point.ColSegmentID]).ToString());
                        lineM.Lat = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad3Point.ColLat2]).ToString());
                        lineM.Lng = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad3Point.ColLng2]).ToString());
                        lineM.Position = SpeedProviderCons.Position.MID;

                        lineE.SegmentID = Convert.ToInt64((linesUpload[(int)DataSpeedUpLoad3Point.ColSegmentID]).ToString());
                        lineE.Lat = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad3Point.ColLat3]).ToString());
                        lineE.Lng = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad3Point.ColLng3]).ToString());
                        lineE.Position = SpeedProviderCons.Position.END;

                        listUpload.Add(lineS);
                        listUpload.Add(lineM);
                        listUpload.Add(lineE);
                    }
                    file.Close();

                    return listUpload;
                }
            }
            return new List<SpeedProviderUpLoadVm>();
        }


        #endregion
    }
}
