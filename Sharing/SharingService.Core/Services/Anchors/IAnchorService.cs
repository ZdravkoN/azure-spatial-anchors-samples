using SharingService.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharingService.Core.Services.Anchors
{
    /// <summary>
    /// An interface representing an anchor key cache.
    /// </summary>
    public interface IAnchorService
    {
        /// <summary>
        /// Determines whether the cache contains the specified anchor identifier.
        /// </summary>
        /// <param name="anchorId">The anchor identifier.</param>
        /// <returns>A <see cref="Task{System.Boolean}"/> containing true if the identifier is found; otherwise false.</returns>
        Task<bool> ContainsAsync(int anchorId);

        /// <summary>
        /// Gets the anchor asynchronously.
        /// </summary>
        /// <param name="anchorId">The anchor identifier.</param>
        /// <returns>The anchor key.</returns>
        Task<Anchor> GetAsync(int anchorId);

        /// <summary>
        /// Gets a list of all anchors.
        /// </summary>
        /// <returns>List of all anchors.</returns>
        Task<List<Anchor>> GetAllAsync();

        /// <summary>
        /// Saves an anchor.
        /// </summary>
        /// <param name="anchor">Anchor to be saved.</param>
        /// <returns>Saved anchor.</returns>
        Task<Anchor> SaveAsync(Anchor anchor);

        /// <summary>
        /// Deletes all anchors.
        /// </summary>
        /// <returns>Nothing.</returns>
        Task DeleteAllAsync();
    }
}
