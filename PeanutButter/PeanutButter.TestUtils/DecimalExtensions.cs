﻿using NUnit.Framework;

namespace PeanutButter.TestUtils.Generic
{
    public static class DecimalExtensions
    {
        public static void ShouldMatch(this decimal someValue, decimal otherValue, int toPlaces = 2)
        {
            if (toPlaces < 1)
            {
                Assert.AreEqual((long) someValue, (long) otherValue);
                return;
            }
            var format = "{0:0." + new string('0', toPlaces) + "}";
            var someValueAsString = GetTruncatedStringValueFor(someValue, format);
            var otherValueAsString = GetTruncatedStringValueFor(otherValue, format);
            Assert.AreEqual(someValueAsString, otherValueAsString);
        }

        private static string GetTruncatedStringValueFor(decimal someValue, string format)
        {
            var someValueAsString = string.Format("{0:0.00000000000000000000}", someValue);
            var expectedLength = string.Format(format, someValue).Length;
            return someValueAsString.Substring(0, expectedLength);
        }
    }
}