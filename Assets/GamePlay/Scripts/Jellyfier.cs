// using UnityEngine;
//
// namespace GamePlay.Scripts
// {
//     public class Jellyfier : MonoBehaviour
//     {
//         public float BounceSpeed;
//         public float FallForce;
//         public float Stiffness;
//
//         private MeshFilter MeshFilter;
//         private Mesh _mesh;
//
//         private JellyVertex[] _jellyVertices;
//         private Vector3[] _currentMeshVertices;
//         private void Start()
//         {
//             MeshFilter = GetComponent<MeshFilter>();
//             _mesh = MeshFilter.mesh;
//
//             GetVertices();
//         }
//         private void Update()
//         {
//             UpdateVertices();
//         }
//         private void GetVertices()
//         {
//             _jellyVertices = new JellyVertex[_mesh.vertices.Length];
//             _currentMeshVertices = new Vector3[_mesh.vertices.Length];
//             for (int i = 0; i < _mesh.vertices.Length; i++)
//             {
//                 _jellyVertices[i] = new JellyVertex(i, _mesh.vertices[i], _mesh.vertices[i], Vector3.zero);
//                 _currentMeshVertices[i] = _mesh.vertices[i];
//             }
//         }
//         private void UpdateVertices()
//         {
//             for (int i = 0; i < _jellyVertices.Length; i++)
//             {
//                 _jellyVertices[i].UpdateVelocity(BounceSpeed);
//                 _jellyVertices[i].Settle(Stiffness);
//
//                 _jellyVertices[i].CurrentVertexPosition += _jellyVertices[i].CurrentVelocity * Time.deltaTime;
//                 _currentMeshVertices[i] = _jellyVertices[i].CurrentVertexPosition;
//             }
//
//             _mesh.vertices = _currentMeshVertices;
//             _mesh.RecalculateBounds();
//             _mesh.RecalculateNormals();
//             _mesh.RecalculateTangents();
//         }
//         
//         public void OnCollisionEnter(Collision other)
//         {
//             Debug.Log("Collision detected with " + other.gameObject.name);
//             ContactPoint[] contactPoints = other.contacts;
//             for (int i = 0; i < contactPoints.Length; i++)
//             {
//                 Vector3 inputPoint = contactPoints[i].point + (contactPoints[i].point * 0.1f);
//                 ApplyPressureToPoint(inputPoint, FallForce);
//             }
//         }
//         public void ApplyPressureToPoint(Vector3 point, float pressure)
//         {
//             for (int i = 0; i < _jellyVertices.Length; i++)
//             {
//                 _jellyVertices[i].ApplyPressureToVertex(transform,point,pressure);
//             }
//         }
//     }
// }
