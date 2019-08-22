using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : Panel
{
    public static UI_Inventory instance;

    [SerializeField] GameObject inventoryItem;
    [SerializeField] GameObject scrollContent;

    List<GameObject> itemContent = new List<GameObject>();

	private void Awake()
	{
        instance = this;
	}

	public void RefreshUI ()
    {
        for (int i = 0; i < itemContent.Count; i++)
        {
            Destroy(itemContent[i]);
        }

        itemContent.Clear();

        List<Inventory.Stack> items = Inventory.instance.CurrentItems;

        for (int i = 0; i < items.Count; i++)
        {
            GameObject cont = Instantiate(inventoryItem, scrollContent.transform);
            InventoryNotifier notifier = cont.GetComponentInChildren<InventoryNotifier>();
            notifier.SetItem(items[i].GUID , items[i].stackID);
            notifier.nameText.text = items[i].itemStack[0].name;
            notifier.stackAmountText.text = items[i].Amount.ToString();

            itemContent.Add(cont);
        }
    }

    public void EquipItem (string _itemGUID)
    {
        Inventory.instance.EquipItem(_itemGUID);
    }
}
