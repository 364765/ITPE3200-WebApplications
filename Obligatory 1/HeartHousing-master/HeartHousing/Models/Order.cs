using System.ComponentModel.DataAnnotations;

namespace HeartHousing.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        //Input validation
        [Range(1, int.MaxValue, ErrorMessage = "Amount of nights must be greater than 0")]
        [Display(Name = "Number of nights you are staying")]
        public int NightsNr { get; set; }
        public int TotalPrice { get; set; }


        //Navigation property
        public virtual Rental Rental { get; set; } = default!;
        //RentalID ForeignKey
        public int RentalID { get; set; }
        //UserID ForeignKey
        public string UserID {  get; set; }

    }
}
