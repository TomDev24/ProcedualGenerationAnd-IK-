using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGenerator : MonoBehaviour
{
    public GameObject[] objsToPick;

    public float itemXspread = 10;
    public float itemYspread = 0;
    public float itemZspread = 10;

    public int spawnAmount = 10;

    private void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            SpreadItem();
        }
    }

    private void SpreadItem()
    {
        var itemPosition = transform.position + new Vector3(Random.Range(-itemXspread, itemXspread), Random.Range(-itemYspread, itemYspread), Random.Range(-itemZspread, itemZspread));
        PickAndSpawn(itemPosition, Quaternion.identity);
    }

    private void PickAndSpawn(Vector3 position, Quaternion rot)
    {
        int randIndex = UnityEngine.Random.Range(0, objsToPick.Length);
        GameObject clone = Instantiate(objsToPick[randIndex], position, rot) as GameObject;
    }
}
