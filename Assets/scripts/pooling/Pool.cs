using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{
    private static Dictionary<PooledMonoBehavior, Pool> pools = new Dictionary<PooledMonoBehavior, Pool>();
    public Queue<PooledMonoBehavior> poolQueue = new Queue<PooledMonoBehavior>();
    public PooledMonoBehavior PooledObjectPrefab;

    public static Pool GetPool (PooledMonoBehavior prefab)
    {
        if (pools.ContainsKey(prefab))
        {
            return pools[prefab];
        }

        var newPool = new GameObject("pool -" + prefab.name).AddComponent<Pool>();
        newPool.PooledObjectPrefab = prefab;
        pools.Add(prefab,newPool);
        newPool.GrowPool();
        return newPool;
    }

    public T Get <T>() where T : PooledMonoBehavior
    {
        if (poolQueue.Count < 1)
        {
            GrowPool();
        }
        var pooledObj = poolQueue.Dequeue();
        return pooledObj as T;
    }

    private void GrowPool()
    {
        for (int i = 0; i < PooledObjectPrefab.initilalPoolSize; i++)
        {
            var pooledObj = Instantiate(PooledObjectPrefab) as PooledMonoBehavior;
            pooledObj.OnReturnToPool += ReturnToPool;

            pooledObj.transform.SetParent(this.transform);
            pooledObj.gameObject.SetActive(false);

        }
    }

    private void ReturnToPool(PooledMonoBehavior PooledObj)
    {
  
        poolQueue.Enqueue(PooledObj);
    }
}
