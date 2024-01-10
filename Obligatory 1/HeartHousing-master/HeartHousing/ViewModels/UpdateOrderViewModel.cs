using Microsoft.AspNetCore.Mvc.Rendering;
using HeartHousing.Models;
using System.ComponentModel.DataAnnotations;

namespace HeartHousing.ViewModels
{
    public class UpdateOrderViewModel
    {
        //Input validation
        [Range(1, int.MaxValue, ErrorMessage = "Amount of nights must be greater than 0")]
        [Display(Name = "Number of nights you are staying")]
        public int NightsNr { get; set; }
        public int OrderID { get; set; }
        public int RentalID { get; set; }
        public int TotalPrice { get; set; }
    }
}



