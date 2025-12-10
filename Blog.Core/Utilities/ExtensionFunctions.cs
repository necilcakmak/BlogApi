using Blog.Dto.User;
using Blog.Entities.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Utilities
{
    public static class ExtensionFunctions
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string ToJson(this object obje)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                MaxDepth = 1
            };
            return JsonConvert.SerializeObject(obje, settings);
        }
        public static string ToWriteImage(this string value, string pathName)
        {
            if (string.IsNullOrEmpty(value))
                return "default.jpg";

            var bytes = Convert.FromBase64String(value
                .Replace("data:image/jpeg;base64,", "")
                .Replace("data:image/png;base64,", ""));

            var fileName = Guid.NewGuid() + ".jpg";
            var filePath = Path.Combine(pathName, fileName);
            File.WriteAllBytes(filePath, bytes);
            return fileName;

        }
    }
}
