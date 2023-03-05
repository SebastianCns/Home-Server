/*Author: SebastianCns Date: 26.01.23
*
*Description:
*Interface for database services
*
*/

namespace Home_Server.Data
{
    public interface IDBService<T, T_List> //T = Model; T_List = List of Models
    {
        Task<T_List> GetAllAsync();
        void AddAsync(T Model);
        Task UpdateAsync(T Model);
        void DeleteAsync(T Model);
    }
}
