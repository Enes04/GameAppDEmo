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

    public float targetposition;
    public bool end= false;
    public bool zipla = false;
    private float posX;
    public float speed;
    private bool zipliyor = false;

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
           
            transform.position = Vector3.Lerp(transform.position,new Vector3(targetposition,transform.position.y,transform.position.z),0.5f);
            posX = Mathf.Clamp(transform.position.x, -2f, 2f);
            transform.position = new Vector3(posX, transform.position.y, transform.position.z);
        }
    }
    public void Zipla()
    {
        if (zipliyor == false)
        {
            zipliyor = true;
            anim.SetBool("Jump", true);
            rb.AddForce(new Vector3(0f, 5f, 0f), ForceMode.Impulse);
            Invoke("JumpClose", 0.5f);
            StartCoroutine(ZiplamaEnd());
        }
    }
    public void SolaDon()
    {
       if(transform.position.x > -2f)
          targetposition = transform.position.x - 2f;
    }
    public void SagaDon()
    {
        if (transform.position.x < 2f)
            targetposition = transform.position.x + 2f;
    }
    public void JumpClose()
    {
        anim.SetBool("Jump", false);
    }
    IEnumerator ZiplamaEnd()
    {
        yield return new WaitForSeconds(1f);
        zipliyor = false;
    }
}
