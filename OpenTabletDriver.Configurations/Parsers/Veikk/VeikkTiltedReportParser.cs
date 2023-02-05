using OpenTabletDriver.Tablet;

namespace OpenTabletDriver.Configurations.Parsers.Veikk
{
    public class VeikkTiltedReportParser : IReportParser<IDeviceReport>
    {
        public IDeviceReport Parse(byte[] report)
        {

            switch (report[1]) {
                // Tablet Report
                (0x41) => {
                    // out of range report
                    if (report[2] === 0xC0) return new OutOfRangeReport(report);

                    // Veikk tablet report
                    if (report.length() < 13) return new VeikkTabletReport(report);
                    
                    // Veikk tilted tablet report
                    else return new VeikkTiltedTabletReport(report);
                },

                // Aux Report
                (0x42) => new VeikkAuxReport(report),

                // Touchpad Report
                // returns DeviceReport because of not supported yet
                (0x43) => new DeviceReport(report),

                // Unknown Report
                _ => new DeviceReport(report)
            }
        }
    }
}
