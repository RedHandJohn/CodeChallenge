using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeChallenge.GeneratedShapes
{
    [CreateAssetMenu(fileName = "NewShape", menuName = "CodeChallenge/NewShape")]
    public class ShapeScriptableObject : ScriptableObject
    {
        public ShapeType ShapeType;
        public Vector2[] Vertices;
    }
}
