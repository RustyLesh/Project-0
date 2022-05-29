using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CSS_SaveLoadSystem : MonoBehaviour {
    public string fileName = "saveOne.txt";
    public string SavePath => $"{Application.persistentDataPath}/" + fileName;

    //Saves file of input path
    public void Save(string fileName) {
        this.fileName = fileName;
        Debug.Log(SavePath);
        var state = LoadFile();
        SaveState(state);
        SaveFile(state);
        Debug.Log("Saved");
    }

    //Loads file of input path
    public void Load(string fileName) {
        var state = LoadFile();
        this.fileName = fileName;
        LoadState(state);
        Debug.Log("Loaded");
    }

    //Saves file of default path
    [ContextMenu("Save")]
    public void Save() {
        Debug.Log(SavePath);
        var state = LoadFile();
        SaveState(state);
        SaveFile(state);
        Debug.Log("Saved");
    }

    //Loads file of default path
    [ContextMenu("Load")]
    public void Load() {
        var state = LoadFile();
        LoadState(state);
        Debug.Log("Loaded");
    }

    //Deletes the input file if exists
    public void DeleteSave(string fileName) {
        string tempPath = $"{Application.persistentDataPath}/" + fileName;
        if (File.Exists(tempPath)) {
            File.Delete(tempPath);
        } else {
            Debug.Log("File does not exist");
        }
    }

    //Saves data as binary file at save path
    public void SaveFile(object state) {
        using (var stream = File.Open(SavePath, FileMode.Create)) {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    //Loads file as dictionary
    Dictionary<string, object> LoadFile() {
        //If file does not exist, print to debug log and return empty dictionary
        if (!File.Exists(SavePath)) {
            Debug.Log("Save file doesn't exist");
            return new Dictionary<string, object>();
        }

        //Load file from save path and deserialize into dictionary
        using (FileStream stream = File.Open(SavePath, FileMode.Open)) {
            var formatter = new BinaryFormatter();
            stream.Position = 0;
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    //Saves data to dictionary
    void SaveState(Dictionary<string, object> state) {
        //Loops through every object with CSS_SaveableEntity implemented
        foreach (var saveable in FindObjectsOfType<CSS_SaveableEntity>()) {
            //Insert object ID and dictionary containing save data into dictionary
            state[saveable.Id] = saveable.SaveState();
            Debug.Log(saveable.Id);
        }
    }

    //Load data from dictionary
    void LoadState(Dictionary<string, object> state) {
        //Loops through every object with CSS_SaveableEntity implemented
        foreach (var saveable in FindObjectsOfType<CSS_SaveableEntity>()) {
            //If try get value is successful, load save dictionary to CSS_SaveableEntity
            //for further loading
            if (state.TryGetValue(saveable.Id, out object savedState)) {
                saveable.LoadState(savedState);
            }
        }
    }

}
