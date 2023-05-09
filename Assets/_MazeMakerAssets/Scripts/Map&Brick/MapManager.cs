using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] List<GameObject> m_Maps;
    int m_CurrentMap = 0;
    GameObject currentMapObj;
    void Start()
    {
        currentMapObj = Instantiate(m_Maps[m_CurrentMap]);
    }
    public void ChangeNextMap()
    {
        if (m_CurrentMap < m_Maps.Count - 1)
        {
            currentMapObj.gameObject.SetActive(false);
            m_CurrentMap++;
            currentMapObj = Instantiate(m_Maps[m_CurrentMap]);
        }
    }
    void RefreshMap()
    {
        for (int i = 0; i < m_Maps.Count; i++)
        {
             m_Maps[i].SetActive(i == m_CurrentMap);
        }
    }
    public Pivote GetPivoteOrigin()
    {
        return m_Maps[m_CurrentMap].GetComponent<MapBase>().GetPivoteOrigin();
    }
    public int GetMapCount()
    {
        return m_Maps.Count;
    }
}
