namespace ElectronicsTester.Services;

public sealed class I2cSettings
{
    public int BusId { get; init; } = 1;
    public int DeviceAddress { get; init; } = 0x40;
    public int ReadLength { get; init; } = 4;
    public int? RegisterAddress { get; init; }
}
