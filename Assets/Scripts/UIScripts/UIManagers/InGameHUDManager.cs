using UnityEngine;
using UnityEngine.UI;

namespace BasketballGame
{
    /// <summary>
    /// Displays team names and scores during a match. Assign UI Text elements
    /// via the Inspector and call UpdateTeamNames() and UpdateScores() at the
    /// appropriate times in your game flow.
    /// </summary>
    public class InGameHUDManager : MonoBehaviour
    {
        [Header("UI Elements")]
        public Text homeTeamNameText;
        public Text awayTeamNameText;
        public Text homeScoreText;
        public Text awayScoreText;

        [Header("References")]
        public MatchSimulator matchSimulator;

        private void Start()
        {
            // Initialize team names at the start of a match
            UpdateTeamNames();
        }

        public void UpdateTeamNames()
        {
            if (matchSimulator != null)
            {
                homeTeamNameText.text = matchSimulator.HomeTeam != null ?
                    matchSimulator.HomeTeam.TeamName : "Home";
                awayTeamNameText.text = matchSimulator.AwayTeam != null ?
                    matchSimulator.AwayTeam.TeamName : "Away";
            }
        }

        public void UpdateScores(int homeScore, int awayScore)
        {
            homeScoreText.text = homeScore.ToString();
            awayScoreText.text = awayScore.ToString();
        }
    }
}
