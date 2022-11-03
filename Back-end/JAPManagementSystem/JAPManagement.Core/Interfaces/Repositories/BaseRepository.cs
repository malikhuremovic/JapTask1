namespace JAPManagement.Core.Interfaces.Repositories
{
    public interface BaseRepository<T, S> where T : class
    {
        public Task<T> GetByIdAsync(S id);
        public Task<List<T>> GetAllAsync();
        public Task<T> Add(T obj);
        public Task<T> Update(T obj);
        public Task<T> Delete(S id);
    }
}
