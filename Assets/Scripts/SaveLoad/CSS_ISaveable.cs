using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CSS_ISaveable {

    //Create a struct with your save data

    //Implement this method to return save data
    object SaveState();

    //Implement this method that recieves save data as a parameter,
    //and set class variables to save data variables
    void LoadState(object state);

}
