[System.Serializable]
public class UpgradeSaveData
{
    public string id;
    public int level;

    public UpgradeSaveData(string id, int level)
    {
        this.id = id;
        this.level = level;
    }
}