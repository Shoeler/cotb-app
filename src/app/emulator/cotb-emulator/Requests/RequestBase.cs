using Cotb.Emulator.Responses;

namespace Cotb.Emulator.Requests
{
    public abstract class RequestBase : IRequest
    {
        protected RequestBase(string code) => Code = code;

        public string Code { get; }

        public abstract IResponse Apply(IRequestProcessor processor);

        public override string ToString() => Code;
    }
}