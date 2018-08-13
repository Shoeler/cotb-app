using Cotb.Emulator.Responses;

namespace Cotb.Emulator.Requests
{
    public class SetProbeTempRequest : RequestBase
    {
        public SetProbeTempRequest(ushort temp) : base(Constants.RequestCodes.SET_PROBE_TEMP) =>
            DesiredTemperature = temp;

        public ushort DesiredTemperature { get; }

        public override IResponse Apply(IRequestProcessor processor) => processor.HandleRequest(this);
    }
}