using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara_handler : MonoBehaviour
{
    public GameObject minimo;
    public GameObject maximo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject billy = GameObject.Find("Billy");

        if((billy.transform.position.x > maximo.transform.position.x) && maximo.transform.position.x < GameObject.Find("Nivel").GetComponent<nivel_handler>().maximo.transform.position.x)
        {
            transform.position += new Vector3(0.02f, 0, 0);
        }
    }
}
