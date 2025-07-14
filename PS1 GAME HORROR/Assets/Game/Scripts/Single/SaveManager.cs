

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Single
{
    public sealed class SaveManager
    {
        public static void SaveData(GameData data)
        {
            BinaryFormatter binary = new BinaryFormatter();
            string path = Application.persistentDataPath + "Memory Pine Lake\\Data.json";
            FileStream file = new FileStream(path, FileMode.Create);

            binary.Serialize(file, data);
            file.Close();
        }

        public static GameData LoadData()
        {
            if (File.Exists(Application.persistentDataPath + "Memory Pine Lake\\Data.json"))
            {
                BinaryFormatter binary = new BinaryFormatter();
                FileStream file = new FileStream(Application.persistentDataPath + "Memory Pine Lake\\Data.json", FileMode.Open);

                var obj = (GameData)binary.Deserialize(file);
                file.Close();
                return obj;
            }
            else
            {
                Debug.LogError("Free fire");
                return null;
            }

        }
    }
}