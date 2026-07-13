using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public GameObject[] itemPrefabs;

    [Range(0, 100)]
    public int itemRate = 30;   //30%で出現

    public void Break()
    {
        if (Random.Range(0, 100) < itemRate)
        {
            int index = Random.Range(0, itemPrefabs.Length);

            Instantiate(
                itemPrefabs[index],
                transform.position,
                Quaternion.identity
            );
        }

        Destroy(gameObject);
    }
}