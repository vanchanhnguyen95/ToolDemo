using Aspose.Gis;
using Aspose.Gis.Formats.Shapefile;
using Aspose.Gis.Geometries;
using Aspose.Gis.SpatialReferencing;
using Catfood.Shapefile;
using ReadSpeedShpFile.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace ReadSpeedShpFile
{
    class Program
    {
        static void Main(string[] args)
        {

            //SqlConnection con;
            //SqlDataReader reader;
            //try
            //{
            //    long segmentID;
            //    con = new SqlConnection(Properties.Settings.Default.ConnectStringDefault);
            //    con.Open();
            //    Console.WriteLine("Enter Employee Id");
            //    segmentID = long.Parse(Console.ReadLine());
            //    reader = new SqlCommand("select * from SpeedLimit3Point where SegmentID=" + segmentID, con).ExecuteReader();
            //    if (reader.HasRows)
            //    {
            //        while (reader.Read())
            //        {
            //            Console.WriteLine(String.Format("{0} \t\t\t | {1} \t | {2}",
            //                reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("No rows found.");
            //    }
            //    reader.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //Aspose.Gis.License license = new Aspose.Gis.License();

            //license.SetLicense(@"E:\library\Aspose.GIS_22.3\license\LICENSE.lic");

            //string dataDir = @"E:\Data_shp\QL1ATest100\";
            //string path = dataDir + "100Line.shp";
            //List<SpeedProviderUpLoadVm> lst = null;
            ////var layer1 = Drivers.Shapefile.OpenLayer(@"E:\Data_shp\QL1ATest100\100Line.shp");
            //var layer1 = Aspose.Gis.Drivers.Shapefile.OpenLayer(@"E:\Data_shp\QL1A\1A.shp");
            //int cnt = 500;
            //lst = new List<SpeedProviderUpLoadVm>();

            //while (cnt > 0)
            //{
            //    // Đọc thông tin từ Point
            //    //using (var layer = Drivers.Shapefile.OpenLayer(@"E:\Data_shp\QL1ATest100\100Line.shp"))
            //    using (var layer = Aspose.Gis.Drivers.Shapefile.OpenLayer(@"E:\Data_shp\QL1A\1A.shp"))
            //    {
            //        //count = layer.Count;

            //        for (int j = 0; j < 50; j++)
            //        {
            //            // Lấy dữ liệu thông tin Field SegmentID
            //            Aspose.Gis.Feature feature = layer[j];
            //            long segmentIValue = feature.GetValue<long>("SegmentID");

            //            // Duyệt từng dòng line
            //            var line = layer[j].Geometry as Aspose.Gis.Geometries.LineString;

            //            // Thêm tọa độ điểm đầu
            //            lst.Add(new SpeedProviderUpLoadVm() { Lat = line.StartPoint.Y, Lng = line.StartPoint.X, SegmentID = segmentIValue, Position = "S" });
            //            // Thêm tọa độ điểm cuối
            //            lst.Add(new SpeedProviderUpLoadVm() { Lat = line.EndPoint.Y, Lng = line.EndPoint.X, SegmentID = segmentIValue, Position = "E" });

            //            // Tính khoảng cách
            //            //double length = line.GetLength();

            //        }

            //        cnt -= 50;

            //    }
            //}

            //Console.WriteLine($"Có {lst.Count} Segmend");

            // Pass the path to the shapefile in as the command line argument
            //if ((args.Length == 0) || (!File.Exists(args[0])))
            //{
            //    Console.WriteLine("Usage: ShapefileDemo <shapefile.shp>");
            //    return;
            //}
            //string path = @"E:\Data_shp\1A2000Line\1A2000Line.shp";
            string path = @"E:\Data_shp\QL1ATest\1A.shp";
            List<SpeedProviderUpLoadVm> lst = new List<SpeedProviderUpLoadVm>();

            if ((!File.Exists(path)))
            {
                Console.WriteLine("Usage: ShapefileDemo <shapefile.shp>");
                return;
            }

            // construct shapefile with the path to the .shp file
            using (Shapefile shapefile = new Shapefile(path))
            {
                // enumerate all shapesx: trường hợp này dữ liệu đầu vào có tất cả shape.Type= Polyline
                foreach (Shape shape in shapefile)
                {
                    if (shape.Type != ShapeType.PolyLine)
                        continue;

                    long segmenId = Convert.ToInt64(shape.GetMetadata("segmentId"));
                    foreach (PointD[] part in (shape as ShapePolyLine).Parts)
                    {
                        // Thêm điểm đầu
                        lst.Add(new SpeedProviderUpLoadVm() { Lat = part[0].Y, Lng = part[0].X, Position = "S", ProviderType = 1, SegmentID = segmenId });
                        // Thêm điểm cuối
                        lst.Add(new SpeedProviderUpLoadVm() { Lat = part[1].Y, Lng = part[1].X, Position = "E", ProviderType = 1, SegmentID = segmenId });
                        segmenId = 0;
                    }
                }

            }


            Console.WriteLine($"Total:  { lst.Count()}");
            Console.WriteLine("----------------------------------------");

            // Create SpeedLimit Data Table
            DataTable speedTable = Common.Common.CreateTableSpeedLimit3Point();
            foreach (SpeedProviderUpLoadVm item in lst)
            {
                speedTable.Rows.Add(item.Lat, item.Lng, item.ProviderType, 0, 0,
                    false, item.SegmentID, item.Position, DateTime.Now, null, "UploadFile", null, 0, 0);
            }
                
            SqlConnection con;
            con = new SqlConnection(Properties.Settings.Default.ConnectStringDefault);
            con.Open();

            SqlCommand cmd = new SqlCommand("dbo.Ins_SpeedLimit3Point", con);
            cmd.CommandType = CommandType.StoredProcedure;

            //Pass table Valued parameter to Store Procedure
            SqlParameter sqlParam = cmd.Parameters.AddWithValue("@SpeedLimit3Point", speedTable);
            sqlParam.SqlDbType = SqlDbType.Structured;

            cmd.ExecuteNonQuery();
            con.Close();

            Console.WriteLine("Exuce store proceduce success");

            /* Create Point*/
            // For complete examples and data files, please go to https://github.com/aspose-gis/Aspose.GIS-for-.NET
            var parameters = new ProjectedSpatialReferenceSystemParameters
            {
                Name = "WGS 84 / World Mercator",
                Base = SpatialReferenceSystem.Wgs84,
                ProjectionMethodName = "Mercator_1SP",
                LinearUnit = Unit.Meter,
                XAxis = new Axis("Easting", AxisDirection.East),
                YAxis = new Axis("Northing", AxisDirection.North),
                AxisesOrder = ProjectedAxisesOrder.XY,
            };
            parameters.AddProjectionParameter("central_meridian", 0);
            parameters.AddProjectionParameter("scale_factor", 1);
            parameters.AddProjectionParameter("false_easting", 0);
            parameters.AddProjectionParameter("false_northing", 0);

            var projectedSrs = SpatialReferenceSystem.CreateProjected(parameters, Identifier.Epsg(3395));


            Console.WriteLine("Create ShapeFile!");
            //using (var layer = VectorLayer.Create(@"E:\OutputFile\1A_out1.shp", new ShapefileOptions(), projectedSrs))
            using (var layer = Drivers.Shapefile.CreateLayer(@"E:\OutputFile\1A_out1.shp", new ShapefileOptions(), projectedSrs))
            {
                // add attributes before adding features
                layer.Attributes.Add(new FeatureAttribute("SegmentID", AttributeDataType.Long));
                //layer.Attributes.Add(new FeatureAttribute("X", AttributeDataType.Double));//
                //layer.Attributes.Add(new FeatureAttribute("Y", AttributeDataType.Double));
                layer.Attributes.Add(new FeatureAttribute("MinSpeed", AttributeDataType.Integer));
                layer.Attributes.Add(new FeatureAttribute("MaxSpeed", AttributeDataType.Integer));

                Feature feature = null;
                int cnt = 0;
                foreach (SpeedProviderUpLoadVm item in lst)
                {
                    if (cnt > 50)
                        break;

                    feature = layer.ConstructFeature();
                    feature.Geometry = new Point(item.Lat, item.Lng);
                    object[] data = new object[3] { item.SegmentID, 0,0 };
                    feature.SetValues(data);
                    //feature.SetValue("SegmentID", item.SegmentID);
                    //feature.SetValue("MinSpeed", 0);
                    //feature.SetValue("MaxSpeed", 0);
                    layer.Add(feature);
                    cnt++;
                }

                //// case 1: sets values
                //Feature firstFeature = layer.ConstructFeature();
                //firstFeature.Geometry = new Point(33.97, -118.25);
                //firstFeature.SetValue("name", "John");
                //firstFeature.SetValue("age", 23);
                //firstFeature.SetValue("dob", new DateTime(1982, 2, 5, 16, 30, 0));
                //layer.Add(firstFeature);

                //Feature secondFeature = layer.ConstructFeature();
                //secondFeature.Geometry = new Point(35.81, -96.28);
                //secondFeature.SetValue("name", "Mary");
                //secondFeature.SetValue("age", 54);
                //secondFeature.SetValue("dob", new DateTime(1984, 12, 15, 15, 30, 0));
                //layer.Add(secondFeature);

                //// case 2: sets new values for all of the attributes.
                //Feature thirdFeature = layer.ConstructFeature();
                //secondFeature.Geometry = new Point(34.81, -92.28);
                //object[] data = new object[3] { "Alex", 25, new DateTime(1989, 4, 15, 15, 30, 0) };
                //secondFeature.SetValues(data);
                //layer.Add(thirdFeature);
            }
            Console.WriteLine("===================================");
            Console.WriteLine("===================================");

            //#region Create Polyline
            //Console.WriteLine("Create Polyline");
            //using (VectorLayer layer = VectorLayer.Create(@"E:\OutputFile\1A_out1.shp", Drivers.Shapefile))
            //{
            //    // add attributes before adding features
            //    layer.Attributes.Add(new FeatureAttribute("name", AttributeDataType.String));
            //    layer.Attributes.Add(new FeatureAttribute("age", AttributeDataType.Integer));
            //    layer.Attributes.Add(new FeatureAttribute("dob", AttributeDataType.DateTime));

            //    Feature firstFeature = layer.ConstructFeature();
            //    firstFeature.Geometry = new LinearRing();

            //    LinearRing ring = new LinearRing();
            //    ring.AddPoint(50.02, 36.22);
            //    ring.AddPoint(49.99, 36.26);
            //    ring.AddPoint(49.97, 36.23);
            //    ring.AddPoint(49.98, 36.17);

            //    firstFeature.Geometry = new LinearRing(ring);
            //    firstFeature.SetValue("name", "John");
            //    firstFeature.SetValue("age", 23);
            //    firstFeature.SetValue("dob", new DateTime(1982, 2, 5, 16, 30, 0));
            //    layer.Add(firstFeature);
            //}
            //Console.WriteLine("===================================");
            //#endregion Create Polyline


            Console.ReadLine();
        }
    }
}
