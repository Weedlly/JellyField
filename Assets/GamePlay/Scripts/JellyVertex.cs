// using UnityEngine;
//
// namespace GamePlay.Scripts
// {
//     public class JellyVertex
//     {
//         public int VertexIndex;
//         public Vector3 InitialVertexPosition;
//         public Vector3 CurrentVertexPosition;
//         
//         public Vector3 CurrentVelocity;
//         
//         public JellyVertex(int vertexIndex, Vector3 initialVertexPosition, Vector3 currentVertexPosition, Vector3 currentVelocity)
//         {
//             VertexIndex = vertexIndex;
//             InitialVertexPosition = initialVertexPosition;
//             CurrentVertexPosition = currentVertexPosition;
//             CurrentVelocity = currentVelocity;
//         }
//         public Vector3 GetCurrentDisplacement()
//         {
//             return CurrentVertexPosition - InitialVertexPosition;
//         }
//         public void UpdateVelocity(float bounceSpeed)
//         {
//             CurrentVelocity -= GetCurrentDisplacement() * (bounceSpeed * Time.deltaTime);
//         }
//         public void Settle(float stiffness)
//         {
//             CurrentVelocity *= 1f - stiffness * Time.deltaTime;
//         }
//         public void ApplyPressureToVertex(Transform transform, Vector3 position, float pressure)
//         {
//             Vector3 distanceVerticePoint = CurrentVertexPosition - transform.InverseTransformPoint(position);
//             float adaptedPressure = pressure / (1f + distanceVerticePoint.sqrMagnitude);
//             float velocity = adaptedPressure * Time.deltaTime;
//             CurrentVelocity += distanceVerticePoint.normalized * velocity;
//         }
//     }
// }
