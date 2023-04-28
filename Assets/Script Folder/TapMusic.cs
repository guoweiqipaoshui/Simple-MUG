using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Òô½Ú½Å±¾
public class TapMusic : MonoBehaviour
{
    void Update()
    {
        if (transform.position.z <= -4)
        {
            gameObject.SetActive(false);
        }
        if(transform.position.z <= 0)
        {
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_DiffuseColor", Color.blue);
        }
        if (transform.position.z <= -2)
        {
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_DiffuseColor", Color.red);
        }
    } 
}
