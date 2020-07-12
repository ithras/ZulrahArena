using UnityEngine;
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
}
