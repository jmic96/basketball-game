using UnityEngine;

namespace BasketballGame
{
    public enum TrainingType
    {
        Shooting,
        Dribbling,
        Finishing,
        Passing,
        Defense
    }

    public class TrainingManager : MonoBehaviour
    {
        [Header("Training Configuration")]
        public int statIncreasePerSession = 1;

        // 850 XP per session brings a level‑5 player to level 100 after ~10 years of weekly training.
        public int xpPerSession = 850;

        public void TrainPlayer(Player player, TrainingType type)
        {
            if (player == null)
            {
                Debug.LogWarning("TrainingManager: Cannot train a null player.");
                return;
            }

            // Increase the targeted stat
            switch (type)
            {
                case TrainingType.Shooting:  player.Stats.Shooting  += statIncreasePerSession; break;
                case TrainingType.Dribbling: player.Stats.Dribbling += statIncreasePerSession; break;
                case TrainingType.Finishing: player.Stats.Finishing += statIncreasePerSession; break;
                case TrainingType.Passing:   player.Stats.Passing   += statIncreasePerSession; break;
                case TrainingType.Defense:   player.Stats.Defense   += statIncreasePerSession; break;
            }

            // Award experience points
            player.Progression.AddExperience(xpPerSession);
        }
    }
}
