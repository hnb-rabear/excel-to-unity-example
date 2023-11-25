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
        
        public string[] array1;
        public int[] array2;
        public int[] array3;
        public bool[] array4;
        public int[] array5;
        public string[] array6;
        public Example json1;
    }
}