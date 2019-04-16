using SharingService.Data.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingService.Data
{
    public class FileSystemAnchorCache : IAnchorKeyCache
    {
        private readonly AnchorsDbContext _context;
        public FileSystemAnchorCache(AnchorsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Anchor>> AllAsync()
        {
            var result = _context.Anchors.ToList();
            return result;
        }

        public async Task<bool> ContainsAsync(long anchorId)
        {
            var id = (int)anchorId;
            var result = _context.Anchors.FirstOrDefault(anchor => anchor.Id == id);
            return result != null;
        }

        public async Task<string> GetAnchorKeyAsync(long anchorId)
        {
            var id = (int)anchorId;
            var result = _context.Anchors
                .Where(anchor => anchor.Id == id)
                .Select(anchor => anchor.Key)
                .FirstOrDefault();
            return result;
        }

        public async Task<string> GetLastAnchorKeyAsync()
        {
            var result = _context.Anchors
                .OrderByDescending(anchor => anchor.Id)
                .Select(anchor => anchor.Key)
                .FirstOrDefault();
            return result;
        }

        public async Task<long> SetAnchorKeyAsync(string anchorKey)
        {
            var anchor = new Anchor
            {
                Key = anchorKey
            };
            _context.Anchors.Add(anchor);
            await _context.SaveChangesAsync();
            return anchor.Id;
        }
    }
}
