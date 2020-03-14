using System;
using TMPro;
using UnityEngine;

namespace CodeChallenge
{
    public class Manager : MonoBehaviour
    {
        [Header("References")]
        public ShapeController Shape;
        public GameObject MaskedImage;
        public TextMeshProUGUI TopText;

        [Header("Texts")]
        public string DesktopText;
        public float DesktopTopMargin;
        public string MobileText;
        public float MobileTopMargin;

        private void Awake()
        {
            Vector3 initialPosition = TopText.rectTransform.anchoredPosition;
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            TopText.text = MobileText;
            TopText.rectTransform.anchoredPosition = new Vector3(initialPosition.x, -MobileTopMargin, initialPosition.z);
#else
            TopText.text = DesktopText;
            TopText.rectTransform.anchoredPosition = new Vector3(initialPosition.x, -DesktopTopMargin, initialPosition.z);
#endif
        }

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

        public void OnQuitButtonClicked()
        {
#if !UNITY_EDITOR
            Application.Quit();
#endif
        }
    }
}
