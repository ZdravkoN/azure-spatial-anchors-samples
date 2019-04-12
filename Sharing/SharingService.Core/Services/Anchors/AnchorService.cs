using SharingService.Data.Model;
using SharingService.Data.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharingService.Core.Services.Anchors
{
    public class AnchorService : IAnchorService
    {
        private readonly IAnchorRepository _anchorRepository;
        public AnchorService(IAnchorRepository anchorRepository)
        {
            _anchorRepository = anchorRepository;
        }

        public async Task<bool> ContainsAsync(int anchorId)
        {
            var anchor = await _anchorRepository.GetByIdAsync(anchorId);
            return anchor != null;
        }

        public async Task<List<Anchor>> GetAllAsync()
        {
            var result = await _anchorRepository.AllAsync();
            return result;
        }

        public async Task<Anchor> GetAsync(int anchorId)
        {
            var anchor = await _anchorRepository.GetByIdAsync(anchorId);
            return anchor;
        }

        public async Task<Anchor> SaveAsync(Anchor anchor)
        {
            await _anchorRepository.SaveAsync(anchor);
            return anchor;
        }
    }
}
