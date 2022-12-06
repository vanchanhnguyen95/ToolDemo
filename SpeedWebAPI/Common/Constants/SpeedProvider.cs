namespace SpeedWebAPI.Common.Constants
{
    public static class SpeedProviderCons
    {
        public const string FOLDER_UPLOADS = "Uploads\\";
        public const string HEADER_FILE_SPEED_LIMIT = "SegmentID,Lng,Lat,MinSpeed,MaxSpeed";
        public const string HEADER_FILE_DOWLOAD_SPEED_LIMIT = "SegmentID,ET_X,ET_Y,MinSpeed,MaxSpeed";
        //public const string HEADER_FILE_UPD_3_POINT = "SegmentID,X1,Y1,X2,Y2,X3,Y3,MinSpeed1,MaxSpeed1,MinSpeed2,MaxSpeed2,MinSpeed3,MaxSpeed3";
        public const string HEADER_FILE_UPD_3_POINT = "SegmentID,MinSpeed1,MaxSpeed1,MinSpeed2,MaxSpeed2,MinSpeed3,MaxSpeed3";

        public static class Position
        {
            public const string START = "S";// Start Point
            public const string MID = "M";// Mid Point
            public const string END = "E";// End Point
            public const string ORTHER = "0";// Orther Point
        }
    }
}
