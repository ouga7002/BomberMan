using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("移動")]
    public float moveSpeed = 5f;

    private Gamepad pad;

    [Header("爆弾")]
    public GameObject bombPrefab;

    // 最大設置数
    public int maxBombCount = 3;

    // 現在設置中の爆弾
    private int currentBombCount = 0;
    //爆風
    public int power = 2;


    [Header("移動判定")]
    public LayerMask obstacleLayer;

    [Header("プレイヤー設定")]
    public int playerNumber = 1; // 1=P1, 2=P2


    // 一マスのサイズ（グリッドサイズ）
    public float gridSize = 1.2f;


    private Animator animator;
    private bool isMoving = false;
    private Vector3 targetPosition;

    void Start()
    {
        // Animator取得
        animator = GetComponent<Animator>();

        targetPosition = transform.position;

    }

    void Update()
    {
        Move();
        PlaceBomb();
    }
 
    void Move()
    {
        Debug.Log("Gamepad Count = " + Gamepad.all.Count);

        if (playerNumber == 1)
            pad = Gamepad.all.Count > 0 ? Gamepad.all[0] : null;

        if (playerNumber == 2)
            pad = Gamepad.all.Count > 1 ? Gamepad.all[1] : null;

        // 移動中なら目的地まで進む
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }

            if (animator != null)
                animator.SetFloat("Speed", 1);

            return;
        }

        float x = 0f;
        float z = 0f;

        if (pad != null)
        {
            x = Mathf.Round(pad.leftStick.x.ReadValue());
            z = Mathf.Round(pad.leftStick.y.ReadValue());
        }

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

        // 斜め移動を禁止（横優先）
        if (x != 0)
        {
            z = 0;
        }


        Vector3 dir = new Vector3(x, 0, z);

        if (dir != Vector3.zero)
        {
            transform.forward = dir;

            Vector3 nextPos = transform.position + dir * gridSize;

            // 移動先にあるColliderを取得
            Collider[] hits = Physics.OverlapBox(
                nextPos,
                new Vector3(0.45f, 0.5f, 0.45f),
                Quaternion.identity,
                obstacleLayer
            );

            bool canMove = true;

            foreach (Collider hit in hits)
            {
                // 壁なら進めない
                if (hit.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    canMove = false;
                    break;
                }
                if (hit.gameObject.layer == LayerMask.NameToLayer("BreakableWall"))
                {
                    canMove = false;
                    break;
                }

                // 爆弾なら判定
                Bomb bomb = hit.GetComponent<Bomb>();

                if (bomb != null)
                {
                    // 通り抜け可能な爆弾なら無視
                    if (bomb.CanPass(this))
                        continue;

                    canMove = false;
                    break;
                }
            }

            if (canMove)
            {
                transform.forward = dir;
                targetPosition = nextPos;
                isMoving = true;
            }
        }

        if (animator != null && !isMoving)
            animator.SetFloat("Speed", 0);
    }

    void PlaceBomb()
    {
        if (bombPrefab == null)
            return;

        // 置ける個数を超えていたら置けない
        if (currentBombCount >= maxBombCount)
            return;

        Vector3 spawnPos = new Vector3(
            Mathf.Round(transform.position.x / gridSize) * gridSize,
            transform.position.y,
            Mathf.Round(transform.position.z / gridSize) * gridSize
        );

        bool push = false;

        if (pad != null)
        {
            push = pad.buttonSouth.wasPressedThisFrame;
        }

        if (!push)
            return;

        GameObject bomb = Instantiate(
            bombPrefab,
            spawnPos,
            Quaternion.identity
        );

        Bomb bombScript = bomb.GetComponent<Bomb>();

        if (bombScript != null)
        {
            // プレイヤーの火力を爆弾へ渡す
            bombScript.power = power;
            bombScript.SetPlayer(this);
        }

        currentBombCount++;
    }

    public void ReturnBomb()
    {
        currentBombCount--;

        if (currentBombCount < 0)
            currentBombCount = 0;
    }

}
