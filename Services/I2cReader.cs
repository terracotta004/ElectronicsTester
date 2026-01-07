using System.Device.I2c;
using Microsoft.Extensions.Options;

namespace ElectronicsTester.Services;

public sealed class I2cReader : II2cReader
{
    private readonly IOptions<I2cSettings> _settings;
    private readonly ILogger<I2cReader> _logger;

    public I2cReader(IOptions<I2cSettings> settings, ILogger<I2cReader> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public Task<I2cReadResult> ReadAsync(CancellationToken cancellationToken)
    {
        var snapshot = _settings.Value;
        var result = new I2cReadResult { Timestamp = DateTimeOffset.UtcNow };

        if (snapshot.ReadLength <= 0)
        {
            return Task.FromResult(new I2cReadResult
            {
                Timestamp = result.Timestamp,
                Error = "I2C read length must be greater than zero."
            });
        }

        if (snapshot.RegisterAddress is < 0 or > 0xFF)
        {
            return Task.FromResult(new I2cReadResult
            {
                Timestamp = result.Timestamp,
                Error = "I2C register address must be between 0 and 255."
            });
        }

        try
        {
            var connection = new I2cConnectionSettings(snapshot.BusId, snapshot.DeviceAddress);
            using var device = I2cDevice.Create(connection);

            if (snapshot.RegisterAddress.HasValue)
            {
                device.WriteByte((byte)snapshot.RegisterAddress.Value);
            }

            var buffer = new byte[snapshot.ReadLength];
            device.Read(buffer);
            return Task.FromResult(new I2cReadResult
            {
                Timestamp = result.Timestamp,
                Data = buffer
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to read I2C device.");
            return Task.FromResult(new I2cReadResult
            {
                Timestamp = result.Timestamp,
                Error = ex.Message
            });
        }
    }
}
