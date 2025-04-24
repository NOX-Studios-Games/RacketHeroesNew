using Core.EventBus;

namespace Characters
{
    public struct SetupAttributesEvent : IEvent
    {
        public readonly float MovementSpeed;
        public float SwingForce;
        public int Damage;
        public int CriticalRate;
        public int Health;
        public int Armor;

        public SetupAttributesEvent(float movementSpeed, float swingForce, int damage, int criticalRate, int health, int armor)
        {
            MovementSpeed = movementSpeed;
            SwingForce = swingForce;
            Damage = damage;
            CriticalRate = criticalRate;
            Health = health;
            Armor = armor;
        }
    }
}