using UnityEngine;

namespace GamePlay.Scripts
{
    public class JellyMesh : MonoBehaviour
    {
        [SerializeField] private float _intensity = 1f;
        [SerializeField] private float _mass = 1f;
        [SerializeField] private float _stiffness = 1f;
        [SerializeField] private float _damping = 0.75f;
    
        private Mesh _originalMesh, _meshClone;
        private MeshRenderer _meshRenderer;
        private JellyVertex[] _jv;
        private Vector3[] _vertexArray;

        private void Start()
        {
            _originalMesh = GetComponent<MeshFilter>().sharedMesh;
            _meshClone = Instantiate(_originalMesh);
            GetComponent<MeshFilter>().sharedMesh = _meshClone;
            _meshRenderer = GetComponent<MeshRenderer>();
            _jv = new JellyVertex[_meshClone.vertices.Length];
            for (int i = 0; i < _meshClone.vertices.Length; i++)
                _jv[i] = new JellyVertex(i, transform.TransformPoint(_meshClone.vertices[i]));
        }
        private void FixedUpdate()
        {
            _vertexArray = _originalMesh.vertices;
            foreach (JellyVertex t in _jv)
            {
                Vector3 target = transform.TransformPoint(_vertexArray[t.ID]);
                var bounds = _meshRenderer.bounds;
                float intensity = (1 - (bounds.max.y - target.y) / bounds.size.y) * _intensity;
                t.Shake(target, _mass, _stiffness, _damping);
                target = transform.InverseTransformPoint(t.Position);
                _vertexArray[t.ID] = Vector3.Lerp(_vertexArray[t.ID], target, intensity);
            }
            _meshClone.vertices = _vertexArray;
        }
    
    }
}
