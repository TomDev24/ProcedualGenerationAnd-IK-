using UnityEngine;

public class SpawnPrefabGrid : MonoBehaviour
{
    public bool haveY = false; 

    public GameObject[] objsToPick;
    public int gridX = 5;
    public int gridZ = 5;
    public int gridY = 5;
    public float offset = 1f;
    public Vector3 gridOrigin = Vector3.zero;
    public Vector3 randomizeRange = Vector3.zero;
    public bool setGridPositionToObj = true;

    private void Start()
    {
        if (setGridPositionToObj)
            gridOrigin = transform.position;

        if (!haveY)
            SpawnGrid();
        else
            SpawnGridWithY();
    }

    private void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                var pos = new Vector3(x * offset, 0, z * offset) + gridOrigin;
                PickAndSpawn(RandomizePosition(pos), Quaternion.identity);
            }
        }
    }

    private void SpawnGridWithY()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                for (int y = 0; y < gridY; y++)
                {
                    Vector3 pos = new Vector3(x * offset, y * offset, z * offset) + gridOrigin;
                    PickAndSpawn(RandomizePosition(pos), Quaternion.identity);
                }
            }
        }
    }

    private Vector3 RandomizePosition(Vector3 pos)
    {
        var randPos = pos + new Vector3(Random.Range(-randomizeRange.x, randomizeRange.x), Random.Range(-randomizeRange.y, randomizeRange.y), Random.Range(-randomizeRange.z, randomizeRange.z));
        return randPos;
    }

    private void PickAndSpawn(Vector3 position, Quaternion rot)
    {
        int randIndex = UnityEngine.Random.Range(0, objsToPick.Length);
        GameObject clone = Instantiate(objsToPick[randIndex], position, rot) as GameObject;
    }
}
