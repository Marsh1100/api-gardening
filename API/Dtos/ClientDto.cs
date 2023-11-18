using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class ClientDto
{
    public int Id { get; set; }
    [Required]
    public string NameClient { get; set; }
    public string NameContact { get; set; }
    public string LastnameContact { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string Fax { get; set; }
    [Required]
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    [Required]
    public string City { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public int? IdEmployee { get; set; }
    public decimal? CreditLimit { get; set; }
    
}

public class Client2Dto
{
    public int Id { get; set; }
    public string NameClient { get; set; }
    public string NameContact { get; set; }
    public string LastnameContact { get; set; }
    public string PhoneNumber { get; set; }
    public string Fax { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    
}


public class ClientIdDto
{
    public int Id { get; set; }

}


