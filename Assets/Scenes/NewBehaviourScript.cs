using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody rigidbody;
    private RaycastHit hit;
    private RaycastHit[] hits;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public float hiz;
    private int skor = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "elma")
        {
            skor++;
            float rast = Random.Range(0.5F,11.5F);
            collision.gameObject.transform.position = new Vector3(rast, 2, 2);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(hiz*Time.deltaTime,0,0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-hiz * Time.deltaTime, 0, 0);
        }
    }


    /*

       hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.down));
       for (int i = 0; i < hits.Length; i++)
       {
           if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.down),out hits[i], 10F))
           {
               Debug.DrawRay(transform.position, Vector3.down);
               Debug.Log("Yere deðiyor");
           }
           else
           {
               Debug.Log("Havada");
           }
       }*/
}
