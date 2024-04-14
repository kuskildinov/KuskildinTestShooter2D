using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float BulletLiveTime = 2f;
    [SerializeField] private Rigidbody2D _rigidbody;

    private float _damage;
   public void Initialize(Vector3 velocity,float damage)
    {
        _rigidbody.velocity = velocity;
        _damage = damage;
        StartCoroutine(DestroyDeley());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {           
            enemy.ApplyDamage(_damage);
            gameObject.SetActive(false);
        }
    }
    private IEnumerator DestroyDeley()
    {
        yield return new WaitForSecondsRealtime(BulletLiveTime);
        gameObject.SetActive(false);
    }
}
