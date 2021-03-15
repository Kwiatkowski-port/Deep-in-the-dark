using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float jumpForce;
    public float health;
    public float timeBetweenAttacks;
    public float attackRange;

    public int damage;
    public int numOfHearts;
    public int extraJumpValue;

    float nextAttackTime;
    int extraJump;

    public Transform groundCheck;
    public Transform frontCheck;
    public Transform attackPoints;
    public LayerMask enemyLayer;
    public LayerMask whatIsGround;
    public Animator animPlayer;

    public AudioClip jumpSound;
    public AudioClip hurtSound;

    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHearts;

    bool facingRight = true;
    public bool isGrounded;

    Rigidbody2D rb;
    AudioSource source;

    [SerializeField] private string Lose;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animPlayer = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);

        if(input > 0 && facingRight==false)
        {
            Flip();
            animPlayer.SetBool("Running", true);
        }
        else if (input < 0 && facingRight == true)
        {
            Flip();
        }

        if(input != 0)
        {
            animPlayer.SetBool("Running", true);
        }
        else
        {
            animPlayer.SetBool("Running", false);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded == true)
        {
            extraJump = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJump--;
            source.clip = jumpSound;
            source.Play();
        }

        if (isGrounded == true)
        {
            animPlayer.SetBool("Jumping", false);
        }
        else
        {
            animPlayer.SetBool("Jumping", true);
        }

        //klasa health
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHearts;
            }
            else
            {
                hearts[i].sprite = emptyHearts;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }
    // obrażenia 
    public void TakeDamage(int damage)
    {
        health -= damage;
        source.clip = hurtSound;
        source.Play();
        if (health <= 0)
        {
            SceneManager.LoadScene(Lose);
            Destroy(gameObject);
            Score.instance.UpdateHightScore();
        }
    }
}
