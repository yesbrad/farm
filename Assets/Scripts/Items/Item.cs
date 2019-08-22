using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/BasicItem" , fileName ="New Item")]
public class Item : ScriptableObject
{
    public string name;
    public string id;
    public bool isKeyItem;

	private int amount;

    public Sprite icon;

    private string guid;

    public string GUID { get { return guid; }}

    public void SetGUID ()
    {
        guid = id + Random.Range(0f, 10000f);
    }

    public virtual bool Interact ()
    {
        return true;
    }

    public virtual bool Interact(bool _disable)
    {
        return !_disable;
    }
}
