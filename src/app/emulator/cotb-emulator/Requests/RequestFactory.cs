using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static Cotb.Emulator.Constants.RequestCodes;

namespace Cotb.Emulator.Requests
{
    public static class RequestFactory
    {
        private static readonly Dictionary<string, Func<Match, IRequest>> FactoryMaps =
            new Dictionary<string, Func<Match, IRequest>>
            {
                {POWER_ON, code => new PowerOnRequest()},
                {POWER_OFF, code => new PowerOffRequest()},
                {GRILL_INFO, code => new GrillInfoRequest()},
                {GRILL_ID, code => new GrillIdRequest()},
                {GRILL_FIRMWARE, code => new GrillFirmwareRequest()},
                {SET_GRILL_TEMP, ParseGetGrillTemp},
                {SET_PROBE_TEMP, ParseGetProbeTemp}
            };

        public static IRequest CreateFromBytes(byte[] commandCodeBytes)
        {
            if (commandCodeBytes == null) throw new ArgumentNullException(nameof(commandCodeBytes));

            var commandCode = Encoding.ASCII.GetString(commandCodeBytes);
            foreach (var map in FactoryMaps)
            {
                var pattern = map.Key;
                var factory = map.Value;
                var match = Regex.Match(commandCode, pattern);
                if (!match.Success) continue;

                var cmd = factory.Invoke(match);
                return cmd;
            }

            return null;
        }

        private static IRequest ParseGetGrillTemp(Match codeMatch)
        {
            var tempData = codeMatch.Groups[1].Value;
            var temp = ushort.Parse(tempData);
            return new SetGrillTempRequest(temp);
        }

        private static IRequest ParseGetProbeTemp(Match codeMatch)
        {
            var tempData = codeMatch.Groups[1].Value;
            var temp = ushort.Parse(tempData);
            return new SetProbeTempRequest(temp);
        }
    }
}