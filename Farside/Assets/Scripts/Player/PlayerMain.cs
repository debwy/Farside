using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    internal PlayerInput input;
    [SerializeField]
    internal PlayerMovement movement;
    [SerializeField]
    internal PlayerCollision collision;
    [SerializeField]
    internal PlayerCombat combat;

    internal Animator ani;
    internal Rigidbody2D body;

    internal bool faceRight;
    internal bool enableMovement;
    internal bool enableActions;

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
    }

    // Update is called once per frame
    void Update()
    {
        //added for freezing player movement when talking to NPC
        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            ani.SetBool("Dialogue", true);
            return;
        } else {
            ani.SetBool("Dialogue", false);
        }

        if (enableActions) {
            PlayerActions();
        }
    }

    private void PlayerActions() {
        if (input.attacking) {
            ani.SetTrigger("Attack");
            combat.MeleeAttack();
        }

        if (input.shooting) {
            ani.SetTrigger("Shoot");
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

    private void PlayerMovement() {
        if(input.Grounded()) {
            ani.SetBool("Ground", true);
        } else {
            ani.SetBool("Ground", false);
        }

        ani.SetBool("Run", input.moveHorizontal != 0);

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
            movement.VerticalMovement(input.moveVertical);
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
        Debug.Log(DataPersistenceManager.instance.IsLoadedFromMenu());
        if (DataPersistenceManager.instance.IsLoadedFromMenu()) {
            this.transform.position = data.playerPosition;
        } else {
            Loader.PositionPlayer(data.lastSavedScene);
        }
    }

    public void SaveData(GameData data) {
        combat.Save(data);
        data.lastSavedScene = Loader.CurrentSceneIndex();

        //should only be overriten at a campfire
        if (DataPersistenceManager.instance.IsSavedFromCheckpoint()) {
            data.playerPosition = this.transform.position;
        }
    }

}
