using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
    //item spawn info
    public GameObject[] itemsToSpawn;
    public Transform spawnArea;       
    public Vector2 areaSize = new Vector2(60, 10);

    private void Start()
    {
        StartCoroutine(SpawnItemsOverTime(10f)); //spawning an item every 10 seconds
    }

    //spawning items over a certain amount of time
    private IEnumerator SpawnItemsOverTime(float itemSpawn)
    {
        while (true)
        {
            //picking a random item out of the 2
            GameObject itemPrefab = itemsToSpawn[Random.Range(0, itemsToSpawn.Length)];


            //generate a random 2D position for item spawn
            Vector2 randomPos = new Vector2(
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                Random.Range(-areaSize.y / 2f, areaSize.y / 2f));

            //getting spawn area
            Vector3 spawnPos = spawnArea.position + (Vector3)randomPos;

            //spawning item
            GameObject spawnedItem = Instantiate(itemPrefab, spawnPos, Quaternion.identity);
            spawnedItem.transform.parent = spawnArea;

            yield return new WaitForSeconds(itemSpawn);
        }
    }

    //visualize the spawn area in the Scene
    void OnDrawGizmosSelected()
    {
        if (spawnArea != null)
        {
            Gizmos.color = Color.green;
            Vector2 size3D = new Vector2(areaSize.x, areaSize.y);
            Gizmos.DrawWireCube(spawnArea.position, size3D);
        }
    }
}
