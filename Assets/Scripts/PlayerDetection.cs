using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerDetection : MonoBehaviour
{
    public static PlayerDetection instance;

    Crop currentCrop;
    public Crop CurrentCrop { get { return currentCrop; } set { currentCrop = value; }}

    public Interactable currentInterractable;

    Bounds currentCellBounds;

    private bool OverCrop {get{return currentCrop != null;}}

	private void Awake()
	{
        instance = this;
	}

	void Update()
    {
        CheckForNewCrop();
        CheckForNewEvent();
    }

    void CheckForNewCrop ()
    {
        if(currentCellBounds != GetBoundsOfCell())
        {
            currentCellBounds = GetBoundsOfCell();

            RaycastHit2D hit2Ds = new RaycastHit2D();

            hit2Ds = Physics2D.BoxCast(GetCenterCellPosition() , Vector2.one , 0 ,Vector2.zero , 1 , 1 << LayerMask.NameToLayer("Crop"));
            
            if(hit2Ds.collider != null)
                currentCrop = hit2Ds.collider.GetComponent<Crop>();
            else
                currentCrop = null;
        }
    }

    void CheckForNewEvent()
    {
        //if (currentCellBounds != GetBoundsOfCell())
        //{

        Debug.DrawRay(transform.position, PlayerController.instance.faceDirection);


            RaycastHit2D hit2Ds = new RaycastHit2D();

        hit2Ds = Physics2D.BoxCast(GetCenterCellPosition() + PlayerController.instance.faceDirection * PlayerController.instance.tileDistance, Vector2.one, 0, Vector2.zero, 1, 1 << LayerMask.NameToLayer("Interractable"));

            if (hit2Ds.collider != null)
                currentInterractable = hit2Ds.collider.GetComponent<Interactable>();
            else
                currentInterractable = null;
        //}
    }

    Bounds GetBoundsOfCell ()
    {
        return TilemapGroup.plant.GetBoundsLocal(TilemapGroup.plant.WorldToCell(FarmUtilites.GetCenterOfCell(TilemapGroup.plant , transform.position)));
    }

    Vector3 GetCenterCellPosition ()
    {
        return FarmUtilites.GetCenterOfCell(TilemapGroup.plant , transform.position);
    }
}
