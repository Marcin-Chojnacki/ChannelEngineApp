using System;

namespace CeApp.ApiDataAccess
{
    public class ApiException : Exception
    {
        private const string Desc = "Cannot fetch data from ChannelEngine API. More details in Response property.";

        public string Response { get; set; }

        public ApiException() : base(Desc)
        {
        }

        public ApiException(string responseContent) : base(Desc)
        {
            Response = responseContent;
        }
    }
}
