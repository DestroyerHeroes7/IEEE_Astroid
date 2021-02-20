using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public struct RangeFloat
    {
        public float min;
        public float max;
        public RangeFloat(float _min,float _max)
        {
            min = _min;
            max = _max;
        }
    }
}
