using ElectronicMenu.DataAccess.Entities;
using System.Linq.Expressions;

namespace ElectronicMenu.DataAccess
{
    public interface IRepository<T> where T : class, IBaseEntity
    {
        IEnumerable<T> GetAll(); //Получение всех записей
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter); //Получение всех записей, подходящих по фильтру
        //IEnumerable<T> GetAll(IComparer<T> comparer); //Получение всех записей, отсортированных с помощью компаратора
        IEnumerable<T> GetAll<TKey>(Expression<Func<T, TKey>> keySelector); //Получение всех записей, отсортированных по ключу
        T? GetById(int id); //Получение по Id
        T? GetById(Guid id); //Получение по ExternalId
        T Save(T entity); //Добавление/Обновление сущности
        void Delete(T entity); //Удаление сущности

        //Еще возможные методы
        //void DeleteById(int id);
        //void DeleteById(Guid id);
    }
}
