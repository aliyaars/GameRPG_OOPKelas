// Item.cs
public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ItemType Type { get; set; }
    public int Value { get; set; }

    public Item(string name, string description, ItemType type, int value)
    {
        Name = name;
        Description = description;
        Type = type;
        Value = value;
    }
}

public enum ItemType
{
    HealingPotion,
    Weapon,
    Armor,
    SpecialItem
}