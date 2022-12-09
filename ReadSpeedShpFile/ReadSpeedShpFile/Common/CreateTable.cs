using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadSpeedShpFile.Common
{
    public class Common
    {
        public class SpeedLimit3PointData
        {
            public double Lat { get; set; }// Y
            public double Lng { get; set; }// X
            public int? MinSpeed { get; set; } = 0;
            public int? MaxSpeed { get; set; } = 0;
            public int? ProviderType { get; set; } = 1;//1:Navital; 2:VietMap
            public bool? PointError { get; set; } = false;//True: Tọa độ cung cấp bị lỗi, False: Tọa độ cung cấp ko bị lỗi
            public long? SegmentID { get; set; }
            public string Position { get; set; }

            public DateTime? CreatedDate { get; set; }
            public DateTime? UpdatedDate { get; set; }

            public string CreatedBy { get; set; }

            public string UpdatedBy { get; set; }
            public int? DeleteFlag { get; set; } = 0;
            public int? UpdateCount { get; set; } = 0;
        }
        public class SpeedLimit3PointDataCollection : List<SpeedLimit3PointData>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {
                SqlDataRecord ret = new SqlDataRecord(
                    new SqlMetaData("Lat", SqlDbType.Float),
                    new SqlMetaData("Lng", SqlDbType.Float),
                    new SqlMetaData("ProviderType", SqlDbType.Int),
                    new SqlMetaData("MinSpeed", SqlDbType.Int),
                    new SqlMetaData("MaxSpeed", SqlDbType.Int),
                    new SqlMetaData("PointError", SqlDbType.Bit),
                    new SqlMetaData("SegmentID", SqlDbType.BigInt),
                    new SqlMetaData("Position", SqlDbType.VarChar,1),
                    new SqlMetaData("CreatedDate", SqlDbType.DateTime),
                    new SqlMetaData("UpdatedDate", SqlDbType.DateTime),
                    new SqlMetaData("CreatedBy", SqlDbType.VarChar),
                    new SqlMetaData("UpdatedBy", SqlDbType.VarChar),
                    new SqlMetaData("DeleteFlag", SqlDbType.Int),
                    new SqlMetaData("UpdateCount", SqlDbType.Int)
                    );

                foreach (SpeedLimit3PointData data in this)
                {
                    ret.SetDouble(0, (double)data.Lat) ;
                    ret.SetDouble(0, (double)data.Lng);
                    ret.SetInt32(0, (int)data.ProviderType);
                    ret.SetInt32(0, (int)data.MinSpeed);
                    ret.SetInt32(0, (int)data.MaxSpeed);
                    ret.SetBoolean(0, (bool)data.PointError);
                    ret.SetInt64(0, (long)data.SegmentID);
                    ret.SetString(0, data.Position);
                    ret.SetDateTime(0, data.CreatedDate??DateTime.Now);
                    ret.SetDateTime(0, data.UpdatedDate??DateTime.Now);
                    ret.SetString(0, data.CreatedBy);
                    ret.SetString(0, data.UpdatedBy);
                    ret.SetInt32(0, (int)data.DeleteFlag);
                    ret.SetInt32(0, (int)data.UpdateCount);
                    yield return ret;
                }
            }
        }

        public static DataTable CreateTableSpeedLimit3Point()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Lat", typeof(double));
            dt.Columns.Add("Lng", typeof(double));
            dt.Columns.Add("ProviderType", typeof(Int32));
            dt.Columns.Add("MinSpeed", typeof(Int32));
            dt.Columns.Add("MaxSpeed", typeof(Int32));
            dt.Columns.Add("PointError", typeof(bool));
            dt.Columns.Add("SegmentID", typeof(Int64));
            dt.Columns.Add("Position", typeof(string));
            dt.Columns.Add("CreatedDate", typeof(DateTime));
            dt.Columns.Add("UpdatedDate", typeof(DateTime));
            dt.Columns.Add("CreatedBy", typeof(string));
            dt.Columns.Add("UpdatedBy", typeof(string));
            dt.Columns.Add("DeleteFlag", typeof(Int32));
            dt.Columns.Add("UpdateCount", typeof(Int32));
            return dt;
        }
    }

    
}
