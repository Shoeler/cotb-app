using Cotb.Emulator.Responses;

namespace Cotb.Emulator.Requests
{
    public interface IRequest
    {
        string Code { get; }
        
        IResponse Apply(IRequestProcessor processor);
    }
}