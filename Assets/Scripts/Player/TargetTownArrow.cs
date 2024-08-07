using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTownArrow : MonoBehaviour
{
    public Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.townObj == null || 
            GameManager.instance.townObj.activeSelf == false || 
            !GameManager.instance.isLive)
            return;
        Vector3 dir = targetPos - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetArrow(Vector3 target)
    {
        this.targetPos = target;
        this.gameObject.SetActive(true);
    }

    public void UnSetArrow()
    {
        this.gameObject.SetActive(false);
    }
}
