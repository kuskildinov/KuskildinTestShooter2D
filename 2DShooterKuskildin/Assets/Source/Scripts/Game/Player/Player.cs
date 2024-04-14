using UnityEngine;

public class Player : MonoBehaviour, IDamageble
{
    [SerializeField] private float _maxHealth;
    private Health _health;

    public Health Health => _health;

    private void Start()
    {
        _health = new Health(_maxHealth);
        _health.Die += Dead;
    }
    public void ApplyDamage(float damage)
    {
        _health.Decrease(damage);
        UIRoot.Instance.SetHpToHealthBar(_health.CurrentHealth);
    }

    public void Dead()
    {
        _health.Die -= Dead;
        Destroy(gameObject);
        LevelRoot.Instance.StopGame();
        UIRoot.Instance.GameOver();
    }
}
