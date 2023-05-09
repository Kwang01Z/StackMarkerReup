using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBase : MonoBehaviour
{
    [SerializeField] Pivote m_OriginPivote;
    public Pivote GetPivoteOrigin()
    {
        return m_OriginPivote;
    }

}
