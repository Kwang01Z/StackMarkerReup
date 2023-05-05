using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Pivote m_OriginPosition;
    [SerializeField] PlayerAnim m_PlayerAnim;
	[SerializeField] float m_Speed;
	Pivote m_CurrentPivote;
	bool m_IsSwiping;
	Vector2 m_StartingTouch;
	public float pivoteRadiusCheck = 0.32f;
	int m_BrickCheckedCount = 0;
	PlayerCollection m_PlayerCollection;
	bool m_WinCollision;
    void Start()
    {
        m_CurrentPivote = m_OriginPosition;
        SetPosition(m_CurrentPivote);
    }
    void Update()
    {
		if (m_WinCollision) return;
        MoveToBrick();
		InputUpdate();
		CheckPivoteAuto();
	}
	void SetNewBrick(Pivote a_Pivote)
    {
		if (a_Pivote)
		{
			m_CurrentPivote = a_Pivote;
			m_PlayerAnim.TriggerJump();
		}
    }
    void MoveToBrick()
    {
        transform.position = Vector3.MoveTowards(transform.position, Pivote.GetPosToStand(m_CurrentPivote),m_Speed*Time.deltaTime);
    }
	public int GetBrickCheckedCount()
	{
		return m_BrickCheckedCount;
	}
    void SetPosition(Pivote a_Pivote)
    {
        Vector3 newPos = Pivote.GetPosToStand(a_Pivote);
        transform.position = newPos;
    }
	void CheckPivoteAuto()
	{
		Collider[] pivotes = Physics.OverlapSphere(transform.position, pivoteRadiusCheck);
		if (pivotes.Length > 0)
		{
			foreach (Collider collider in pivotes)
			{
				if (collider.gameObject.GetComponent<WinTrigger>())
				{
					m_WinCollision = true;
				}
				if (collider.gameObject.GetComponent<Pivote>())
				{
					Pivote pivote = collider.gameObject.GetComponent<Pivote>();
					if (!pivote.IsChecked())
					{
						pivote.SetCheck(true);
						if (pivote.IsFilled())
						{
							m_BrickCheckedCount++;
							m_PlayerCollection.SpawnBrick();
						}
						else
						{
							m_BrickCheckedCount--;
							m_PlayerCollection.DespawnBrick();
						}
						Debug.Log(m_BrickCheckedCount);
					}
				}
			}
		}
	}
	public bool IsCollisionWin()
	{
		return m_WinCollision;
	}
	void InputUpdate()
	{
		if (m_CurrentPivote)
		{
			if (Vector3.Distance(transform.position, m_CurrentPivote.transform.position) >= 2.85f)
			{
				return;
			}
		}
#if UNITY_EDITOR || UNITY_STANDALONE
		InputFromComputer();
#else
        InputFromMobile();
#endif
	}
	void InputFromMobile()
	{
		if (Input.touchCount == 1)
		{
			if (m_IsSwiping)
			{
				Vector2 diff = Input.GetTouch(0).position - m_StartingTouch;

				diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

				if (diff.magnitude > 0.01f)
				{
					if (Mathf.Abs(diff.y) <= Mathf.Abs(diff.x))
					{
						if (diff.x < 0)
						{
							GoLeft();
						}
						else
						{
							GoRight();
						}
					}
					else
					{
						if (diff.y > 0)
						{
							GoFoward();
						}
						else if (diff.y < 0)
						{
							// Go down
							GoBehind();
						}
					}
					m_IsSwiping = false;
				}
			}

			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				m_StartingTouch = Input.GetTouch(0).position;
				m_IsSwiping = true;
			}
			else if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				m_IsSwiping = false;
			}
		}
	}
	void InputFromComputer()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			GoLeft();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			GoRight();
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			GoFoward();
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			GoBehind();
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPaused = !UnityEditor.EditorApplication.isPaused;
#endif
		}
	}
	public void GoLeft()
	{
		m_PlayerAnim.transform.localRotation = Quaternion.Euler(180f, 150f, -180f);
		GoToDir(Pivote.PivoteDirection.LEFT);
	}
	public void GoRight()
	{
		m_PlayerAnim.transform.localRotation = Quaternion.Euler(180f, -30f, -180f);
		GoToDir(Pivote.PivoteDirection.RIGHT);
	}
	public void GoFoward()
	{
		m_PlayerAnim.transform.localRotation = Quaternion.Euler(180f, 240f, -180f);
		GoToDir(Pivote.PivoteDirection.FRONT);
	}
	public void GoBehind()
	{
		m_PlayerAnim.transform.localRotation = Quaternion.Euler(180f, 60f, -180f);
		GoToDir(Pivote.PivoteDirection.BEHIND);
	}
	void GoToDir(Pivote.PivoteDirection a_Dir)
	{
        while (m_CurrentPivote.IsBrickCollion(a_Dir))
        {
            SetNewBrick(m_CurrentPivote.GetCollionBrick(a_Dir));
        }
    }
	public void SetPlayerCollection(PlayerCollection collection)
	{
		m_PlayerCollection = collection;
	}
	public PlayerAnim GetPlayerAnim()
	{
		return m_PlayerAnim;
	}
    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, pivoteRadiusCheck);
    }
}
