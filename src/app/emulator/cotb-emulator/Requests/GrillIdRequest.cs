using Cotb.Emulator.Responses;

namespace Cotb.Emulator.Requests
{
    public class GrillIdRequest : RequestBase
    {
        public GrillIdRequest() : base(Constants.RequestCodes.GRILL_ID)
        {
        }

        public override IResponse Apply(IRequestProcessor processor) => processor.HandleRequest(this);
    }
}