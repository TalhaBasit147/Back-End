using ToDo_List.Models;

namespace ToDo_List.Services
{
    public interface IToDoService
    {
        Task CreateAsync(ToDoItem newItem);

        Task<ToDoItem?> GetAsync(string id);

        Task<List<ToDoItem>> GetAsync();
        Task UpdateAsync(string id, ToDoItem updatedItem);
        Task RemoveAsync(string id);
    }
}
