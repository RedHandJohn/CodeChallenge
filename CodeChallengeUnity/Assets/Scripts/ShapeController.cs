using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeChallenge
{
    public class ShapeController : MonoBehaviour
    {
        [Header("Configuration")]
        [Tooltip("Double Click Window for mouse input. Not used for mobile devices")]
        public float DoubleClickWindow;
        [Header("References")]
        public Animator Animator;
        public List<Shape> Shapes;

        [SerializeField]
        private Shape _currentShape;
        private bool _allowDoubleClick = false;


        private void Start()
        {
            SetShapeColor();
        }

#if  !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        private void Update()
        {
            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).tapCount == 2)
            {
                Vector2 raw = Input.GetTouch(0).position;
                Vector3 screenToWorldSpace = Camera.main.ScreenToWorldPoint(raw);
                Vector3 screenToViewport = Camera.main.ScreenToViewportPoint(raw);
                RaycastHit2D raycastHit = Physics2D.Raycast(screenToWorldSpace, Vector2.zero);
                if(raycastHit.collider != null && CompareTag(raycastHit.collider.tag))
                {
                    OnDoubleClick();
                }
            }
        }
#else
        private void OnMouseDown()
        {
            Debug.Log("OnMouseDown");

            if (_allowDoubleClick)
            {
                OnDoubleClick();
                _allowDoubleClick = false;
            }
            else
            {
                StartCoroutine(FirstClickCoroutine());
            }
        }

        private IEnumerator FirstClickCoroutine()
        {
            _allowDoubleClick = true;

            yield return new WaitForSeconds(DoubleClickWindow);
            _allowDoubleClick = false;
        }
#endif

        /// <summary>
        /// Called as Animation Event inside DoubleClickAnim clip
        /// </summary>
        private void OnDoubleClick()
        {
            Debug.Log("Double click!");
            Animator.Play("DoubleClick");
        }

        private void SetShapeColor()
        {
            _currentShape.SpriteRenderer.color = Util.GenerateRandomColor();
        }

        public void SwitchShape(ShapeType shapeType)
        {
            Shape shape = Shapes.Find(x => x.ShapeType.Equals(shapeType));
            if(shape == null)
            {
                Debug.LogError($"Could not find shape type {shapeType}");
            }
            else
            {
                _currentShape.gameObject.SetActive(false);
                shape.gameObject.SetActive(true);
                _currentShape = shape;
                SetShapeColor();
            }
        }
    }
}
