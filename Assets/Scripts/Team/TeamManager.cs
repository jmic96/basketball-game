using System.Collections.Generic;
using UnityEngine;

namespace BasketballGame
{
    /// <summary>
    /// Responsible for creating and managing teams.  Generates demo teams
    /// during MVP development.
    /// </summary>
    public class TeamManager : MonoBehaviour
    {
        public List<Team> Teams = new List<Team>();

        [Header("Random Name Lists")]
        [SerializeField] private string[] firstNames = { "Alex", "Jordan", "Chris", "Taylor", "Morgan", "Riley", "Sam" };
        [SerializeField] private string[] lastNames  = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis" };

        private void Start()
        {
            if (Teams.Count == 0)
            {
                CreateDemoTeams();
            }
        }

        public void CreateDemoTeams()
        {
            Teams.Clear();
            Team teamA = CreateRandomTeam("Team A");
            Team teamB = CreateRandomTeam("Team B");
            Teams.Add(teamA);
            Teams.Add(teamB);
        }

        public Team CreateRandomTeam(string teamName)
        {
            Team team = new Team(teamName);
            for (int i = 0; i < 5; i++)
            {
                // Create a new GameObject so the Player component exists in the scene
                GameObject playerGO = new GameObject($"{teamName} Player {i + 1}");
                playerGO.transform.SetParent(transform);

                Player player = playerGO.AddComponent<Player>();
                player.PlayerName     = GenerateRandomName();
                player.PlayerPosition = (Position)(i % 5); // cycle through PG, SG, SF, PF, C

                // Randomize stats (50–90) for testing
                player.Stats.Shooting  = Random.Range(50, 90);
                player.Stats.Dribbling = Random.Range(50, 90);
                player.Stats.Finishing = Random.Range(50, 90);
                player.Stats.Passing   = Random.Range(50, 90);
                player.Stats.Defense   = Random.Range(50, 90);

                team.Players.Add(player);
            }
            return team;
        }

        private string GenerateRandomName()
        {
            string first = firstNames[Random.Range(0, firstNames.Length)];
            string last  = lastNames[Random.Range(0, lastNames.Length)];
            return $"{first} {last}";
        }
    }
}
