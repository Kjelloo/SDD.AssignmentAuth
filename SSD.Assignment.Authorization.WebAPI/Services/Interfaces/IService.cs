namespace SSD.Assignment.Authorization.WebAPI.Services.Interfaces;

public interface IService<T>
{
    T Add(T entity);
    IEnumerable<T> GetAll();
    T Get(int id);
    T Update(T entity);
    T Delete(int id);
}