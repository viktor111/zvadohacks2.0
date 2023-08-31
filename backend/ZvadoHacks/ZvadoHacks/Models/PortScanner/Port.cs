namespace ZvadoHacks.Models.PortScanner
{
    public class Port
    {
        public string Host { get; set; }

        public int Number { get; set; }

        public bool IsOpen { get; set; }

        public string Description { get; set; }
    }
}
