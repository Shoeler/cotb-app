using Cotb.Emulator.Responses;

namespace Cotb.Emulator.Requests
{
    public class SetGrillTempRequest : RequestBase
    {
        public SetGrillTempRequest(ushort temp) : base(Constants.RequestCodes.SET_GRILL_TEMP) =>
            DesiredTemperature = temp;

        public ushort DesiredTemperature { get; }

        public override IResponse Apply(IRequestProcessor processor) => processor.HandleRequest(this);
    }
}