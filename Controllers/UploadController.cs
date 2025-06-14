using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

#region Giới thiệu Cloudinary
// Cloudinary là dịch vụ đám mây chuyên dùng để lưu trữ, xử lý và phân phối hình ảnh, video.
// Ưu điểm: tự động tối ưu hóa, hỗ trợ CDN, chỉnh sửa ảnh/video linh hoạt qua URL.
// Website: https://cloudinary.com
// Thư viện .NET: CloudinaryDotNet
#endregion

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly Cloudinary _cloudinary;
    private readonly CloudinarySettings _settings;

    public UploadController(IOptions<CloudinarySettings> config)
    {
        _settings = config.Value;
        var account = new Account(
            _settings.CloudName,
            _settings.ApiKey,
            _settings.ApiSecret
        );
        _cloudinary = new Cloudinary(account);
    }

    [HttpPost("image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("❌ No file uploaded.");

        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation().Width(800).Height(800).Crop("limit"),
            Folder = "test_upload"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok(new
            {
                message = "✅ Upload successful",
                url = uploadResult.SecureUrl.ToString()
            });
        }

        return StatusCode(500, $"❌ Upload failed: {uploadResult.Error?.Message}");
    }

    [HttpPost("video")]
    public async Task<IActionResult> UploadVideo(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("❌ No video uploaded.");

        await using var stream = file.OpenReadStream();

        var uploadParams = new VideoUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "test_upload_video"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok(new
            {
                message = "✅ Video uploaded successfully",
                url = uploadResult.SecureUrl.ToString()
            });
        }

        return StatusCode(500, $"❌ Upload failed: {uploadResult.Error?.Message}");
    }
}
