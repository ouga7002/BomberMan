using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("爆発までの時間")]
    public float explodeTime = 2f;

    [Header("爆発エフェクト")]
    public GameObject explosionPrefab;

    void Start()
    {
        Invoke(nameof(Explode), explodeTime);
    }

    void Explode()
    {
        // 爆発エフェクト生成
        if (explosionPrefab != null)
        {
            Instantiate(
                explosionPrefab,
                transform.position,
                Quaternion.identity
            );
        }

        // 爆弾を削除
        Destroy(gameObject);
    }
}