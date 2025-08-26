using System.Collections.Generic;
using UnityEngine;

namespace BasketballGame
{
    /// <summary>
    /// Represents a basketball team containing a roster of players.
    /// </summary>
    [System.Serializable]
    public class Team
    {
        public string TeamName;
        public List<Player> Players = new List<Player>();

        public Team() { }

        public Team(string name)
        {
            TeamName = name;
        }

        /// <summary>
        /// Average of Shooting and Finishing across all players.
        /// </summary>
        public float GetTeamOffenseRating()
        {
            if (Players.Count == 0) return 0f;
            float total = 0f;
            foreach (var p in Players)
            {
                total += (p.Stats.Shooting + p.Stats.Finishing) / 2f;
            }
            return total / Players.Count;
        }

        /// <summary>
        /// Average of Defense across all players.
        /// </summary>
        public float GetTeamDefenseRating()
        {
            if (Players.Count == 0) return 0f;
            float total = 0f;
            foreach (var p in Players)
            {
                total += p.Stats.Defense;
            }
            return total / Players.Count;
        }
    }
}
