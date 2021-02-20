using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    public static Player Instance;
    public UIManager uiManager;
    public LevelManager levelManager;
    public GameObject laserPrefab;
    public float moveSpeed;
    public float minX;
    public float maxX;

    public float fireDelay;
    public float timer;

    public bool canShoot = true;

    private Coroutine shootCoroutine;

    public int damage = 10;
    private void Awake()
    {
        Instance = this;
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
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(fireDelay);
        }
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
