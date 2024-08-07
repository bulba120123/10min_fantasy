using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public Transform magnetDest;
    public MagnetField magnetField;
    public float fieldRange;
    public bool magnetOn = false;

    const float DEFAULT_FIELD_RANGE = 3f;
    WaitForSeconds waitFor = new WaitForSeconds(0.1f);

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        magnetOn = true;
        fieldRange = fieldRange == 0 ? DEFAULT_FIELD_RANGE : fieldRange;
        magnetField.GetComponent<CircleCollider2D>().radius = fieldRange;
    }

    public void MagnetOff()
    {
        magnetOn = false;
    }

    private void Update()
    {
        if (magnetOn)
        {
            foreach (Transform target in magnetField.targets)
            {
                target.position = Vector2.MoveTowards(target.position, magnetDest.position, 0.1f);
            }
        }
    }
}
