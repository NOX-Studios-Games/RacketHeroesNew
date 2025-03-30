using UnityEngine;

namespace RacketHeroes.Characters
{
    [CreateAssetMenu(menuName = "RacketHeroes/CharactersAttributes", fileName = "CharactersAttributes")]
    public class CharactersAttributesSO : ScriptableObject
    {
        public float movementSpeed;
        public float swingForce;
        public int damage;
        public int criticalRate;
        public int health;
        public int armor;
    }
}