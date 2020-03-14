using UnityEngine;

namespace CodeChallenge
{
    public class Shape : MonoBehaviour
    {
        public ShapeType ShapeType;
        public SpriteRenderer SpriteRenderer;

        private void Awake()
        {
            if(ShapeType == ShapeType.Undefined)
            {
                Debug.LogWarning("Oopsie! Forgot to set shape type.");
            }
        }
    }
}
