using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortManager : MonoBehaviour {

	private static SortManager s_instance;
	public static SortManager Instance
	{
		get
		{
			if(s_instance == null)
			{
				GameObject gameObject = new GameObject("SortManager");
				s_instance = gameObject.AddComponent<SortManager>();
			}
			return s_instance;
		}
	}

	private List<Sortable>m_sortables = new List<Sortable>();

	public void AddSortable(Sortable sortable)
	{
		if(m_sortables.IndexOf(sortable)!= -1) return;
		m_sortables.Add(sortable);
	}

	public void RemoveSortable(Sortable sortable)
	{
		m_sortables.Remove(sortable);
	}

	private void Update()
	{
		m_sortables.Sort(delegate(Sortable a, Sortable b)
		{
			if(a.GetSortDistance() < b.GetSortDistance()) return 1;
			if(a.GetSortDistance() > b.GetSortDistance()) return -1;
			else return 0;
		});

		int numSortables = m_sortables.Count;
		for(int i=0; i<numSortables; i++)
		{
			m_sortables[i].SetOrder(i);
		}
	}
}
