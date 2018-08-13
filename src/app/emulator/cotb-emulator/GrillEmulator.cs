using System;
using Cotb.Emulator.Enums;
using Cotb.Emulator.Requests;
using Cotb.Emulator.Responses;

namespace Cotb.Emulator
{
    public class GrillEmulator : IRequestProcessor
    {
        private const bool DEFAULT_IS_ON = false;
        private const ushort DEFAULT_TARGET_GRILL_TEMP = 0;
        private const ushort DEFAULT_TARGET_PROBE_TEMP = 0;
        private const ushort DEFAULT_CURRENT_GRILL_TEMP = 82;
        private const ushort DEFAULT_CURRENT_PROBE_TEMP = 20;

        public string GrillId { get; } = "GMG10116294";
        public bool IsOn { get; private set; }

        public ushort TargetGrillTemp { get; private set; }
        public ushort TargetProbeTemp { get; private set; }

        public ushort CurrentGrillTemp { get; private set; }
        public ushort CurrentProbeTemp { get; private set; }

        public string Firmware { get; }

        public GrillEmulator(
            bool isOn = DEFAULT_IS_ON,
            ushort targetGrillTemp = DEFAULT_TARGET_GRILL_TEMP,
            ushort targetProbeTemp = DEFAULT_TARGET_PROBE_TEMP,
            ushort currentGrillTemp = DEFAULT_CURRENT_GRILL_TEMP,
            ushort currentProbeTemp = DEFAULT_CURRENT_PROBE_TEMP
        )
        {
            IsOn = isOn;
            TargetGrillTemp = targetGrillTemp;
            TargetProbeTemp = targetProbeTemp;
            CurrentGrillTemp = currentGrillTemp;
            CurrentProbeTemp = currentProbeTemp;
            Firmware = Guid.NewGuid().ToString();
        }

        public IResponse HandleRequest(IRequest request)
        {
            var response = request.Apply(this);
            return response;
        }

        public IResponse HandleRequest(GrillInfoRequest request) => new GrillInfoResponse(
            grillTemp: CurrentGrillTemp,
            probeTemp: CurrentProbeTemp,
            grillSetTemp: TargetGrillTemp,
            probeSetTemp: TargetProbeTemp,
            grillState: IsOn ? GrillState.ON : GrillState.OFF
        );

        public IResponse HandleRequest(PowerOffRequest request)
        {
            IsOn = false;
            Reset();
            return new MessageResponse("OK");
        }

        public IResponse HandleRequest(PowerOnRequest request)
        {
            IsOn = true;
            return new MessageResponse("OK");
        }

        public IResponse HandleRequest(SetGrillTempRequest request)
        {
            TargetGrillTemp = request.DesiredTemperature;
            InterpolateTargetToCurrentGtillTemp();
            return new MessageResponse("OK");
        }

        public IResponse HandleRequest(SetProbeTempRequest request)
        {
            TargetProbeTemp = request.DesiredTemperature;
            InterpolateTargetToCurrentProbeTemp();
            return new MessageResponse("OK");
        }

        public IResponse HandleRequest(GrillIdRequest request) =>
            new MessageResponse(GrillId);

        public IResponse HandleRequest(GrillFirmwareRequest request) =>
            new MessageResponse(Firmware);

        private void InterpolateTargetToCurrentGtillTemp()
        {
            CurrentGrillTemp = TargetGrillTemp;
        }

        private void InterpolateTargetToCurrentProbeTemp()
        {
            CurrentProbeTemp = TargetProbeTemp;
        }

        private void Reset()
        {
            IsOn = DEFAULT_IS_ON;
            TargetGrillTemp = DEFAULT_TARGET_GRILL_TEMP;
            TargetProbeTemp = DEFAULT_TARGET_PROBE_TEMP;
            CurrentGrillTemp = DEFAULT_CURRENT_GRILL_TEMP;
            CurrentProbeTemp = DEFAULT_CURRENT_PROBE_TEMP;
        }
    }
}