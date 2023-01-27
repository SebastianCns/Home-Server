/*Author: SebastianCns Date: 26.01.23
*
*Description:
*Interface for database services
*
*/

namespace Home_Server.Data
{
    public interface IDBService<T, T_List>  // Need to create objects of 'T' inside the class which implements the interface
    {
        Task<T_List> GetAllAsync();
        void AddAsync(T Model);
        void UpdateAsync(T Model);
        void DeleteAsync(T Model);
    }
}
