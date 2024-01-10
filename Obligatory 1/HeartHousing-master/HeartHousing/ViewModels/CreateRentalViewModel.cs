using System.ComponentModel.DataAnnotations;

namespace HeartHousing.ViewModels;

public class CreateRentalViewModel
{
    public string Name { get; set; }

    //Input validation Address
    [RegularExpression(@"^[A-Za-z0-9.\-\s,]*$", ErrorMessage = "Invalid characters in the Address field.")]
    [Display(Name = "Rental address")]
    public string Address { get; set; }

    //Input validation Price per night
    [Range(1, int.MaxValue, ErrorMessage = "Price per nights must be greater than 0")]
    public int PricePrNight { get; set; }

    //Input validation Rental Type
    [RegularExpression(@"^[A-Za-z.\-\s,]*$", ErrorMessage = "Invalid characters in the rental type field.")]
    [Display(Name = "Rental type")]
    public string RentalType { get; set; }

    //Input validation Bedroom
    [Range(1, int.MaxValue, ErrorMessage = "Bedroom number must be greater than 0")]
    public int BedNr { get; set; }

    //Input validation Bathroom
    [Range(1, int.MaxValue, ErrorMessage = "Bathroom number must be greater than 0")]
    public int BathNr { get; set; }

    //Input validation Area
    [Range(1, int.MaxValue, ErrorMessage = "The Area Amount must be greater than 0.")]
    public int Area { get; set; }

    //Input validation Description
    [StringLength(200)]
    public string? Description { get; set; }

    public string? ImgUrl { get; set; }
    public string? ImgUrl2 { get; set; }
    public string? ImgUrl3 { get; set; }
    public float? RentalRating { get; set; }
    public int UserID { get; set; }
}