using System.IO;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    public static bool isNewSave;

    public PlayerController _playerController;

    private static string jsonSavePath;
    public SaveData _save;

    void Awake()
    {
        jsonSavePath = Application.persistentDataPath + "/data.json";
    }

    public void Save()
    {
        //Creating file
        FileStream fileStream = new FileStream(jsonSavePath, FileMode.OpenOrCreate);

        //Data to save
        _save._playerData.health = _playerController._statisticsController.health + 1;
        _save._playerData.money = _playerController._statisticsController.money;


        _playerController._statisticsController.TakeDamage(1, true);

        //Saving data
        SaveData(fileStream);
    }

    public void NewSave()
    {
        isNewSave = true;

        FileStream fileStream = new FileStream(jsonSavePath, FileMode.Create);
        SaveData(fileStream);
    }

    private void SaveData(FileStream fileStream)
    {
        string jsonData = JsonUtility.ToJson(_save, true);

        fileStream.Close();
        File.WriteAllText(jsonSavePath, jsonData);
    }

    public void Load()
    {
        if (isNewSave)
        {
            isNewSave = false;
            return;
        }

        //Load data
        string json = ReadFromFile();
        JsonUtility.FromJsonOverwrite(json, _save);

        //Set data
    }

    public static bool CheckIfSaveExists()
    {
        if (File.Exists(jsonSavePath)) return true;

        return false;
    }

    private string ReadFromFile()
    {
        using (StreamReader Reader = new StreamReader(jsonSavePath))
        {
            string json = Reader.ReadToEnd();
            return json;
        }
    }
}
