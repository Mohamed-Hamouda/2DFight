using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public int PlayerNum = 1;
    Transform enemy;
    public Collider2D UpperBody;
    public Collider2D LowerBody;
    public bool specialAttack;

    public GameObject Projectile;
    public bool PC = true;
    InputMan inputManager;
    Rigidbody2D rig2D;
    Animator anim;
    float horizontal;
    float vertical;

    public float Max_Speed;
    Vector3 movement;
    bool crouch;

    public float JumpForce = 20;
    public float JumpDuration = 0.1f;

    float jumpDuration;
    float jumpForce;
    bool jumpKey;
    bool falling;
    bool grounded;


    public bool damage;
    public float noDamage = 1;
    float noDamageTimer;

    public float AttackRate = 0.3f;
    public int AttackCount = 2;
    bool[] attack ;
    float[] attackTimer ;
    int[] timesPressed ;
	// Use this for initialization
	void Start () {
        rig2D = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        jumpForce = JumpForce;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject item in players)
        {
            if (item.transform != transform)
            {
                enemy = item.transform;
            }
        }
        attack = new bool[AttackCount];
        attackTimer = new float[AttackCount];
        timesPressed = new int[AttackCount];
        if (PC)
        {
            inputManager = gameObject.AddComponent<PCInputMan>();
            //inputManager = new PCInputMan(PlayerNum, AttackCount);
            inputManager.AttacksCount = AttackCount;
            ((PCInputMan)inputManager).PlayerNum = PlayerNum;
        }
	}
    void Update()
    {
        AttackInput();
        ScaleCheck();
        Damage();
        SpecialAttack();
        UpdateAnim();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        UpperBody.enabled = !crouch;
        horizontal = inputManager.Horizontal;
        vertical = inputManager.Vertical;

        movement = Vector3.right * horizontal;
        crouch = vertical < -0.1f ;

        if (vertical > 0.1)
        {
            
            if (jumpKey)
            {
                
                jumpDuration += Time.deltaTime;
                jumpForce += Time.deltaTime;
                if (jumpDuration < JumpDuration)
                {
                    rig2D.velocity += (Vector2.up * jumpForce);
                }
                else
                {
                    jumpKey = false;
                }
            }
        }
        if (vertical < 0.1f && !grounded)
        {
            falling = true;
        }
        bool attacksSum = false;
        for (int i = 0; i < AttackCount; i++)
		{
            attacksSum = attacksSum  || attack[i];
		}

        if (attacksSum && jumpKey)
        {
            movement = Vector3.zero;
        }
        if (!crouch)
        {
            
            rig2D.AddForce(movement * Max_Speed);
            //rig2D.velocity = movement * Max_Speed;
        }
        else
        {
            rig2D.velocity = Vector3.zero;
        }
	}

    void AttackInput()
    {
        for (int i = 0; i < AttackCount; i++)
        {
            if (inputManager.Attacks[i])
            {
                attack[i] = true;
                attackTimer[i] = 0;
                timesPressed[i]++;
            }
            if (attack[i])
            {
                attackTimer[i] += Time.deltaTime;
                if (attackTimer[i] > AttackRate || timesPressed[i] >= 4)
                {
                    attackTimer[i] = 0;
                    attack[i] = false;
                    timesPressed[i] = 0;
                }
            }
        }
        
    }
    void ScaleCheck()
    {
        if (transform.position.x < enemy.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = Vector3.one;
    }
    void UpdateAnim()
    {
        anim.SetBool("OnGround", grounded);
        anim.SetBool("Falling", falling);
        anim.SetBool("Crouch", crouch);
        anim.SetFloat("Movement", Mathf.Abs(horizontal));
        for (int i = 0; i < AttackCount; i++)
        {
            anim.SetBool("Attack" + (i + 1), attack[i]);

        }
    }
    void Damage()
    {
        if (damage)
        {
            noDamageTimer += Time.deltaTime;
            if (noDamageTimer > noDamage)
            {
                damage = false;
                noDamageTimer = 0;
            }
        }
    }

    void SpecialAttack()
    {
        if (specialAttack)
        {
            GameObject pr = Instantiate(Projectile, transform.position, Quaternion.identity) as GameObject;
            pr.transform.parent = transform;
            Destroy(pr, 2);
            Vector3 nrDir = new Vector3(enemy.position.x, transform.position.y, 0);
            Vector3 dir = nrDir - transform.position;
            pr.GetComponent<Rigidbody2D>().AddForce(dir * 10, ForceMode2D.Impulse);
            specialAttack = false;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Ground"){
            grounded = true;
            jumpDuration = 0;
            jumpKey = true;
            jumpForce = JumpForce;
            falling = false;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
            grounded = false;
    }
}
