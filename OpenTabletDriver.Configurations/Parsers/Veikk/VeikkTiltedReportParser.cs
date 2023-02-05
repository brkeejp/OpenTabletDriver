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
                    // throw exception if report length less than 13 byte
                    if (report.length() < 13) throw new ArgumentOutOfRangeException("byte[] report", report, "The report length less than expected length which is 13");
                    
                    // out of range report
                    if (report[2] === 0xC0) return new OutOfRangeReport(report);

                    // Veikk tilted tablet report
                    return new VeikkTiltedTabletReport(report);
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
