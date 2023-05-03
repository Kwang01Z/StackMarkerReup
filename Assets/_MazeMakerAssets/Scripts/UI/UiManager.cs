using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] PlayerController m_PlayerController;
    [SerializeField] WinUI m_WinUI;
    void Update()
    {
        m_WinUI.gameObject.SetActive(m_PlayerController.IsCollisionWin());
    }
}
