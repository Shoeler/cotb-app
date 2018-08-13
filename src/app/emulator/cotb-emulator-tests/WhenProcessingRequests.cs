using Cotb.Emulator.Enums;
using Cotb.Emulator.Requests;
using Cotb.Emulator.Responses;
using Xunit;

namespace Cotb.Emulator.Tests
{
    public class WhenProcessingRequests
    {
        [Fact]
        public void ShouldPowerOnGrill()
        {
            // Arrange
            var sut = new GrillEmulator();

            // Act
            var initialState = sut.IsOn;
            var response = sut.HandleRequest(new PowerOnRequest()) as MessageResponse;

            // Assert
            Assert.NotNull(response);
            Assert.False(initialState);
            Assert.True(sut.IsOn);
            Assert.Equal(Constants.ResponseCodes.OK, response.Message);
        }

        [Fact]
        public void ShouldPowerOffGrill()
        {
            // Arrange
            var sut = new GrillEmulator(isOn: true);

            // Act
            var initialState = sut.IsOn;
            var response = sut.HandleRequest(new PowerOffRequest()) as MessageResponse;

            // Assert
            Assert.NotNull(response);
            Assert.True(initialState);
            Assert.False(sut.IsOn);
            Assert.Equal(Constants.ResponseCodes.OK, response.Message);
        }

        [Fact]
        public void ShouldSendGrillInfo()
        {
            // Arrange
            var sut = new GrillEmulator();

            // Act
            var response = sut.HandleRequest(new GrillInfoRequest()) as GrillInfoResponse;

            // Assert
            Assert.NotNull(response);
            Assert.True(response.GrillState == GrillState.OFF);
            Assert.True(response.GrillTemp == sut.CurrentGrillTemp);
        }

        [Fact]
        public void ShouldSendGrillId()
        {
            // Arrange
            var sut = new GrillEmulator();

            // Act
            var response = sut.HandleRequest(new GrillIdRequest()) as MessageResponse;

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Message == sut.GrillId);
        }

        [Fact]
        public void ShouldSendGrillFirmware()
        {
            // Arrange
            var sut = new GrillEmulator();

            // Act
            var response = sut.HandleRequest(new GrillFirmwareRequest()) as MessageResponse;

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Message == sut.Firmware);
        }

        [Fact]
        public void ShouldSetGrillTemp()
        {
            // Arrange
            var sut = new GrillEmulator(currentGrillTemp: 0);

            // Act
            var initialState = sut.CurrentGrillTemp;
            var request = new SetGrillTempRequest(temp: 200);

            var response = sut.HandleRequest(request) as MessageResponse;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(0, initialState);
            Assert.Equal(200, sut.CurrentGrillTemp);
            Assert.Equal(Constants.ResponseCodes.OK, response.Message);
        }

        [Fact]
        public void ShouldSetProbeTemp()
        {
            // Arrange
            var sut = new GrillEmulator(currentGrillTemp: 0);

            // Act
            var initialState = sut.CurrentGrillTemp;
            var request = new SetProbeTempRequest(temp: 200);

            var response = sut.HandleRequest(request) as MessageResponse;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(0, initialState);
            Assert.Equal(200, sut.CurrentProbeTemp);
            Assert.Equal(Constants.ResponseCodes.OK, response.Message);
        }
    }
}