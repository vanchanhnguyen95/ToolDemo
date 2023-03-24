using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SpeedWebAPI.Common.Enum;
using SpeedWebAPI.Common.Models;
using SpeedWebAPI.Common.Constants;
using SpeedWebAPI.Infrastructure;
using SpeedWebAPI.Models;
using SpeedWebAPI.Services.Base;
using SpeedWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using Catfood.Shapefile;

namespace SpeedWebAPI.Services
{
    #region interface
    public interface ISpeedProviderFileService : IBaseService<SpeedLimit>
    {
        /// <summary>
        /// Update List Speed Provider
        /// </summary>
        /// <param name="postedFile">upload file .txt</param>
        /// <returns></returns>
        Task<IResultFile<object>> UpdateListSpeedProvider(IFormFile postedFile);

        /// <summary>
        /// Get File List Speed From File Updload
        /// </summary>
        /// <param name="postedFile">upload file .txt</param>
        /// <returns></returns>
        Task<IResultFile<object>> GetFileListSpeedFromFileUpd(IFormFile postedFile);

        Task<IResultFile<object>> UpdSpeedProviderFromShpFile(string pathFile);
    }
    #endregion
    public class SpeedProviderFileService : ISpeedProviderFileService
    {
        public void Add(SpeedLimit obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(SpeedLimit obj)
        {
            throw new NotImplementedException();
        }

        public SpeedLimit Get(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SpeedLimit> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<SpeedLimit> GetAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IResultFile<object>> GetFileListSpeedFromFileUpd(IFormFile postedFile)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task<IResultFile<object>> UpdateListSpeedProvider(IFormFile postedFile)
        {
            throw new NotImplementedException();
        }

        public Task<IResultFile<object>> UpdSpeedProviderFromShpFile(string pathFile)
        {
            throw new NotImplementedException();
        }
    }

    //public class SpeedProviderFileService : BaseService<SpeedLimit, ApplicationDbContext>, ISpeedProviderFileService
    //{
    //    private readonly ISpeedLimitService _speedLimitService;
    //    [Obsolete]
    //    private IHostingEnvironment _environment;

    //    [Obsolete]
    //    public SpeedProviderFileService(ApplicationDbContext db, ISpeedLimitService speedLimitService, IHostingEnvironment environment) : base(db)
    //    {
    //        _speedLimitService = speedLimitService;
    //        _environment = environment;
    //    }

    //    public async Task<IResultFile<object>> UpdSpeedProviderFromShpFile(string pathFile)
    //    {
    //        try
    //        {
    //            if (string.IsNullOrEmpty(pathFile))
    //            {
    //                return ResultFile<object>.Error(pathFile, ErrMessage.NOT_FIND_UPD);
    //            }

    //            if (Path.GetExtension(pathFile) != ".shp")
    //            {
    //                return ResultFile<object>.Error(string.Empty, ErrMessage.UPD_FILE_FORMAT_SHP);
    //            }

    //            List<SpeedProviderUpLoadVm> listSpeed = await GetSpeedProviderFromShpFile(pathFile);

    //            if (!listSpeed.Any() || listSpeed.Count() == 0)
    //            {
    //                // Delete file temp
    //                //File.Delete(pathFile);
    //                return ResultFile<object>.Error(string.Empty, ErrMessage.UPD_FILE_FORMAT_SHP);
    //            }

    //            //if (listSpeed.Any() && listSpeed.Count() > 1000)
    //            //{
    //            //    // Delete file temp
    //            //    //File.Delete(pathFile);
    //            //    return ResultFile<object>.Error(string.Empty, ErrMessage.UPD_NUM_LINE_SHP);
    //            //}

    //            //await _speedLimitService.up(listSpeed);

    //            // Delete file temp
    //            //File.Delete(pathFile);
    //            return ResultFile<object>.Success(null, pathFile, Message.SUCCESS);
    //        }
    //        catch (Exception ex)
    //        {
    //            return ResultFile<object>.Error(ex.ToString());
    //        }
    //    }

    //    private async Task<List<SpeedProviderUpLoadVm>> GetSpeedProviderFromShpFile(string pathFile)
    //    {
    //        List<SpeedProviderUpLoadVm> lst = new List<SpeedProviderUpLoadVm>();

    //        // construct shapefile with the path to the .shp file
    //        using (Shapefile shapefile = new Shapefile(pathFile))
    //        {
    //            // enumerate all shapesx: trường hợp này dữ liệu đầu vào có tất cả shape.Type= Polyline
    //            foreach (Shape shape in shapefile)
    //            {
    //                if (shape.Type != ShapeType.PolyLine)
    //                    continue;

    //                long segmenId = Convert.ToInt64(shape.GetMetadata("segmentId"));
    //                foreach (PointD[] part in (shape as ShapePolyLine).Parts)
    //                {
    //                    // Thêm điểm đầu
    //                    lst.Add(new SpeedProviderUpLoadVm()
    //                    {
    //                        Lat = (decimal)part[0].Y,
    //                        Lng = (decimal)part[0].X,
    //                        Position = SpeedProviderCons.Position.START,
    //                        ProviderType = 1,
    //                        SegmentID = segmenId
    //                    });
    //                    // Thêm điểm cuối
    //                    lst.Add(new SpeedProviderUpLoadVm()
    //                    {
    //                        Lat = (decimal)part[1].Y,
    //                        Lng = (decimal)part[1].X,
    //                        Position = SpeedProviderCons.Position.END,
    //                        ProviderType = 1,
    //                        SegmentID = segmenId
    //                    });
    //                    segmenId = 0;
    //                }
    //            }

    //        }

    //        return lst;
    //    }

    //    public async Task<IResultFile<object>> UpdateListSpeedProvider(IFormFile postedFile)
    //    {
    //        try
    //        {
    //            if (Path.GetExtension(postedFile.FileName) != ".txt")
    //            {
    //                return ResultFile<object>.Error(string.Empty, ErrMessage.UPD_FILE_FORMAT_TXT);
    //            }

    //            string filePath = await GetLinkFileUpLoad(postedFile);

    //            if (string.IsNullOrEmpty(filePath))
    //            {
    //                // Delete file temp
    //                File.Delete(filePath);
    //                return ResultFile<object>.Error(filePath, ErrMessage.NOT_FIND_UPD);
    //            }

    //            List<SpeedProviderUpLoadVm> listSpeed = await GetSpeedProviderFromUpload(filePath);

    //            if (!listSpeed.Any() || listSpeed.Count() == 0)
    //            {
    //                // Delete file temp
    //                File.Delete(filePath);
    //                return ResultFile<object>.Error(string.Empty, ErrMessage.GET_DATA_FILE_TXT);
    //            }

    //            await _speedLimitService.UpdloadSpeedProvider(listSpeed);

    //            // Delete file temp
    //            File.Delete(filePath);

    //            return ResultFile<object>.Success(null, filePath, Message.SUCCESS);
    //        }
    //        catch (Exception ex)
    //        {
    //            return ResultFile<object>.Error(ex.ToString());
    //        }
    //    }

    //    public async Task<IResultFile<object>> GetFileListSpeedFromFileUpd(IFormFile postedFile)
    //    {
    //        try
    //        {
    //            if (Path.GetExtension(postedFile.FileName) != ".txt")
    //            {
    //                return ResultFile<object>.Error(string.Empty, ErrMessage.UPD_FILE_FORMAT_TXT);
    //            }

    //            // Get link FileUpLoad
    //            string filePath = await GetLinkFileUpLoadOutput(postedFile);

    //            if (string.IsNullOrEmpty(filePath))
    //            {
    //                // Delete file temp
    //                File.Delete(filePath);
    //                return ResultFile<object>.Error(filePath, ErrMessage.NOT_FIND_UPD);
    //            }

    //            // Get list SpeedProvider from File UpLoad
    //            List<SpeedProviderUpLoadVm> listSpeedUpload = await GetSpeedProviderFromUpload(filePath);

    //            if (!listSpeedUpload.Any() || listSpeedUpload.Count() == 0)
    //            {
    //                // Delete file temp
    //                File.Delete(filePath);
    //                return ResultFile<object>.Error(string.Empty, ErrMessage.GET_DATA_FILE_TXT);
    //            }

    //            // Get list SpeedProvider from Database
    //            List<SpeedProviderDbVm> listSpeedDb = await GetSpeedProviderFromDb(listSpeedUpload);

    //            if (!listSpeedDb.Any() || listSpeedDb.Count() == 0)
    //            {
    //                // Delete file temp
    //                File.Delete(filePath);
    //                return ResultFile<object>.Success(listSpeedDb,string.Empty, ErrMessage.NO_DATA_SPEED);
    //            }

    //            await WriteFileFromListUpd(listSpeedDb, filePath);
    //            return ResultFile<object>.Success(listSpeedDb,filePath, Message.GET_SPEED_SUCCESS);
    //        }
    //        catch (Exception ex)
    //        {
    //            return ResultFile<object>.Error(string.Empty, ex.ToString());
    //        }
    //    }


    //    #region private method

    //    private async Task<string> GetLinkFileUpLoad(IFormFile postedFile)
    //    {
    //        try
    //        {
    //            //Create the Directory.
    //            string pathFile = Path.Combine(_environment.WebRootPath, "Uploads\\");
    //            if (!Directory.Exists(pathFile))
    //            {
    //                Directory.CreateDirectory(pathFile);
    //            }

    //            //Fetch the File.
    //            //Microsoft.AspNetCore.Http.IFormFile fileInput = postedFile;

    //            //Fetch the File Name.
    //            //string fileName = Request.Form["fileName"] +Path.GetExtension(postedFile.FileName);
    //            string fileName = postedFile.FileName;

    //            //Save the File.
    //            using (FileStream stream = new FileStream(Path.Combine(pathFile, fileName), FileMode.Create))
    //            {
    //                await postedFile.CopyToAsync(stream);

    //                return pathFile + fileName;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return ex.ToString();
    //        }

    //    }

    //    private async Task<string> GetLinkFileUpLoadOutput(IFormFile postedFile)
    //    {
    //        try
    //        {
    //            //Create the Directory.
    //            string pathFile = Path.Combine(_environment.WebRootPath, SpeedProviderCons.FOLDER_UPLOADS);
    //            if (!Directory.Exists(pathFile))
    //            {
    //                Directory.CreateDirectory(pathFile);
    //            }

    //            //Fetch the File.
    //            //Microsoft.AspNetCore.Http.IFormFile fileInput = postedFile;

    //            //Fetch the File Name.
    //            //string fileName = Request.Form["fileName"] +Path.GetExtension(postedFile.FileName);

    //            var oldfileName = postedFile.FileName.Replace(Path.GetExtension(postedFile.FileName), "");

    //            string fileName = oldfileName + "_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day
    //                            + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second
    //                            + Path.GetExtension(postedFile.FileName);

    //            //Save the File.
    //            using (FileStream stream = new FileStream(Path.Combine(pathFile, fileName), FileMode.Create))
    //            {
    //                await postedFile.CopyToAsync(stream);

    //                return pathFile + fileName;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return ex.ToString();
    //        }

    //    }

    //    private async Task<List<SpeedProviderUpLoadVm>> GetSpeedProviderFromUpload(string filePath)
    //    {
    //        if (File.Exists(filePath))
    //        {
    //            // Read file using StreamReader. Reads file line by line  
    //            using (StreamReader file = new StreamReader(filePath))
    //            {
    //                string ln;
    //                int count = 0;
    //                List<SpeedProviderUpLoadVm> listUpload = new List<SpeedProviderUpLoadVm>();
    //                SpeedProviderUpLoadVm lineAdd;
    //                while ((ln = await file.ReadLineAsync()) != null)
    //                {
    //                    count++;
    //                    lineAdd = new SpeedProviderUpLoadVm();
    //                    List<string> linesUpload = ln.Split(',').ToList();

    //                    if (count < 2) continue; // Bỏ qua dòng header: Tên cột

    //                    // Lấy dữ liệu
    //                    lineAdd.SegmentID = Convert.ToInt64((linesUpload[(int)DataSpeedUpLoad.ColSegmentID]));
    //                    lineAdd.Lat = Convert.ToDecimal(linesUpload[(int)DataSpeedUpLoad.ColLat]);
    //                    lineAdd.Lng = Convert.ToDecimal(linesUpload[(int)DataSpeedUpLoad.ColLng]);
    //                    //lineAdd.Note = (linesUpload[(int)DataSpeedUpLoad.ColNote]);
    //                    listUpload.Add(lineAdd);
    //                }
    //                file.Close();

    //                return listUpload;
    //            }
    //        }
    //        return new List<SpeedProviderUpLoadVm>();
    //    }

    //    private async Task<List<SpeedProviderDbVm>> GetSpeedProviderFromDb(List<SpeedProviderUpLoadVm> listUpload)
    //    {
    //        List<SpeedProviderDbVm> listResult = new List<SpeedProviderDbVm>();

    //        if (!listUpload.Any())
    //        {
    //            return listResult;
    //        }

    //        SpeedProviderDbVm lineAdd;
    //        foreach (SpeedProviderUpLoadVm itemSpeed in listUpload)
    //        {
    //            var obj = await Db.SpeedLimits
    //                .Where(x => x.Lat == itemSpeed.Lat && x.Lng == itemSpeed.Lng && x.DeleteFlag == 0).AsNoTracking().FirstOrDefaultAsync();

    //            if (obj == null)
    //            {
    //                continue;
    //            }

    //            lineAdd = new SpeedProviderDbVm();
    //            lineAdd.Lat = obj.Lat;
    //            lineAdd.Lng = obj.Lng;
    //            lineAdd.SegmentID = obj.SegmentID ?? 0;
    //            lineAdd.MinSpeed = obj.MinSpeed;
    //            lineAdd.MaxSpeed = obj.MaxSpeed;

    //            listResult.Add(lineAdd);
    //        }

    //        return listResult;
    //    }

    //    private async Task<string> WriteFileFromListUpd(List<SpeedProviderDbVm> listSpeedDb, string filePath)
    //    {
    //        if (!listSpeedDb.Any())
    //        {
    //            return string.Empty;
    //        }

    //        StreamWriter writerheader = new StreamWriter(filePath, false);
    //        //string header = SpeedProviderCons.HEADER_FILE_SPEED_LIMIT;
    //        string header = SpeedProviderCons.HEADER_FILE_DOWLOAD_SPEED_LIMIT;
    //        writerheader.Write(header);
    //        writerheader.Close();

    //        foreach (SpeedProviderDbVm item in listSpeedDb)
    //        {
    //            // Read old data
    //            StreamReader reader = new StreamReader(filePath);
    //            string readedData = await reader.ReadToEndAsync();
    //            reader.Close();

    //            // Write  data
    //            StreamWriter writer = new StreamWriter(filePath, false);
    //            // write old data
    //            await writer.WriteLineAsync(readedData);

    //            // write new data
    //            string line = item.SegmentID + "," + item.Lng + "," + item.Lat
    //                 + "," + item.MinSpeed + "," + item.MaxSpeed;
    //            await writer.WriteAsync(line);
    //            writer.Close();
    //        }

    //        return filePath;

    //    }

    //    #endregion
    //}
}
