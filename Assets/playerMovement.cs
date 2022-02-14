using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
	public List<Animator> anim;
  Vector2 mov;
  Rigidbody2D rbody;
  Collider2D myCollider;

  public bool reading = false;

	float speed = 2f;
  public float runspeed = 8f;
  public float walkspeed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        for(int i = 0; i < 3; i++)
        {
          anim[i].speed = 0.1f;
        }
        GameManager.instance.ApplyCharacterSkin(); //Set the skin accordingly
    }

    // Update is called once per frame
    void Update()
    {
      //Detectar inputs verticales y horizontales
      if(!reading)
      {
        mov = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
      }
      //ANIMATION
      for(int i = 0; i < 3; i++)
      {
        if(mov != Vector2.zero)
        {
          anim[i].SetFloat("movX", mov.x);
          anim[i].SetFloat("movY", mov.y);
          if(speed == runspeed) //Animation speed while moving depends on whether you're sprinting or not
          {
            anim[i].speed = 0.2f;
          }
          else
          {
            anim[i].speed = 0.1f;
          }
        }
        else
        {
          anim[i].Play("",0, 0);
          anim[i].speed = 0f;
        }
      }

      //Sprint ability---------------------------
      if(Input.GetButton("Fire2") && speed != runspeed)
      {
        speed = runspeed;
      }
      else if (speed != walkspeed)
      {
        speed = walkspeed;
      }

      //Open Menu (Skin menu for now)
      if(Input.GetButtonDown("Fire3") && !reading && GameObject.Find("skinMenu") == null)
      {
        GameManager.instance.OpenSkinMenu();
      }

      /* DEBUG READING
      if(Input.GetKeyDown(KeyCode.Q))
      {
        if(!reading)
        {
          stopMovement();
        }
        else
        {
          resumeMovement();
        }
      }
      */
    }
    void FixedUpdate()
    {
        rbody.MovePosition(rbody.position + mov * speed * Time.deltaTime);
    }

    public void stopMovement()
    {
      reading = true;
      mov = new Vector2(0, 0);
    }
    public void resumeMovement()
    {
      reading = false;
    }

    public void ForceFaceFront()
    {
        for(int i = 0; i < anim.Count; i++)
        {
          anim[i].SetFloat("movX", 0);
          anim[i].SetFloat("movY", -1);
        }
    }
}
