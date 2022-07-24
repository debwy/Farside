using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour, IDataPersistence
{
    //const string kAlphaCode = "<color=#00000000>";
    const float kMaxTextTime = 0.02f;
    public static int TextSpeed = 2;

    [Header("Params")]
    //[SerializeField] private float typingSpeed = 0.001f;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject[] choices;

    [Header("Choices UI")]
    private TextMeshProUGUI[] choicesText;

    //keep track of which Ink file to display
    private Story currentStory;

    //keeps track of whether dialogue is currently playing.
    //dialogueIsPlaying is public but is read-only
    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    private const string SAVE_TAG = "save";
    private const string END_TAG = "end";

    private DialogueVariables dialogueVariables;

    //save on quit
    /*public void OnApplicationQuit()
    {
        dialogueVariables.SaveVariables();
    }
    */

    public void LoadData(GameData data) {
        //Debug.Log("Load data called from dialogue manager");
        dialogueVariables = new DialogueVariables(loadGlobalsJSON, data);
    }

    public void SaveData(GameData data) {
        //Debug.Log("Load data called from dialogue manager");
        dialogueVariables.SaveData(data);
    }

    //initialise the instance
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);

    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        //get all choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index += 1;
        }
    }

    //logic to traverse Ink story
    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (canContinueToNextLine &&
            currentStory.currentChoices.Count == 0 &&
            (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            ContinueStory();
        }
    }

    //enter dialogue mode from TriggerDialogue script
    //takes in text asset from Ink JSON file
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        //monitor dialogue variable changes
        dialogueVariables.StartListening(currentStory);

        //play sound: click
        //FindObjectOfType<AudioManager>().Play("DialogueClick");

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0f);

        //stop monitoring for dialogue variable changes in ink
        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        //reset text to empty string
    }

    private void ContinueStory()
    {
        HandleTags(currentStory.currentTags);

        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
        }
        else //cant continue, empty JSON file
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    //handle tags (for saving)
    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("tag length != 2");
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SAVE_TAG:
                    //Debug.Log("save=" + tagValue);
                    //save the dialogue variables
                    //
                    //dialogueVariables.SaveVariables();
                    //dialogueVariables.SaveData(DataPersistenceManager.instance.gameData);
                    DataPersistenceManager.instance.MenuSaveGame();
                    break;
                case END_TAG:
                    Debug.Log("game has ended:" + tagValue);

                    //input scene transition to Closing


                    break;
                default:
                    Debug.LogWarning("Tag came in: " + tag);
                    break;
            }
        }
    }

    //typing effect
    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        string originalText = line;

        foreach (char letter in line.ToCharArray())
        {
            //check for rich text tag, if found, add immediately
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;

                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                dialogueText.maxVisibleCharacters++;

                yield return new WaitForSeconds(kMaxTextTime / TextSpeed);
            }

        }
        continueIcon.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        //defensive check
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. " +
                           "Number of choices given: " + currentChoices.Count);
        }

        //loop through all the choices and display them
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index += 1;
        }

        for (int i = index; i < choices.Length; i += 1)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            //play sound: choice
            FindObjectOfType<AudioManager>().Play("DialogueChoice");

            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }

    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    // this method will allow a variable defined in globals.ink to be set using C# code
    public void SetVariableState(string variableName, Ink.Runtime.Object variableValue)
    {
        if (dialogueVariables.variables.ContainsKey(variableName))
        {
            dialogueVariables.variables.Remove(variableName);
            dialogueVariables.variables.Add(variableName, variableValue);
        }
        else
        {
            Debug.LogWarning("Tried to update variable that wasn't initialized by globals.ink: " + variableName);
        }
    }

}
