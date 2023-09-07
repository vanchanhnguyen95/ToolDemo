using BAGeocoding.Api.Models.PBD;
using BAGeocoding.Entity.Public;
using System.Net;

namespace BAGeocoding.Api.Dto;

public class BGCElasticRequestCreateDto
{
    public string? address { get; set; }
    public List<Coord>? coords { get; set; } = new List<Coord>();
    public string? kindname { get; set; }
    public string? name { get; set; }
    public string? searchstr { get; set; }
    public int? shapeid { get; set; }

    public BGCElasticRequestCreateDto() { }

    public BGCElasticRequestCreateDto(BGCElasticRequestCreateDto item)
    {
        address = item.address;
        coords.Add(new Coord(0,0));
        kindname = item?.kindname;
        name = item?.name;
        shapeid = item?.shapeid;
    }

    public BGCElasticRequestCreateDto(BGCElasticRequestCreate item)
    {
        address = item.address;
        coords.Add(new Coord(item?.Lat??0, item?.Lng??0));
        kindname = item?.kindname;
        name = item?.name;
        shapeid = item?.shapeid;
    }

    public BGCElasticRequestCreateDto(RPBLAddressResultV2? item)
    {
        //address = ""; { get; set; }

        if(item.Building != null && item.Building > 0)
            address = item.Building.ToString() + ", ";

        if (!string.IsNullOrEmpty(item.Road))
            address += item.Road + ", ";

        if (!string.IsNullOrEmpty(item.Commune))
            address += item.Commune + ", ";

        if (!string.IsNullOrEmpty(item.District))
            address += item.District + ", ";

        if (!string.IsNullOrEmpty(item.Province))
            address += item.Province;

        coords.Add(new Coord((decimal)item?.Lat, (decimal)item?.Lng));
        //kindname = item?.kindname;
        kindname = "Đường";
        name = item?.Road;
        //shapeid = item?.shapeid;
        shapeid = 2;
    }

    public BGCElasticRequestCreateDto(BGCElasticRequestCreate item, int building)
    {
        address = item.address;
        coords.Add(new Coord(item?.Lat ?? 0, item?.Lng ?? 0));
        kindname = item?.kindname;
        //name = item?.name;
        shapeid = item?.shapeid;
        item.name = item?.name?.Replace("Tỉnh/Thành", "").Replace("Quận/Huyện", "").Replace("Phường/Xã", "").Trim();

        if (item?.shapeid == 2 && building > 0)
        {
            name = $"{building}, {item?.name}";
            searchstr = $"{building}, {item?.address}";
        }    
        else
        {
            name = item?.name;
        }    
        //dataItem.name = string.Format("{0} {1}", searchRegex.Number, dataItem.name);
        //dataItem.searchstr = string.Format("{0}, {1}", searchRegex.Number, dataItem.address);
    }

    public BGCElasticRequestCreateDto(BGCElasticRequestCreate item, BuildingInfo building)
    {
        address = item.address;
        coords.Add(new Coord(item?.Lat ?? 0, item?.Lng ?? 0));
        kindname = item?.kindname;
        //name = item?.name;
        shapeid = item?.shapeid;
        item.name = item?.name?.Replace("Tỉnh/Thành", "").Replace("Quận/Huyện", "").Replace("Phường/Xã", "").Trim();

        if (item?.shapeid == 2 && building.building > 0 && building.index == 1)
        {
            name = $"{building.building}, {item?.name}";
            searchstr = $"{building.building}, {item?.address}";
        }
        else
        {
            name = item?.name;
        }
        //dataItem.name = string.Format("{0} {1}", searchRegex.Number, dataItem.name);
        //dataItem.searchstr = string.Format("{0}, {1}", searchRegex.Number, dataItem.address);
    }

}
