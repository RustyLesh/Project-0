using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadSystem : MonoBehaviour {
    public string fileName = "saveOne.txt";
    public string SavePath => $"{Application.persistentDataPath}/" + fileName;

    public void Save(string fileName) {
        this.fileName = fileName;
        Debug.Log(SavePath);
        var state = LoadFile();
        SaveState(state);
        SaveFile(state);
        Debug.Log("Saved");
    }

    public void Load(string fileName) {
        var state = LoadFile();
        this.fileName = fileName;
        LoadState(state);
        Debug.Log("Loaded");
    }

    [ContextMenu("Save")]
    public void Save() {
        Debug.Log(SavePath);
        var state = LoadFile();
        SaveState(state);
        SaveFile(state);
        Debug.Log("Saved");
    }

    [ContextMenu("Load")]
    public void Load() {
        var state = LoadFile();
        LoadState(state);
        Debug.Log("Loaded");
    }

    public void SaveFile(object state) {
        using (var stream = File.Open(SavePath, FileMode.Create)) {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    Dictionary<string, object> LoadFile() {
        if (!File.Exists(SavePath)) {
            Debug.Log("Save file doesn't exist");
            return new Dictionary<string, object>();
        }

        using (FileStream stream = File.Open(SavePath, FileMode.Open)) {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    void SaveState(Dictionary<string, object> state) {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>()) {
            state[saveable.Id] = saveable.SaveState();
            Debug.Log(saveable.Id);
        }
    }

    void LoadState(Dictionary<string, object> state) {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>()) {
            if (state.TryGetValue(saveable.Id, out object savedState)) {
                saveable.LoadState(savedState);
            }
        }
    }

}
