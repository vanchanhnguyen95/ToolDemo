using RDOS.TMK_DisplayAPI.Models.External.Enum;
using RDOS.TMK_DisplayAPI.Models.Paging;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Models.External
{
    public class CustomerShiptoModel
    {
        public Guid Id { get; set; }
        public string ShiptoCode { get; set; }
        public string ShiptoName { get; set; }
        public string Avatar { get; set; }
        public Status Status { get; set; }
        public string BusinessStatus { get; set; }
        public string ClassType { get; set; }
        public string Address { get; set; }
        public Guid? Wards { get; set; }
        public Guid? District { get; set; }
        public Guid? City { get; set; }
        public Guid? Province { get; set; }
        public Guid? State { get; set; }
        public Guid? Region { get; set; }
        public Guid Country { get; set; }
        public string Street { get; set; }
        public string DeptNo { get; set; }
        public double Longtiue { get; set; }
        public double Lattitue { get; set; }
        public bool IsChecked { get; set; }

        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid CustomerInfomationId { get; set; }
        public Guid MainContactId { get; set; }
        public List<CustomerShiptoContactModel> CustomerShiptoContacts { get; set; }
        public List<CustomerDmsAttributeUpsertModel> CustomerDmsAttributes { get; set; }
    }

    public class CustomerShiptoUpsertRequest
    {
        public Guid? Id { get; set; }
        public string ShiptoCode { get; set; }
        public string ShiptoName { get; set; }
        public string Avatar { get; set; }
        public Status Status { get; set; }
        public string BusinessStatus { get; set; }
        public string ClassType { get; set; }
        public string Address { get; set; }
        public Guid? Wards { get; set; }
        public Guid? District { get; set; }
        public Guid? City { get; set; }
        public Guid? Province { get; set; }
        public Guid? State { get; set; }
        public Guid? Region { get; set; }
        public Guid Country { get; set; }
        public string Street { get; set; }
        public string DeptNo { get; set; }
        public double Longtiue { get; set; }
        public double Lattitue { get; set; }
        public Guid CustomerInfomationId { get; set; }
        public Guid MainContactId { get; set; }
        public List<CustomerShiptoContactModel> CustomerShiptoContacts { get; set; }
        public List<CustomerDmsAttribute> CustomerDmsAttributes { get; set; }
    }

    public class CustomerDmsAttribute
    {
        public Guid CustomerAttributeId { get; set; }
        public Guid Id { get; set; }
        public CustomerAttributeModel CustomerAttribute { get; set; }
    }

    public class CustomerShiptoContactModel
    {
        public Guid Id { get; set; }
        public Guid CustomerShiptoId { get; set; }
        public Guid CustomerContactId { get; set; }
        public string ContactCode { get; set; }
        public string FullName { get; set; }
    }

    public class CustomerDmsAttributeUpsertModel
    {
        public Guid? Id { get; set; }

        public Guid CustomerAttributeId { get; set; }

        public CustomerAttributeModel CustomerAttribute { get; set; }
    }

    public class DmsAttributeCustomerAttributeCreateModel
    {
        public Guid CustomerAttributeId { get; set; }

        public Guid CustomerSettingHierarchyId { get; set; }
    }
    public class CustomerShiptoListModel
    {
        public List<CustomerShiptoModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
    public class CustomerShiptoUpload
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ShiptoName { get; set; }
        public string ShiptoCode { get; set; }
        public string ContactCode { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public Status Status { get; set; }
        public string BusinessStatus { get; set; }
        public string ClassType { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Wards { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string DeptNo { get; set; }
        public double Longtiue { get; set; }
        public double Lattitue { get; set; }
        public List<DMSAttribute> DmsAttributes { get; set; } = new List<DMSAttribute>();
    }

    public class CustomerShiptoUploadItemRequest
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ShiptoName { get; set; }
        public string ShiptoCode { get; set; }
        public string ContactCode { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public Status Status { get; set; }
        public string BusinessStatus { get; set; }
        public string ClassType { get; set; }
        public string Address { get; set; }
        public Guid? Country { get; set; }
        public Guid? Province { get; set; }
        public Guid? District { get; set; }
        public Guid? Wards { get; set; }
        public Guid? City { get; set; }
        public Guid? State { get; set; }
        public Guid? Region { get; set; }
        public string Street { get; set; }
        public string DeptNo { get; set; }
        public double Longtiue { get; set; }
        public double Lattitue { get; set; }
        public List<DMSAttribute> DmsAttributes { get; set; } = new List<DMSAttribute>();
    }

    public class CustomerShiptoDownloadTemplate
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ShiptoName { get; set; }
        public string ShiptoCode { get; set; }
        public string ContactCode { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public Status Status { get; set; }
        public string BusinessStatus { get; set; }
        public string ClassType { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Wards { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string DeptNo { get; set; }
        public double Longtiue { get; set; }
        public double Lattitue { get; set; }
        public string Channel { get; set; }
        public string SubChannel { get; set; }
        public string BusinessPartnerType { get; set; }
        public string BusinessModel { get; set; }
        public string TypeOfModel { get; set; }
        public string Location { get; set; }
        public string Area { get; set; }
        public string AverageMonthlyRevenue { get; set; }
        public string ContributedRevenue { get; set; }
        public string Acreage { get; set; }
    }

    public class DMSAttribute
    {
        public string AttributeMaster { get; set; }
        public string AttributeValue { get; set; }
    }

    public class CustomerShiptoUploadList
    {
        public List<CustomerShiptoUpload> Items { get; set; } = new List<CustomerShiptoUpload>();
    }

    public class CustomerShiptoUploadRequest
    {
        public List<CustomerShiptoUpload> Data { get; set; } = new List<CustomerShiptoUpload>();
        public bool UpdateConfirm { get; set; }
    }

    public class CustomerShiptoSearch : EcoParameters
    {
        public List<Guid> CustomerAttributeIds { get; set; }
    }
}
