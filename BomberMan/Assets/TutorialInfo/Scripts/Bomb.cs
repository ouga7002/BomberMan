using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("爆発設定")]
    public float explodeTime = 2f;
    public int power = 2;
    public float gridSize = 1.2f;

    [Header("プレハブ")]
    public GameObject explosionPrefab;

    [Header("レイヤー")]
    public LayerMask wallLayer;

    public bool canPass = true;

    private PlayerController owner;
    private Collider bombCollider;
    private Collider playerCollider;
    private bool collisionEnabled = false;
    private bool exploded = false;

    void Start()
    {
        bombCollider = GetComponent<Collider>();

        Invoke(nameof(Explode), explodeTime);
    }

    public void SetPlayer(PlayerController player)
    {
        owner = player;

        playerCollider = player.GetComponent<Collider>();

        if (playerCollider != null && bombCollider != null)
        {
            Physics.IgnoreCollision(
                playerCollider,
                bombCollider,
                true
            );
        }
    }

    void Update()
    {
        if (!collisionEnabled &&
            playerCollider != null &&
            bombCollider != null)
        {
            if (!bombCollider.bounds.Intersects(playerCollider.bounds))
            {
                Physics.IgnoreCollision(
                    playerCollider,
                    bombCollider,
                    false
                );

                canPass = false;
                collisionEnabled = true;
            }
        }
    }

    public void Explode()
    {
        // 二重爆発防止
        if (exploded)
            return;

        exploded = true;

        // 予約していた爆発をキャンセル
        CancelInvoke(nameof(Explode));

        CreateExplosion(transform.position);

        ExplodeDirection(Vector3.forward);
        ExplodeDirection(Vector3.back);
        ExplodeDirection(Vector3.left);
        ExplodeDirection(Vector3.right);

        if (owner != null)
        {
            owner.ReturnBomb();
        }

        Destroy(gameObject);
    }

    void ExplodeDirection(Vector3 dir)
    {
        for (int i = 1; i <= power; i++)
        {
            Vector3 pos = transform.position + dir * i * gridSize;

            Collider[] hits = Physics.OverlapBox(
                pos,
                new Vector3(0.45f, 0.45f, 0.45f)
            );

            bool stop = false;

            foreach (Collider hit in hits)
            {
                // 壊れない壁
                if (hit.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    stop = true;
                    break;
                }

                // 壊せる壁
                if (hit.gameObject.layer == LayerMask.NameToLayer("BreakableWall"))
                {
                    Destroy(hit.gameObject);

                    CreateExplosion(pos);

                    stop = true;
                    break;
                }
                // 他の爆弾なら誘爆
                Bomb bomb = hit.GetComponent<Bomb>();

                if (bomb != null && bomb != this)
                {
                    bomb.Explode();
                }
            }

            if (stop)
                break;

            CreateExplosion(pos);
        }
    }

    void CreateExplosion(Vector3 position)
    {
        if (explosionPrefab == null)
            return;

        Instantiate(
            explosionPrefab,
            position,
            Quaternion.identity
        );
    }

    public bool CanPass(PlayerController player)
    {
        return player == owner && canPass;
    }
}