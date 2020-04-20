using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryNotifier : MonoBehaviour
{
    private string itemGUID;
    private string id;

	public Text nameText;
    public Text stackAmountText;
    public Image iconImage;
    public Button button; 
    
    public GameObject selector;

	private void Start()
	{
        Inventory.instance.onEquppedItem += RefrashSelector;
	}

	public void EquipItem ()
    {
        if (!string.IsNullOrEmpty(itemGUID))
            UI_Inventory.instance.EquipItem(itemGUID);
        else
            Debug.LogError("ItemGUID:" + itemGUID + " isMissingorNull");
    }

    internal void SetItem (string _itemGUID, string _id)
    {
        itemGUID = _itemGUID;
		id = _id;
    }

    void RefrashSelector () 
    {
        if(this != null)
            selector.SetActive((Inventory.instance.EquippedItem.id == id));
        else{
            Inventory.instance.onEquppedItem -= this.RefrashSelector;
        }
    }

}
