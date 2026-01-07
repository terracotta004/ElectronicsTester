namespace ElectronicsTester.Models;

public sealed class I2cStatusViewModel
{
    public string? DataHex { get; init; }
    public int BytesRead { get; init; }
    public string? Error { get; init; }
    public DateTimeOffset Timestamp { get; init; }
    public int BusId { get; init; }
    public int DeviceAddress { get; init; }
    public int ReadLength { get; init; }
    public int? RegisterAddress { get; init; }
}
