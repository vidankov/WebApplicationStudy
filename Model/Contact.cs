public class Contact
{
    private static int _currentId = 0;

    public int Id { get; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public Contact()
    {
        Id = ++_currentId;
    }
}
