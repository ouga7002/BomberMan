using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移動")]
    public float moveSpeed = 5f;

    [Header("爆弾")]
    public GameObject bombPrefab;

    [Header("プレイヤー設定")]
    public int playerNumber = 1; // 1=P1, 2=P2

    void Update()
    {
        Move();
        PlaceBomb();
    }

    void Move()
    {
        float x = 0f;
        float z = 0f;

        // Player1
        if (playerNumber == 1)
        {
            if (Input.GetKey(KeyCode.A)) x = -1;
            if (Input.GetKey(KeyCode.D)) x = 1;
            if (Input.GetKey(KeyCode.W)) z = 1;
            if (Input.GetKey(KeyCode.S)) z = -1;
        }
        // Player2
        else if (playerNumber == 2)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) x = -1;
            if (Input.GetKey(KeyCode.RightArrow)) x = 1;
            if (Input.GetKey(KeyCode.UpArrow)) z = 1;
            if (Input.GetKey(KeyCode.DownArrow)) z = -1;
        }

        Vector3 move = new Vector3(x, 0, z).normalized;

        transform.position += move * moveSpeed * Time.deltaTime;

        // 移動方向を向く
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
    }

    void PlaceBomb()
    {
        // Player1
        if (playerNumber == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(
                    bombPrefab,
                    transform.position,
                    Quaternion.identity
                );
            }
        }
        // Player2
        else if (playerNumber == 2)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Instantiate(
                    bombPrefab,
                    transform.position,
                    Quaternion.identity
                );
            }
        }
    }
}