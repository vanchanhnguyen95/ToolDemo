﻿using SpeedWebAPI.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedWebAPI.Models
{
    [Table("SpeedLimit")]
    public class SpeedLimit : DisAuditableEntity
    {
        [Key]
        [Column(Order = 1)]
        public double Lat { get; set; }// Y
        [Key]
        [Column(Order = 2)]
        public double Lng { get; set; } // X
        [Key]
        [Column(Order = 3)]
        [MaxLength(1)]
        public int? ProviderType { get; set; } = 1;//1:Navital; 2:VietMap
        [Key]
        [Column(Order = 4)]
        [MaxLength(50)]
        public string Position { get; set; }//S:Start, E:End: M-1, M-2: Middle-1,2,...

        public int? MinSpeed { get; set; } = 0;
        public int? MaxSpeed { get; set; } = 0;
        
        public bool? PointError { get; set; } = false;//True: Tọa độ cung cấp bị lỗi, False: Tọa độ cung cấp ko bị lỗi
        public long? SegmentID { get; set; }
        
    }
}
