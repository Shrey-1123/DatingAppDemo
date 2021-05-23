using System;
using System.Threading.Tasks;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            // this is a account we created on cloudinary
            Account acc = new Account
            (
                // be carefull for ordering
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            
            _cloudinary = new Cloudinary(acc);
            
            
            
            
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {   try
            {
                  var uploadResult = new ImageUploadResult();

                    if(file.Length>0)
                    {
                        // add logic to add our file to cloudinary
                        using var stream = file.OpenReadStream();
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(file.FileName, stream),
                            Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                        };
                    
                            uploadResult = await _cloudinary.UploadAsync(uploadParams);  
                    
                        
                    }
                return uploadResult;
            }
            catch(Exception)
            {

            }

            return null;
            
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}