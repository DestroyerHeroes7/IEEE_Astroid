using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    public static Player Instance;
    public UIManager uiManager;
    public LevelManager levelManager;
    public GameObject laserPrefab;
    public GameObject shield;
    public float moveSpeed;
    public float minX;
    public float maxX;

    public float fireDelay;
    public float timer;

    public bool canShoot = true;
    public bool isDoubleLaser;

    private Coroutine shootCoroutine;
    private Coroutine buffExpiredCoroutine;

    public int damage = 1;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

    }
    void Update()
    {
        if (!canShoot)
            timer += Time.deltaTime;

        if (timer >= fireDelay)
            canShoot = true;

        float x = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * x * moveSpeed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);

        if(Input.GetKey(KeyCode.Space) && canShoot)
            shootCoroutine = StartCoroutine(ShootLaser());
        if (Input.GetKeyUp(KeyCode.Space))
            StopCoroutine(shootCoroutine);
    }
    private IEnumerator ShootLaser()
    {
        while(canShoot)
        {
            timer = 0;
            canShoot = false;
            if(isDoubleLaser)
            {
                Instantiate(laserPrefab, transform.position + Vector3.right * 0.1f, Quaternion.identity);
                Instantiate(laserPrefab, transform.position + Vector3.left * 0.1f , Quaternion.identity);
            }
            else
                Instantiate(laserPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(fireDelay);
        }
    }
    private IEnumerator OnBuffExpired(Global.Buff buff)
    {
        yield return new WaitForSeconds(Global.buffLifeTime[buff]);
        switch (buff)
        {
            case Global.Buff.FireRate:
                fireDelay *= 2;
                break;
            case Global.Buff.DoubleLaser:
                isDoubleLaser = false;
                break;
            case Global.Buff.Speed:
                moveSpeed /= 2;
                break;
            case Global.Buff.Shield:
                shield.SetActive(false);
                break;
        }
    }
    public void OnGetBuff(Global.Buff buff)
    {
        switch (buff)
        {
            case Global.Buff.FireRate:
                fireDelay /= 2;
                break;
            case Global.Buff.DoubleLaser:
                isDoubleLaser = true;
                break;
            case Global.Buff.Speed:
                moveSpeed *= 2;
                break;
            case Global.Buff.Shield:
                shield.SetActive(true);
                break;
        }
        buffExpiredCoroutine = StartCoroutine(OnBuffExpired(buff));
    }
    public void OnShieldHitByAsteroid()
    {
        shield.SetActive(false);
        StopCoroutine(buffExpiredCoroutine);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Asteroid"))
        {
            levelManager.OnPlayerDestroyed();
            uiManager.OnPlayerDestroyed();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
