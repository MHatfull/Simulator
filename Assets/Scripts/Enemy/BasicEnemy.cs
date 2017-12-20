public class BasicEnemy : Enemy
{
    public override float Health
    {
        get
        {
            return _currentHealth;
        }
        protected set
        {
            _currentHealth = value;
        }
    }

    private float _currentHealth = 10;
}
