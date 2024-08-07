using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carmone
{
    public class MathUtil
    {
        public static bool MyApproximation(float a, float b, float tolerance)
        {
            return (Mathf.Abs(a - b) < tolerance);
        }
    }
}
