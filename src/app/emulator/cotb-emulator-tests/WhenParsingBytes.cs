using System.Text;
using Cotb.Emulator.Constants;
using Cotb.Emulator.Requests;
using Xunit;

namespace Cotb.Emulator.Tests
{
    public class WhenParsingBytes
    {
        [Fact]
        public void ShouldParsePowerOnGrill()
        {
            // Arrange
            var bytes = Encoding.ASCII.GetBytes(RequestCodes.POWER_ON);

            // Act
            var parsed = RequestFactory.CreateFromBytes(bytes);

            // Assert
            Assert.Equal(RequestCodes.POWER_ON, parsed.Code);
        }

        [Fact]
        public void ShouldParsePowerOffGrill()
        {
            // Arrange
            var bytes = Encoding.ASCII.GetBytes(RequestCodes.POWER_OFF);

            // Act
            var parsed = RequestFactory.CreateFromBytes(bytes);

            // Assert
            Assert.Equal(RequestCodes.POWER_OFF, parsed.Code);
        }

        [Fact]
        public void ShouldParseSendGrillInfo()
        {
            // Arrange
            var bytes = Encoding.ASCII.GetBytes(RequestCodes.GRILL_INFO);

            // Act
            var parsed = RequestFactory.CreateFromBytes(bytes);

            // Assert
            Assert.Equal(RequestCodes.GRILL_INFO, parsed.Code);
        }

        [Fact]
        public void ShouldParseSendGrillId()
        {
            // Arrange
            var bytes = Encoding.ASCII.GetBytes(RequestCodes.GRILL_ID);

            // Act
            var parsed = RequestFactory.CreateFromBytes(bytes);

            // Assert
            Assert.Equal(RequestCodes.GRILL_ID, parsed.Code);
        }

        [Fact]
        public void ShouldParseSendGrillFirmware()
        {
            // Arrange
            var bytes = Encoding.ASCII.GetBytes(RequestCodes.GRILL_FIRMWARE);

            // Act
            var parsed = RequestFactory.CreateFromBytes(bytes);

            // Assert
            Assert.Equal(RequestCodes.GRILL_FIRMWARE, parsed.Code);
        }

        [Fact]
        public void ShouldParseSetGrillTemp()
        {
            // Arrange
            var bytes = Encoding.ASCII.GetBytes("UT300!");

            // Act
            var parsed = RequestFactory.CreateFromBytes(bytes);

            // Assert
            Assert.Equal(RequestCodes.SET_GRILL_TEMP, parsed.Code);
        }

        [Fact]
        public void ShouldParseSetProbeTemp()
        {
            // Arrange
            var bytes = Encoding.ASCII.GetBytes("UF300!");

            // Act
            var parsed = RequestFactory.CreateFromBytes(bytes);

            // Assert
            Assert.Equal(RequestCodes.SET_PROBE_TEMP, parsed.Code);
        }
    }
}