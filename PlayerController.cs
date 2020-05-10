using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public MovementController Movement;
    public float speed = 20f;
    float direction = 0f;

    bool jump = false;

    public bool OnASwitch = false;
    public bool OnExit = false;
    public int[] SwitchColours;

    public Activator activator;

    public Transform StartPosition;

    private audioManager Audio;

    [SerializeField] private Transform m_GroundCheck;
    const float k_GroundedRadius = .2f;
    private bool isGrounded;
    private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private bool m_AirControl = true;
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private float m_JumpForce = 2750f;
    public LayerMask m_WhatIsGround;

    void Start()
    {
        transform.position = StartPosition.position;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        Audio = GameObject.Find("Audio Manager").GetComponent<audioManager>();
    }

    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        jump = Input.GetButtonDown("Jump");
        if (Input.GetButtonDown("Action Switch")) {
            if (OnASwitch) {
                ActionSwitch();
            }
            if (OnExit) {
                ExitLevel();
            }
        }
        if (Input.GetButtonDown("Cancel")) {
            Cursor.visible = true;
            SceneManager.LoadScene("Level Selector", LoadSceneMode.Single);
        }
        if (transform.position.y < 0f)
        {
            Reset();
        }

        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
    		for (int i = 0; i < colliders.Length; i++)
    		{
    			if (colliders[i].gameObject != gameObject)
    			{
    				isGrounded = true;
    			}
    		}
    }

    void FixedUpdate()
    {
        if (isGrounded || m_AirControl)
        {
            Vector3 targetVelocity = new Vector2(direction * 10f, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }

        if (isGrounded && jump)
    		{
    			isGrounded = false;
    			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
          jump = false;
    		}

    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.layer == LayerMask.NameToLayer("Switch")) {
            OnASwitch = true;
            SwitchColours = obj.gameObject.GetComponent<SwitchController>().colours;
        }
        if (obj.gameObject.name == "Exit") {
            OnExit = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.layer == LayerMask.NameToLayer("Switch")) {
            OnASwitch = false;
        }
        if (obj.gameObject.name == "Exit") {
            OnExit = false;
        }

    }

    void ActionSwitch()
    {
        Audio.Play("Switch");
        foreach (int colour in SwitchColours) {
            activator.ToggleColour(colour);
        }
    }

    void ExitLevel()
    {
        Audio.Play("Win");
        Cursor.visible = true;
        SceneManager.LoadScene("Level Selector", LoadSceneMode.Single);
    }

    void Reset() {
        transform.position = StartPosition.position;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
        activator.Setup();
    }
}
