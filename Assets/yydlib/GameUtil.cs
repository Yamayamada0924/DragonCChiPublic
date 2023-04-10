//#define ASSERT_FORCE_OFF

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace yydlib
{
    public static class GameUtil
    {
        static readonly float DestroyDelayTime = 0.5f;

#if ASSERT_FORCE_ON
    private const string UNITY_ASSERTIONS_STRING = "ASSERT_FORCE_ON";
#elif ASSERT_FORCE_OFF
    private const string UNITY_ASSERTIONS_STRING = "ASSERT_DISABLE";
#else
        private const string UNITY_ASSERTIONS_STRING = "UNITY_ASSERTIONS";
#endif

        [Conditional(UNITY_ASSERTIONS_STRING)]
        public static void Assert(bool condition)
        {
            if (condition)
            {
                return;
            }
            UnityEngine.Debug.Assert(condition);
            System.Diagnostics.Debugger.Break();
        }
        [Conditional(UNITY_ASSERTIONS_STRING)]
        public static void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }
        [Conditional(UNITY_ASSERTIONS_STRING)]
        public static void LogWithoutStack(string message)
        {
            var logType = LogType.Log;
            var oldStackTrace = Application.GetStackTraceLogType(logType);
            Application.SetStackTraceLogType(logType, StackTraceLogType.None);
            
            UnityEngine.Debug.Log(message);
            
            Application.SetStackTraceLogType(logType, oldStackTrace);
        }
        [Conditional(UNITY_ASSERTIONS_STRING)]
        public static void LogWarning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }
        [Conditional(UNITY_ASSERTIONS_STRING)]
        public static void LogError(string message)
        {
            UnityEngine.Debug.LogError(message);
        }
        public static void LogRelease(string message)
        {
            UnityEngine.Debug.Log(message);
        }


        /// <summary>
        /// 指定された GameObject を複製して返します
        /// </summary>
        public static GameObject Clone(GameObject go, string name = "")
        {
            var clone = Object.Instantiate(go, go.transform.parent, true) as GameObject;
            clone.transform.localPosition = go.transform.localPosition;
            clone.transform.localRotation = go.transform.localRotation;
            clone.transform.localScale = go.transform.localScale;
            if (name != "")
            {
                clone.name = name;
            }
            return clone;
        }

        public static float GetLinear(float startParam, float endParam, float start, float end, float now)
        {
            now = start < end ? Mathf.Clamp(now, start, end) : Mathf.Clamp(now, end, start);

            return startParam + (endParam - startParam) * (now - start) / (end - start);
        }
        public static Vector3 EasedValue(Vector3 from, Vector3 to, float lifetimePercentage, DG.Tweening.Ease easeType)
        {
            float easedRate = DG.Tweening.DOVirtual.EasedValue(0.0f, 1.0f, lifetimePercentage, easeType);
            return new Vector3(
                from.x * (1.0f - easedRate) + to.x * easedRate,
                from.y * (1.0f - easedRate) + to.y * easedRate,
                from.z * (1.0f - easedRate) + to.z * easedRate);
        }
        public static Vector3 CurveValue(Vector3 from, Vector3 to, float rate, AnimationCurve animationCurve)
        {
            float easedRate = animationCurve.Evaluate(rate);
            return new Vector3(
                from.x * (1.0f - easedRate) + to.x * easedRate,
                from.y * (1.0f - easedRate) + to.y * easedRate,
                from.z * (1.0f - easedRate) + to.z * easedRate);
        }

        public static float Normalize180Angle(float angle)
        {
            return Mathf.Repeat(angle + 180.0f, 360.0f) - 180.0f;
        }

        public static bool Contains(RectTransform rectTransform, Vector2 position)
        {
            //var bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(rectTransform);

            return rectTransform.rect.Contains(position);

            //return bounds.Contains(position);
        }

        public static IEnumerator DelayMethod(float waitTime, System.Action action)
        {
            yield return new WaitForSeconds(waitTime);
            GameUtil.Assert(action != null);
            action?.Invoke();
        }

        public static void DestroyAction(GameObject gameObject)
        {
            Object.Destroy(gameObject, DestroyDelayTime);
        }

        public static int GetEnumCount(Type type)
        {
            return System.Enum.GetValues(type).Length;
        }
    }
}
