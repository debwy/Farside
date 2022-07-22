using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour, IDataPersistence
{
    public static GameStats instance {get; private set;}

    int slimeDeaths;
    int golemDeaths;
    int batDeaths;
    int chestOpenCount;

    void Start() {
        EventManager.instance.SlimeDeathEvent += AddSlimeDeathCount;
        EventManager.instance.GolemDeathEvent += AddGolemDeathCount;
        EventManager.instance.BatDeathEvent += AddBatDeathCount;
        EventManager.instance.ChestOpenEvent += AddChestOpenCount;
        slimeDeaths = 0;
        golemDeaths = 0;
        batDeaths = 0;
        chestOpenCount = 0;
    }

    void OnDisable() {
        EventManager.instance.SlimeDeathEvent -= AddSlimeDeathCount;
        EventManager.instance.GolemDeathEvent -= AddGolemDeathCount;
        EventManager.instance.BatDeathEvent -= AddBatDeathCount;
        EventManager.instance.ChestOpenEvent -= AddChestOpenCount;
    }

    public void LoadData(GameData data) {
        this.slimeDeaths = data.slimeDeaths;
        this.golemDeaths = data.golemDeaths;
        this.batDeaths = data.batDeaths;
        this.chestOpenCount = data.chestOpenCount;
    }

    public void SaveData(GameData data) {
        data.slimeDeaths = this.slimeDeaths;
        data.golemDeaths = this.golemDeaths;
        data.batDeaths = this.batDeaths;
        data.chestOpenCount = this.chestOpenCount;
    }

    public int GetEnemyDeathCount() {
        return slimeDeaths + golemDeaths + batDeaths;
    }

    public void AddSlimeDeathCount() {
        slimeDeaths++;
        Debug.Log("Adding slime deaths");
        Debug.Log(slimeDeaths);
    }

    public void AddGolemDeathCount(int golemId) {
        golemDeaths++;
        Debug.Log("Adding golem deaths");
        Debug.Log(golemDeaths);
    }

    public void AddBatDeathCount() {
        batDeaths++;
        Debug.Log("Adding bat deaths");
        Debug.Log(batDeaths);
    }

    public void AddChestOpenCount() {
        chestOpenCount++;
        Debug.Log("Adding chest open");
        Debug.Log(chestOpenCount);
    }

}
