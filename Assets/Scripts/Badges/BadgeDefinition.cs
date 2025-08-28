using UnityEngine;

namespace BasketballGame
{
    /// <summary>
    /// Defines a badge's name, description, and how it modifies stats.
    /// Create instances of this ScriptableObject in your project via
    /// the Assets → Create → BasketballGame → Badge menu.
    /// </summary>
    [CreateAssetMenu(fileName = "Badge", menuName = "BasketballGame/Badge", order = 0)]
    public class BadgeDefinition : ScriptableObject
    {
        public string badgeName;
        public string description;

        public int shootingModifier;
        public int dribblingModifier;
        public int finishingModifier;
        public int passingModifier;
        public int defenseModifier;
    }
}
