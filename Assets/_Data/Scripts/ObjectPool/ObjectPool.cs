using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : BaseManager<ObjectPool>
{
    [HideInInspector]
    public List<Bullet> pooledObjects;
    public Bullet objectToPool;
    private int amountToPool;

    private void Start()
    {
        pooledObjects = new List<Bullet>();
        amountToPool = 30;
        Bullet bullet;
        for (int i = 0; i < amountToPool; i++)
        {
            bullet = Instantiate(objectToPool, this.transform, true);
            bullet.Deactive();
            pooledObjects.Add(bullet);
        }
    }

    public Bullet GetPooledObject()
    {
        foreach (Bullet bullet in pooledObjects)
        {
            if (!bullet.IsActive)
            {
                return bullet;
            }
        }
        Bullet newBullet = Instantiate(objectToPool, this.transform, true);
        pooledObjects.Add(newBullet);
        return newBullet;
    }
}
