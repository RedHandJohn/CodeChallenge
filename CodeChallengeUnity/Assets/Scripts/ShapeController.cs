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
        public Camera MainCamera;
        public Animator Animator;
        public AudioSource AudioSource;
        public List<Shape> Shapes;

        [Header("SFX")]
        public AudioClip Bark;
        public AudioClip Moo;

        [Header("Debugging")]
        [SerializeField]
        private Shape _currentShape;

        private bool _allowDoubleClick = false;

        public void SwitchShape(ShapeType shapeType)
        {
            Shape shape = Shapes.Find(x => x.ShapeType.Equals(shapeType));
            if (shape == null)
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


        private void Start()
        {
            SetShapeColor();
        }

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        private void Update()
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began && touch.tapCount == 2)
                {
                    Vector3 screenToWorldSpace = MainCamera.ScreenToWorldPoint(touch.position);
                    RaycastHit2D raycastHit = Physics2D.Raycast(screenToWorldSpace, Vector2.zero);
                    if (raycastHit.collider != null && CompareTag(raycastHit.collider.tag))
                    {
                        OnDoubleClick();
                    }
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
        /// Calls SetShapeColor as Animation Event inside DoubleClickAnim clip
        /// </summary>
        private void OnDoubleClick()
        {
            Debug.Log("Double click!");
            Animator.Play("DoubleClick");
            PlaySfx();
        }

        private void SetShapeColor()
        {
            _currentShape.SpriteRenderer.color = Util.GenerateRandomColor();
        }

        private void PlaySfx()
        {
            if(UnityEngine.Random.Range(0,4) <= 2)
            {
                AudioSource.PlayOneShot(Bark);
            }
            else
            {
                AudioSource.PlayOneShot(Moo);
            }
        }
    }
}
