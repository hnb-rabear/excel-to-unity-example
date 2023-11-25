using System;
using RCore.Framework.Data;

namespace Excel2Unity.Basic
{
    [Serializable]
    public class DataAdvanced : AttributesCollection<AttributeParse>
    {
        public int id;
        public string name;
    }
}