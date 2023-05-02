// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

namespace SF2022User15Lib
{
    public class Calculations
    {
        public static string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
        {
            List<TimeSpan> busyTimes = new List<TimeSpan>();
            for (int i = 0; i < startTimes.Length; i++)
            {
                TimeSpan startTime = startTimes[i];
                int duration = durations[i];
                TimeSpan endTime = startTime.Add(new TimeSpan(0, duration, 0));
                busyTimes.Add(startTime);
                busyTimes.Add(endTime);
            }

            List<TimeSpan> availablePeriods = new List<TimeSpan>();

            TimeSpan currentTime = beginWorkingTime;

            while (currentTime.Add(new TimeSpan(0, consultationTime, 0)) <= endWorkingTime)
            {
                bool isAvailable = true;

                foreach (var busyTime in busyTimes)
                {
                    if (currentTime >= busyTime && currentTime < busyTime.Add(new TimeSpan(0, consultationTime, 0)))
                    {
                        isAvailable = false;
                        break;
                    }
                }

                if (isAvailable)
                {
                    availablePeriods.Add(currentTime);
                }

                currentTime = currentTime.Add(new TimeSpan(0, 30, 0)); // Переходим к следующему возможному времени
            }

            string[] availablePeriodsFormatted = new string[availablePeriods.Count];
            for (int i = 0; i < availablePeriods.Count; i++)
            {
                availablePeriodsFormatted[i] = availablePeriods[i].ToString(@"hh\:mm") + "-" + availablePeriods[i].Add(new TimeSpan(0, consultationTime, 0)).ToString(@"hh\:mm");
            }

            return availablePeriodsFormatted;
        }
    }
}