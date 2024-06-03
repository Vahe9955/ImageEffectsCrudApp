using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ImageEffectsCrudApp.RequestModels
{
    public class ImageRequestModel
    {
        public string Name { get; set; }
        [Required]
        public int Px { get; set; }
        public string Effects { get; set; }
    }
}
