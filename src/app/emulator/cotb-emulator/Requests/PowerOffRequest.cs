using Cotb.Emulator.Responses;

namespace Cotb.Emulator.Requests
{
    public class PowerOffRequest : RequestBase
    {
        public PowerOffRequest() : base(Constants.RequestCodes.POWER_OFF)
        {
        }

        public override IResponse Apply(IRequestProcessor processor) => processor.HandleRequest(this);
    }
}