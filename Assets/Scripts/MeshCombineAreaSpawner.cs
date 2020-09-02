using System.Collections.Generic;
using UnityEngine;

public class MeshCombineAreaSpawner : MonoBehaviour
{
    public GameObject itemToSpread;

    public int amountToSpawn = 10;

    public float itemXspread = 10;
    public float itemYspread = 0;
    public float itemZspread = 10;

    public Vector3 randomizeByAxis = Vector3.zero;

    public GameObject[] materialContainers;

    private List<GameObject> gameObjToCombine = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            SpreadItem();
        }

        for (int j = 0; j < materialContainers.Length; j++)
        {
            CombineMeshes(materialContainers[j]);
        }
    }


    private void SpreadItem()
    {
        var itemPosition = transform.position + new Vector3(Random.Range(-itemXspread, itemXspread), Random.Range(-itemYspread, itemYspread), Random.Range(-itemZspread, itemZspread));

        GameObject clone = Instantiate(itemToSpread, itemPosition, itemToSpread.transform.rotation);
        RandomizeByAxis(clone.transform);

        clone.transform.SetParent(materialContainers[Random.Range(0, materialContainers.Length)].transform);
        gameObjToCombine.Add(clone);
    }

    private void CombineMeshes(GameObject obj)
    {
        //Temporoly set position to zero, to simplify matrix calculation
        Vector3 pos = obj.transform.position;
        obj.transform.position = Vector3.zero;

        MeshFilter[] meshFilters = obj.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < combine.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false); //can be destroyed
        }

        obj.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        obj.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine, true, true); // two getComponent not very good, 
        obj.gameObject.SetActive(true); // both true means save coords and merge in one sub mesh.

        //restoring position;
        obj.transform.position = pos;

        //obj.AddComponent<MeshCollider>();
    }

    private void RandomizeByAxis(Transform rotationObj)
    {
        Quaternion randomRot = Quaternion.Euler(
            rotationObj.rotation.eulerAngles.x + Random.Range(-randomizeByAxis.x, randomizeByAxis.x),
            rotationObj.rotation.eulerAngles.y + Random.Range(-randomizeByAxis.y, randomizeByAxis.y),
            rotationObj.rotation.eulerAngles.z + Random.Range(-randomizeByAxis.z, randomizeByAxis.z)
            );

        rotationObj.rotation = randomRot;
    }
}
