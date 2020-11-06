using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public InputManager inputmanagr;
    public Transform player;
    private Rigidbody rb;
    public Animator anim;

    private Quaternion desiredRotation;
    private Quaternion targetRotation;
    
    public bool end= false;
    public bool zipla = false;
    private float posX;
    public float speed;

    private static PlayerControl instance;
    public static PlayerControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerControl>();
            }
            return PlayerControl.instance;
        }
    }

    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
    }
    private void Update()
    { 
        if(end == false)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            posX = Mathf.Clamp(transform.position.x, -9.3f, 9.3f);
            transform.position = new Vector3(posX, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * 1f * Time.deltaTime);
        }
    }
    public void Zipla()
    {
        anim.SetBool("Jump", true);
        rb.AddForce(new Vector3(0f, 5f, 0f), ForceMode.Impulse);
        Invoke("JumpClose", 0.5f);
    }
    public void SolaDon()
    {
        targetRotation *= Quaternion.AngleAxis(-90, Vector3.up);
    }
    public void SagaDon()
    {
        targetRotation *= Quaternion.AngleAxis(90, Vector3.up);
    }
    public void JumpClose()
    {
        anim.SetBool("Jump", false);
    }
}
