using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour,IDamageble
{
    [SerializeField] private float _damage;
    [SerializeField] private float _scoreValue;
    [SerializeField] private float _maxHealth;
    [SerializeField] private NavMeshAgent _navMeshAgent;   
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _deadDelayTime;
    private Health _health;
    private Transform _player;
    public Health Health => _health;
    public void Initialize()
    {
        _health = new Health(_maxHealth);
        _player = FindObjectOfType<Player>().transform;
        _health.Die += Dead;
        
    }

    private void Update()
    {
        _navMeshAgent.updateRotation = false;
        if (_player == null)
            return;
        _navMeshAgent.SetDestination(_player.position);
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        RotateToPlayerSide();

    }

    private void RotateToPlayerSide()
    {
        if (_player.position.x > transform.position.x)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    public void ApplyDamage(float damage)
    {
        _health.Decrease(damage);
    }

    private void Dead()
    {
        _animator.SetBool("Dead", true);       
        _navMeshAgent.speed = 0;
        StartCoroutine(DeadDelay());
    }

    private void OnDisable()
    {
        _health.Die -= Dead;
    }

    private IEnumerator DeadDelay()
    {
       
        yield return new WaitForSecondsRealtime(_deadDelayTime);
        LevelRoot.Instance.EnemyIsDead();
        ScoreRoot.Instance.Add(_scoreValue);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            _animator.SetTrigger("Attack");
            player.ApplyDamage(_damage);
        }
    }
}
