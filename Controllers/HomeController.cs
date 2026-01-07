using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ElectronicsTester.Models;
using ElectronicsTester.Services;
using Microsoft.Extensions.Options;

namespace ElectronicsTester.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly II2cReader _i2cReader;
    private readonly IOptions<I2cSettings> _i2cSettings;

    public HomeController(
        ILogger<HomeController> logger,
        II2cReader i2cReader,
        IOptions<I2cSettings> i2cSettings)
    {
        _logger = logger;
        _i2cReader = i2cReader;
        _i2cSettings = i2cSettings;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var reading = await _i2cReader.ReadAsync(cancellationToken);
        var settings = _i2cSettings.Value;

        var viewModel = new I2cStatusViewModel
        {
            BusId = settings.BusId,
            DeviceAddress = settings.DeviceAddress,
            ReadLength = settings.ReadLength,
            RegisterAddress = settings.RegisterAddress,
            Timestamp = reading.Timestamp,
            BytesRead = reading.Data?.Length ?? 0,
            DataHex = reading.Data is null ? null : BitConverter.ToString(reading.Data),
            Error = reading.Error
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
