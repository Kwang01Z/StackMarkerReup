using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] Animator m_PlayerAnim;
    public Animator GetAnimator => m_PlayerAnim;
    private void Reset()
    {
        m_PlayerAnim = GetComponent<Animator>();
    }
    public void TriggerWin() 
    {
        m_PlayerAnim.SetTrigger("win");
    }
    public void TriggerJump()
    {
        m_PlayerAnim.SetTrigger("jump");
    }
}
