using UnityEngine;
public class Player : MonoBehaviour
{
    public GameObject laserPrefab;
    public float moveSpeed;
    public float minX;
    public float maxX;

    public int damage = 10;
    void Start()
    {

    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * x * moveSpeed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
    }
}
