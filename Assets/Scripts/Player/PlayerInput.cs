using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    internal PlayerMain player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    internal float moveHorizontal;
    internal float moveVertical;
    internal float faceDirectionX;
    internal bool attacking;
    internal bool isJumping;

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        faceDirectionX = moveHorizontal;
        attacking = Input.GetKeyDown(KeyCode.Space);
        isJumping = !Grounded();

        if(faceDirectionX > 0) {
            transform.localScale = new Vector2(5, 5);
        } else if (faceDirectionX < 0) {
            transform.localScale = new Vector2(-5, 5);
        }
    }

    public GameObject groundRay;
    private RaycastHit2D hitGround;
    public float groundedValue = 0.1f;

    void FixedUpdate() 
    {
        hitGround = Physics2D.Raycast (groundRay.transform.position, -Vector2.up);
        Debug.DrawRay (groundRay.transform.position, -Vector2.up * hitGround.distance, Color.red);
        //Debug.Log(hitGround.distance);
    }

    internal float DistanceFromGround() {
        return hitGround.distance;
    }

    internal bool Grounded() {
        return DistanceFromGround() < groundedValue;
    }
}
