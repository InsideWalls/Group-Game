using UnityEngine;

public class BulletTimer : MonoBehaviour
{
    public float timeLimit;
    private float uptime;

    void Start()
    {
        uptime = 0;
    }

    void Update()
    {
        uptime += Time.deltaTime;
        if (uptime >= timeLimit)
            Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
