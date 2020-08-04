using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

	public float speed;
	public Rigidbody2D rig;

	Vector3 moveDirection;
    public Vector3 faceDirection;

    public Vector3 MoveDirection { get { return moveDirection; }}

	public Animator animator;

    public float tileDistance = 1.28f;

    public bool playerlocked;

	private void Awake()
	{
		instance = this;
    }

    void Start ()
    {
		transform.position = FarmUtilites.GetCenterOfCell(TilemapGroup.normal, transform.position);
		moveDirection = transform.position;
	}

    public void LockPlayer (bool _isLocked)
    {
        playerlocked = _isLocked;
    }

	void Update ()
	{
        if(GameManager.instance.CurrentState != GameState.Game)
            return;
        
		if(transform.position != moveDirection)
		{
            rig.position = Vector3.MoveTowards(transform.position , moveDirection , speed * Time.deltaTime);
		}	
		
		if (!playerlocked)
		{
			if(transform.position == moveDirection)
			{
                if(Input.GetButton("Up") && CanMove(Vector3.up) && !Input.GetButton("Left") && !Input.GetButton("Right"))
				{
					Move(Vector3.up);
				}

                if(Input.GetButton("Down") && CanMove(Vector3.down) && !Input.GetButton("Left") && !Input.GetButton("Right"))
				{
					Move(Vector3.down);
				}

                if(Input.GetButton("Left") && CanMove(Vector3.left) && !Input.GetButton("Up") && !Input.GetButton("Down"))
				{
					Move(Vector3.left);
				}

                if(Input.GetButton("Right") && CanMove(Vector3.right) && !Input.GetButton("Up") && !Input.GetButton("Down"))
				{
				    Move(Vector3.right);
				}
			}
		}
	}

	public void Move(Vector3 direction)
	{
		moveDirection += direction * tileDistance;
		faceDirection = direction.normalized;
		animator.SetFloat("Horizontal", direction.normalized.x);
		animator.SetFloat("Vertical", direction.normalized.y);
	}

	public bool CanMove(Vector3 _direction)
	{
        return !Physics2D.BoxCast(transform.position + _direction * tileDistance, Vector2.one , 0 ,Vector2.zero , 1, ~(1 << LayerMask.NameToLayer("FloorInteract")));
	}


    public void Save()
    {
        Vector3 savePos = transform.position;

        string save = JsonUtility.ToJson(savePos);
        PlayerPrefs.SetString(SaveData.c_player, save);
    }

    public void Load()
    {
        string a = PlayerPrefs.GetString(SaveData.c_player);

        if (string.IsNullOrEmpty(a))
            return;

        Vector3 newPos = (Vector3)JsonUtility.FromJson(a , typeof(Vector3));

        transform.position = newPos;
        moveDirection = newPos;
    }
}
