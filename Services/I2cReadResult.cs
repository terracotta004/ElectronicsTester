namespace ElectronicsTester.Services;

public sealed class I2cReadResult
{
    public byte[]? Data { get; init; }
    public string? Error { get; init; }
    public DateTimeOffset Timestamp { get; init; }
}
