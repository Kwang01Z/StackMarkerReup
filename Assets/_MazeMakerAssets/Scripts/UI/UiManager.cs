using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] PlayerController m_PlayerController;
    [SerializeField] WinUI m_WinUI;
    bool m_Checked;
    private void Start()
    {
        m_WinUI.gameObject.SetActive(false);
    }
    void Update()
    {
        WinUpload();
    }
    void WinUpload()
    {
        if (!m_PlayerController.IsCollisionWin()) return;
        if (!m_Checked)
        {
            m_Checked = true;
            m_PlayerController.GetPlayerAnim().TriggerWin();
            StartCoroutine(OpenWinLayout());
        }
    }
    IEnumerator OpenWinLayout()
    {
        yield return new WaitForSeconds(3);
        m_WinUI.gameObject.SetActive(true);
    }
    public void ChangeNextMap()
    {
        m_PlayerController.ResetGame();
        m_PlayerController.ChangeNextMap();
        m_WinUI.gameObject.SetActive(false);
        m_Checked = false;
        
    }
}
