using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class Stack
    {
        public string stackID;
        public List<Item> itemStack = new List<Item>();

        public Stack(Item _item)
        {
            stackID = _item.id;
            itemStack.Add(_item);
            guid = stackID + Random.Range(0f, 10000f);
        }

        private string guid;
        public string GUID { get { return guid; } }
        public int Amount { get { return itemStack.Count; }}
    }

    public System.Action onEquppedItem;

    public static Inventory instance;

    [SerializeField] List<Item> defaultItems;

    [SerializeField]
    private List<Stack> currentItems = new List<Stack>();
    private bool inventoryOpen;
    private Item primaryEquipped;
	
    public List<Stack> CurrentItems { get { return currentItems; }}
    public Item EquippedItem { get { return primaryEquipped; }}

    public ItemsSave itemsSave;

    public class ItemsSave
    {
        public List<Item> items = new List<Item>();
    }

	private void Awake()
	{
        instance = this;

        if (!SaveData.HasKey(SaveData.c_inData))
            GiveDefaultItems();
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

                        for (int i = 0; i < stack.itemStack.Count; i++)
                        {
                            if (primaryEquipped.id == stack.itemStack[i].id)
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
        for (int i = 0; i < currentItems.Count; i++)
        {
            if(currentItems[i].GUID == _selectedGUID)
            {
                primaryEquipped = currentItems[i].itemStack[0];
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

    private void GiveDefaultItems ()
    {
        if (defaultItems.Count < 1)
            return;

        for (int i = 0; i < defaultItems.Count; i++)
        {
            AddItem(defaultItems[i]);
        }
    }

    public void AddToStack (Item _item)
    {
        Stack stack = GetStack(_item.id);

        if (stack != null)
        {
            stack.itemStack.Add(_item);
            return;            
        }

        currentItems.Add(new Stack(_item));
    }

    public void RemoveItemFromStack (Item _item)
    {
        Stack stack = GetStack(_item.id);

        if (stack != null)
        {
            if(stack.itemStack.Count < 2)
            {
                currentItems.Remove(stack);
                return;
            }

            stack.itemStack.RemoveAt(0);
        }
    }

    public Item GetItemFromStacks (Item _item) 
    {
        Stack stack = GetStack(_item.id);
        return stack.itemStack[0];
    }

    public Stack GetStack (string _id)
    {
        for (int i = 0; i < currentItems.Count; i++)
        {
            if (currentItems[i].stackID == _id)
            {
                return currentItems[i];
            }
        }

        return null;
    }

    public bool HasItem (Item _item , int _amount = 1)
    {
        for (int i = 0; i < currentItems.Count; i++)
        {
            if (currentItems[i].stackID == _item.id && currentItems[i].Amount >= _amount)
                return true;
        }

        return false;
    }

    public void ClearInventory ()
    {
        currentItems.Clear();
        DeEquip();
        RefreshUI();
    }

    public void Save ()
    {
        ItemsSave a = new ItemsSave();

        for (int i = 0; i < currentItems.Count; i++)
        {
            for (int x = 0; x < currentItems[i].itemStack.Count; x++)
            {
                a.items.Add(currentItems[i].itemStack[x]);
            }
        }

        string save = JsonUtility.ToJson(a);
        PlayerPrefs.SetString(SaveData.c_inData ,save);
    }

    public void Load ()
    {
        ClearInventory();

        string a = PlayerPrefs.GetString(SaveData.c_inData);

        if (string.IsNullOrEmpty(a))
            return;

        ItemsSave save = (ItemsSave)JsonUtility.FromJson(a, typeof(ItemsSave));

        for (int i = 0; i < save.items.Count; i++)
        {
            AddToStack(save.items[i]);
        }

        UI_Inventory.instance.RefreshUI();
    }
}
