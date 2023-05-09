using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    [SerializeField] PlayerController m_PlayerController;
    [SerializeField] GameObject m_MainBrick;
    [SerializeField] Transform m_CharacterSprite;
    [SerializeField] Transform m_StackBrick;
    Stack<GameObject> m_SpawnBricks = new Stack<GameObject>();
    Stack<GameObject> m_SetBricks = new Stack<GameObject>();
    private void Start()
    {
        m_PlayerController.SetPlayerCollection(this);
    }
    void Update()
    {
        PositionUpdate();
    }
    void PositionUpdate()
    {
        Vector3 newPos = m_StackBrick.position + Vector3.up * 0.3f * m_PlayerController.GetBrickCheckedCount();
        m_CharacterSprite.position = newPos;
    }
    public void SpawnBrick()
    {
        GameObject brickClone = m_SpawnBricks.Count>0? m_SpawnBricks.Pop():Instantiate(m_MainBrick,m_StackBrick);
        brickClone.SetActive(true);
        /*brickClone.transform.rotation = Quaternion.Euler(new Vector3(-90,90, -212.305f));*/
        brickClone.gameObject.transform.position = m_StackBrick.position + Vector3.up * 0.3f * (m_PlayerController.GetBrickCheckedCount()-1);
        brickClone.transform.parent = m_StackBrick;
        m_SetBricks.Push(brickClone);
    }
    public void DespawnBrick()
    {
        if (m_SetBricks.Count <= 0) return;
        GameObject brickClone = m_SetBricks.Pop();
        brickClone.SetActive(false);
        m_SpawnBricks.Push(brickClone);
    }
   
}
