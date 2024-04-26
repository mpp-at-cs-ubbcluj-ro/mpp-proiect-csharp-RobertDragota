using System;

namespace AppNetworking.RcpProtocol
{
    [Serializable]
    public class Response
    {
        public ResponseType Type { get; private set; }
        public object Data { get; private set; }

        private Response() { }

        public override string ToString()
        {
            return $"Response{{type='{Type}', data='{Data}'}}";
        }

        public  class Builder
        {
            private readonly Response _response = new Response();

            public Builder Type(ResponseType type)
            {
                _response.Type = type;
                return this;
            }

            public Builder Data(object data)
            {
                _response.Data = data;
                return this;
            }

            public Response Build()
            {
                return _response;
            }
        }
    }
}