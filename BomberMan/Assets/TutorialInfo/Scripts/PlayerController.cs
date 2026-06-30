using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移動")]
    public float moveSpeed = 5f;

    [Header("爆弾")]
    public GameObject bombPrefab;

    [Header("プレイヤー設定")]
    public int playerNumber = 1; // 1=P1, 2=P2

    private Animator animator;

    void Start()
    {
        // Animator取得
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        PlaceBomb();
    }

    void Move()
    {
        float x = 0f;
        float z = 0f;

        // Player1（WASD）
        if (playerNumber == 1)
        {
            if (Input.GetKey(KeyCode.A)) x = -1;
            if (Input.GetKey(KeyCode.D)) x = 1;
            if (Input.GetKey(KeyCode.W)) z = 1;
            if (Input.GetKey(KeyCode.S)) z = -1;
        }
        // Player2（矢印キー）
        else if (playerNumber == 2)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) x = -1;
            if (Input.GetKey(KeyCode.RightArrow)) x = 1;
            if (Input.GetKey(KeyCode.UpArrow)) z = 1;
            if (Input.GetKey(KeyCode.DownArrow)) z = -1;
        }

        // 移動ベクトル
        Vector3 move = new Vector3(x, 0f, z).normalized;

        // 移動
        transform.position += move * moveSpeed * Time.deltaTime;

        // 向き変更
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        // アニメーション
        if (animator != null)
        {
            animator.SetFloat("Speed", move.magnitude);
        }
    }

    void PlaceBomb()
    {
        // 爆弾Prefabが設定されていなければ何もしない
        if (bombPrefab == null)
            return;

        // Player1（Space）
        if (playerNumber == 1 &&
            Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(
                bombPrefab,
                transform.position,
                Quaternion.identity
            );
        }

        // Player2（Enter）
        if (playerNumber == 2 &&
            Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(
                bombPrefab,
                transform.position,
                Quaternion.identity
            );
        }
    }
}