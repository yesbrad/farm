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
    private GameState currentState;

    public GameState CurrentState { get { return currentState; }}

    public static EventIDs CurrentID
    {
        get 
        { 
            if(string.IsNullOrEmpty(PlayerPrefs.GetString(StaticStrings.CurrentID)))
                PlayerPrefs.SetString(StaticStrings.CurrentID, EventIDs.ZWelcome.ToString());

            return (EventIDs)System.Enum.Parse(typeof(EventIDs), PlayerPrefs.GetString(StaticStrings.CurrentID)); 
        }

        set { PlayerPrefs.SetString(StaticStrings.CurrentID, value.ToString()); }
    }

    private void Awake()
	{   
        instance = this;

        InitTilemaps();

            //CurrentID = EventIDs.ZWelcome;
	}

	private void Start()
	{
        ChangeState(defaultState);

        if (defaultState == GameState.Game)
            Load();
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
		SaveData.NewGame();
        ChangeState(GameState.Game);
    }

    public void Save ()
    {
        Debug.Log("Game has been Saved!");
        CropManager.instance.Save();
        Inventory.instance.Save();
        PlayerController.instance.Save();
    }

    public void Load()
    {
        Debug.Log("Game has been Loaded!");
        CropManager.instance.Load();
        Inventory.instance.Load();
        PlayerController.instance.Load();
    }
}
