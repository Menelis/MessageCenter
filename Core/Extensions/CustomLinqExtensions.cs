using System;
namespace Core.Extensions
{
    public static class CustomLinqExtensions
    {
        /// <summary>
        /// Returns the age base on the supplied Date of Birth
        /// </summary>
        /// <param name="dateOfBirth"> Date of Birth</param>
        /// <returns></returns>
        public static int GetAge(this DateTime dateOfBirth)
        {
            int today = int.Parse(DateTime.Today.ToString("yyyyMMdd"));
            int dob = int.Parse(dateOfBirth.ToString("yyyyMMdd"));

            return (today - dob) / 10000;
        }
    }
}