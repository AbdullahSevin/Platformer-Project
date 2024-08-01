using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : MonoBehaviour
{
    public bool Holded;
    public Transform plyr;
    public Vector3 offset;
    public Animator animator;
    public Rigidbody2D rb;
    public float speed;

    public Camera cam;
    Vector2 movement;
    Vector2 mousepos;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousepos - rb.position;
        float x = Mathf.Abs(lookDir.x);
        float y = Mathf.Abs(lookDir.y);

        if (Input.GetButtonDown("Fire1") && Holded == true)
        {
            FindObjectOfType<PlayerMoves>().Holdrot();
            Debug.Log("pressed");
            
        }
        if (Input.GetButtonUp("Fire1") && Holded == true)
        {
            speed = (x + y) *1.5f;
            Holded = false;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = transform.right * speed;
            FindObjectOfType<PlayerMoves>().Holdrotd();
            FindObjectOfType<PlayerMoves>().cantholdreverse();

        }


        
    }

    void FixedUpdate()
    {
        if (Holded == true)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gameObject.transform.position = plyr.position + offset;
            gameObject.transform.rotation = plyr.rotation;
            animator.SetBool("Holded", true);
            animator.SetBool("isHolding", false);
        }
    }


    void OnTriggerEnter2D(Collider2D colinfo)
    {
        if (colinfo.gameObject.tag == "HoldPoint")
        {
            FindObjectOfType<PlayerMoves>().Holdpointd();

            if (this.tag == "Box")
            {
                FindObjectOfType<AudioManager>().Play("Boxcol");
                Holded = true;
                FindObjectOfType<PlayerMoves>().canthold();
            }
            if (this.name == "Koltuk")
            {
                FindObjectOfType<AudioManager>().Play("Koltukcol");
                Holded = true;
                FindObjectOfType<PlayerMoves>().canthold();
            }
            if (this.name == "Enaktar")
            {
                FindObjectOfType<AudioManager>().Play("Metals3");
                Holded = true;
                FindObjectOfType<PlayerMoves>().canthold();
            }


        }
    }

    void OnCollisionEnter2D(Collision2D colinfo)
    {
        // FindObjectOfType<AudioManager>().Play("Boxcol");

        int Metalsound = Random.Range(1, 4);

        if (this.name == "Koltuk")
        {
            FindObjectOfType<AudioManager>().Play("Koltukcol");
        }
        if (this.name == "Enaktar")
        {
            if (Metalsound == 1)
            {
                FindObjectOfType<AudioManager>().Play("Metals1");
            }
            if (Metalsound == 2)
            {
                FindObjectOfType<AudioManager>().Play("Metals2");
            }
            if (Metalsound == 3)
            {
                FindObjectOfType<AudioManager>().Play("Metals3");
            }

        }
        if (this.tag == "Box")
        {
            FindObjectOfType<AudioManager>().Play("Boxcol");
        }
    }
}
