using System;
using System.Collections.Generic; // Required for List<T>

namespace ExerciseTracking
{
    // Base class for all exercise activities
    public abstract class Activity
    {
        // Private member variables for encapsulation
        private string _date;
        private double _durationMinutes;

        /// <summary>
        /// Initializes the base Activity with date and duration.
        /// </summary>
        /// <param name="date">The date of the activity (e.g., "03 Nov 2022").</param>
        /// <param name="durationMinutes">The duration of the activity in minutes.</param>
        public Activity(string date, double durationMinutes)
        {
            _date = date;
            _durationMinutes = durationMinutes;
        }

        // Abstract methods that must be implemented by derived classes
        public abstract double GetDistance();
        public abstract double GetSpeed();
        public abstract double GetPace();

        /// <summary>
        /// Provides a summary string for the activity, using polymorphic calls
        /// to GetDistance, GetSpeed, and GetPace.
        /// </summary>
        /// <returns>A formatted summary string of the activity.</returns>
        public string GetSummary()
        {
            // Parse and format the date to match the example "03 Nov 2022"
            string displayDate = _date;
            try
            {
                // Attempt to parse the date for consistent formatting if possible
                if (DateTime.TryParse(_date, out DateTime parsedDate))
                {
                    displayDate = parsedDate.ToString("dd MMM yyyy");
                }
            }
            catch (FormatException)
            {
                // If parsing fails, use the original date string
            }

            // Get values using polymorphic method calls
            double distance = GetDistance();
            double speed = GetSpeed();
            double pace = GetPace();

            // Format the output with 2 decimal places for calculated values
            return $"{displayDate} {this.GetType().Name} ({_durationMinutes:F0} min): " +
                   $"Distance {distance:F2} km, Speed: {speed:F2} kph, Pace: {pace:F2} min per km";
        }
    }

    // Derived class for running activities
    public class Running : Activity
    {
        private double _distanceKm;

        /// <summary>
        /// Initializes a Running activity.
        /// </summary>
        /// <param name="date">The date of the activity.</param>
        /// <param name="durationMinutes">The duration in minutes.</param>
        /// <param name="distanceKm">The distance run in kilometers.</param>
        public Running(string date, double durationMinutes, double distanceKm)
            : base(date, durationMinutes) // Call the base class constructor
        {
            _distanceKm = distanceKm;
        }

        /// <summary>
        /// Returns the distance for running.
        /// </summary>
        /// <returns>The distance in kilometers.</returns>
        public override double GetDistance()
        {
            return _distanceKm;
        }

        /// <summary>
        /// Calculates the speed for running.
        /// Speed (kph) = (distance / minutes) * 60
        /// </summary>
        /// <returns>The speed in kph.</returns>
        public override double GetSpeed()
        {
            if (_durationMinutes == 0)
            {
                return 0.0; // Avoid division by zero
            }
            return (_distanceKm / _durationMinutes) * 60;
        }

        /// <summary>
        /// Calculates the pace for running.
        /// Pace (min per km) = minutes / distance
        /// </summary>
        /// <returns>The pace in min/km.</returns>
        public override double GetPace()
        {
            if (_distanceKm == 0)
            {
                return 0.0; // Avoid division by zero
            }
            return _durationMinutes / _distanceKm;
        }
    }

    // Derived class for stationary bicycle activities
    public class Cycling : Activity
    {
        private double _speedKph;

        /// <summary>
        /// Initializes a Cycling activity.
        /// </summary>
        /// <param name="date">The date of the activity.</param>
        /// <param name="durationMinutes">The duration in minutes.</param>
        /// <param name="speedKph">The average speed in kilometers per hour.</param>
        public Cycling(string date, double durationMinutes, double speedKph)
            : base(date, durationMinutes) // Call the base class constructor
        {
            _speedKph = speedKph;
        }

        /// <summary>
        /// Calculates the distance for cycling.
        /// Distance (km) = (speed / 60) * minutes
        /// </summary>
        /// <returns>The distance in kilometers.</returns>
        public override double GetDistance()
        {
            return (_speedKph / 60) * _durationMinutes;
        }

        /// <summary>
        /// Returns the speed for cycling.
        /// </summary>
        /// <returns>The speed in kph.</returns>
        public override double GetSpeed()
        {
            return _speedKph;
        }

        /// <summary>
        /// Calculates the pace for cycling.
        /// Pace = 60 / speed
        /// </summary>
        /// <returns>The pace in min/km.</returns>
        public override double GetPace()
        {
            if (_speedKph == 0)
            {
                return 0.0; // Avoid division by zero
            }
            return 60 / _speedKph;
        }
    }

    // Derived class for swimming activities
    public class Swimming : Activity
    {
        private int _laps;
        private const double LAP_LENGTH_METERS = 50; // A lap is 50 meters

        /// <summary>
        /// Initializes a Swimming activity.
        /// </summary>
        /// <param name="date">The date of the activity.</param>
        /// <param name="durationMinutes">The duration in minutes.</param>
        /// <param name="laps">The number of laps swam.</param>
        public Swimming(string date, double durationMinutes, int laps)
            : base(date, durationMinutes) // Call the base class constructor
        {
            _laps = laps;
        }

        /// <summary>
        /// Calculates the distance for swimming based on laps.
        /// Distance (km) = swimming laps * 50 / 1000
        /// </summary>
        /// <returns>The distance in kilometers.</returns>
        public override double GetDistance()
        {
            return (_laps * LAP_LENGTH_METERS) / 1000.0;
        }

        /// <summary>
        /// Calculates the speed for swimming.
        /// Speed (kph) = (distance / minutes) * 60
        /// </summary>
        /// <returns>The speed in kph.</returns>
        public override double GetSpeed()
        {
            double distance = GetDistance(); // Use the polymorphic GetDistance
            if (_durationMinutes == 0)
            {
                return 0.0; // Avoid division by zero
            }
            return (distance / _durationMinutes) * 60;
        }

        /// <summary>
        /// Calculates the pace for swimming.
        /// Pace (min per km) = minutes / distance
        /// </summary>
        /// <returns>The pace in min/km.</returns>
        public override double GetPace()
        {
            double distance = GetDistance(); // Use the polymorphic GetDistance
            if (distance == 0)
            {
                return 0.0; // Avoid division by zero
            }
            return _durationMinutes / distance;
        }
    }

    // Main program class
    class Program
    {
        static void Main(string[] args)
        {
            // Create instances of different activities
            Running runningActivity = new Running("03 Nov 2022", 30, 4.8); // 4.8 km in 30 min
            Cycling cyclingActivity = new Cycling("04 Nov 2022", 45, 20); // 20 kph for 45 min
            Swimming swimmingActivity = new Swimming("05 Nov 2022", 30, 40); // 40 laps (50m/lap) in 30 min

            // Put all activities in the same list (List<Activity> allows polymorphism)
            List<Activity> activities = new List<Activity>
            {
                runningActivity,
                cyclingActivity,
                swimmingActivity
            };

            Console.WriteLine("--- Exercise Summary ---");
            // Iterate through the list and call GetSummary on each item
            // This demonstrates polymorphism, as GetSummary calls the correct
            // overridden GetDistance, GetSpeed, and GetPace methods for each activity type.
            foreach (Activity activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }

            Console.WriteLine("\n--- End of Summary ---");
        }
    }
}
