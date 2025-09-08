using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Prog2_Act02.Utils
{
    public class CustomResponse
    {
        public string Status { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Data { get; set; }

        public CustomResponse(string status, string? message = null, object? data = null)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public static CustomResponse Success(string? message = null, object ? data = null)
        {
            return new CustomResponse("success", null, data);
        }

        public static CustomResponse Error(string? message = null, object? data = null)
        {
            return new CustomResponse("error", message, data);
        }
    }
}
