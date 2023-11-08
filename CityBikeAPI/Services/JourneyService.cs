namespace CityBikeAPI.Services
{
    public class JourneyService
    {
        // Journey-related methods:

        public string metersToKilometers(double? meters)
        {
            double? kilometers = meters / 1000;
            double? roundedKm = Math.Round((double)kilometers.GetValueOrDefault(), 2);
            string result = roundedKm.GetValueOrDefault().ToString();
            return result;
        }

        public string secondsToHoursAndMinutes(int durationSeconds)
        {
            int seconds = durationSeconds % 60;
            int minutes = (durationSeconds - seconds) / 60;
            string result = "";

            if (minutes > 60)
            {
                int newMinutes = minutes % 60;
                int hours = (minutes - newMinutes) / 60;
                result = hours.ToString() + "h " + newMinutes.ToString() + "m " + seconds.ToString() + "s";
            }
            else
            {
                result = minutes.ToString() + "m " + seconds.ToString() + "s";
            }
            return result;
        }

    }
}
