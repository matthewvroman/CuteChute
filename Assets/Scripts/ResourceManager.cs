using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

	private static ResourceManager s_instance;
	public static ResourceManager Instance
	{
		get
		{
			if(s_instance == null)
			{
				GameObject gameObject = new GameObject("ResourceManager");
				s_instance = gameObject.AddComponent<ResourceManager>();
			}
			return s_instance;
		}
	}

	private Dictionary<string, GameObject>m_resources = new Dictionary<string, GameObject>();

	public GameObject GetResource(string resource)
	{
		if(m_resources.ContainsKey(resource))
		{
			return m_resources[resource];
		}
		else
		{
			GameObject gameObject = Resources.Load<GameObject>(resource);
			m_resources[resource] = gameObject;
			return gameObject;
		}
	}
}
