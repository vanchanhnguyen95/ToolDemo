﻿using BAGeocoding.Entity.Enum.MapObject;
using System;

namespace BAGeocoding.Entity.Public
{
    public class RPBLAddressResult
    {
        public float Lng { get; set; }
        public float Lat { get; set; }
        public short Building { get; set; }
        public string Road { get; set; }
        public string Commune { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public bool Accurate { get; set; }
        public byte MinSpeed { get; set; }
        public byte MaxSpeed { get; set; }
        public int DataExt { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }

        public RPBLAddressResult() { }

        public RPBLAddressResult(RPBLAddressResult other)
        {
            Lng = other.Lng;
            Lat = other.Lat;
            Building = other.Building;
            Road = other.Road;
            Commune = other.Commune;
            District = other.District;
            Province = other.Province;
            Accurate = other.Accurate;
            MinSpeed = other.MinSpeed;
            MaxSpeed = other.MaxSpeed;
            DataExt = other.DataExt;
            ProvinceID = other.ProvinceID;
            DistrictID = other.DistrictID;
        }

        public bool DataExtGet(EnumMOBSegmentDataExt dataExt)
        {
            return ((DataExt & (int)Math.Pow(2, (int)dataExt)) > 0);
        }
    }

    public class RPBLAddressResultV2
    {
        public double Lng { get; set; }
        public double Lat { get; set; }
        public short Building { get; set; }
        public string Road { get; set; }
        public string Commune { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public bool Accurate { get; set; }
        public byte MinSpeed { get; set; }
        public byte MaxSpeed { get; set; }
        public int DataExt { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }

        public RPBLAddressResultV2() { }
        public RPBLAddressResultV2(RPBLAddressResultV2 orther) {
            Lng = orther.Lng;
            Lat = orther.Lat;
            Building = orther.Building;
            Road = orther.Road;
            Commune = orther.Commune;
            District = orther.District;
            Province = orther.Province;
            Accurate = orther.Accurate;
            MinSpeed = orther.MinSpeed;
            MaxSpeed = orther.MaxSpeed;
            DataExt = orther.DataExt;
            ProvinceID = orther.ProvinceID;
            DistrictID = orther.DistrictID;
        }

        //public RPBLAddressResultV2(RPBLAddressResultV2 other)
        //{
        //    Lng = other.Lng;
        //    Lat = other.Lat;
        //    Building = other.Building;
        //    Road = other.Road;
        //    Commune = other.Commune;
        //    District = other.District;
        //    Province = other.Province;
        //    Accurate = other.Accurate;
        //    MinSpeed = other.MinSpeed;
        //    MaxSpeed = other.MaxSpeed;
        //    DataExt = other.DataExt;
        //    ProvinceID = other.ProvinceID;
        //    DistrictID = other.DistrictID;
        //}

        public bool DataExtGet(EnumMOBSegmentDataExt dataExt)
        {
            return ((DataExt & (int)Math.Pow(2, (int)dataExt)) > 0);
        }
    }

    public class RPBLAddressResultV3
    {
        public double Lng { get; set; }
        public double Lat { get; set; }
        public short Building { get; set; }
        public string Road { get; set; }
        public string Commune { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }

        public RPBLAddressResultV3() { }
        public RPBLAddressResultV3(RPBLAddressResultV2 orther)
        {
            Lng = orther.Lng;
            Lat = orther.Lat;
            Building = orther.Building;
            Road = orther.Road;
            Commune = orther.Commune;
            District = orther.District;
            Province = orther.Province;

            if (!string.IsNullOrEmpty(orther.Commune))
                Address = orther.Commune + ", ";

            if (!string.IsNullOrEmpty(orther.District))
                Address += orther.District + ", ";

            if (!string.IsNullOrEmpty(orther.District))
                Address += orther.Province;
        }
    }

    public class PBLAddressResult
    {
        public float Lng { get; set; }
        public float Lat { get; set; }
        public short Building { get; set; }
        public string Road { get; set; }
        public string Commune { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public bool Accurate { get; set; }

        public PBLAddressResult() { }

        public PBLAddressResult(PBLAddressResult other)
        {

        }

        public PBLAddressResult(FPBLAddressResult other)
        {
            Lng = other.Lng;
            Lat = other.Lat;
            Building = other.Building;
            Road = other.Road;
            Commune = other.Commune;
            District = other.District;
            Province = other.Province;
            Accurate = other.Accurate;
        }
        public PBLAddressResult(RPBLAddressResult other)
        {
            Lng = other.Lng;
            Lat = other.Lat;
            Building = other.Building;
            Road = other.Road;
            Commune = other.Commune;
            District = other.District;
            Province = other.Province;
            Accurate = other.Accurate;
        }
    }

    public class PBLAddressResultV2
    {
        public double Lng { get; set; }
        public double Lat { get; set; }
        public short Building { get; set; }
        public string Road { get; set; }
        public string Commune { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public bool Accurate { get; set; }

        public PBLAddressResultV2() { }

        public PBLAddressResultV2(PBLAddressResultV2 other)
        {

        }

        public PBLAddressResultV2(FPBLAddressResultV2 other)
        {
            Lng = other.Lng;
            Lat = other.Lat;
            Building = other.Building;
            Road = other.Road;
            Commune = other.Commune;
            District = other.District;
            Province = other.Province;
            Accurate = other.Accurate;
        }
        public PBLAddressResultV2(RPBLAddressResultV2 other)
        {
            Lng = other.Lng;
            Lat = other.Lat;
            Building = other.Building;
            Road = other.Road;
            Commune = other.Commune;
            District = other.District;
            Province = other.Province;
            Accurate = other.Accurate;
        }
    }

    public class PBLAddressResultV3
    {
        public double Lng { get; set; }
        public double Lat { get; set; }
        public short Building { get; set; }
        public string Road { get; set; }
        public string Commune { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }

        public PBLAddressResultV3() { }

        
        public PBLAddressResultV3(RPBLAddressResultV2 other)
        {
            Lng = other.Lng;
            Lat = other.Lat;
            Building = other.Building;
            Road = other.Road;
            Commune = other.Commune;
            District = other.District;
            Province = other.Province;

            if (!string.IsNullOrEmpty(other.Commune))
                Address = other.Commune + ", ";

            if (!string.IsNullOrEmpty(other.District))
                Address += other.District + ", ";

            if (!string.IsNullOrEmpty(other.Province))
                Address += other.Province;
        }
    }

    public class SPBLAddressResult
    {
        public float Lng { get; set; }
        public float Lat { get; set; }
        public short Building { get; set; }
        public string Road { get; set; }
        public string Commune { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public bool Accurate { get; set; }
        public byte MinSpeed { get; set; }
        public byte MaxSpeed { get; set; }
        public bool Highway { get; set; }

        public SPBLAddressResult() { }

        public SPBLAddressResult(SPBLAddressResult other)
        {
            Lng = other.Lng;
            Lat = other.Lat;
            Building = other.Building;
            Road = other.Road;
            Commune = other.Commune;
            District = other.District;
            Province = other.Province;
            Accurate = other.Accurate;
            MinSpeed = other.MinSpeed;
            MaxSpeed = other.MaxSpeed;
            Highway = other.Highway;
        }

        public SPBLAddressResult(RPBLAddressResult other)
        {
            Lng = other.Lng;
            Lat = other.Lat;
            Building = other.Building;
            Road = other.Road;
            Commune = other.Commune;
            District = other.District;
            Province = other.Province;
            Accurate = other.Accurate;
            MinSpeed = other.MinSpeed;
            MaxSpeed = other.MaxSpeed;

            Highway = other.DataExtGet(EnumMOBSegmentDataExt.HighWay);
        }
    }

    public class FPBLAddressResult : PBLAddressResult
    {
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
        public byte MinSpeed { get; set; }
        public byte MaxSpeed { get; set; }

        public FPBLAddressResult() : base() { }

        public FPBLAddressResult(RPBLAddressResult other) : base(other)
        {
            ProvinceID = other.ProvinceID;
            DistrictID = other.DistrictID;
            MinSpeed = other.MinSpeed;
            MaxSpeed = other.MaxSpeed;
        }
    }

    public class FPBLAddressResultV2 : PBLAddressResultV2
    {
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
        public byte MinSpeed { get; set; }
        public byte MaxSpeed { get; set; }

        public FPBLAddressResultV2() : base() { }

        public FPBLAddressResultV2(RPBLAddressResultV2 other) : base(other)
        {
            ProvinceID = other.ProvinceID;
            DistrictID = other.DistrictID;
            MinSpeed = other.MinSpeed;
            MaxSpeed = other.MaxSpeed;
        }
    }
}