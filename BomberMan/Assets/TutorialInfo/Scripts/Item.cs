using UnityEngine;

public enum ItemType
{
    FireUp,
    BombUp,
    SpeedUp,
    HPUp
}

public class Item : MonoBehaviour
{
    public ItemType itemType;

    public int addPower = 1;
    public int addBomb = 1;
    public float addSpeed = 1f;
    public int addHP = 1;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null)
            return;

        switch (itemType)
        {
            case ItemType.FireUp:
                player.power ++;
                break;

            case ItemType.BombUp:
                player.maxBombCount += addBomb;
                break;

            case ItemType.SpeedUp:
                player.moveSpeed += addSpeed;
                break;

            case ItemType.HPUp:
                PlayerHealth hp = player.GetComponent<PlayerHealth>();

                if (hp != null)
                {
                    hp.Heal(addHP);
                }
                break;
        }

        Destroy(gameObject);
    }
}