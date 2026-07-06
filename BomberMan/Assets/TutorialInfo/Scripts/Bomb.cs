using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("爆発までの時間")]
    public float explodeTime = 2f;

    [Header("爆発エフェクト")]
    public GameObject explosionPrefab;

    public bool canPass = true;

    private PlayerController owner;

    private Collider bombCollider;
    private Collider playerCollider;
    private bool collisionEnabled = false;

    void Start()
    {
        bombCollider = GetComponent<Collider>();

        Invoke(nameof(Explode), explodeTime);
    }

    public void SetPlayer(PlayerController player)
    {
        owner = player;
        playerCollider = player.GetComponent<Collider>();

        Physics.IgnoreCollision(playerCollider, bombCollider, true);
    }

    void Update()
    {
        if (!collisionEnabled && playerCollider != null)
        {
            // プレイヤーが爆弾から離れたら当たり判定を戻す
            if (!bombCollider.bounds.Intersects(playerCollider.bounds))
            {
                Physics.IgnoreCollision(playerCollider, bombCollider, false);
                canPass = false;
                collisionEnabled = true;
            }
        }
    }

    void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
    public bool CanPass(PlayerController player)
    {
        return player == owner && canPass;
    }
}