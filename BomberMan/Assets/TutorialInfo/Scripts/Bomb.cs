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

    void Explode()
    {
        CreateExplosion(transform.position);

        ExplodeDirection(Vector3.forward);
        ExplodeDirection(Vector3.back);
        ExplodeDirection(Vector3.left);
        ExplodeDirection(Vector3.right);

        Destroy(gameObject);
    }

    void ExplodeDirection(Vector3 dir)
    {
        for (int i = 1; i <= power; i++)
        {
            Vector3 pos =
                transform.position +
                dir * gridSize * i;

            bool hitWall = Physics.CheckBox(
                pos,
                new Vector3(0.4f, 0.4f, 0.4f),
                Quaternion.identity,
                wallLayer
            );

            if (hitWall)
            {
                break;
            }

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