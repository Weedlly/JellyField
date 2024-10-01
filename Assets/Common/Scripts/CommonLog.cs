using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Common.Scripts
{
    public static class CommonLog
    {
        [Conditional("ALL_LOG")]
        public static void LogError(string mess)
        {
            Debug.LogError(mess);
        }
    }
}
