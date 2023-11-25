using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using RCore.Common;
using UnityEditor;
#endif

namespace Excel2Unity.Basic
{
    [CreateAssetMenu(fileName = "DataCollectionBasic", menuName = "Excel2Unity/DataCollectionBasic", order = 1)] //[]
    public class DataCollectionBasic : ScriptableObject
    {
        public List<DataBasic1> dataBasic1;
        public List<DataBasic2> dataBasic2;
        public List<DataAdvanced> dataAdvanced;
        
        public void LoadData()
        {
#if UNITY_EDITOR
            var txt =  AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Basic/Data/ExampleDataBasic1.txt");
            dataBasic1 = JsonHelper.ToList<DataBasic1>(txt.text);
            txt =  AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Basic/Data/ExampleDataBasic2.txt");
            dataBasic2 = JsonHelper.ToList<DataBasic2>(txt.text);
            txt = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Basic/Data/ExampleDataAdvanced.txt");
            dataAdvanced = JsonHelper.ToList<DataAdvanced>(txt.text);
#endif
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(DataCollectionBasic))]
    public class DataCollectionEditor : Editor
    {
        private DataCollectionBasic m_target;

        private void OnEnable()
        {
            m_target = target as DataCollectionBasic;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Load Data"))
            {
                m_target.LoadData();
            }
        }
    }
#endif
}