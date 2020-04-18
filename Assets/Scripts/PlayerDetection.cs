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
        if (currentCellBounds != GetBoundsOfCell())
        {
            currentCellBounds = GetBoundsOfCell();
            CheckForNewCrop();
            CheckForNewEvent();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(currentInterractable != null && CurrentCrop == null)
            {
                if(!UI_Speech.instance.ShowingMessage)
                    currentInterractable.Interact();
            }
        }
    }

    void CheckForNewCrop ()
    {
        RaycastHit2D hit2D = new RaycastHit2D();

            hit2D = Physics2D.BoxCast(GetCenterCellPosition() , Vector2.one , 0 ,Vector2.zero , 1 , 1 << LayerMask.NameToLayer("FloorInteract"));

            currentCrop = null;

            if (hit2D.collider != null)
            {
                switch (hit2D.collider.tag)
                {
                    case "Crop":
                        currentCrop = hit2D.collider.GetComponent<Crop>();
                        break;
                    case "Script":
                        hit2D.collider.GetComponent<Interactable>().Interact();
                        break;
                }
            }
        
    }

    void CheckForNewEvent()
    {
        RaycastHit2D hit2Ds = new RaycastHit2D();

        hit2Ds = Physics2D.BoxCast(GetCenterCellPosition() + PlayerController.instance.faceDirection * PlayerController.instance.tileDistance, Vector2.one, 0, Vector2.zero, 1, 1 << LayerMask.NameToLayer("Interractable"));

        if (hit2Ds.collider != null)
            currentInterractable = hit2Ds.collider.GetComponent<Interactable>();
        else
            currentInterractable = null;
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
