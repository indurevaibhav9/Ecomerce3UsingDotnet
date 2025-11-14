namespace InventoryService.Interfaces
{
    public interface IInventoryRepository
    {
        public Task<Models.Inventory> CreateInventory(Models.Inventory inventory);
        public Task<Models.Inventory> UpdateInventory(Models.Inventory inventory);
        public Task<bool> DeleteInventory(int inventoryId);
        public Task<Models.Inventory> GetInventory(int inventoryId);
        public Task<List<Models.Inventory>> GetAllInventories();
    }
}
