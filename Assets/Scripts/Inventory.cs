using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class Stack
    {
        public string stackID;
        public List<Item> stackItems = new List<Item>();

        public Stack(Item _item)
        {
            stackID = _item.id;
            stackItems.Add(_item);
            guid = stackID + Random.Range(0f, 10000f);
        }

        private string guid;
        public string GUID { get { return guid; } }
        public int Amount { get { return stackItems.Count; }}
    }

    public System.Action onEquppedItem;

    public static Inventory instance;

    [SerializeField] List<Item> defaultItems;

    [SerializeField]
    private List<Stack> currentStacks = new List<Stack>();
    private bool inventoryOpen;
    private Item primaryEquipped;
	
    public List<Stack> CurrentStacks { get { return currentStacks; }}
    public Item EquippedItem { get { return primaryEquipped; }}

    public ItemsSave itemsSave;

    [Serializable]
    public class ItemsSave
    {
        public List<string> saveIDs = new List<string>();
    }

	private void Awake()
	{
        instance = this;
    }

	private void Update()
	{
        if(Input.GetButtonDown("Inventory") && GameManager.instance.CurrentState == GameState.Game)
        {
            inventoryOpen = !inventoryOpen;
            UIManager.instance.EnablePanel(PanelID.Inventory , inventoryOpen);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(primaryEquipped != null)
            {
                Stack stack = GetStack(primaryEquipped.id);

                if(stack != null)
                {
                    if(primaryEquipped.Interact() && !primaryEquipped.isKeyItem)
                    {
                        int itemAmount = 0;

                        for (int i = 0; i < stack.stackItems.Count; i++)
                        {
                            if (primaryEquipped.id == stack.stackItems[i].id)
                                itemAmount++;
                        }

                        RemoveItem(primaryEquipped);

                        if (itemAmount < 2)
                            DeEquip();
					}
				}
            }
        }

	}

	public void AddItem (Item _item)
    {
        Item newItem = _item;
        newItem.SetGUID();

        AddToStack(newItem);

        RefreshUI();
    }

    public void RemoveItem(Item _item)
    {
        RemoveItemFromStack(_item);
        RefreshUI();
    }

    public void RefreshUI ()
    {
        UI_Inventory.instance.RefreshUI();
    }

    public void EquipItem (Item _item)
    {
        primaryEquipped = _item;
        UI_HUD.instance.RefreshEquip(primaryEquipped);

    }

    public void EquipItem(string _selectedGUID)
    {
        for (int i = 0; i < currentStacks.Count; i++)
        {
            if(currentStacks[i].GUID == _selectedGUID)
            {
                primaryEquipped = currentStacks[i].stackItems[0];
                UI_HUD.instance.RefreshEquip(primaryEquipped);
				onEquppedItem.Invoke();
                return;
            }
        }

        throw new UnityException("Theres no items in our inventory with matching GUID: " + _selectedGUID);
    }

    public void DeEquip ()
    {
        primaryEquipped = null;
        UI_HUD.instance.RefreshEquip(primaryEquipped);
    }

    public void GiveDefaultItems ()
    {
        if (defaultItems.Count < 1)
            return;

        for (int i = 0; i < defaultItems.Count; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                AddItem(defaultItems[i]);   
            }
        }
    }

    public void AddToStack (Item _item)
    {
        Stack stack = GetStack(_item.id);
        
        if (stack != null)
        {
            stack.stackItems.Add(_item);
            return;            
        }

        currentStacks.Add(new Stack(_item));
    }

    public void RemoveItemFromStack (Item _item)
    {
        Stack stack = GetStack(_item.id);

        if (stack != null)
        {
            if(stack.stackItems.Count < 2)
            {
                currentStacks.Remove(stack);
                return;
            }

            stack.stackItems.RemoveAt(0);
        }
    }

    public Item GetItemFromStacks (Item _item) 
    {
        Stack stack = GetStack(_item.id);
        return stack.stackItems[0];
    }

    public Stack GetStack (string _id)
    {
        for (int i = 0; i < currentStacks.Count; i++)
        {
            if (currentStacks[i].stackID == _id)
            {
                return currentStacks[i];
            }
        }

        return null;
    }

    public Item GetItemData(string id)
    {
        Item[] items = Resources.LoadAll<Item>("Items");

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].id == id)
                return items[i];
        }

        Debug.LogError("Get Item has returned Null Cause: ID doesnt match any Items in resources");
        return null;
    }

    public bool HasItem (Item _item , int _amount = 1)
    {
        for (int i = 0; i < currentStacks.Count; i++)
        {
            if (currentStacks[i].stackID == _item.id && currentStacks[i].Amount >= _amount)
                return true;
        }

        return false;
    }

    public void ClearInventory ()
    {
        currentStacks.Clear();
        DeEquip();
        RefreshUI();
    }

    public void Save ()
    {
        ItemsSave items = new ItemsSave();
        
        for (int i = 0; i < currentStacks.Count; i++)
        {
            for (int x = 0; x < currentStacks[i].stackItems.Count; x++)
            {
                items.saveIDs.Add(currentStacks[i].stackItems[x].id);
            }
        }

        string save = JsonUtility.ToJson(items);
        PlayerPrefs.SetString(SaveData.c_inData ,save);
    }

    public void Load ()
    {
        ClearInventory();

        string a = PlayerPrefs.GetString(SaveData.c_inData);

        if (!string.IsNullOrEmpty(a))
        {
            ItemsSave save = (ItemsSave) JsonUtility.FromJson(a, typeof(ItemsSave));
            
            for (int i = 0; i < save.saveIDs.Count; i++)
            {
                AddToStack(GetItemData(save.saveIDs[i]));
            }

            UI_Inventory.instance.RefreshUI();
        }
    }
}
