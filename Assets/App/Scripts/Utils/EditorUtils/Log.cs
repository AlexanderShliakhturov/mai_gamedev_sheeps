using UnityEngine;

namespace App.Scripts.Utils.EditorUtils
{
    public static class Log
    {
#if UNITY_EDITOR_64
        
        public static void Info(string message) => Debug.Log($"<color=cyan>{message}</color>");

        public static void Warning(string message) => Debug.LogWarning($"<color=yellow>{message}</color>");

        public static void Error(string message) => Debug.LogError($"<color=red><b>{message}</b></color>");

        public static void Success(string message) => Debug.Log($"<color=green>{message}</color>");
#endif

    }
}