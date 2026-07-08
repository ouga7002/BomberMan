using UnityEngine;

public class Explosion : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 0.5f);
    }
    void OnTriggerEnter(Collider other)
    {
        PlayerHealth hp = other.GetComponent<PlayerHealth>();

        if (hp != null)
        {
            hp.Damage(1);
        }
    }
}