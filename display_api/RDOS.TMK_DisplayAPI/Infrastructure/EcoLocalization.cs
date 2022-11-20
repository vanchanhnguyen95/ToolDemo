using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class EcoLocalization
    {
        public int Id { get; set; }
        public string ResourceId { get; set; }
        public string Value { get; set; }
        public string LocaleId { get; set; }
        public string ResourceSet { get; set; }
        public string PrincipalCode { get; set; }
        public string Type { get; set; }
        public byte[] BinFile { get; set; }
        public string TextFile { get; set; }
        public string FileName { get; set; }
        public string Comment { get; set; }
        public int ValueType { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
