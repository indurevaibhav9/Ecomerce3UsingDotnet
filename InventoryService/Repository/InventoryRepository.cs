using InventoryService.Data;
using InventoryService.Interfaces;
using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

public class InventoryRepository : IInventoryRepository
{
    private readonly InventoryDbContext _context;

    public InventoryRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<Inventory> CreateInventory(Inventory inventory)
    {
        _context.InventoryItems.Add(inventory);
        await _context.SaveChangesAsync();
        return inventory;
    }

    public async Task<List<Inventory>> GetAllInventories()
    {
        return await _context.InventoryItems.ToListAsync();
    }

    public async Task<Inventory> GetInventory(int productId)
    {
        return await _context.InventoryItems
            .FirstOrDefaultAsync(i => i.ProductId == productId);
    }

    public async Task<Inventory> UpdateInventory(Inventory updatedInventory)
    {
        var existing = await _context.InventoryItems
            .FirstOrDefaultAsync(i => i.ProductId == updatedInventory.ProductId);

        if (existing == null)
            throw new Exception($"Inventory with productId Id {updatedInventory.ProductId} not found.");

        // ✅ Update only the necessary fields
        existing.Quantity = updatedInventory.Quantity;
        existing.ProductId = updatedInventory.ProductId;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteInventory(int id)
    {
        var existing = await _context.InventoryItems.FirstOrDefaultAsync(i => i.Id == id);
        if (existing == null)
            return false;

        _context.InventoryItems.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
