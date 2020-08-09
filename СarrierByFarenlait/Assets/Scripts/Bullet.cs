using UnityEngine;

public class Bullet : MonoBehaviour
{
    int speed = 10;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Respawn")
        {
            Destroy(this.gameObject);
        }
    }
}
