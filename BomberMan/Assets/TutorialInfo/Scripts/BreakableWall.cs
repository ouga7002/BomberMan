using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [Header("出現するアイテム")]
    public GameObject[] itemPrefabs;

    [Range(0, 100)]
    public int dropRate = 30;   //30%で出現

    public void Break()
    {
        if (itemPrefabs.Length > 0 &&
            Random.Range(0, 100) < dropRate)
        {
            int index = Random.Range(0, itemPrefabs.Length);

            Instantiate(
                 itemPrefabs[index],
                 transform.position,
                 itemPrefabs[index].transform.rotation
            );
        }

        Destroy(gameObject);
    }
}