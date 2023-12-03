using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Excel2Unity.Basic
{
    [Serializable]
    public class DataBasic2
    {
        [Serializable]
        public class Example
        {
            public int id;
            public string name;
        }
        
        [Serializable]
        public class NestedArrayExample
        {
            public int[] examples;
        }
        
        [Serializable]
        public class NestedJsonExample
        {
            [Serializable]
            public class MyClass
            {
                public string[] name;
            }

            public MyClass examples;
        }
        
        public string[] array1;
        public int[] array2;
        public int[] array3;
        public bool[] array4;
        public int[] array5;
        public string[] array6;
        public Example json1;
        public NestedArrayExample nestedArray;
        public NestedJsonExample nestedJson;
    }
}