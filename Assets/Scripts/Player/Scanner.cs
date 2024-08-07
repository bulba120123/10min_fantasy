using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using System.Linq;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public List<LayerMask> targetLayers;
    public List<RaycastHit2D> targets;
    public Transform? NearestTarget { get; private set; }

    private void FixedUpdate()
    {
        targets = targetLayers.Aggregate(new List<RaycastHit2D>(), (result, layer) =>
        {
            List<RaycastHit2D> temp = new List<RaycastHit2D>(); 
            temp.AddRange(Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, layer));
            result.AddRange(temp);
            return result;
        });
        NearestTarget = GetNearst();
    }

    Transform GetNearst()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }

}
