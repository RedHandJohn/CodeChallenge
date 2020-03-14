using System;
using UnityEngine;

namespace CodeChallenge
{
    public class Manager : MonoBehaviour
    {
        public ShapeController Shape;
        public GameObject MaskedImage;

        public void OnShapeButtonClicked(int shapeType)
        {
            if(Enum.IsDefined(typeof(ShapeType), shapeType))
            {
                Shape.SwitchShape((ShapeType)shapeType);
            }
            else
            {
                Debug.LogError($"Tried to pass invalid shape type. Int value {shapeType}");
            }
        }

        public void OnMaskedImageToggle(bool toggle)
        {
            MaskedImage.SetActive(toggle);
        }
    }
}
