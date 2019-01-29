using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private const int MAX_LEVEL_SIZE = 100;

    private string _dataPath;

    public GameObject LevelTilePrefab;
    public GameObject TileGrid;

    public InputField LevelIdInput;

    void Start()
    {
        _dataPath = Application.persistentDataPath + "/Levels/";

        LevelIdInput.text = SceneManager.LevelId.ToString();

        LoadLevelFromFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLevelID()
    {
        if (LevelIdInput.text == "") return;

        Debug.Log("Level ID changed -> " + LevelIdInput.text);
        SceneManager.LevelId = Convert.ToInt32(LevelIdInput.text);
    }

    public void SaveLevelToFile()
    {
        Debug.Log("Saving...");
        File.WriteAllText(GetLevelFile(), "");//TODO: Header

        //Find all level tiles and serialize them
        LevelTile[] tiles = FindObjectsOfType<LevelTile>();
        TileData[] tileData = new TileData[tiles.Length];
        for(int i = 0; i < tiles.Length; i++)
        {
            tileData[i] = new TileData(tiles[i]);
        }
        //Save level tiles
        SaveObjectToFile(tileData, "tiles");

        Debug.Log("Level Layout saved!");
    }

    private void SaveObjectToFile(object obj, string suffix)
    {
        string targetPath = GetLevelFile() + "." + suffix;

        if (File.Exists(targetPath))
        {
            File.Delete(targetPath);
        }

        FileStream file = File.Create(targetPath, 1024, FileOptions.None);
        DataContractSerializer bf = new DataContractSerializer(obj.GetType());
        //MemoryStream streamer = new MemoryStream();
        bf.WriteObject(file, obj);
        //streamer.Seek(0, SeekOrigin.Begin);
        //file.Write(streamer.GetBuffer(), 0, streamer.GetBuffer().Length);
        file.Close();
    }

    public void LoadLevelFromFile()
    {
        if (File.Exists(GetLevelFile()))
        {
            TileData[] tileData = (TileData[])LoadObjectFromFile(typeof(TileData[]), "tiles");
            foreach (TileData tile in tileData)
            {
                tile.Instantiate(LevelTilePrefab, TileGrid);
            }
        }
    }

    private object LoadObjectFromFile(Type type, string suffix)
    {
        string targetPath = GetLevelFile() + "." + suffix;

        FileStream file = File.OpenRead(targetPath);
        DataContractSerializer bf = new DataContractSerializer(type);
        object obj = bf.ReadObject(file);
        file.Close();
        return obj;
    }

    private string GetLevelFile()
    {
        string targetPath = "";
#if UNITY_EDITOR
        targetPath = Application.dataPath + "/Resources/Levels/" + SceneManager.LevelId;
#else
        targetPath = _dataPath + SceneManager.LevelId;
#endif

        return targetPath;
    }
}
