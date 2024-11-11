namespace HerculesBattle.Models
{
    public class Inventory
    {
        private List<Item> items;
        private int maxCapacity;

        public Inventory(int capacity)
        {
            items = new List<Item>();
            maxCapacity = capacity;
        }

        public bool AddItem(Item item)
        {
            if (items.Count >= maxCapacity)
                return false;

            items.Add(item);
            return true;
        }

        public bool RemoveItem(Item item)
        {
            return items.Remove(item);
        }

        public List<Item> GetItems() => items;

        public List<Item> GetItemsByType(ItemType type)
        {
            return items.Where(i => i.Type == type).ToList();
        }
    }
}