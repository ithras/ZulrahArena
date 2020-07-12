using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class DropdownManager : MonoBehaviour
{
    public static DropdownManager instance;

	[Header("Equipment Panel")]
    public GameObject equipmentUI;
    public TMP_Dropdown ammoDropdown;
    public TMP_Dropdown amuletDropdown;
    public TMP_Dropdown bootsDropdown;
    public TMP_Dropdown capeDropdown;
    public TMP_Dropdown chestDropdown;
    public TMP_Dropdown glovesDropdown;
    public TMP_Dropdown helmetDropdown;
    public TMP_Dropdown legsDropdown;
    public TMP_Dropdown offhandDropdown;
    public TMP_Dropdown ringDropdown;
    public TMP_Dropdown weaponDropdown;

    [Header("Inventory Panel")]
    public GameObject inventoryUI;
    public TMP_Dropdown foodDropdown;
    public TMP_Dropdown potionsDropdown;
    public TMP_Dropdown amuletInventoryDropdown;
    public TMP_Dropdown bootsInventoryDropdown;
    public TMP_Dropdown capeInventoryDropdown;
    public TMP_Dropdown chestInventoryDropdown;
    public TMP_Dropdown glovesInventoryDropdown;
    public TMP_Dropdown helmetInventoryDropdown;
    public TMP_Dropdown legsInventoryDropdown;
    public TMP_Dropdown weaponInventoryDropdown;

    public Dictionary<string, Item> ammoMap { get; private set; }
    public Dictionary<string, Item> amuletMap { get; private set; }
    public Dictionary<string, Item> bootsMap { get; private set; }
    public Dictionary<string, Item> capeMap { get; private set; }
    public Dictionary<string, Item> chestMap { get; private set; }
    public Dictionary<string, Item> glovesMap { get; private set; }
    public Dictionary<string, Item> helmetMap { get; private set; }
    public Dictionary<string, Item> legsMap { get; private set; }
    public Dictionary<string, Item> offhandMap { get; private set; }
    public Dictionary<string, Item> ringMap { get; private set; }
    public Dictionary<string, Item> weaponMap { get; private set; }
    public Dictionary<string, Item> foodMap { get; private set; }
    public Dictionary<string, Item> potionsMap { get; private set; }

    private void Awake()
    {
        instance = this;

        ammoMap = new Dictionary<string, Item>();
        amuletMap = new Dictionary<string, Item>();
        bootsMap = new Dictionary<string, Item>();
        capeMap = new Dictionary<string, Item>();
        chestMap = new Dictionary<string, Item>();
        glovesMap = new Dictionary<string, Item>();
        helmetMap = new Dictionary<string, Item>();
        offhandMap = new Dictionary<string, Item>();
        legsMap = new Dictionary<string, Item>();
        ringMap = new Dictionary<string, Item>();
        weaponMap = new Dictionary<string, Item>();
        foodMap = new Dictionary<string, Item>();
        potionsMap = new Dictionary<string, Item>();

        loadItemsToMap(ammoMap, "Equipment/Ammo");
        loadItemsToMap(amuletMap, "Equipment/Amulet");
        loadItemsToMap(bootsMap, "Equipment/Boots");
        loadItemsToMap(capeMap, "Equipment/Cape");
        loadItemsToMap(chestMap, "Equipment/Chest");
        loadItemsToMap(glovesMap, "Equipment/Gloves");
        loadItemsToMap(helmetMap, "Equipment/Helmet");
        loadItemsToMap(legsMap, "Equipment/Legs");
        loadItemsToMap(offhandMap, "Equipment/Offhand");
        loadItemsToMap(ringMap, "Equipment/Ring");
        loadItemsToMap(weaponMap, "Equipment/Weapon");
        loadItemsToMap(foodMap, "Food");
        loadItemsToMap(potionsMap, "Potions");
    }

    void Start()
    {
        StartCoroutine(loadItemsToDropdown(ammoDropdown, ammoMap));
        StartCoroutine(loadItemsToDropdown(amuletDropdown, amuletMap));
        StartCoroutine(loadItemsToDropdown(bootsDropdown, bootsMap));
        StartCoroutine(loadItemsToDropdown(capeDropdown, capeMap));
        StartCoroutine(loadItemsToDropdown(chestDropdown, chestMap));
        StartCoroutine(loadItemsToDropdown(glovesDropdown, glovesMap));
        StartCoroutine(loadItemsToDropdown(helmetDropdown, helmetMap));
        StartCoroutine(loadItemsToDropdown(offhandDropdown, offhandMap));
        StartCoroutine(loadItemsToDropdown(legsDropdown, legsMap));
        StartCoroutine(loadItemsToDropdown(ringDropdown, ringMap));
        StartCoroutine(loadItemsToDropdown(weaponDropdown, weaponMap));
        StartCoroutine(loadItemsToDropdown(foodDropdown, foodMap));
        StartCoroutine(loadItemsToDropdown(potionsDropdown, potionsMap));
        StartCoroutine(loadItemsToDropdown(amuletInventoryDropdown, amuletMap));
        StartCoroutine(loadItemsToDropdown(bootsInventoryDropdown, bootsMap));
        StartCoroutine(loadItemsToDropdown(capeInventoryDropdown, capeMap));
        StartCoroutine(loadItemsToDropdown(chestInventoryDropdown, chestMap));
        StartCoroutine(loadItemsToDropdown(glovesInventoryDropdown, glovesMap));
        StartCoroutine(loadItemsToDropdown(helmetInventoryDropdown, helmetMap));
        StartCoroutine(loadItemsToDropdown(legsInventoryDropdown, legsMap));
        StartCoroutine(loadItemsToDropdown(weaponInventoryDropdown, weaponMap));
    }

    public IEnumerator loadItemsToDropdown(TMP_Dropdown dropdown, Dictionary<string, Item> map)
    {
        List<string> options = new List<string>();

        foreach(var item in map)
        {
            options.Add(item.Key);
        }

        dropdown.AddOptions(options);

        addListener(dropdown, map);

        yield return null;
    }

    private void loadItemsToMap(Dictionary<string, Item> map, string path)
    {
        var items = Resources.LoadAll(path, typeof(Item));
        
        foreach (var item in items)
        {
            map.Add(item.name, item as Item);
        }
        
    }

    void addListener(TMP_Dropdown dropdown, Dictionary<string, Item> map)
    {
        dropdown.onValueChanged.AddListener(delegate
        {
            dropdownValueChanged(dropdown, map);
        });
    }

    void dropdownValueChanged(TMP_Dropdown dropdown, Dictionary<string, Item> map)
    {
        map[dropdown.captionText.text].Use(false);
    }
}
