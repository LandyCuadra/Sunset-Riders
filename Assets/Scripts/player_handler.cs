using UnityEngine;
using System.Collections;

public class player_handler : MonoBehaviour {

    Vector2 velocidad;
    bool is_grounded = true;
    public float vel_desp;
    public float vel_salto;
    public GameObject spr1;
    public GameObject spr2;
    public GameObject bala;
    private Vector2 pos_min;
    private Vector2 pos_max;

    // Use this for initialization
    void Start() {
        
        pos_max = GameObject.Find("max").transform.position;
    }

    // Update is called once per frame
    void Update() {

        Vector3 posicion = transform.position;
        pos_min = GameObject.Find("cam_min").transform.position;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) // caminar izquierda
        {
            velocidad.x -= vel_desp;
            if (!spr2.GetComponent<SpriteRenderer>().flipX)
            {
                spr1.GetComponent<SpriteRenderer>().flipX = true;
                spr2.GetComponent<SpriteRenderer>().flipX = true;
                if (is_grounded)
                    spr2.transform.position += new Vector3(-0.05f, 0, 0);
                else
                    spr2.transform.position += new Vector3(0.06f, 0, 0);

            }
            if (is_grounded)
                GetComponent<Animator>().SetInteger("estado", 1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) // caminar derecha
        {
            velocidad.x += vel_desp;
            if (spr2.GetComponent<SpriteRenderer>().flipX)
            {
                spr1.GetComponent<SpriteRenderer>().flipX = false;
                spr2.GetComponent<SpriteRenderer>().flipX = false;
                if (is_grounded)
                    spr2.transform.position += new Vector3(0.05f, 0, 0);
                else
                    spr2.transform.position += new Vector3(-0.06f, 0, 0);                
            }

            if (is_grounded)
                GetComponent<Animator>().SetInteger("estado", 1);
        }

            if (Input.GetKeyDown(KeyCode.UpArrow) && is_grounded) // saltar
            {
                GetComponent<Animator>().SetInteger("estado", 2);
                velocidad.y += vel_salto;
                is_grounded = false;
                if (!spr2.GetComponent<SpriteRenderer>().flipX)
                    spr2.transform.position += new Vector3(-0.06f, 0, 0);
                else
                    spr2.transform.position += new Vector3(0.05f, 0, 0);
            }

            if(Input.GetKeyDown(KeyCode.X)) {
            GameObject nueva_bala = Instantiate(bala, transform.position, Quaternion.identity);
        }
        if ((Input.GetKeyUp(KeyCode.LeftArrow) && velocidad.x < 0)|| Input.GetKeyUp(KeyCode.RightArrow) && velocidad.x > 0) // dejar de caminar
        {
            velocidad.x = 0.0f;
            if (is_grounded)
                GetComponent<Animator>().SetInteger("estado", 0);
        }
    }       


    private void FixedUpdate()
    {
        if (!is_grounded)
            velocidad += Physics2D.gravity * Time.deltaTime;

        GetComponent<Rigidbody2D>().position += velocidad * Time.deltaTime;

        check_limites();
    }

    void check_limites()
    {
        if(GetComponent<Rigidbody2D>().position.x > pos_max.x)
            GetComponent<Rigidbody2D>().position = new Vector2(pos_max.x, GetComponent<Rigidbody2D>().position.y);
        else if(GetComponent<Rigidbody2D>().position.x < pos_min.x)
            GetComponent<Rigidbody2D>().position = new Vector2(pos_min.x, GetComponent<Rigidbody2D>().position.y);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo") // colision con el suelo
        {
            is_grounded = true;
            if (is_grounded)
            {
                velocidad.y = 0;

                if(velocidad.x != 0)
                    GetComponent<Animator>().SetInteger("estado", 1);
                else 
                    GetComponent<Animator>().SetInteger("estado", 0
                        );


                if (!spr2.GetComponent<SpriteRenderer>().flipX)
                    spr2.transform.position += new Vector3(0.06f, 0, 0);
                else
                    spr2.transform.position += new Vector3(-0.05f, 0, 0);
            }            
        }

    }

}
