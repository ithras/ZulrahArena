using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    Inventory inventory;

    InventorySlot[] slots;

    public Item[] items;

    public Potion prayerPot;
    public Potion rangingPot;
    public Potion magicPot;
    public Potion antiVenomPot;
    public Potion bastionPot;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        foreach(Item item in items)
        {
            inventory.Add(item);
        }

        inventory.Add(new Potion(prayerPot));
        inventory.Add(new Potion(prayerPot));
        inventory.Add(new Potion(rangingPot));
        inventory.Add(new Potion(rangingPot));
        inventory.Add(new Potion(magicPot));
        inventory.Add(new Potion(magicPot));
        inventory.Add(new Potion(bastionPot));
        inventory.Add(new Potion(bastionPot));
        inventory.Add(new Potion(antiVenomPot));
        inventory.Add(new Potion(antiVenomPot));
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (inventory.items[i] != null)
                slots[i].AddItem(inventory.items[i]);
            else
                slots[i].ClearSlot();
        }
    }
}
