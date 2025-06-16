using System;
using System.Collections.Generic;

namespace ExerciseTracking
{
    // Base class for all activities
    public abstract class Activity
    {
        protected string _date;
        protected double _durationMinutes;

        public Activity(string date, double durationMinutes)
        {
            _date = date;
            _durationMinutes = durationMinutes;
        }

        public abstract double GetDistance();  // in kilometers
        public abstract double GetSpeed();     // in km/h
        public abstract double GetPace();      // in min/km

        public virtual string GetSummary()
        {
            string displayDate = _date;
            if (DateTime.TryParse(_date, out DateTime parsedDate))
            {
                displayDate = parsedDate.ToString("dd MMM yyyy");
            }

            return $"{displayDate} {this.GetType().Name} ({_durationMinutes:F0} min): " +
                   $"Distance {GetDistance():F2} km, Speed: {GetSpeed():F2} kph, Pace: {GetPace():F2} min per km";
        }
    }

    // Running activity
    public class Running : Activity
    {
        private double _distanceKm;

        public Running(string date, double durationMinutes, double distanceKm)
            : base(date, durationMinutes)
        {
            _distanceKm = distanceKm;
        }

        public override double GetDistance()
        {
            return _distanceKm;
        }

        public override double GetSpeed()
        {
            return (_distanceKm / _durationMinutes) * 60;
        }

        public override double GetPace()
        {
            return _durationMinutes / _distanceKm;
        }
    }

    // Cycling activity
    public class Cycling : Activity
    {
        private double _speedKph;

        public Cycling(string date, double durationMinutes, double speedKph)
            : base(date, durationMinutes)
        {
            _speedKph = speedKph;
        }

        public override double GetDistance()
        {
            return (_speedKph / 60) * _durationMinutes;
        }

        public override double GetSpeed()
        {
            return _speedKph;
        }

        public override double GetPace()
        {
            return 60 / _speedKph;
        }
    }

    // Swimming activity
    public class Swimming : Activity
    {
        private int _laps;
        private const double LAP_LENGTH_METERS = 50;

        public Swimming(string date, double durationMinutes, int laps)
            : base(date, durationMinutes)
        {
            _laps = laps;
        }

        public override double GetDistance()
        {
            return (_laps * LAP_LENGTH_METERS) / 1000.0;
        }

        public override double GetSpeed()
        {
            double distance = GetDistance();
            return (distance / _durationMinutes) * 60;
        }

        public override double GetPace()
        {
            double distance = GetDistance();
            return _durationMinutes / distance;
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            // Create example activities
            var running = new Running("03 Nov 2022", 30, 4.8);
            var cycling = new Cycling("04 Nov 2022", 45, 20);
            var swimming = new Swimming("05 Nov 2022", 30, 40);

            List<Activity> activities = new List<Activity>
            {
                running,
                cycling,
                swimming
            };

            Console.WriteLine("--- Exercise Summary ---");
            foreach (var activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
            Console.WriteLine("--- End of Summary ---");
        }
    }
}
