using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Utils
{
    public static class Profile
    {
        private static PlayerData playerData;
        private static ProgressData progressData;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CheckData()
        {
            playerData = null;
            progressData = null;
            
            SetPlayerData();
            SetProgressData();
        }

        private static void SetProgressData()
        {
            if (progressData != null) return;
            progressData = GetData<ProgressData>("ProgressData");
        }

        private static void SetPlayerData()
        {
            if (playerData != null) return;
            playerData = GetData<PlayerData>("PlayerData");
        }

        private static T GetData<T>(string key) where T: new()
        {
            if (File.Exists(Application.persistentDataPath + "/" + typeof(T) + ".json"))
            {
                var file = File.ReadAllText(Application.persistentDataPath + "/" + typeof(T) + ".json");
                return JsonUtility.FromJson<T>(file);
            }

            var data = new T();
            var js = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/" + typeof(T) + ".json", js);
            return data;
        }

        public static void Save(bool player = false, bool progress = false)
        {
            if (player)
            {
                var js = JsonUtility.ToJson(playerData);
                File.WriteAllText(Application.persistentDataPath + "/" + typeof(PlayerData) + ".json", js);
            }

            if (progress)
            {
                var js = JsonUtility.ToJson(progressData);
                File.WriteAllText(Application.persistentDataPath + "/" + typeof(ProgressData) + ".json", js);
            }
        }

        public static int Coins
        {
            get => playerData.coins;
            set
            {
                playerData.coins = value;
                Save(player: true);
            }
        }
    
        public static int CurrentLevel
        {
            get => playerData.currentLevelIndex;
            set
            {
                playerData.currentLevelIndex = value;
                Save(player: true);
            }
        }
    
        public static Vector3 Position
        {
            get => new Vector3(playerData.x, playerData.y, playerData.z);
            set
            {
                playerData.x = value.x;
                playerData.y = value.y;
                playerData.z = value.z;
                Save(player: true);
            }
        }

        public static bool[] GetLevelCoinsData(int index)
        {
            return progressData.levelsProgress[index].isCollected;
        }

        public static void UpdateLevelCoinsData(int level, int coinIndex)
        {
            progressData.levelsProgress[level].isCollected[coinIndex] = true;
        }

        public static void SaveLevelProgress()
        {
            Save(progress: true);
        }
    }
}