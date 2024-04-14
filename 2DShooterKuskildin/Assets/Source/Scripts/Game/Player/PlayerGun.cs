using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private SpriteRenderer _gunSpriteRenderer;
    [Header("Bullet Settings")]
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private Transform _bulletsContainer;
    [Header("Pistol")]
    [SerializeField] private Sprite _pistolSprite;
    [SerializeField] private float _pistolBulletSpeed;
    [SerializeField] private float _pistolDamage;
    [SerializeField] private float _pistolShootDeley;
    [Header("Revolver")]
    [SerializeField] private Sprite _revolverSprite;
    [SerializeField] private float _revolverBulletSpeed;
    [SerializeField] private float _revolverDamage;
    [SerializeField] private float _revolverShootDeley;
    [Header("Shotgun")]
    [SerializeField] private Sprite _shotGunSprite;
    [SerializeField] private float _shotgunBulletSpeed;
    [SerializeField] private float _shotgunDamage;
    [SerializeField] private float _shotgunShootDeley;
    private IInput _input;
    private Pool<Bullet> _pool;
    private Vector3 _mousePosition;
    private float _lastShootTime;
    [SerializeField] private GunType _currentGunType;
    public void Initialize(IInput input)
    {
        _input = input;
        _pool = new Pool<Bullet>(_bulletTemplate,_bulletsContainer,30);
    }

    private void Update()
    {
        ReadInput();
        RotateGun();
        if(_input.Shoot())
        {            
            CheckWeaponType();
        }
    }
    private void CheckWeaponType()
    {
        switch (_currentGunType)
        {
            case GunType.PISTOL:
                {
                    Shoot(_pistolBulletSpeed, _pistolDamage, _pistolShootDeley,1);
                    break;
                }
            case GunType.REVOLVER:
                {
                    Shoot(_revolverBulletSpeed, _revolverDamage, _revolverShootDeley,1);
                    break;
                }
            case GunType.SHOTGUN:
                {
                    Shoot(_shotgunBulletSpeed, _shotgunDamage, _shotgunShootDeley,3);
                    break;
                }
        }
    }

    private void Shoot(float speed,float damage,float delay,float oneShootBulletsCount)
    {
        
        if (Time.time - _lastShootTime < delay)
            return;
        _lastShootTime = Time.time;
        for (int i = 1; i <= oneShootBulletsCount; i++)
        {
            Vector3 velocity;
            if (_currentGunType == GunType.SHOTGUN)
            {
                velocity = transform.right * i * 0.5f * speed;
            }
            else
            {
                velocity = transform.right * speed;
            }
            
            Bullet bullet = _pool.GetFreeElement();
            bullet.transform.position = _bulletPoint.position;
            bullet.gameObject.SetActive(true);
            bullet.Initialize(velocity, damage);
        }
        
    }

    private void RotateGun()
    {
        Vector3 difference = _mousePosition - _root.transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        _root.rotation = Quaternion.Euler(0f,0f, rotateZ);
        CheckLookDirection(rotateZ);
    }

    private void CheckLookDirection(float value)
    {
        Vector3 newScale = Vector3.one;
        if (value > 90 || value < -90)
            newScale.y = -1;
        else
            newScale.y = 1;
        _root.localScale = newScale;
    }

    private void ReadInput()
    {        
        _mousePosition = _input.MousePosition();
    }

    public void ChangeGun(GunType type)
    {
        _currentGunType = type;

        if(_currentGunType == GunType.PISTOL)
        {
            _gunSpriteRenderer.sprite = _pistolSprite;
        }
        else if(_currentGunType == GunType.REVOLVER)
        {
            _gunSpriteRenderer.sprite = _revolverSprite;
        }
        else if (_currentGunType == GunType.SHOTGUN)
        {
            _gunSpriteRenderer.sprite = _shotGunSprite;
        }
    }
}

public enum GunType
{
    PISTOL,
    REVOLVER,
    SHOTGUN
}
