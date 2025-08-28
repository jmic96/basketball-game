using System.IO;
using UnityEngine;

namespace BasketballGame
{
    /// <summary>
    /// Handles saving and loading player data to JSON files. Uses
    /// Application.persistentDataPath/Saves by default for cross-platform persistence.
    /// </summary>
    public class SaveManager : MonoBehaviour
    {
        public string saveDirectory = "Saves";
        public string fileNamePrefix = "save_slot_";

        private string GetSavePath(int slotIndex)
        {
            string dir = Path.Combine(Application.persistentDataPath, saveDirectory);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            return Path.Combine(dir, $"{fileNamePrefix}{slotIndex}.json");
        }

        public void SavePlayer(Player player, int slotIndex)
        {
            if (player == null)
            {
                Debug.LogWarning("SaveManager: Player reference is null, cannot save.");
                return;
            }

            var data = new PlayerSaveData
            {
                PlayerName       = player.PlayerName,
                Hometown         = player.Hometown,
                Position         = player.PlayerPosition.ToString(),
                Level            = player.Progression.Level,
                CurrentExperience= player.Progression.CurrentExperience,
                StatPoints       = player.Progression.StatPoints,
                YearsPro         = player.Progression.YearsPro,
                Shooting         = player.Stats.Shooting,
                Dribbling        = player.Stats.Dribbling,
                Finishing        = player.Stats.Finishing,
                Passing          = player.Stats.Passing,
                Defense          = player.Stats.Defense
                // TODO: save appearance, badges, awards as needed
            };

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(GetSavePath(slotIndex), json);
            Debug.Log($"Saved to slot {slotIndex}: {GetSavePath(slotIndex)}");
        }

        public bool LoadPlayer(Player player, int slotIndex)
        {
            string path = GetSavePath(slotIndex);
            if (!File.Exists(path))
            {
                Debug.LogWarning($"SaveManager: No save file found at {path}");
                return false;
            }

            string json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<PlayerSaveData>(json);
            if (data == null)
                return false;

            // Apply loaded values to the player
            player.PlayerName = data.PlayerName;
            player.Hometown   = data.Hometown;
            player.PlayerPosition = (Position)System.Enum.Parse(typeof(Position), data.Position);
            player.Progression.Level            = data.Level;
            player.Progression.CurrentExperience= data.CurrentExperience;
            player.Progression.StatPoints       = data.StatPoints;
            player.Progression.YearsPro         = data.YearsPro;
            player.Stats.Shooting  = data.Shooting;
            player.Stats.Dribbling = data.Dribbling;
            player.Stats.Finishing = data.Finishing;
            player.Stats.Passing   = data.Passing;
            player.Stats.Defense   = data.Defense;
            // TODO: load appearance, badges, awards as needed

            Debug.Log($"Loaded slot {slotIndex} from {path}");
            return true;
        }

        public bool HasSave(int slotIndex)
        {
            return File.Exists(GetSavePath(slotIndex));
        }

        public void DeleteSave(int slotIndex)
        {
            string path = GetSavePath(slotIndex);
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log($"Deleted save file at slot {slotIndex}");
            }
        }
    }

    /// <summary>
    /// Serializable container for saving a player's state. You can extend this
    /// to include appearance, badges, awards, and team information.
    /// </summary>
    [System.Serializable]
    public class PlayerSaveData
    {
        public string PlayerName;
        public string Hometown;
        public string Position;
        public int Level;
        public int CurrentExperience;
        public int StatPoints;
        public int YearsPro;
        public int Shooting;
        public int Dribbling;
        public int Finishing;
        public int Passing;
        public int Defense;
        // Additional fields (appearance, badges, awards) can be added here
    }
}
