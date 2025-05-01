using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance { get; private set; }

        private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
        private Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void CreatePool(string tag, GameObject prefab, int size, Transform parent)
        {
            if (poolDictionary.ContainsKey(tag)) return;

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < size; i++)
            {
                GameObject obj = Instantiate(prefab, parent);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(tag, objectPool);
            prefabDictionary.Add(tag, prefab);
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"풀 {tag}가 없습니다.");
                return null;
            }

            GameObject objectToSpawn;

            if (poolDictionary[tag].Count > 0)
            {
                objectToSpawn = poolDictionary[tag].Dequeue();
            }
            else
            {
                if (!prefabDictionary.TryGetValue(tag, out GameObject prefab))
                {
                    Debug.LogError($"Prefab이 등록되지 않은 태그: {tag}");
                    return null;
                }

                objectToSpawn = Instantiate(prefab);
                Debug.LogWarning($"풀 '{tag}'의 크기를 초과했습니다.");
            }

            objectToSpawn.transform.SetPositionAndRotation(position, rotation);
            objectToSpawn.SetActive(true);

            return objectToSpawn;
        }

        public void ReturnToPool(string tag, GameObject obj)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"풀 {tag}가 없습니다.");
                Destroy(obj);
                return;
            }

            obj.SetActive(false);

            poolDictionary[tag].Enqueue(obj);
        }
    }
}
