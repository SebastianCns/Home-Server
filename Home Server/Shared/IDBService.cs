/*Author: SebastianCns Date: 26.01.23
*
*Description:
*Interface for database services
*
*/

namespace Home_Server.Shared
{
    public interface IDBService<T>
    {
        List<T> GetAll();
        void Add(T Model);
        void Update(T Model);
        void Delete(T Model);
    }
}
