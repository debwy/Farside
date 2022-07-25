using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour, IDataPersistence
{
    public int slimeDeaths = 0;
    public int golemDeaths = 0;
    public int dogQuestGolemDeaths = 0;
    public int batDeaths = 0;
    public int chestOpenCount = 0;
    public int totalDeaths = 0;

    private int snapshotBatDeaths = 0; 

    void Update()
    {
        //variable counts to be sent to ink
        Ink.Runtime.Object slime = new Ink.Runtime.IntValue(slimeDeaths);
        DialogueManager.GetInstance().SetVariableState("slimeDeaths_global", slime);

        Ink.Runtime.Object bat = new Ink.Runtime.IntValue(batDeaths);
        DialogueManager.GetInstance().SetVariableState("batDeaths_global", bat);

        Ink.Runtime.Object golem = new Ink.Runtime.IntValue(golemDeaths);
        DialogueManager.GetInstance().SetVariableState("golemDeaths_global", golem);

        Ink.Runtime.Object chest = new Ink.Runtime.IntValue(chestOpenCount);
        DialogueManager.GetInstance().SetVariableState("chestOpenCount_global", chest);

        totalDeaths = slimeDeaths + golemDeaths + batDeaths;
        Ink.Runtime.Object total = new Ink.Runtime.IntValue(totalDeaths);
        DialogueManager.GetInstance().SetVariableState("totalDeaths_global", total);
    }

    void snapshotBatQuest() {//call at quest start
        snapshotBatDeaths = batDeaths;
        Ink.Runtime.Object bats = new Ink.Runtime.IntValue(snapshotBatDeaths);
        DialogueManager.GetInstance().SetVariableState("snapshotBatDeaths_global", bats);
        Debug.Log("updating currentBatDeaths in ink");
    }

    int differenceInBatDeaths() {//call when checking quest progress
        return batDeaths - snapshotBatDeaths;
        
        //function is called at start of kill bats quest to snapshot bat deaths
        /*Ink.Runtime.Object bats = new Ink.Runtime.IntValue(batDeaths);
        DialogueManager.GetInstance().SetVariableState("currentBatDeaths_global", bats);
        Debug.Log("updating currentBatDeaths in ink");
        */
    }

    void Start() {
        EventManager.instance.SlimeDeathEvent += AddSlimeDeathCount;
        EventManager.instance.GolemDeathEvent += AddGolemDeathCount;
        EventManager.instance.BatDeathEvent += AddBatDeathCount;
        EventManager.instance.ChestOpenEvent += AddChestOpenCount;
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
        this.dogQuestGolemDeaths = data.dogQuestGolemDeaths;
        this.batDeaths = data.batDeaths;
        this.chestOpenCount = data.chestOpenCount;
        //Debug.Log("Loading game stats");
        //Debug.Log(this.slimeDeaths);
    }

    public void SaveData(GameData data) {
        Debug.Log("Saving game stats");
        Debug.Log(this.slimeDeaths);
        data.slimeDeaths = this.slimeDeaths;
        data.golemDeaths = this.golemDeaths;
        data.dogQuestGolemDeaths = this.dogQuestGolemDeaths;
        data.batDeaths = this.batDeaths;
        data.chestOpenCount = this.chestOpenCount;
        //Debug.Log("Finished Saving game stats");
        //Debug.Log(this.slimeDeaths);
    }

    public int GetEnemyDeathCount() {
        return slimeDeaths + golemDeaths + batDeaths;
    }
    
    public void AddSlimeDeathCount() {
        Debug.Log("Slime count updated");
        slimeDeaths++;
        Debug.Log(slimeDeaths);
    }

    public void AddGolemDeathCount(int golemId) {
        golemDeaths++;
        if (golemId == 0) {
            dogQuestGolemDeaths++;
        }
    }

    public void AddBatDeathCount() {
        batDeaths++;
    }

    public void AddChestOpenCount() {
        chestOpenCount++;
        Debug.Log("chest opened + 1");
    }

}
