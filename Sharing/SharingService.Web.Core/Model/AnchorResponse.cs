namespace SharingService.Web.Core.Model
{
    public class AnchorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
