namespace Application.Persistence;

public interface IReadOnlyRepo<T>
{
    IEnumerable<T> GetAll();
}