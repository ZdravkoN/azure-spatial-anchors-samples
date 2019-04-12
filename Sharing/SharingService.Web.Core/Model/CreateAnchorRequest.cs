using System.ComponentModel.DataAnnotations;

namespace SharingService.Web.Core.Model
{
    public class CreateAnchorRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Key { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
