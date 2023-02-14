using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDo_List.Models;

namespace ToDo_List.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IMongoCollection<ToDoItem> _toDoItemCollection;

        public ToDoService(
     IOptions<DatabaseSettingscs> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _toDoItemCollection = mongoDatabase.GetCollection<ToDoItem>(
                databaseSettings.Value.ToDoItemsCollectionName);
        }

        public async Task CreateAsync(ToDoItem newItem) => await _toDoItemCollection.InsertOneAsync(newItem);

        public async Task<ToDoItem?> GetAsync(string id) =>
            await _toDoItemCollection.Find(x => x.ItemId == id).FirstOrDefaultAsync();


        public async Task UpdateAsync(string id, ToDoItem updatedItem) =>
            await _toDoItemCollection.ReplaceOneAsync(x => x.ItemId == id, updatedItem);

        public async Task RemoveAsync(string id) =>
            await _toDoItemCollection.DeleteOneAsync(x => x.ItemId == id);
    }
}
