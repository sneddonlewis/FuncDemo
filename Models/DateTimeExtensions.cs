﻿namespace Models;

using static Enumerable;

public static class DateTimeExtensions
{
    public static IEnumerable<(int year, int month)> GetYearsMonths(this DateTime time) =>
        time.Year.GetYearMonths();

    public static IEnumerable<(int year, int month)> GetDecadeMonths(this DateTime time) =>
        Range(time.Year.ToDecadeBeginning(), 10).SelectMany(GetYearMonths);

    private static int ToDecadeBeginning(this int year) => year / 10 * 10 + 1;

    private static IEnumerable<(int year, int month)> GetYearMonths(this int year) =>
        Range(1, 12).Select(month => (year, month));
}