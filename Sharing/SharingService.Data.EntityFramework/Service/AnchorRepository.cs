using SharingService.Data.Model;
using SharingService.Data.Service;

namespace SharingService.Data.EntityFramework.Service
{
    public class AnchorRepository: AbstractRepository<Anchor>, IAnchorRepository
    {
        public AnchorRepository(SharingServiceContext context) : base(context)
        {
        }

        protected override void CopyProperties(Anchor source, Anchor destination)
        {
            destination.Name = source.Name;
            destination.Longitude = source.Longitude;
            destination.Latitude = source.Latitude;
        }
    }
}
