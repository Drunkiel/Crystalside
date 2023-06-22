using System.IO;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
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


        //Saving data
        SaveData(fileStream);
    }

    public void NewSave()
    {
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
