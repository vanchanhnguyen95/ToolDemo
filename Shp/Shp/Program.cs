using Aspose.Gis;
using Aspose.Gis.Geometries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shp
{
    class Program
    {
        static void Main(string[] args)
        {

            //using (VectorLayer layer2 = VectorLayer.Create(@"E:\Project\Shp\1A.shp", Drivers.Shapefile))
            //{
            //    // Add attributes before adding features
            //    layer2.Attributes.Add(new FeatureAttribute("name", AttributeDataType.String));
            //    layer2.Attributes.Add(new FeatureAttribute("age", AttributeDataType.Integer));
            //    layer2.Attributes.Add(new FeatureAttribute("dob", AttributeDataType.DateTime));

            //    // Add feature and set values
            //    Feature firstFeature = layer2.ConstructFeature();
            //    firstFeature.Geometry = new Point(33.97, -118.25);
            //    firstFeature.SetValue("name", "John");
            //    firstFeature.SetValue("age", 23);
            //    firstFeature.SetValue("dob", new DateTime(1982, 2, 5, 16, 30, 0));
            //    layer2.Add(firstFeature);

            //    // Add another feature and set values
            //    Feature secondFeature = layer2.ConstructFeature();
            //    secondFeature.Geometry = new Point(35.81, -96.28);
            //    secondFeature.SetValue("name", "Mary");
            //    secondFeature.SetValue("age", 54);
            //    secondFeature.SetValue("dob", new DateTime(1984, 12, 15, 15, 30, 0));
            //    layer2.Add(secondFeature);
            //}


            //string path = Path.Combine(dataDir, "NewShapeFile_out.shp");
            //string path = @"E:\Project\Shp\1A.shp";
            string path = @"E:\Chanh\Shp\shapefile\ShapefileDemo\Data\1A5Line\1A5Line.shp";

            // Open a layer
            //var layer = Drivers.Shapefile.OpenLayer(path);

            //foreach (Feature feature in layer)
            //{
            //    foreach (var attribute in layer.Attributes)
            //    {
            //        // Show Attribute details
            //        Console.WriteLine(attribute.Name);
            //    }

            //    // Check for Point geometry
            //    if (feature.Geometry.GeometryType == GeometryType.Point)
            //    {
            //        // Read Points
            //        Point point = (Point)feature.Geometry;
            //        Console.WriteLine(point.AsText() + " X: " + point.X + " Y: " + point.Y);
            //        Console.WriteLine("---------------------");
            //    }
            //}


            // Lấy giá trị của Open attribute table
            //using (VectorLayer layer3 = VectorLayer.Open(path, Drivers.Shapefile))
            //{
            //    for (int i = 0; i < layer3.Count; i++)
            //    {
            //        Feature feature = layer3[i];
            //        Console.WriteLine("Entry {0} information\n ========================", i);

            //        // case 1
            //        string X1 = feature.GetValue<string>("X1"); // attribute name is case-sensitive
            //        Console.WriteLine("Attribute value for feature #{0} is: {1}, {2}", x1, x1, x1);
            //    }
            //}

            //using (VectorLayer source = VectorLayer.Open(path, Drivers.Shapefile))
            //{
            //    using (VectorLayer destination = VectorLayer.Create(@"E:\Project\Shp" + "01_out.shp", Drivers.Shapefile))
            //    {
            //        foreach (Feature sourceFeature in source)
            //        {
            //            Polygon polygon = (Polygon)sourceFeature.Geometry;
            //            LineString line = new LineString(polygon.ExteriorRing);
            //            Feature destinationFeature = destination.ConstructFeature();
            //            destinationFeature.Geometry = line;
            //            destination.Add(destinationFeature);
            //        }
            //    }
            //}

            // For complete examples and data files, please go to https://github.com/aspose-gis/Aspose.GIS-for-.NET
            //string path = Path.Combine(dataDir, "point_xyz_out", "point_xyz.shp");

            using (var layer = Drivers.Shapefile.EditLayer(path))
            {
                var feature = layer.ConstructFeature();
                feature.SetValue<int>("ID", 5);
                feature.Geometry = new Point(-5, 5) { Z = 2 };
                layer.Add(feature);
            }


            Console.ReadLine();
        }
    }
}
