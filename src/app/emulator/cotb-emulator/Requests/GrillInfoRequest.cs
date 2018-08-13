using Cotb.Emulator.Responses;

namespace Cotb.Emulator.Requests
{
    public class GrillInfoRequest : RequestBase
    {
        public GrillInfoRequest() : base(Constants.RequestCodes.GRILL_INFO)
        {
        }
        
        public override IResponse Apply(IRequestProcessor processor) => processor.HandleRequest(this);
    }
}