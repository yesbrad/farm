using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD : Panel
{
    public static UI_HUD instance;

    [SerializeField] public Text testEventIdText;
    
    [Header("Equip")]
    [SerializeField] public Image primaryContentSlot;

    private void Awake()
	{
        instance = this;
	}

    public void RefreshEquip (Item _primaryItem)
    {
        primaryContentSlot.color = _primaryItem == null ? Color.clear : Color.white;
        primaryContentSlot.sprite = _primaryItem == null ? null : _primaryItem.icon;
    }

    public void UpdateTestIDText(EventIDs id)
    {
        if (testEventIdText == null) return;
        
        testEventIdText.gameObject.SetActive(true);
        testEventIdText.text = "CurrentID: " + id.ToString();
    }
}
