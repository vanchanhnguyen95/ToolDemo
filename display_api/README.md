# RDOS.Promotion

RDOS.Promotion

## Add your files

```
cd existing_repo
git remote add origin http://git.rdos.online/RDOS.Promotion.git
git branch -M main
git push -uf origin main
```

## Update entity from database server

`dotnet ef dbcontext scaffold "Server=db.rdos.online;Port=5494;Database=onesdev_system;User Id=postgresrdos;Password=PAssword65464" Npgsql.EntityFrameworkCore.PostgreSQL -o Infratructure -c RDOSContext -f `



#  Quy tắc & hướng dẫn khi sử dụng dụng giả lập API


Khi cần sử dụng data từ 1 API trả về. Tuy nhiên, API đó chưa hoặc đang phát triển và không cung cấp kiệp thời thì cần sử dụng **giả lập API**.

Ví dụ: nhóm chúng ta phát triển tính năng **Price**. Chúng ta cần lấy danh sách sản phẩm, tuy nhiên, API trả về danh sách sản phẩm chưa phát triển. Thì để có data phát triển thì chúng ta sẽ tạo ra API giả lập (mock api). 


### 1. Hướng dẫn


1. Tạo table tương ứng với json data cần trả về, với prefix bắt đầu là Temp_[Tên table]. Như ví dụ là: Temp_GetListItems


    | Id  | ItemCode | ItemName  |
    | --- | -------- | --------- |
    | 1   | I01      | ThinkPad  |
    | 2   | I02      | ThinkBook |

2. Tạo API để lấy thông tin từ bảng Temp đã tạo ở bước 1. Với RouteName ví dụ như: /api/v1/ControllerName/GetListItems.
3. Đăng ký thông tin API giả lập lên table ApiMapping.

    | Id                                   | ApiName      | ApiVersion | ApiURL                                                            | ServiceCode | FeatureCode | TempTableName     | IsCompleted |     |     |
    | ------------------------------------ | ------------ | ---------- | ----------------------------------------------------------------- | ----------- | ----------- | ----------------- | ----------- | --- | --- |
    | 6e7f9641-3cfa-4065-9717-3ae67b7cdb33 | GetListItems | 1          | http://192.158.1.7/api/v%7Bversion%7D/ControllerName/GetListItems | null        | Price       | Temp_GetListItems | false       |     |     |

    1. Id: Kiểu guid.
    2. ApiName: tên method.
    3. ApiVersion: version api sử dụng. như trên là v1 => 1.
    4. ApiURL: url của đị chỉ API.
    5. ServiceCode: để null.
    6. FeatureCode: tên feature đang phát triển, nên khai báo 1 biến const để quy định tên feature. như trên là Price.   
4.  Gọi API : ```https://{domain}/systemadminapi/v1/ApiMaping/GetCompetitorById ``` để lấy thông tin về lại.
    vd:
    ``` curl -X 'GET' 'https://{domain}/systemadminapi/v1/ApiMaping/GetCompetitorById/Price'-H 'accept: */*' ```

5. Nên sử đăng ký service lấy thông tin giả lập API là singletion & lấy 1 lần duy nhất khi startup project.

### 2. Quy tắc

1. Table phải cần bắt đầu với prefix "Temp_".
2. Bắt buộc phải gọi API to API để lấy thông tin.
3. Đăng ký đầy đủ thông tin ngoài trừ cột ServiceCode trong table ApiMapping.
