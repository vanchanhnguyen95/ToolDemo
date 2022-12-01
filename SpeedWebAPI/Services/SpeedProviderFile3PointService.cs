using Microsoft.AspNetCore.Hosting;
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

                // Get List Segment from File UpLoad
                List<SpeedProviderUpLoad3PointVm> lstSegment = await GetSegmentFromUpload3Point(filePath);
                if (!lstSegment.Any() || lstSegment.Count() == 0)
                {
                    // Delete file temp
                    File.Delete(filePath);
                    return ResultFile<object>.Error(string.Empty, ErrMessage.GET_DATA_FILE_TXT);
                }

                List<SpeedProviderUpLoad3PointVm> listSpeedDb = await GetSpeedProvider3PointFromLstSegment(lstSegment);
                if (!listSpeedDb.Any() || listSpeedDb.Count() == 0)
                {
                    // Delete file temp
                    File.Delete(filePath);
                    return ResultFile<object>.Success(listSpeedDb, string.Empty, ErrMessage.NO_DATA_SPEED);
                }

                //return ResultFile<object>.Success(listSpeedDb, string.Empty, ErrMessage.NO_DATA_SPEED);

                //// Get List SegmentID from File UpLoad
                //List<long> lstSegmentId = await GetSegmentIDFromUpload3Point(filePath);
                //if (!lstSegmentId.Any() || lstSegmentId.Count() == 0)
                //{
                //    // Delete file temp
                //    File.Delete(filePath);
                //    return ResultFile<object>.Error(string.Empty, ErrMessage.GET_DATA_FILE_TXT);
                //}

                //List<SpeedProviderUpLoad3PointVm> listSpeedDb = await GetSpeedProvider3PointFromLstSegmentID(lstSegmentId);
                //if (!listSpeedDb.Any() || listSpeedDb.Count() == 0)
                //{
                //    // Delete file temp
                //    File.Delete(filePath);
                //    return ResultFile<object>.Success(listSpeedDb, string.Empty, ErrMessage.NO_DATA_SPEED);
                //}

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
                string line = item.SegmentID.ToString()
                //+ "," + item.Lng1.ToString() + "," + item.Lat1.ToString()
                //+ "," + item.Lng2.ToString() + "," + item.Lat2.ToString()
                //+ "," + item.Lng3.ToString() + "," + item.Lat3.ToString()
                + "," + item.MinSpeed1.ToString() + "," + item.MaxSpeed1.ToString()
                + "," + item.MinSpeed2.ToString() + "," + item.MaxSpeed2.ToString()
                + "," + item.MinSpeed3.ToString() + "," + item.MaxSpeed3.ToString(); 

                await writer.WriteAsync(line);
                writer.Close();
            }

            return filePath;

        }

        private async Task<List<SpeedProviderUpLoad3PointVm>> GetSegmentFromUpload3Point(string filePath)
        {
            if (File.Exists(filePath))
            {
                // Read file using StreamReader. Reads file line by line  
                using (StreamReader file = new StreamReader(filePath))
                {
                    string ln;
                    int count = 0;

                    List<SpeedProviderUpLoad3PointVm> lstSegment = new List<SpeedProviderUpLoad3PointVm>();
                    SpeedProviderUpLoad3PointVm lineSegment;

                    while ((ln = await file.ReadLineAsync()) != null)
                    {
                        count++;
                        List<string> linesUpload = ln.Split(',').ToList();
                        lineSegment = new SpeedProviderUpLoad3PointVm();

                        if (count < 2) continue; // Bỏ qua dòng header: Tên cột

                        long segmentId = Convert.ToInt64((linesUpload[(int)DataSpeedUpLoad3Point.ColSegmentID]).ToString());
                        var itCheck = lstSegment.Where(x => x.SegmentID == segmentId).FirstOrDefault();

                        // Có rồi thì không lấy SegmentID
                        if (itCheck != null)
                            continue;

                        lineSegment.SegmentID = Convert.ToInt64((linesUpload[(int)DataSpeedUpLoad3Point.ColSegmentID]).ToString());
                        lineSegment.Lat1 = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad3Point.ColLat1]).ToString());
                        lineSegment.Lng1 = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad3Point.ColLng1]).ToString());
                        lineSegment.Lat2 = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad3Point.ColLat2]).ToString());
                        lineSegment.Lng2 = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad3Point.ColLng2]).ToString());
                        lineSegment.Lat3 = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad3Point.ColLat3]).ToString());
                        lineSegment.Lng3 = Convert.ToDouble((linesUpload[(int)DataSpeedUpLoad3Point.ColLng3]).ToString());

                        //lstSegment.Add(Convert.ToInt64((linesUpload[(int)DataSpeedUpLoad3Point.ColSegmentID]).ToString()));
                        lstSegment.Add(lineSegment);
                        itCheck = null;
                        lineSegment = null;
                    }
                    file.Close();

                    return lstSegment;
                }
            }
            return new List<SpeedProviderUpLoad3PointVm>();
        }

        private async Task<List<long>> GetSegmentIDFromUpload3Point(string filePath)
        {
            if (File.Exists(filePath))
            {
                // Read file using StreamReader. Reads file line by line  
                using (StreamReader file = new StreamReader(filePath))
                {
                    string ln;
                    int count = 0;

                    List<long> lstSegmentID = new List<long>();

                    while ((ln = await file.ReadLineAsync()) != null)
                    {
                        count++;
                        List<string> linesUpload = ln.Split(',').ToList();

                        if (count < 2) continue; // Bỏ qua dòng header: Tên cột

                        long segmentId = Convert.ToInt64((linesUpload[(int)DataSpeedUpLoad3Point.ColSegmentID]).ToString());

                        var itCheck = lstSegmentID.Where(x => x == segmentId).FirstOrDefault();

                        // Có rồi thì không lấy SegmentID
                        if (itCheck != 0)
                            continue;

                        lstSegmentID.Add(Convert.ToInt64((linesUpload[(int)DataSpeedUpLoad3Point.ColSegmentID]).ToString()));
                    }
                    file.Close();

                    return lstSegmentID;
                }
            }
            return new List<long>();
        }


        private async Task<List<SpeedProviderUpLoad3PointVm>> GetSpeedProvider3PointFromLstSegment(List<SpeedProviderUpLoad3PointVm> lstSegment)
        {
            List<SpeedProviderUpLoad3PointVm> lstResult = new List<SpeedProviderUpLoad3PointVm>();
            SpeedProviderUpLoad3PointVm line;

            foreach (SpeedProviderUpLoad3PointVm item in lstSegment)
            {
                line = new SpeedProviderUpLoad3PointVm();
                line.SegmentID = item.SegmentID;


                // Lấy dữ liệu MinSpeed, MaxSpeed trong Db để dowload
                // S
                var itemS = await Db.SpeedLimit3Points.Where(s => s.SegmentID == item.SegmentID && s.Position == SpeedProviderCons.Position.START).FirstOrDefaultAsync();
                if (itemS != null)
                {
                    line.Lat1 = item.Lat1 == 0 ? itemS.Lat : item.Lat1;
                    line.Lng1 = item.Lng1 == 0 ? itemS.Lng : item.Lng1;
                    line.MinSpeed1 = itemS.MinSpeed;
                    line.MaxSpeed1 = itemS.MaxSpeed;
                }

                // M
                var itemM = await Db.SpeedLimit3Points.Where(s => s.SegmentID == item.SegmentID && s.Position == SpeedProviderCons.Position.MID).FirstOrDefaultAsync();
                if (itemM != null)
                {
                    line.Lat2 = item.Lat2 == 0 ? itemM.Lat : item.Lat2;
                    line.Lng2 = item.Lng2 == 0 ? itemM.Lng : item.Lng2;
                    line.MinSpeed2 = itemM.MinSpeed;
                    line.MaxSpeed2 = itemM.MaxSpeed;
                }

                // E
                var itemE = await Db.SpeedLimit3Points.Where(s => s.SegmentID == item.SegmentID && s.Position == SpeedProviderCons.Position.END).FirstOrDefaultAsync();
                if (itemE != null)
                {
                    line.Lat3 = item.Lat3 == 0 ? itemE.Lat : item.Lat3;
                    line.Lng3 = item.Lng3 == 0 ? itemE.Lng : item.Lng3;
                    line.MinSpeed3 = itemE.MinSpeed;
                    line.MaxSpeed3 = itemE.MaxSpeed;
                }
                lstResult.Add(line);

                itemS = null;
                itemM = null;
                itemE = null;
                line = null;
            }
            return lstResult;
        }

        private async Task<List<SpeedProviderUpLoad3PointVm>> GetSpeedProvider3PointFromLstSegmentID(List<long> lstSegmentID)
        {
            List<SpeedProviderUpLoad3PointVm> lstResult = new List<SpeedProviderUpLoad3PointVm>();
            SpeedProviderUpLoad3PointVm line;

            foreach (long item in lstSegmentID)
            {
                line = new SpeedProviderUpLoad3PointVm();
                line.SegmentID = item;


                // Lấy dữ liệu MinSpeed, MaxSpeed trong Db để dowload
                // S
                var itemS = await Db.SpeedLimit3Points.Where(s => s.SegmentID == item && s.Position == SpeedProviderCons.Position.START).FirstOrDefaultAsync();
                if(itemS != null)
                {
                    line.MinSpeed1 = itemS.MinSpeed;
                    line.MaxSpeed1 = itemS.MaxSpeed;

                    //line.Lat1 = itemS.Lat;
                    //line.Lng1 = itemS.Lng;
                }

                // M
                var itemM = await Db.SpeedLimit3Points.Where(s => s.SegmentID == item && s.Position == SpeedProviderCons.Position.MID).FirstOrDefaultAsync();
                if (itemM != null)
                {
                    line.MinSpeed2 = itemM.MinSpeed;
                    line.MaxSpeed2 = itemM.MaxSpeed;

                    //line.Lat2 = itemM.Lat;
                    //line.Lng2 = itemM.Lng;
                }

                // E
                var itemE = await Db.SpeedLimit3Points.Where(s => s.SegmentID == item && s.Position == SpeedProviderCons.Position.END).FirstOrDefaultAsync();
                if (itemE != null)
                {
                    line.MinSpeed3 = itemE.MinSpeed;
                    line.MaxSpeed3 = itemE.MaxSpeed;

                    //line.Lat3 = itemE.Lat;
                    //line.Lng3 = itemE.Lng;
                }
                lstResult.Add(line);

                itemS = null;
                itemM = null;
                itemE = null;
                line = null;
            }  
            return lstResult;
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
