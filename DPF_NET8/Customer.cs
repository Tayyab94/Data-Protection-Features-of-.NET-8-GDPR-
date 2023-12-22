namespace DPF_NET8
{
    //public class Customer
    //{

    //    public string Name { get; set; }
    //    public string Email { get; set; }
    //    public DateOnly DateOfBirht { get; set; }
    //    public Guid Id { get; set; }
    //}

    public record Customer(string Name, string Email, DateOnly DateOfBirht)
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
