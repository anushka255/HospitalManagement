using API.Helpers;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using API.Interfaces;

namespace API.Services;

public class PhotoUploadService : IPhotoService
{
    private readonly IMapper _mapper;
    private readonly Cloudinary _cloudinary;

    public PhotoUploadService(IOptions<CloudinarySettings> config, IMapper mapper)
    {
        // var acc = new Account
        // (
        //     config.Value.CloudName,
        //     config.Value.ApiKey,
        //     config.Value.ApiSecret
        // );
        //
        var acc = new Account
        (
       "dx9okns0w",
            "197134716312781",
            "QJXP2XlJCJXr4U3swA2N1MPWRxc"
        );
        _mapper = mapper;
        _cloudinary = new Cloudinary(acc);
    }

    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();

        if (file.Length > 0)
        {
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500)
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }

        return uploadResult;
    }

    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deleteParams);

        return result;
    }
}
