namespace frontendLossSounds.Models
{
    public class WeatherModel
    {
    }

    public class WeatherResponse
    {
        public Main Main { get; set; }
        public string Name { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
        public double Feels_Like { get; set; }
        public double Temp_Min { get; set; }
        public double Temp_Max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }
}
