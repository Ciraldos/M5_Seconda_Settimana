namespace W8.D3.WebApi.Controllers.Api.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }

        public override bool Equals(object? obj) => obj is Contact && obj.GetHashCode() == GetHashCode();
        public override int GetHashCode() => Id;
    }
}
