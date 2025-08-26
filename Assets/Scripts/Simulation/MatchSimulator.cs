using UnityEngine;

namespace BasketballGame
{
    /// <summary>
    /// Simulates a game between two teams based on offense/defense ratings.
    /// </summary>
    public class MatchSimulator : MonoBehaviour
    {
        [Header("Teams")]
        public Team HomeTeam;
        public Team AwayTeam;

        [Header("Simulation Settings")]
        public int possessionsPerTeam = 40;
        public float threePointChance = 0.3f;

        private void Start()
        {
            // Auto‑assign teams if not set in the Inspector
            if (HomeTeam == null || AwayTeam == null)
            {
                TeamManager tm = FindObjectOfType<TeamManager>();
                if (tm != null && tm.Teams.Count >= 2)
                {
                    HomeTeam = tm.Teams[0];
                    AwayTeam = tm.Teams[1];
                }
            }

            if (HomeTeam != null && AwayTeam != null)
            {
                SimulateGame();
            }
            else
            {
                Debug.LogWarning("MatchSimulator: HomeTeam and AwayTeam must be assigned or found via TeamManager.");
            }
        }

        public void SimulateGame()
        {
            int homeScore = SimulateTeamScore(HomeTeam, AwayTeam);
            int awayScore = SimulateTeamScore(AwayTeam, HomeTeam);
            Debug.Log($"Final Score: {HomeTeam.TeamName} {homeScore} – {awayScore} {AwayTeam.TeamName}");
        }

        private int SimulateTeamScore(Team offenseTeam, Team defenseTeam)
        {
            float offenseRating = offenseTeam.GetTeamOffenseRating();
            float defenseRating = defenseTeam.GetTeamDefenseRating();
            float chanceToScore = offenseRating / (offenseRating + defenseRating + 1f);
            int totalPoints = 0;

            for (int i = 0; i < possessionsPerTeam; i++)
            {
                if (Random.value <= chanceToScore)
                {
                    int points = Random.value <= threePointChance ? 3 : 2;
                    totalPoints += points;
                }
            }
            return totalPoints;
        }
    }
}
