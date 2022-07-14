using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    //Script only has to read the data
    void LoadData(GameData data);

    //Wants script to be able to edit the data
    void SaveData(GameData data);
}
