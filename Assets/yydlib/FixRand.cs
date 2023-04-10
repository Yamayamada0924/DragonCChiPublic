using System;
using UnityEngine;

namespace yydlib
{
    public struct FixRand
    {
        private uint _x, _y, _z, _w;

        public FixRand(uint seed) {
            _x = seed; _y = _x * 3266489917U + 1; _z = _y * 3266489917U + 1; _w = _z * 3266489917U + 1;
        }

        public FixRand(int seed) : this((uint)((long)seed - int.MinValue))
        {
        }
            

        public uint GetNext() {
            uint t = _x ^ (_x << 11); 
            _x = _y; 
            _y = _z; 
            _z = _w; 
            _w = (_w ^ (_w >> 19)) ^ (t ^ (t >> 8));
            return _w;
        }

        public int Range(int minInclude, int maxExclude) {
            return minInclude + Math.Abs((int)GetNext()) % (maxExclude - minInclude);
        }
        
        public float Range6Digit(float minInclude, float maxInclude)
        {
            const float digit6 = 100000.0f;
            int range = Range((int)(minInclude * digit6), (int)(maxInclude * digit6) + 1);
            return Mathf.Clamp((float)range / digit6, minInclude, maxInclude);
        }
    }
}