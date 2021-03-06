// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2014/08/17 19:40
// 
// License Copyright (c) Daniele Giardini.
// This work is subject to the terms at http://dotween.demigiant.com/license.php

using System;
using System.Reflection;
using UnityEngine;

namespace DG.Tweening.Core
{
    public static class Utils
    {
        static Assembly[] _loadedAssemblies;
        static readonly string[] _defAssembliesToQuery = new[] { // First assemblies to look into before checking all of them
            "Assembly-CSharp", "Assembly-CSharp-firstpass"
        };

        /// <summary>
        /// Returns a Vector3 with z = 0
        /// </summary>
        public static Vector3 Vector3FromAngle(float degrees, float magnitude)
        {
            float radians = degrees * Mathf.Deg2Rad;
            return new Vector3(magnitude * Mathf.Cos(radians), magnitude * Mathf.Sin(radians), 0);
        }

        /// <summary>
        /// Returns the 2D angle between two vectors
        /// </summary>
        public static float Angle2D(Vector3 from, Vector3 to)
        {
            Vector2 baseDir = Vector2.right;
            to -= from;
            float ang = Vector2.Angle(baseDir, to);
            Vector3 cross = Vector3.Cross(baseDir, to);
            if (cross.z > 0) ang = 360 - ang;
            ang *= -1f;
            return ang;
        }

        /// <summary>
        /// Uses approximate equality on each axis instead of Unity's Vector3 equality,
        /// because the latter fails (in some cases) when assigning a Vector3 to a transform.position and then checking it.
        /// </summary>
        public static bool Vector3AreApproximatelyEqual(Vector3 a, Vector3 b)
        {
            return Mathf.Approximately(a.x, b.x)
                   && Mathf.Approximately(a.y, b.y)
                   && Mathf.Approximately(a.z, b.z);
        }

        /// <summary>
        /// Looks for the type withing all possible project assembly names
        /// </summary>
        public static Type GetLooseScriptType(string typeName)
        {
            // Check in default assemblies (Unity 2017 and later)
            for (int i = 0; i < _defAssembliesToQuery.Length; ++i) {
                Type result = Type.GetType(string.Format("{0}, {1}", typeName, _defAssembliesToQuery[i]));
                if (result == null) continue;
                return result;
            }
            // Check in all assemblies
            if (_loadedAssemblies == null) _loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < _loadedAssemblies.Length; ++i) {
                Type result = Type.GetType(string.Format("{0}, {1}", typeName, _loadedAssemblies[i].GetName()));
                if (result == null) continue;
                return result;
            }
            return null;
        }

        // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████
        // ███ public CLASSES ████████████████████████████████████████████████████████████████████████████████████████████████
        // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████

        // Uses code from BK > http://stackoverflow.com/a/1280832
        // (scrapped > doesn't work with IL2CPP)
//        public class InstanceCreator<T> where T : new()
//        {
//            static readonly Expression<Func<T>> _TExpression = () => new T();
//            static readonly Func<T> _TBuilder = _TExpression.Compile();
//            public static T Create() { return _TBuilder(); }
//        }
    }
}