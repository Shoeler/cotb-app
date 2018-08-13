using Cotb.Emulator.Responses;

namespace Cotb.Emulator.Requests
{
    public class GrillFirmwareRequest : RequestBase
    {
        public GrillFirmwareRequest() : base(Constants.RequestCodes.GRILL_FIRMWARE)
        {
        }

        public override IResponse Apply(IRequestProcessor processor) => processor.HandleRequest(this);
    }
}