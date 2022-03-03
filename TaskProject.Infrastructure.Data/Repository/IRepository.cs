
namespace TaskProject.Infrastructure.Data.Repository
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void Delete(T entity);
        List<T> GetAll();
        T GetById(int id);
        void SaveChanges();
        void Update(T entity);
    }
}