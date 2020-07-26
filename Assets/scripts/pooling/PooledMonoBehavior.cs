using UnityEngine;
using System;
public class PooledMonoBehavior : MonoBehaviour
{
    [SerializeField]protected int initialPoolSize = 50;
    public int initilalPoolSize { get { return initialPoolSize; } protected set { initialPoolSize = value; } }


    public event Action<PooledMonoBehavior> OnReturnToPool;

    protected virtual void OnDisable()
    {
        OnReturnToPool?.Invoke(this);
    }

    public T Get<T>(bool shouldEnable = true) where T : PooledMonoBehavior
    {
        var pool = Pool.GetPool(this);
        var pooledObj = pool.Get<T>();

        if (shouldEnable)
        {
            pooledObj.gameObject.SetActive(true);
        }
        return pooledObj;
    }

    public T Get<T> (Vector3 position, Quaternion rotation) where T : PooledMonoBehavior
    {
        var pooledObj = Get<T>();

        pooledObj.transform.position = position;
        pooledObj.transform.rotation = rotation;

        return pooledObj;
    }

    protected void ReturnToPool(float delay = 0)
    {
        Invoke("BackToPool", delay);
    }
    //void BackToPool()
    //{
    //    gameObject.SetActive(false);
    //}

    public void SetPosition (Transform targetpos)
    {
        transform.position = targetpos.position;
    }
}