using System.Collections;
using UnityEngine;

namespace BasketballGame
{
    /// <summary>
    /// Orchestrates the flow of a basketball season: weekly training sessions,
    /// scheduled games, and season progression for the player's team.
    /// </summary>
    public class SeasonManager : MonoBehaviour
    {
        [Header("Season Settings")]
        [Tooltip("Number of games each week")]
        public int gamesPerWeek = 5;

        [Tooltip("Number of weeks in a season")]
        public int weeksPerSeason = 12;

        [Tooltip("How many seasons to simulate")]
        public int totalSeasons = 1;

        [Header("Dependencies")]
        public TeamManager teamManager;
        public TrainingManager trainingManager;
        public MatchSimulator matchSimulator;

        [Header("Player Team")]
        public Team playerTeam; // Assign in Inspector or leave null to auto‑assign

        private void Start()
        {
            // Find dependencies if not set via Inspector
            if (teamManager == null)   teamManager   = FindObjectOfType<TeamManager>();
            if (trainingManager == null) trainingManager = FindObjectOfType<TrainingManager>();
            if (matchSimulator == null) matchSimulator = FindObjectOfType<MatchSimulator>();

            // Auto‑assign or create the player's team
            if (playerTeam == null)
            {
                if (teamManager.Teams.Count > 0)
                {
                    playerTeam = teamManager.Teams[0];
                }
                else
                {
                    playerTeam = teamManager.CreateRandomTeam("Player Team");
                    teamManager.Teams.Add(playerTeam);
                }
            }

            // Start the seasonal simulation
            StartCoroutine(RunSeasons());
        }

        /// <summary>
        /// Runs multiple seasons in sequence. For each season it loops through weeks,
        /// performs training sessions, plays scheduled games, and then increments years pro.
        /// </summary>
        private IEnumerator RunSeasons()
        {
            for (int season = 1; season <= totalSeasons; season++)
            {
                Debug.Log($"== Starting Season {season} ==");

                // Loop through all weeks in the season
                for (int week = 1; week <= weeksPerSeason; week++)
                {
                    Debug.Log($"-- Week {week} --");

                    // TRAINING: One session per stat category
                    foreach (TrainingType type in System.Enum.GetValues(typeof(TrainingType)))
                    {
                        foreach (Player player in playerTeam.Players)
                        {
                            trainingManager.TrainPlayer(player, type);
                        }
                    }

                    // GAMES: Play the scheduled games this week
                    for (int game = 1; game <= gamesPerWeek; game++)
                    {
                        // Generate a new opponent team for each game
                        Team opponent = teamManager.CreateRandomTeam($"Season{season}_Week{week}_Game{game}");
                        matchSimulator.HomeTeam = playerTeam;
                        matchSimulator.AwayTeam = opponent;
                        matchSimulator.SimulateGame();
                    }

                    // You could wait a frame or add delays here if using UI/animations
                    yield return null;
                }

                // Increment years pro for each player at the end of the season
                foreach (Player player in playerTeam.Players)
                {
                    player.Progression.YearsPro += 1;
                }

                Debug.Log($"== Season {season} complete (Years Pro is now {playerTeam.Players[0].Progression.YearsPro}) ==");
            }
        }
    }
}
