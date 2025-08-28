using UnityEngine;
using UnityEngine.SceneManagement;

namespace BasketballGame
{
    /// <summary>
    /// Handles actions from the main menu, including starting a new game,
    /// loading existing saves, selecting save slots, opening settings, and exiting.
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        [Header("Scenes")]
        [Tooltip("Name of the scene to load when starting a new game")]
        public string gameSceneName = "GameScene";

        [Tooltip("Name of the settings scene (if using a separate scene)")]
        public string settingsSceneName = "SettingsScene";

        /// <summary>
        /// Starts a fresh game by loading the gameplay scene. Make sure the
        /// scene is added to Build Settings.
        /// </summary>
        public void StartNewGame()
        {
            if (!string.IsNullOrEmpty(gameSceneName))
            {
                SceneManager.LoadScene(gameSceneName);
            }
            else
            {
                Debug.LogWarning("MainMenu: Game scene name is not set.");
            }
        }

        /// <summary>
        /// Stub for loading the most recently used save. In a future version, you
        /// might display a list of saves and call SelectSaveSlot().
        /// </summary>
        public void LoadGame()
        {
            Debug.Log("LoadGame() called. Implement your save/load system here.");
            // Example: default to slot 0 if you want to auto-load the first slot
            // SelectSaveSlot(0);
        }

        /// <summary>
        /// Called when the user selects a specific save slot. The index (0, 1, or 2)
        /// identifies which of the three slots to use.
        /// </summary>
        /// <param name="slotIndex">0-based index of the save slot (0..2).</param>
        public void SelectSaveSlot(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= 3)
            {
                Debug.LogWarning($"Invalid save slot index {slotIndex}. Must be 0, 1, or 2.");
                return;
            }

            Debug.Log($"SelectSaveSlot() called for slot {slotIndex}. Implement load logic here.");
            // TODO: call your load system and pass in slotIndex
            // Your save system would open, deserialize the saved data, and load it into the game.
        }

        /// <summary>
        /// Opens the settings menu. If you're using a separate scene for settings,
        /// this will load it; otherwise you could toggle a settings panel here.
        /// </summary>
        public void OpenSettings()
        {
            if (!string.IsNullOrEmpty(settingsSceneName))
            {
                // Load a dedicated settings scene
                SceneManager.LoadScene(settingsSceneName);
            }
            else
            {
                Debug.Log("OpenSettings() called. No settings scene specified.");
                // Alternatively, show/hide a settings panel UI here.
            }
        }

        /// <summary>
        /// Quits the game. Stops play mode if in the editor.
        /// </summary>
        public void ExitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
