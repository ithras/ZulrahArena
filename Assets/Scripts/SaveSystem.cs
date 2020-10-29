using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveEquipment()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/equipment.zulrah";
        FileStream stream = new FileStream(path, FileMode.Create);

        EquipmentData data = new EquipmentData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveInventory()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.zulrah";
        FileStream stream = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static EquipmentData LoadEquipment()
    {
        string path = Application.persistentDataPath + "/equipment.zulrah";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EquipmentData data = formatter.Deserialize(stream) as EquipmentData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void LoadInGameEquipment()
    {
        EquipmentData data = LoadEquipment();
        int numSlots = Enum.GetNames(typeof(EquipmentSlot)).Length;
        EquipmentManager.instance.currentEquipment = new Equipment[numSlots];
    }

    public static InventoryData LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.zulrah";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventoryData data = formatter.Deserialize(stream) as InventoryData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
