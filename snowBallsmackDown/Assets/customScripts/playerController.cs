using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; // controls how fast our player will move
    [SerializeField] float jumpForce = 5f; // controls how high the player will jump
    [SerializeField] Vector2 moveInput; //
    [SerializeField] Vector2 moveInput2;
    [SerializeField] float throwSeconds = .85f;
    [SerializeField] public int pIndex;

    [SerializeField] bool facingRight;
    [SerializeField] bool canThrow;

    PlayerInputAction playerInputA; // reference to our playerinputaction we created in the input manager 
    Player2InputAction playerTwoInput; // referemce to our player two input action
    Rigidbody2D rb; // reference to our players rigid body
    Animator anim; // referecne to animator 

    [SerializeField] bool isGrounded; // check to see if the player is grounded or not 
    public Transform groundCheck; // refernce to our transform
    public LayerMask groundLayer; // determines what is the ground

    //throwing snowballs 
    public int playerOneSBalls = 5;
    public int Snowballs = 2;
    
    [SerializeField] float ballSpeed = 100f; // speed of snowball 
    [SerializeField] float ballHeight = 50f; // height of snowball
    public Rigidbody2D sbPrefab; // reference to our snowball prefabs
    public Transform ballSpawn; // referecne to where our ball will spawn
    private void Awake()
    {
        playerInputA = new PlayerInputAction();
        playerTwoInput = new Player2InputAction();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
        canThrow = true;
    }

    private void OnEnable()
    {
        playerInputA.Enable();
        playerTwoInput.Enable();
    }

    private void onDisable()
    {
        playerInputA.Disable();
        playerTwoInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if(pIndex == 1)anim.SetFloat("XSpeed", Mathf.Abs(moveInput.x)); // allows the animation to play if the move input speed is greater than .1
        if(pIndex == 2)anim.SetFloat("XSpeed", Mathf.Abs(moveInput2.x)); // allows the animation to play if the move input speed is greater than .1
        anim.SetFloat("YSpeed", rb.velocity.y); // allows the animation to play if the user jumps in the air
        anim.SetBool("Grounded", isGrounded); // allows the animation to play if the user is grounded

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if(pIndex == 1 ) moveInput = playerInputA.Player.Movement.ReadValue<Vector2>(); // our move input will become the values set in our movement actions
        if(pIndex == 2) moveInput2 = playerTwoInput.Player2.Movement.ReadValue<Vector2>(); // our move input will become the values set in our movement actions
        if (rb.velocity.x > 0 && !facingRight) FlipPlayer(); // flips the players direction to right if looking left
        if (rb.velocity.x < 0 && facingRight) FlipPlayer(); // flips the player direction to left if looking right 

        if (pIndex == 1)
        {
            if (playerInputA.Player.Jump.triggered && isGrounded) rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // if the player has pressed space adn is grounded push the player up
            if (playerInputA.Player.Throw.triggered && canThrow && playerOneSBalls > 0) StartCoroutine(ThrowProjectile());
        }

        if (pIndex == 2)
        {
            if (playerTwoInput.Player2.Jump.triggered && isGrounded) rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // if the player has pressed space adn is grounded push the player up
            
        }

    }

    private void FixedUpdate()
    {
        if(pIndex == 1)rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y); //  allows the player to move
        if(pIndex == 2) rb.velocity = new Vector2(moveInput2.x * moveSpeed, rb.velocity.y); //  allows the player to move

    }

    void FlipPlayer()
    {
        if (facingRight) transform.localScale = new Vector2(-1, 1); // if facing right is true keep the players position the same
        else transform.localScale = new Vector2(1, 1);    // else makes the player face the other direction
        facingRight = !facingRight;
    }

    IEnumerator ThrowProjectile()
    {
        canThrow = false; // cannot throw 
        if(pIndex == 1) playerOneSBalls--; // decrement number of snowballs 
        
        anim.SetTrigger("Throw"); //allows the throw animation to play

        Rigidbody2D snowBall; // reference to our rigidbody
        snowBall = Instantiate(sbPrefab, ballSpawn.position, ballSpawn.localRotation) as Rigidbody2D; // snowball will get the prefab positiona nd rotation of the snowball rigidbody

        if(facingRight)
        {
            snowBall.AddForce(Vector2.right * ballSpeed); // throws the ball to the right
            snowBall.AddForce(Vector2.up * ballHeight); // forces the ball to have an arc
        }
        else
        {
            snowBall.AddForce(-Vector2.right * ballSpeed); // throws the ball to the left 
            snowBall.AddForce(Vector2.up * ballHeight);// forces the ball to have an arc
        }

        yield return new WaitForSeconds(throwSeconds);
        canThrow = true;
    }
}
