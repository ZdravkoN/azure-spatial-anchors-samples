using System;

namespace SharingService.Data.Model
{
    public class Anchor: AbstractEntity
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
