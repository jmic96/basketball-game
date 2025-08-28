using UnityEngine;
using UnityEngine.UI;

namespace BasketballGame
{
    /// <summary>
    /// Updates on-screen text for the current player and handles training/game actions
    /// during the season. Attach this to a UI manager object and assign the text
    /// elements and button callbacks via the Inspector.
    /// </summary>
    public class SeasonHUDManager : MonoBehaviour
    {
        [Header("References")]
        public Player player;
        public TrainingManager trainingManager;
        public MatchSimulator matchSimulator;

        [Header("UI Elements")]
        public Text nameText;
        public Text levelText;
        public Text xpText;
        public Text statPointsText;
        public Text statsText;
        public Text yearsProText;

        private void Start()
        {
            UpdateHUD();
        }

        // Button callback methods for each training type
        public void TrainShooting()  { Train(TrainingType.Shooting); }
        public void TrainDribbling() { Train(TrainingType.Dribbling); }
        public void TrainFinishing() { Train(TrainingType.Finishing); }
        public void TrainPassing()   { Train(TrainingType.Passing); }
        public void TrainDefense()   { Train(TrainingType.Defense); }

        public void PlayNextGame()
        {
            if (matchSimulator != null)
            {
                matchSimulator.SimulateGame();
                // Optionally update the HUD if match results affect stats
                UpdateHUD();
            }
        }

        private void Train(TrainingType type)
        {
            if (player != null && trainingManager != null)
            {
                trainingManager.TrainPlayer(player, type);
                UpdateHUD();
            }
        }

        public void UpdateHUD()
        {
            if (player == null) return;

            nameText.text       = player.PlayerName;
            levelText.text      = $"Level: {player.Progression.Level}";
            xpText.text         = $"XP: {player.Progression.CurrentExperience}/{player.Progression.ExperienceToNextLevel}";
            statPointsText.text = $"SP: {player.Progression.StatPoints}";
            statsText.text      = $"Shoot: {player.Stats.Shooting}\n" +
                                  $"Drib:  {player.Stats.Dribbling}\n" +
                                  $"Finish:{player.Stats.Finishing}\n" +
                                  $"Pass:  {player.Stats.Passing}\n" +
                                  $"Def:   {player.Stats.Defense}";
            yearsProText.text   = $"Years Pro: {player.Progression.YearsPro}";
        }
    }
}
