using Apose02.Models;
using Aspose.Gis;
using Aspose.Gis.Geometries;
using System;
using System.Collections.Generic;

namespace Apose02
{
    class Program
    {
       
        static void Main(string[] args)
        {
            //Aspose.Gis.License license = new Aspose.Gis.License();

            //license.SetLicense(@"E:\library\Aspose.GIS_22.3\license\LICENSE.lic");

            string dataDir = @"E:\Data_shp\QL1ATest100\";
            string path = dataDir + "100Line.shp";
            List<SpeedProviderUpLoadVm> lst = null;
            //var layer1 = Drivers.Shapefile.OpenLayer(@"E:\Data_shp\QL1ATest100\100Line.shp");
            var layer1 = Drivers.Shapefile.OpenLayer(@"E:\Data_shp\QL1A\1A.shp");
            int cnt = 500;
            lst = new List<SpeedProviderUpLoadVm>();

            while (cnt > 0)
            {
                // Đọc thông tin từ Point
                //using (var layer = Drivers.Shapefile.OpenLayer(@"E:\Data_shp\QL1ATest100\100Line.shp"))
                using (var layer = Drivers.Shapefile.OpenLayer(@"E:\Data_shp\QL1A\1A.shp"))
                {
                    //sum = layer.Count;
                    //int count = layer.Count;
                    //int count = 200;
                    
                    SpeedProviderUpLoadVm lineS = null;
                    SpeedProviderUpLoadVm lineE = null;
                    for (int j = 0; j < 50; j++)
                    {
                        // Lấy dữ liệu thông tin Field SegmentID
                        Feature feature = layer[j];
                        long segmentIValue = feature.GetValue<long>("SegmentID");

                        // Duyệt từng dòng line
                        var line = layer[j].Geometry as Aspose.Gis.Geometries.LineString;

                        // Thêm tọa độ điểm đầu
                        lineS = new SpeedProviderUpLoadVm() { Lat = line.StartPoint.Y, Lng = line.StartPoint.X, SegmentID = segmentIValue, Position = "S" };
                        // Thêm tọa độ điểm cuối
                        lineE = new SpeedProviderUpLoadVm() { Lat = line.EndPoint.Y, Lng = line.EndPoint.X, SegmentID = segmentIValue, Position = "E" };

                        lst.Add(lineS);
                        lst.Add(lineE);

                        lineS = null; lineE = null;

                        // Tính khoảng cách
                        //double length = line.GetLength();

                    }

                    cnt -= 50;

                }
            }    

            //// Đọc thông tin từ Point
            //using (var layer = Drivers.Shapefile.OpenLayer(@"E:\Data_shp\QL1ATest100\100Line.shp"))
            //{
            //    int count = layer.Count;
            //    lst = new List<SpeedProviderUpLoadVm>();
            //    SpeedProviderUpLoadVm lineS = null;
            //    SpeedProviderUpLoadVm lineE = null;
            //    for (int j = i; j < i; j++)
            //    {
            //        // Lấy dữ liệu thông tin Field SegmentID
            //        Feature feature = layer[j];
            //        long segmentIValue = feature.GetValue<long>("SegmentID");

            //        // Duyệt từng dòng line
            //        var line = layer[j].Geometry as Aspose.Gis.Geometries.LineString;
                    

            //        // Thêm tọa độ điểm đầu
            //        lineS = new SpeedProviderUpLoadVm() { Lat = line.StartPoint.Y, Lng = line.StartPoint.X, SegmentID = segmentIValue };
            //        // Thêm tọa độ điểm cuối
            //        lineE = new SpeedProviderUpLoadVm() { Lat = line.EndPoint.Y, Lng = line.EndPoint.X, SegmentID = segmentIValue };

            //        lst.Add(lineS);
            //        lst.Add(lineE);

            //        lineS = null;  lineE = null;

            //        // Tính khoảng cách
            //        //double length = line.GetLength();

            //    }

            //}
            Console.WriteLine($"Có {lst.Count} Segmend");

            Console.ReadLine();
        }
    }
}
