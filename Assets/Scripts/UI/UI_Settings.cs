
public class UI_Settings : Panel
{
    public void Save()
    {
        SaveManager.Save();
    }

    public void Load()
    {
        SaveManager.Load();
    }
}
