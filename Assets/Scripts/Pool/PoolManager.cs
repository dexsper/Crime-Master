using System;
using System.Collections.Generic;
using MonsterLove.Collections;
using UnityEngine;
using Zenject;

public class PoolManager : MonoBehaviour
{
	public bool logStatus;

	private Dictionary<GameObject, ObjectPool<GameObject>> prefabLookup;
	private Dictionary<GameObject, ObjectPool<GameObject>> instanceLookup; 
	
	private bool dirty = false;

	[Inject]
	private DiContainer _container;
	
	private void Awake () 
	{
		prefabLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
		instanceLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
	}

	private void Update()
	{
		if(logStatus && dirty)
		{
			PrintStatus();
			dirty = false;
		}
	}

	public void WarmPool(GameObject prefab, int size)
	{
		if(prefabLookup.ContainsKey(prefab))
		{
			throw new Exception("Pool for prefab " + prefab.name + " has already been created");
		}
		var pool = new ObjectPool<GameObject>(() => { return InstantiatePrefab(prefab); }, size);
		prefabLookup[prefab] = pool;

		dirty = true;
	}

	public GameObject SpawnObject(GameObject prefab)
	{
		return SpawnObject(prefab, Vector3.zero, Quaternion.identity);
	}

	public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
	{
		if (!prefabLookup.ContainsKey(prefab))
		{
			WarmPool(prefab, 1);
		}

		var pool = prefabLookup[prefab];

		var clone = pool.GetItem();
		clone.transform.SetPositionAndRotation(position, rotation);
		clone.transform.parent = parent;

		instanceLookup.Add(clone, pool);
		dirty = true;
		return clone;
	}

	public void ReleaseObject(GameObject clone)
	{
		clone.SetActive(false);
		clone.transform.parent = transform;

		if(instanceLookup.ContainsKey(clone))
		{
			instanceLookup[clone].ReleaseItem(clone);
			instanceLookup.Remove(clone);
			dirty = true;
		}
		else
		{
			Debug.LogWarning("No pool contains the object: " + clone.name);
		}
	}


	private GameObject InstantiatePrefab(GameObject prefab)
	{
		var go = _container.InstantiatePrefab(prefab);
		go.transform.parent = transform;
		return go;
		
	}

	public void PrintStatus()
	{
		foreach (KeyValuePair<GameObject, ObjectPool<GameObject>> keyVal in prefabLookup)
		{
			Debug.Log(string.Format("Object Pool for Prefab: {0} In Use: {1} Total {2}", keyVal.Key.name, keyVal.Value.CountUsedItems, keyVal.Value.Count));
		}
	}
}


