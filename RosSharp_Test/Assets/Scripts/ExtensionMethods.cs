using Microsoft.MixedReality.Toolkit.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

namespace NRISVTE {
    public static class ExtensionMethods {

        // List Extensions
        public static bool Empty<T>(this List<T> l) {
            return l.Count == 0;
        }

        public static int MaxIndex<T>(this IEnumerable<T> source) {
            IComparer<T> comparer = Comparer<T>.Default;
            using (var iterator = source.GetEnumerator()) {
                if (!iterator.MoveNext()) {
                    throw new InvalidOperationException("Empty sequence");
                }
                int maxIndex = 0;
                T maxElement = iterator.Current;
                int index = 0;
                while (iterator.MoveNext()) {
                    index++;
                    T element = iterator.Current;
                    if (comparer.Compare(element, maxElement) > 0) {
                        maxElement = element;
                        maxIndex = index;
                    }
                }
                return maxIndex;
            }
        }

        public static void Resize<T>(this List<T> l, int desiredSize) {
            while (l.Count < desiredSize) {
                l.Add(default(T));
            }
        }

        // Stack Extensions
        public static bool Empty<T>(this Stack<T> s) {
            return s.Count == 0;
        }

        // Hashset Extensions
        public static bool Empty<T>(this HashSet<T> h) {
            return h.Count == 0;
        }

        // Dictionary Extensions
        public static bool Empty<T, V>(this Dictionary<T, V> h) {
            return h.Count == 0;
        }

        // Queue Extensions
        public static bool Empty<T>(this Queue<T> h) {
            return h.Count == 0;
        }

        // String Extensions
        public static string ReplaceFirst(this string text, string search, string replace) {
            int pos = text.IndexOf(search);
            if (pos < 0) {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        // TextMesh Extensions
        public static void ForceTextUpdate(this TextMeshPro tmp) {
            tmp.enabled = false;
            tmp.enabled = true;
        }

        // Transform Extensions
        public static void SnapToParent(this Transform t, Transform prospectiveParent) {
            t.rotation = prospectiveParent.rotation;
            t.SetParent(prospectiveParent);
        }
        public static void SnapToParent(this Transform t, Transform prospectiveParent, Vector3 newLocalPosition) {
            t.rotation = prospectiveParent.rotation;
            t.SetParent(prospectiveParent);
            t.localPosition = newLocalPosition;
        }

        public static T GetComponentInChildrenOnlyDepthOne<T>(this Transform t) where T : Component {
            foreach (Transform tmp in t) {
                T cmp = tmp.GetComponent<T>();
                if (cmp != null) {
                    return cmp;
                }
            }
            return null;
        }


        public static void RotateTowardsUser(this Transform t) {
            t.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }

        // Gameobject Extensions
        public static T GetComponentInChildrenOnlyDepthOne<T>(this GameObject go) where T : Component {
            return go.transform.GetComponentInChildrenOnlyDepthOne<T>();
        }
        // Gameobject Extensions
        public static string TryGetNiceNameOfObjectForLogging(this GameObject go) {
            return go.name;
        }

        // ObjectManipulator
        public static void RemoveTwoHandedScaling(this ObjectManipulator om) {
            om.TwoHandedManipulationType = om.TwoHandedManipulationType &
                (Microsoft.MixedReality.Toolkit.Utilities.TransformFlags.Move | Microsoft.MixedReality.Toolkit.Utilities.TransformFlags.Rotate);
        }

        // Float
        public static float TimeSince(this float f) {
            return Time.time - f;
        }
        // sorting random
        private static System.Random rng = new System.Random();
        public static void Shuffle<T>(this IList<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}