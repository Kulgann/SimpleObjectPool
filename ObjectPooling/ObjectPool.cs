using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;

    public List<PooledObject> ObjectsToPool;

    private List<List<GameObject>> poledObjects;

    private List<int> ringBufferPositions;

    public static ObjectPool Instance { get => _instance;}

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        poledObjects = new List<List<GameObject>>(ObjectsToPool.Count);
        for (int i = 0; i < ObjectsToPool.Count; i++)
        {
            poledObjects.Add(new List<GameObject>());
        }
        ringBufferPositions = new List<int>();
        for (int i = 0; i < ObjectsToPool.Count; i++)
        {
            ringBufferPositions.Add(0);
        }
        foreach (var item in ObjectsToPool)
        {
            for (int i = 0; i < item.amoutToPool; i++)
            {
                //We instantiate the object and add it to the pool
                GameObject temp = Instantiate(item.prefab, transform);
                temp.SetActive(false);
                poledObjects[item.id].Add(temp);
            }
           
        }
    }

    #region GetPooledObject Overloads
    public GameObject GetPooledObject(int id,Transform parent)
    {
        int nRingBufferSize = poledObjects[id].Count;
        ringBufferPositions[id] = (ringBufferPositions[id] + 1) % nRingBufferSize;
        GameObject temp = poledObjects[id][ringBufferPositions[id]];
        temp.transform.SetParent(parent);
        temp.SetActive(true);
        return temp;
    }
    public GameObject GetPooledObject(int id, Vector3 pos)
    {
        int nRingBufferSize = poledObjects[id].Count;
        ringBufferPositions[id] = (ringBufferPositions[id] + 1) % nRingBufferSize;
        GameObject temp = poledObjects[id][ringBufferPositions[id]];
        temp.transform.position = pos;
        temp.SetActive(true);
        return temp;
    }
    public GameObject GetPooledObject(int id, Vector3 pos, Quaternion rot)
    {
        int nRingBufferSize = poledObjects[id].Count;
        ringBufferPositions[id] = (ringBufferPositions[id] + 1) % nRingBufferSize;
        GameObject temp = poledObjects[id][ringBufferPositions[id]];
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        return temp;
    }
    public GameObject GetPooledObject(int id, Vector3 pos , Quaternion rot , Transform parent)
    {
        int nRingBufferSize = poledObjects[id].Count;
        ringBufferPositions[id] = (ringBufferPositions[id] + 1) % nRingBufferSize;
        GameObject temp = poledObjects[id][ringBufferPositions[id]];
        temp.transform.SetParent(parent);
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.SetActive(true);
        return temp;
    }
    #endregion
}
[System.Serializable]
public class PooledObject 
{
    [ShowOnly] public int id;
    public int amoutToPool;
    public GameObject prefab;
}