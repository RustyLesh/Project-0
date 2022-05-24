using System.Collections.Generic;
using UnityEngine;

public class CSS_SaveableEntity : MonoBehaviour {

    [SerializeField] private string id;
    public string Id => id;

    //Generate ID for game object
    [ContextMenu("Generate ID")]
    private void GenerateId() {
        id = System.Guid.NewGuid().ToString();
    }

    //Get save data from components within game object
    public object SaveState() {
        var state = new Dictionary<string, object>();
        //Loop through components with ISaveable implemented
        foreach (var saveable in GetComponents<CSS_ISaveable>()) {
            //Insert name and save data into dictionary
            state[saveable.GetType().ToString()] = saveable.SaveState();
        }
        return state;
    }

    //Load save data from a dictionary
    public void LoadState(object state) {
        var stateDictionary = (Dictionary<string, object>)state;

        //Loop through components with ISaveable implemented
        foreach (var saveable in GetComponents<CSS_ISaveable>()) {
            string typeName = saveable.GetType().ToString();
            //If try get value is successful, call ISaveable LoadState to load data
            if (stateDictionary.TryGetValue(typeName, out object savedState)) {
                saveable.LoadState(savedState);
            }
        }
    }
}

