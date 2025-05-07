

using System.IO;
using UnityEngine;

namespace Single
{
    public sealed class SaveManager
    {
        static string path = "/Data.json";
        public static void SaveData(GameData data)
        {
            string file = JsonUtility.ToJson(data, true);
            File.Create(Application.persistentDataPath);
            File.WriteAllText(Application.persistentDataPath + path, file);
        }

        public static GameData LoadData()
        {
            if (File.Exists(Application.persistentDataPath + path))
            {
                string json = File.ReadAllText(Application.persistentDataPath + path);
                return JsonUtility.FromJson<GameData>(json);
            }
            return new GameData();
        }
    }
}