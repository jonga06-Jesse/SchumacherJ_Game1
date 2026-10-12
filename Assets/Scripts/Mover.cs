using UnityEngine;

public class Mover : MonoBehaviour
{
    private float speed; // Firing speed

    void Start()
    {
        speed = 20;
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision collision) // Kill enemy upon impact
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            // destroy this game object
        }
        else
        {
            Destroy(gameObject); // Destroy bullet upon colliding with anything
        }
    }
}
