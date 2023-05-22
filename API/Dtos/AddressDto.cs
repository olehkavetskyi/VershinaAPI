using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class AddressDto
{
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Street { get; set; } = null!;
    [Required]
    public string City { get; set; } = null!;

    public string? State { get; set; }
    [Required]
    public string Zipcode { get; set; } = null!;
}
