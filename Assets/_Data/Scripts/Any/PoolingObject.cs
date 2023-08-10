using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingObject : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int amount = 7;
    [SerializeField] private List<GameObject> pools = new List<GameObject>();
    [SerializeField] private Transform spawnPool;

    private void Start()
    {
        if (this.pools.Count < this.amount)
        {
            int need = this.amount - this.pools.Count;
            for (int i = 0; i < need; i++)
            {
                GameObject newObj = Instantiate(this.objectPrefab, this.spawnPool);
                newObj.SetActive(false);
                this.pools.Add(newObj);
            }
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < this.pools.Count; i++)
        {
            if (this.pools[i].activeSelf == false)
            {
                this.pools[i].transform.position = position;
                this.pools[i].transform.rotation = rotation;
                this.pools[i].SetActive(true);
                return this.pools[i];
            }
        }

        GameObject newObj = Instantiate(this.objectPrefab, position, rotation, this.spawnPool);
        this.pools.Add(newObj);
        return newObj;
    }
}
