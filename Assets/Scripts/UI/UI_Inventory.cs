using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : Panel
{
    public static UI_Inventory instance;

    [SerializeField] GameObject inventoryItem;
    [SerializeField] GameObject scrollContent;

    List<InventoryNotifier> itemNotifiers = new List<InventoryNotifier>();

	private void Awake()
	{
        instance = this;
	}
    
    public override void Slide (bool isIn)
    {
        if (itemNotifiers.Count > 0 && isIn)
        {
            UIManager.instance.SetCurrentButton(itemNotifiers[0].button.gameObject);
        }
        
        PlayerController.instance.LockPlayer(isIn);

        base.Slide(isIn);
    }

	public void RefreshUI ()
    {
        for (int i = 0; i < itemNotifiers.Count; i++)
        {
            Destroy(itemNotifiers[i].gameObject);
        }

        itemNotifiers.Clear();

        List<Inventory.Stack> items = Inventory.instance.CurrentStacks;

        for (int i = 0; i < items.Count; i++)
        {
            GameObject cont = Instantiate(inventoryItem, scrollContent.transform);
            cont.SetActive(true);
            InventoryNotifier notifier = cont.GetComponentInChildren<InventoryNotifier>();
            notifier.SetItem(items[i].GUID , items[i].stackID);
            notifier.nameText.text = items[i].stackItems[0].name;
            notifier.stackAmountText.text = items[i].Amount.ToString();
            notifier.iconImage.sprite = items[i].stackItems[0].icon;
            itemNotifiers.Add(notifier);
        }
    }

    public void EquipItem (string _itemGUID)
    {
        Debug.Log("SELECT MAE");
        Inventory.instance.EquipItem(_itemGUID);
    }
}
