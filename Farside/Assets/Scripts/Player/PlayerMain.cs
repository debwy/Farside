using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour, IDataPersistence
{
    [SerializeField] internal PlayerInput input;
    [SerializeField] internal PlayerMovement movement;
    [SerializeField] internal PlayerCollision collision;
    [SerializeField] internal PlayerCombat combat;

    internal Animator ani;
    internal Rigidbody2D body;

    internal bool faceRight;
    internal bool enableMovement;
    internal bool enableActions;
    internal bool enableDialogue;
    internal bool isCurrentlyInDialogue;

    [SerializeField]
    internal Healthbar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        faceRight = true;
        enableMovement = true;
        enableActions = true;
        enableDialogue = true;
    }

    // Update is called once per frame
    void Update()
    {
        //added for freezing player movement when talking to NPC
        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            isCurrentlyInDialogue = true;
            ani.SetBool("Dialogue", true);
            return;
        } else {
            isCurrentlyInDialogue = false;
            ani.SetBool("Dialogue", false);
        }

        if (enableActions) {
            PlayerActions();
        }
    }

    private void PlayerActions() {
        if (input.attacking) {
            combat.MeleeAttack();
        }

        if (input.shooting) {
            combat.Shoot();
        }

        if (input.dashing) {
            movement.TryDash();
        }
        movement.CheckDash(faceRight);
    }

    void FixedUpdate() {

        //added for freezing player movement when talking to NPC
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        //end of my addition

        if (enableMovement) {
            PlayerMovement();
        }
    }

    internal bool isRunning = false;

    private void PlayerMovement() {
        if(input.Grounded()) {
            ani.SetBool("Ground", true);
        } else {
            ani.SetBool("Ground", false);
        }

        isRunning = input.moveHorizontal != 0;
        ani.SetBool("Run", isRunning);
        if (isRunning) {
            movement.material.friction = 0.5f;
        } else {
            movement.material.friction = 1f;
        }

        if (input.moveHorizontal > 0.1f) {
            if (!faceRight) {
                Flip();
            }
            faceRight = true;
            movement.HorizontalMovement(input.moveHorizontal, input.isJumping);
        }
        if (input.moveHorizontal < -0.1f) {
            if (faceRight) {
                Flip();
            }
            faceRight = false;
            movement.HorizontalMovement(input.moveHorizontal, input.isJumping);
        }
        if(input.Grounded() && input.moveVertical > 0.1f) {
            ani.SetTrigger("Jump");
            movement.VerticalMovement();
        }
    }

    public void TakeDamage(int attackDamage) {
        combat.TakeDamage(attackDamage);
    }

    public void Heal(int healing) {
        combat.Heal(healing);
    }

    private void Flip() {
        transform.Rotate(0f, 180f, 0f);
        healthbar.transform.Rotate(0f, 180f, 0f);
    }

    public int GetShotAttackDmg() {
        return combat.GetShotAttackDmg();
    }

    public void Death() {
        combat.Instakill();
    }

    public void GameOver() {
        DataPersistenceManager.instance.doNotSave = true;
        Loader.LoadScene(Loader.Scenes.MainMenu);
    }

    public void LoadData(GameData data) {
        combat.Load(data);

        //Player is positioned via save if loaded from menu
        //Else player is positioned at scenes-linked point
        if (DataPersistenceManager.instance.IsLoadedFromMenu()) {
            this.transform.position = data.playerPosition;
        } else {
            Loader.PositionPlayer(data.lastScene);
        }
    }

    public void SaveData(GameData data) {
        combat.Save(data);
        data.lastScene = Loader.CurrentSceneIndex();

        if (DataPersistenceManager.instance.IsSavedFromCheckpoint()) {
            Debug.Log("Updating player position");
            data.lastSavedScene = Loader.CurrentSceneIndex();
            data.playerPosition = this.transform.position;
        }
    }

    public void DisablePlayerActions() {
        enableActions = false;
        enableMovement = false;
        enableDialogue = false;
    }

    public void EnablePlayerActions() {
        enableActions = true;
        enableMovement = true;
        enableDialogue = true;
    }

    public bool IsAbleToDialogue() {
        return enableDialogue;
    }

}
