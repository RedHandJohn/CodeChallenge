using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeChallenge
{
    public class Util
    {
        public static Color GenerateRandomColor()
        {
            float r = UnityEngine.Random.Range(0f, 1f);
            float g = UnityEngine.Random.Range(0f, 1f);
            float b = UnityEngine.Random.Range(0f, 1f);

            return new Color(r, g, b);
        }
    }
}
