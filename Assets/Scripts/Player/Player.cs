using System.Collections.Generic;
using UnityEngine;

namespace BasketballGame
{
    /// <summary>
    /// Enumeration of the five standard basketball positions.
    /// </summary>
    public enum Position
    {
        PointGuard,
        ShootingGuard,
        SmallForward,
        PowerForward,
        Center
    }

    /// <summary>
    /// Data container for a player's customizable appearance.  Each field can map to
    /// a prefab, sprite, or other asset that defines how the character looks.
    /// </summary>
    [System.Serializable]
    public class PlayerAppearance
    {
        /// <summary>
        /// Identifier for the player's head model or sprite.
        /// </summary>
        public string Head;

        /// <summary>
        /// Identifier for the player's torso model or sprite.
        /// </summary>
        public string Torso;

        /// <summary>
        /// Identifier for the player's lower body model or sprite.
        /// </summary>
        public string Lower;
    }

    /// <summary>
    /// Core basketball statistics for a player.  These will influence simulated
    /// game outcomes and can be improved through training and leveling up.
    /// </summary>
    [System.Serializable]
    public class PlayerStats
    {
        public int Shooting;
        public int Dribbling;
        public int Finishing;
        public int Passing;
        public int Defense;

        /// <summary>
        /// Allocate stat points to a given stat.  This method can be expanded
        /// to include bounds checking, diminishing returns, or costs.
        /// </summary>
        /// <param name="statName">Name of the stat to increase.</param>
        /// <param name="amount">Amount to increase by.</param>
        public void IncreaseStat(string statName, int amount)
        {
            switch (statName)
            {
                case nameof(Shooting):
                    Shooting += amount;
                    break;
                case nameof(Dribbling):
                    Dribbling += amount;
                    break;
                case nameof(Finishing):
                    Finishing += amount;
                    break;
                case nameof(Passing):
                    Passing += amount;
                    break;
                case nameof(Defense):
                    Defense += amount;
                    break;
                default:
                    Debug.LogWarning($"Unknown stat name: {statName}");
                    break;
            }
        }
    }

    /// <summary>
    /// Placeholder class representing a badge.  Badges grant players specific
    /// bonuses or abilities; the actual effects will be implemented later.
    /// </summary>
    [System.Serializable]
    public class Badge
    {
        public string BadgeName;
        public string Description;
        // Future: Define stat modifiers or skill enhancements.
    }

    /// <summary>
    /// Represents an award earned by the player during their career.
    /// </summary>
    [System.Serializable]
    public class Award
    {
        public string AwardName;
        public int YearReceived;
    }

    /// <summary>
    /// Handles experience and level progression for a player.  Experience
    /// thresholds and stat point rewards can be tuned for game balance.
    /// </summary>
    [System.Serializable]
    public class PlayerProgression
    {
        /// <summary>
        /// Number of years the player has been playing professionally.
        /// </summary>
        public int YearsPro = 0;

        /// <summary>
        /// Current level of the player.  Higher levels typically require
        /// exponentially more experience.
        /// </summary>
        public int Level = 1;

        /// <summary>
        /// Experience accumulated towards the next level.
        /// </summary>
        public int CurrentExperience = 0;

        /// <summary>
        /// Stat points available to allocate to player stats when leveling up.
        /// </summary>
        public int StatPoints = 0;

        /// <summary>
        /// Calculates the experience required to reach the next level.  The formula
        /// can be adjusted to tune the pacing of player progression.
        /// </summary>
        public int ExperienceToNextLevel => Level * 100;

        /// <summary>
        /// Adds experience and handles level ups.  Any excess experience carries
        /// over to the next level.  Each level up grants additional stat points.
        /// </summary>
        /// <param name="amount">Amount of experience to add.</param>
        public void AddExperience(int amount)
        {
            CurrentExperience += amount;
            while (CurrentExperience >= ExperienceToNextLevel)
            {
                CurrentExperience -= ExperienceToNextLevel;
                Level++;
                // Example: grant 5 stat points per level.
                StatPoints += 5;
            }
        }
    }

    /// <summary>
    /// Component representing a player character.  Attach this to a GameObject
    /// representing the player entity in the scene.  It contains data for
    /// appearance, statistics, badges, awards, and progression.
    /// </summary>
    public class Player : MonoBehaviour
    {
        [Header("Identity")]
        public string PlayerName = "New Player";
        public string Hometown = "Hometown";
        public Position PlayerPosition = Position.PointGuard;

        [Header("Appearance")]
        public PlayerAppearance Appearance = new PlayerAppearance();

        [Header("Statistics")]
        public PlayerStats Stats = new PlayerStats();

        [Header("Badges & Awards")]
        public List<Badge> Badges = new List<Badge>();
        public List<Award> Awards = new List<Award>();

        [Header("Progression")]
        public PlayerProgression Progression = new PlayerProgression();

        /// <summary>
        /// Adds a badge to the player's collection.  Future logic could check
        /// prerequisites or limit duplicates.
        /// </summary>
        /// <param name="badge">The badge to add.</param>
        public void AddBadge(Badge badge)
        {
            if (badge != null && !Badges.Contains(badge))
            {
                Badges.Add(badge);
            }
        }

        /// <summary>
        /// Records an award the player has earned.
        /// </summary>
        /// <param name="award">The award to add.</param>
        public void AddAward(Award award)
        {
            if (award != null)
            {
                Awards.Add(award);
            }
        }

        /// <summary>
        /// Example method to train a particular stat.  This simply reduces
        /// available stat points and increases the specified stat.
        /// </summary>
        /// <param name="statName">Name of the stat to increase.</param>
        /// <param name="amount">Amount to increase the stat by.</param>
        public void TrainStat(string statName, int amount)
        {
            if (Progression.StatPoints < amount)
            {
                Debug.LogWarning("Not enough stat points to train.");
                return;
            }
            Stats.IncreaseStat(statName, amount);
            Progression.StatPoints -= amount;
        }
    }
}