using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueVariables : IDataPersistence
{
    //reads in globals.ink, initialise variable values here
    public Dictionary<string, Ink.Runtime.Object> variables;

    public Story globalVariablesStory;
    private const string saveVariablesKey = "INK_VARIABLES";

    public void LoadData(GameData data)
    {
        if (data.jsonDialogueSave == null)
        {
            Debug.Log("json is null so make new story");
        }
        else
        {
            Debug.Log("Loading from dialogye file");
            globalVariablesStory.state.LoadJson(data.jsonDialogueSave);
        }
    }

    public void SaveData(GameData data)
    {
        Debug.Log("Attempting to save global var story");
        if (globalVariablesStory != null)
        {
            Debug.Log("Saving global var story");
            //Load current state of all variables to the globals story
            VariablesToStory(globalVariablesStory);
            //saving a json string of story's current state
            Debug.Log(globalVariablesStory.state.ToJson());
            data.jsonDialogueSave = globalVariablesStory.state.ToJson();
        }
    }

    /*public void LoadData(GameData data) 
    {
        string jsonState = data.globalVariablesStory;
        globalVariablesStory.state.LoadJson(jsonState);
        //this.globalVariablesStory.state.LoadJson(jsonState) = data.globalVariablesStory;
        //VariablesToStory(globalVariablesStory);
    }
    */

    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        //create story
        globalVariablesStory = new Story(loadGlobalsJSON.text);

        //if have saved data, load
        if (PlayerPrefs.HasKey(saveVariablesKey))
        {
            string jsonState = PlayerPrefs.GetString(saveVariablesKey);
            globalVariablesStory.state.LoadJson(jsonState);
        }

        //initialize dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }
    
    public void SaveVariables()
    {
        if (globalVariablesStory != null)
        {
            //load current state of all variables to globals ink story
            VariablesToStory(globalVariablesStory);

            //replace this with actual save/load
            PlayerPrefs.SetString(saveVariablesKey, globalVariablesStory.state.ToJson());
        }
    }
    
    //take in story that we want variable observer to listen to
    public void StartListening(Story story)
    {
        //must send variables to story before assigning the listener
        VariablesToStory(story);

        //VariableChanged is the listener for when the story changes
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }


    //name is the variable name, value is the value of that variable.
    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        //only maintain variables that are initialized from globals ink file
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    //send variables back to ink story
    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

}
