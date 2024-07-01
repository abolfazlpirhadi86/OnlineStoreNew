namespace Common.Logs.Serilog
{
    public class SerilogApplicationEnricherOptions
    {
        public string ApplicationName { get; set; }
        public string ApplicationInstanceId { get; set; }
        public DateTime ApplicationStartDate { get; set; }
    }
}
