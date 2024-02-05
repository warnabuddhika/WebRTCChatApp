namespace Boomerang.Employees.Domain.AggregrateRoots.Applicants
{
    public record Telephone
    {
        public string? Number { get; init; }

        public string? GetCountryCode()
        {
            return Number?.Substring(0, 3);
        }

        public Telephone() { }

        public Telephone(string? phoneNumber)
        {
            Number = phoneNumber;
        }
    }
}
