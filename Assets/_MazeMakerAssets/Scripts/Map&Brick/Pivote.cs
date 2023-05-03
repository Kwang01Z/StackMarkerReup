using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivote : MonoBehaviour
{
    public enum PivoteDirection
    {
        LEFT,
        RIGHT,
        FRONT,
        BEHIND
    }
    [SerializeField] Brick m_Brick;
    [SerializeField] FilledBrick m_FilledBrick;
    bool m_IsFilled;
    bool m_IsChecked;
    private void Reset()
    {
        m_Brick = GetComponentInChildren<Brick>();
        m_FilledBrick = GetComponentInChildren<FilledBrick>();
    }
    private void Start()
    {
        m_IsFilled = m_Brick.gameObject.activeInHierarchy;
    }
    private void Update()
    {
        if (m_IsChecked)
        {
            if (!m_IsFilled)
            {
                m_FilledBrick.gameObject.SetActive(true);
            }
            else
            {
                m_Brick.gameObject.SetActive(false);
            }
        }
    }
    public bool IsFilled()
    {
        return m_IsFilled;
    }
    public void SetCheck(bool a_Check)
    {
        m_IsChecked = a_Check;
    }
    public bool IsChecked()
    {
        return m_IsChecked;
    }
    public static Vector3 GetPosToStand(Pivote a_Pivote)
    {
        return a_Pivote.GetBrick().transform.position + Vector3.up * 0.3f;
    }
    Brick GetBrick()
    {
        return m_Brick;
    }
    
    public bool IsBrickCollion(Pivote.PivoteDirection a_dir)
    {
        return Physics.Raycast(transform.position, GetVectorFromDir(a_dir), 1f, 1 << 31);
    }
    public Pivote GetCollionBrick(PivoteDirection a_dir)
    {
        Pivote result = null;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, GetVectorFromDir(a_dir), 1f, 1 << 31);
        if (hits.Length > 0)
        {
            foreach (RaycastHit raycast in hits)
            {
                Pivote pivote = raycast.transform.GetComponent<Pivote>();
                if (pivote && pivote.GetBrick())
                {
                    result = pivote;
                    return result;
                }
            }
        }
        return result;
    }
    Vector3 GetVectorFromDir(PivoteDirection a_PivoteDirection)
    {
        switch (a_PivoteDirection)
        {
            case PivoteDirection.LEFT:
                return transform.right * -1;
            case PivoteDirection.RIGHT:
                return transform.right;
            case PivoteDirection.FRONT:
                return transform.forward;
            case PivoteDirection.BEHIND:
                return transform.forward * -1;
        }
        return Vector3.zero;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * -1);
        Gizmos.DrawLine(transform.position, transform.position + transform.right);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.Cross(transform.right, transform.forward));
        Gizmos.DrawLine(transform.position, transform.position + Vector3.Cross(transform.right, transform.forward) * -1);
    }
}
