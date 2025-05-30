using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Responses
{
    public class ResponseErrorMessages : ResponseBase
    {
        public List<string> Messages { get; set; } = new();

        public ResponseErrorMessages(int statusCode, bool success, string message)
        {
            StatusCode = statusCode;
            Success = success;
            Messages.Add(message);
        }

        public ResponseErrorMessages(int statusCode, bool success, List<string> messages)
        {
            StatusCode = statusCode;
            Success = success;
            Messages = messages;
        }
    }
}
