using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float BulletSpeed = 100.0f;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * BulletSpeed);
        
        const float bulletLifeTime = 5.0f;
        Destroy(gameObject, bulletLifeTime);
    }
}
