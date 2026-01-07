namespace ElectronicsTester.Services;

public interface II2cReader
{
    Task<I2cReadResult> ReadAsync(CancellationToken cancellationToken);
}
