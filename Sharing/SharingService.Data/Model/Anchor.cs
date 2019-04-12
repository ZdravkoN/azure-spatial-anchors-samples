namespace SharingService.Data.Model
{
    public class Anchor: AbstractEntity
    {
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
