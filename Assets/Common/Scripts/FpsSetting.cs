using UnityEngine;

namespace Common.Scripts
{
    public class FpsSetting : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 60;
        }
    }
}
