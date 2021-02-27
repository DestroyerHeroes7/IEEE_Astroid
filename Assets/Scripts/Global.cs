using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Global : MonoBehaviour
{
    [Serializable]
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
    public enum Buff
    {
        FireRate,
        DoubleLaser,
        Speed,
        Shield
    }
    public static Dictionary<Buff, float> buffLifeTime = new Dictionary<Buff, float>()
    {
        [Buff.FireRate] = 10,
        [Buff.DoubleLaser] = 10,
        [Buff.Speed] = 5,
        [Buff.Shield] = 15
    };
}
