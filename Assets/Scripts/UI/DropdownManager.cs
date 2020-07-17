using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class DropdownManager : MonoBehaviour
{
    public static DropdownManager instance;

	[Header("Equipment Panel")]
    public GameObject equipmentUI;

    public List<TMP_Dropdown> EquipmentDropdownValue = new List<TMP_Dropdown>();
    public List<string> EquipmentDropdownKey = new List<string>();
    public Dictionary<string, TMP_Dropdown> EquipmentDropdownMap = new Dictionary<string, TMP_Dropdown>();
    public Dictionary<string, Dictionary<string, Item>> EquipmentMap { get; private set; }

    [Header("Inventory Panel")]
    public GameObject inventoryUI;
    public List<TMP_Dropdown> InventoryDropdownValue = new List<TMP_Dropdown>();
    public List<string> InventoryDropdownKey = new List<string>();
    public Dictionary<string, TMP_Dropdown> InventoryDropdownMap = new Dictionary<string, TMP_Dropdown>();
    public Dictionary<string, Dictionary<string, Item>> InventoryMap { get; private set; }

    private void Awake()
    {
        instance = this;
        EquipmentMap = new Dictionary<string, Dictionary<string, Item>>();
        InventoryMap = new Dictionary<string, Dictionary<string, Item>>();

        for (int i = 0; i < EquipmentDropdownKey.Count; i++)
        {
            EquipmentDropdownMap.Add(EquipmentDropdownKey[i], EquipmentDropdownValue[i]);
            EquipmentMap.Add(EquipmentDropdownKey[i], new Dictionary<string, Item>());
        }

        for (int i = 0; i < InventoryDropdownKey.Count; i++)
        {
            InventoryDropdownMap.Add(InventoryDropdownKey[i], InventoryDropdownValue[i]);
            InventoryMap.Add(InventoryDropdownKey[i], new Dictionary<string, Item>());
        }

        foreach(var key in EquipmentMap)
        {
            loadItemsToMap(key.Value, key.Key);
        }

        foreach(var key in InventoryMap)
        {
            loadItemsToMap(key.Value, key.Key);
        }

    }

    void Start()
    {
        foreach(var key in EquipmentDropdownMap)
        {
            StartCoroutine(loadItemsToDropdown(key.Value, EquipmentMap[key.Key]));
        }

        foreach(var key in InventoryDropdownMap)
        {
            StartCoroutine(loadItemsToDropdown(key.Value, InventoryMap[key.Key]));
        }
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
        Debug.Log(dropdown.captionText.text);
        map[dropdown.captionText.text].Use(false);
    }

    
    public void LoadEquipment(string itemToEquip, string slotToEquip)
    {
        var map = EquipmentDropdownMap[slotToEquip];

        if (EquipmentMap.ContainsKey(itemToEquip))
            map.value = map.options.IndexOf(map.options.Find(option => option.text == itemToEquip));
    }
    
}
