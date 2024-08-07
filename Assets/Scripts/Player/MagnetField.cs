using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetField : MonoBehaviour
{
    public List<Transform> targets;

    private void Start()
    {
        targets = new List<Transform>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gold")
        {
            targets.Add(collision.transform);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Gold")
        { 
            targets.Remove(collision.transform);

        }
    }
}