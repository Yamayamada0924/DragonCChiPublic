using UnityEngine;

namespace yydlib
{
    public class FixOneRand
    {
        public static uint Rand(uint seed)
        {
            FixRand fixRand = new FixRand(seed);
            return fixRand.GetNext();
        }
        public static uint Rand(int seed)
        {
            FixRand fixRand = new FixRand(seed);
            return fixRand.GetNext();
        }       
        public static int Range(uint seed, int minInclude, int maxExclude)
        {
            FixRand fixRand = new FixRand(seed);
            return fixRand.Range(minInclude, maxExclude);
        }
        public static int Range(int seed, int minInclude, int maxExclude)
        {
            FixRand fixRand = new FixRand(seed);
            return fixRand.Range(minInclude, maxExclude);
        }        
        public static float Range6Digit(uint seed, float minInclude, float maxInclude)
        {
            FixRand fixRand = new FixRand(seed);
            return fixRand.Range6Digit(minInclude, maxInclude);
        }
        public static float Range6Digit(int seed, float minInclude, float maxInclude)
        {
            FixRand fixRand = new FixRand(seed);
            return fixRand.Range6Digit(minInclude, maxInclude);
        }
    }
}