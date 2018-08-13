using Cotb.Emulator.Responses;

namespace Cotb.Emulator.Requests
{
    public class PowerOnRequest : RequestBase
    {
        public PowerOnRequest() : base(Constants.RequestCodes.POWER_ON)
        {
        }

        public override IResponse Apply(IRequestProcessor processor) => processor.HandleRequest(this);
    }
}