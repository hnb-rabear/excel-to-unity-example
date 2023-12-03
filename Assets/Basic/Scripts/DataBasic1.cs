using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Excel2Unity.Basic
{
    [Serializable]
    public class DataBasic1
    {
        [Serializable]
        public class Currency
        {
            public MinMax coin;
            public int gemMin;
            public int gemMax;
            
            [Serializable]
            public class MinMax
            {
                public int min;
                public int max;
            }
        }
        
        [Serializable]
        public class Character
        {
            public string name;
            public IDs.Gender sex;
        }
        
        public int numberExample1;
        public int numberExample2;
        public int numberExample3;
        public bool boolExample;
        public string stringExample;
        public Currency currency;
        public Character character;
    }
}