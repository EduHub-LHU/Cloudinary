# ☁️ Hướng Dẫn Sử Dụng Cloudinary Trong ASP.NET

## 🔰 Cloudinary là gì?

> [Cloudinary](https://cloudinary.com) là dịch vụ lưu trữ **hình ảnh và video trên đám mây**, hỗ trợ upload, resize, tối ưu hóa, chuyển đổi định dạng và phân phối qua CDN.

### 📌 Tại sao nên dùng Cloudinary?
| Ưu điểm | Mô tả |
|--------|------|
| ☁️ Lưu trữ cloud | Không cần host riêng hình/video |
| ⚡ CDN tốc độ cao | Phân phối nội dung toàn cầu nhanh chóng |
| 🛠 Xử lý ảnh qua URL | Resize, crop, đổi định dạng... chỉ qua URL |
| 📸 Hỗ trợ nhiều định dạng | JPG, PNG, MP4, WebP, GIF, HEIC... |
| 🔐 Bảo mật | Chia quyền truy cập nội dung |
| 🚀 Tích hợp dễ dàng | Hỗ trợ nhiều nền tảng: .NET, Node.js, React,... |

---

## 🔑 Lấy thông tin tài khoản Cloudinary

1. Truy cập [Cloudinary Dashboard](https://cloudinary.com/console)
2. Copy 3 thông tin sau:
   - `CloudName`
   - `API Key`
   - `API Secret`

---

## ⚙️ Cấu hình `appsettings.json`

```json
{
  "CloudinarySettings": {
    "CloudName": "your_cloud_name",
    "ApiKey": "your_api_key",
    "ApiSecret": "your_api_secret"
  }
}
```
## 📦 Cài đặt Cloudinary SDK

```bash
dotnet add package CloudinaryDotNet
```
## 🛠 Tạo dịch vụ Cloudinary
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly Cloudinary _cloudinary; // Đối tượng dùng để giao tiếp với Cloudinary
    private readonly CloudinarySettings _settings; // Cấu hình (CloudName, ApiKey, ApiSecret)

    // Constructor khởi tạo Cloudinary từ cấu hình trong appsettings.json
    public UploadController(IOptions<CloudinarySettings> config)
    {
        _settings = config.Value;

        // Debug kiểm tra cấu hình đã được load chưa
        Console.WriteLine("==== Cloudinary Configuration ====");
        Console.WriteLine($"CloudName: {_settings.CloudName}");
        Console.WriteLine($"ApiKey: {_settings.ApiKey}");
        Console.WriteLine($"ApiSecret: {_settings.ApiSecret}");
        Console.WriteLine("==================================");

        // Khởi tạo tài khoản Cloudinary từ cấu hình
        var account = new Account(
            _settings.CloudName,
            _settings.ApiKey,
            _settings.ApiSecret
        );

        // Khởi tạo đối tượng Cloudinary
        _cloudinary = new Cloudinary(account);
    }

    // Các phương thức xử lý ảnh/video sẽ được thêm vào đây
}

```csharp 
public class CloudinarySettings
{
    public string CloudName { get; set; }
    public string ApiKey { get; set; }
    public string ApiSecret { get; set; }
}
```

## Đăng ký dịch vụ Cloudinary trong `Program.cs`
```csharp
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings")
);
```
## Cách sử dụng Cloudinary trong Controller

Clone repository này về máy và mở trong Visual Studio hoặc IDE bạn thích.

Sửa đổi `appsettings.json` với thông tin tài khoản Cloudinary của bạn.

Chạy ứng dụng và truy cập vào API để upload ảnh/video và nhớ thêm /swagger vào cuối URL để xem tài liệu API.


// các hình ảnh đã upload lên Cloudinary sẽ được lưu trong thư mục `wwwroot/images` trong project. , khi bạn upload ảnh, nó sẽ tự động lưu vào Cloudinary và trả về URL của ảnh đã upload.

## 📸 Hình ảnh kết quả

<p align="center">
  <img src="/uploadImage.png" alt="upload ảnh" width="48%" />
  <img src="/ImageSuccessful.png" alt="kết quả upload ảnh" width="48%" />
</p>

<p align="center">
  <img src="/uploadVideo.png" alt="upload video" width="48%" />
  <img src="/VideoSuccessful.png" alt="kết quả upload video" width="48%" />
</p>

## Tài liệu tham khảo
- [Cloudinary Documentation](https://cloudinary.com/documentation)
- [Cloudinary .NET SDK](https://cloudinary.com/documentation/dotnet_integration)
- [Cloudinary API Reference](https://cloudinary.com/documentation/image_upload_api_reference)
- [Cloudinary .NET SDK GitHub](https://github.com/cloudinary/cloudinary_dotnet)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)

## Đóng góp
Nếu bạn muốn đóng góp vào dự án này, hãy tạo một pull request hoặc issue trên GitHub. Mọi ý kiến đóng góp đều được hoan nghênh!

## Liên hệ
Nếu bạn có bất kỳ câu hỏi nào, hãy để lại comment hoặc gửi email cho tôi qua địa chỉ: [nguyenmanh2004devgame@gmail.com](mailto:nguyenmanh2004devgame@gmail.com)

Cảm ơn bạn đã đọc hướng dẫn này! Hy vọng nó sẽ giúp ích cho bạn trong việc sử dụng Cloudinary trong ứng dụng ASP.NET của mình. Nếu bạn thấy hữu ích, hãy chia sẻ với bạn bè và đồng nghiệp nhé! 😊
