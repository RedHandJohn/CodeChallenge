using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CodeChallenge.GeneratedShapes
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
        public MeshFilter MeshFilter;
        public MeshCollider MeshCollider;
        public List<ShapeScriptableObject> Shapes;

        [Header("SFX")]
        public AudioClip Bark;
        public AudioClip Moo;

        private bool _allowDoubleClick;

        private void Start()
        {
            SwitchShape(ShapeType.Hexagon);
        }

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        private void Update()
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began && touch.tapCount == 2)
                {
                    Ray ray = MainCamera.ScreenPointToRay(touch.position);
                    RaycastHit raycastHit;
                    if(Physics.Raycast(ray, out raycastHit) && raycastHit.collider?.tag == "Shape")
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
            Animator.Play("Flip");
            PlaySfx();
        }

        public void SwitchShape(ShapeType shapeType)
        {
            var shape = Shapes.Find(x => x.ShapeType.Equals(shapeType));
            if(shape == null)
            {
                Debug.LogError($"Could not find shape type {shapeType} in scriptable objects.");
            }
            else
            {
                GenerateNewMesh(shape.Vertices);
            }
        }

        private void GenerateNewMesh(Vector2[] vertices2D)
        {
            var vertices3D = Array.ConvertAll<Vector2, Vector3>(vertices2D, v => v);

            // Use the triangulator to get indices for creating triangles
            var triangulator = new Triangulator(vertices2D);
            var indices = triangulator.Triangulate();

            var colors = GetRandomColors(vertices2D.Length);

            // Create the mesh
            var mesh = new Mesh
            {
                vertices = vertices3D,
                triangles = indices,
                colors = colors
            };

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            MeshFilter.mesh = mesh;
            MeshCollider.sharedMesh = mesh;
        }

        private void SetShapeColor()
        {
            MeshFilter.mesh.SetColors(GetRandomColors(MeshFilter.mesh.colors.Length));
        }

        private Color[] GetRandomColors(int arrayLength)
        {
            Color randomColor = UnityEngine.Random.ColorHSV();

            Color[] colors = new Color[arrayLength];
            for(int i = 0; i < arrayLength; i++)
            {
                colors[i] = randomColor;
            }

            return colors;
        }

        private void PlaySfx()
        {
            if (UnityEngine.Random.Range(0, 4) <= 2)
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
