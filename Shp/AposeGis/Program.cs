using Aspose.Gis;
using Aspose.Gis.Geometries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AposeGis
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataDir = @"E:\Chanh\Shp\shapefile\ShapefileDemo\Data\1A5Line\";
            string path = dataDir + "Aspose_out.shp";

            #region  Create Point
            /* Create Point*/
            //
            //Console.WriteLine("Create ShapeFile!");
            //using (VectorLayer layer = VectorLayer.Create(dataDir + "Aspose_out.shp", Drivers.Shapefile))
            //{
            //    // add attributes before adding features
            //    layer.Attributes.Add(new FeatureAttribute("name", AttributeDataType.String));
            //    layer.Attributes.Add(new FeatureAttribute("age", AttributeDataType.Integer));
            //    layer.Attributes.Add(new FeatureAttribute("dob", AttributeDataType.DateTime));

            //    // case 1: sets values
            //    Feature firstFeature = layer.ConstructFeature();
            //    firstFeature.Geometry = new Point(33.97, -118.25);
            //    firstFeature.SetValue("name", "John");
            //    firstFeature.SetValue("age", 23);
            //    firstFeature.SetValue("dob", new DateTime(1982, 2, 5, 16, 30, 0));
            //    layer.Add(firstFeature);

            //    Feature secondFeature = layer.ConstructFeature();
            //    secondFeature.Geometry = new Point(35.81, -96.28);
            //    secondFeature.SetValue("name", "Mary");
            //    secondFeature.SetValue("age", 54);
            //    secondFeature.SetValue("dob", new DateTime(1984, 12, 15, 15, 30, 0));
            //    layer.Add(secondFeature);

            //    // case 2: sets new values for all of the attributes.
            //    Feature thirdFeature = layer.ConstructFeature();
            //    secondFeature.Geometry = new Point(34.81, -92.28);
            //    object[] data = new object[3] { "Alex", 25, new DateTime(1989, 4, 15, 15, 30, 0) };
            //    secondFeature.SetValues(data);
            //    layer.Add(thirdFeature);
            //}
            //Console.WriteLine("===================================");
            #endregion Create Point

            #region Create Polyline
            //Console.WriteLine("Create Polyline");
            //using (VectorLayer layer = VectorLayer.Create(dataDir + "Aspose_out2.shp", Drivers.Shapefile))
            //{
            //    // add attributes before adding features
            //    layer.Attributes.Add(new FeatureAttribute("name", AttributeDataType.String));
            //    layer.Attributes.Add(new FeatureAttribute("age", AttributeDataType.Integer));
            //    layer.Attributes.Add(new FeatureAttribute("dob", AttributeDataType.DateTime));

            //    Feature firstFeature = layer.ConstructFeature();
            //    firstFeature.Geometry  = new LinearRing();

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
            #endregion Create Polyline

            #region EditLayer

            //using (var layer = Drivers.Shapefile.EditLayer(path))
            //{
            //    var feature = layer.ConstructFeature();
            //    feature.SetValue<string>("name", "John 02");
            //    feature.Geometry = new LinearRing();
            //    layer.Add(feature);
            //}

            #endregion EditLayer

            #region Get Features Count In Layer
            // For complete examples and data files, please go to https://github.com/aspose-gis/Aspose.GIS-for-.NET
            //using (VectorLayer layer = VectorLayer.Open(dataDir + "1A5Line.shp", Drivers.Shapefile))
            //{
            //    Console.WriteLine("Total Features in this file: " + layer.Count);
            //}
            #endregion

            //Get Information about Layer Attributes
            //using (VectorLayer layer = VectorLayer.Open(dataDir + "1A5Line.shp", Drivers.Shapefile))
            //{
            //    Console.WriteLine("The layer has {0} attributes defined.\n", layer.Attributes.Count);
            //    foreach (FeatureAttribute attribute in layer.Attributes)
            //    {
            //        Console.WriteLine("Name: {0}", attribute.Name);
            //        Console.WriteLine("Data type: {0}", attribute.DataType);
            //        Console.WriteLine("Can be null: {0}", attribute.CanBeNull);
            //    }
            //}

            #region EditLayer

            //using (VectorLayer layer = VectorLayer.Open(dataDir + "1A5Line.shp", Drivers.Shapefile))
            //{
            //   if(layer.GeometryType == GeometryType.LineString)
            //    {
            //        Geometry item = (Geometry)layer;
            //    }    
            //    //foreach (var item in layer)
            //    //{
            //    //    Console.WriteLine(item.ToString());
            //    //}
            //}

            // Đọc thông tin từ Point
            using (var layer = Drivers.Shapefile.OpenLayer(dataDir + "1A5Line.shp"))
            {
                int count = layer.Count;

                for (int j = 0; j < count; j++)
                {
                    var line = layer[j].Geometry as Aspose.Gis.Geometries.LineString;
                    IPoint sPoint = line.StartPoint;
                    IPoint ePoint = line.EndPoint;

                    // Tính khoảng cách
                    double length = line.GetLength();
                    //double distance = sPoint.Dis;


                    //LineString line = new LineString();
                    foreach (IPoint point in line)
                    {
                        Console.WriteLine(point.X + "," + point.Y);
                    }


                    //double distance = polygon1.GetDistanceTo(new Aspose.Gis.Geometries.Point(32.33, -64.84));
                }

            }


            #endregion EditLayer
            Console.ReadLine();

        }
    }
}
