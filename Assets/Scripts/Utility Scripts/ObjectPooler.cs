using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler instance;

    void Awake()
    {
        instance = this;
    }
    #endregion Singleton

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;



    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject newObj = Instantiate(pool.prefab, transform);
                newObj.SetActive(false);
                objectPool.Enqueue(newObj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }



    public GameObject SpawnFromPool(string tag, Vector2 position)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist!!");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.SetActive(true);

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        pooledObj.OnObjectSpawn();

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public GameObject SpawnExplosionFromPool(Vector2 position, int explosionDamage, float explosionForce, float explosionRadius, GameObject objThatIgnoresDamage)
    {
        string tag = "Explosion";

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist!!");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.SetActive(true);

        Explosion explosion = objectToSpawn.GetComponent<Explosion>();
        explosion.SetExplosionDamage(explosionDamage);
        explosion.SetExplosionForce(explosionForce);
        explosion.SetExplosionRadius(explosionRadius);
        explosion.SetObjThatIgnoresDamage(objThatIgnoresDamage);
        explosion.OnObjectSpawn();

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}