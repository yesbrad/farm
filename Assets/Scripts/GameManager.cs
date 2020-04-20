﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilemapGroup
{
    public static Tilemap plant;
    public static Tilemap collision;
    public static Tilemap normal;
}

public enum GameState 
{
    Menu,
    Game,
    Pause
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Tilemap plant;
    [SerializeField] Tilemap collision;
    [SerializeField] Tilemap normal;

    public GameState defaultState = GameState.Game;

    [Header("DEBUG")] 
    [SerializeField] public bool isTest;
    [SerializeField] public bool hasAllCrops;
    [SerializeField] private EventIDs testID;
    
    private GameState currentState;

    public GameState CurrentState { get { return currentState; }}

    private void Awake()
	{   
        instance = this;
        InitTilemaps();
    }

	private void Start()
	{
        ChangeState(defaultState);

        // Load the default save
        if (defaultState == GameState.Game)
        {
            if (isTest)
            {
                Inventory.instance.GiveDefaultItems();
                SaveManager.CurrentID = testID;
            }
            else
            {
                SaveManager.Load();
            }
        }

    }

    private void Update()
    {
        if (isTest)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SaveManager.CurrentID += 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                SaveManager.CurrentID -= 1;
            }
        }
    }

    public void ChangeState (GameState _state)
    {
        currentState = _state;

        switch(currentState)
        {
            case GameState.Menu:
                UIManager.instance.EnablePanel(PanelID.SOR, true);
                break;
            case GameState.Game:
                UIManager.instance.EnablePanel(PanelID.SOR , false);
                UIManager.instance.LoadGameUI();
                break;
        }
    }

	private void InitTilemaps ()
    {
        TilemapGroup.plant = plant;
        TilemapGroup.collision = collision;
        TilemapGroup.normal = normal; 
    }

    public void NewGame ()
    {
		SaveManager.NewGame();
        ChangeState(GameState.Game);
    }
}
