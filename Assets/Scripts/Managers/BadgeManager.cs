using System.Collections.Generic;
using UnityEngine;

namespace BasketballGame
{
    /// <summary>
    /// Manages awarding badges to players. When a badge is awarded,
    /// it adds the badge to the player's list and applies stat modifiers.
    /// </summary>
    public class BadgeManager : MonoBehaviour
    {
        [Tooltip("List of all available badge definitions in the game")]
        public List<BadgeDefinition> availableBadges = new List<BadgeDefinition>();

        public void AwardBadge(Player player, BadgeDefinition badgeDef)
        {
            if (player == null || badgeDef == null)
                return;

            // Create a new Badge instance for the player
            var badge = new Badge
            {
                BadgeName  = badgeDef.badgeName,
                Description = badgeDef.description
            };
            player.AddBadge(badge);

            // Apply stat modifiers
            player.Stats.Shooting  += badgeDef.shootingModifier;
            player.Stats.Dribbling += badgeDef.dribblingModifier;
            player.Stats.Finishing += badgeDef.finishingModifier;
            player.Stats.Passing   += badgeDef.passingModifier;
            player.Stats.Defense   += badgeDef.defenseModifier;

            Debug.Log($"Awarded badge '{badgeDef.badgeName}' to {player.PlayerName}");
        }
    }
}
