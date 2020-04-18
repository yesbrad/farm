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
            rig.position = Vector3.MoveTowards(transform.position , moveDirection , Time.deltaTime * speed);
		}	
		
		if (!playerlocked)
		{
			if(transform.position == moveDirection)
			{
                if(Input.GetButton("Up") && CanMove(Vector3.up) && !Input.GetButton("Left") && !Input.GetButton("Right"))
				{
					moveDirection += Vector3.up * tileDistance;
                    faceDirection = Vector3.up;
					SetAni("Up");
				}

                if(Input.GetButton("Down") && CanMove(Vector3.down) && !Input.GetButton("Left") && !Input.GetButton("Right"))
				{
					moveDirection += Vector3.down * tileDistance;
                    faceDirection = Vector3.down;
					SetAni("Down");
				}

                if(Input.GetButton("Left") && CanMove(Vector3.left) && !Input.GetButton("Up") && !Input.GetButton("Down"))
				{
				    moveDirection += Vector3.left * tileDistance;
                    faceDirection = Vector3.left;
				    SetAni("Left");
				}

                if(Input.GetButton("Right") && CanMove(Vector3.right) && !Input.GetButton("Up") && !Input.GetButton("Down"))
				{
				    moveDirection += Vector3.right * tileDistance;
                    faceDirection = Vector3.right;
					SetAni("Right");
				}


			}
		}
	}

	public static bool AboutEqual(double x, double y)
    {
        double epsilon = Math.Max(Math.Abs(x), Math.Abs(y)) * 1E-15;
        return Math.Abs(x - y) <= epsilon;
    }

	public void SetAni (string dir)
	{
        animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
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
