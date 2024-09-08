namespace Models;

public readonly record struct Year(int Number)
{
    public Month GetMonth(int ordinal) =>
        ordinal is >= 1 and <= 12 
            ? new Month(this, ordinal) 
            : throw new ArgumentException(nameof(ordinal));

    public IEnumerable<Month> Months =>
        GetMonths(this);

    public Year DecadeBeginning => new(this.Number / 10 * 10 + 1);

    public IEnumerable<Year> Decade =>
        Enumerable.Range(DecadeBeginning.Number, 10)
            .Select(number => new Year(number));

    private static IEnumerable<Month> GetMonths(Year year) =>
        Enumerable.Range(1, 12).Select(ordinal => new Month(year, ordinal));
}