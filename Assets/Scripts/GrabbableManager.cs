using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableManager : MonoBehaviour {

	private static GrabbableManager s_instance;
	public static GrabbableManager Instance
	{
		get
		{
			if(s_instance == null)
			{
				GameObject gameObject = new GameObject("GrabManager");
				s_instance = gameObject.AddComponent<GrabbableManager>();
			}
			return s_instance;
		}
	}

	private List<Grabbable>m_grabbables = new List<Grabbable>();

	public void AddGrabbable(Grabbable grabbable)
	{
		if(m_grabbables.IndexOf(grabbable)!= -1) return;
		m_grabbables.Add(grabbable);
	}

	public void RemoveGrabbable(Grabbable grabbable)
	{
		m_grabbables.Remove(grabbable);
	}

	public Grabbable GetClosestGrabbableInRadius(Vector3 position, float distance)
	{
		Grabbable closestGrabbable = null;

		int numGrabbables = m_grabbables.Count;
		for(int i=0; i<numGrabbables; i++)
		{
			Grabbable grabbable =  m_grabbables[i];
			float separation = Vector3.Distance(grabbable.transform.position, position);
			if(separation < distance)
			{
				if(closestGrabbable == null)
				{
					closestGrabbable = grabbable;
				}
				else if(separation < Vector3.Distance(closestGrabbable.transform.position, position))
				{
					closestGrabbable = grabbable;
				}
			}
		}

		return closestGrabbable;
	}
}
