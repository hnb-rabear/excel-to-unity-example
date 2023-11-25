using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Excel2Unity.Basic
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button m_btnChangeLanguage;
        [SerializeField] private TextMeshProUGUI m_txtCurrentLanguage;
        [SerializeField] private TextMeshProUGUI m_txtExample;
        
        private void Start()
        {
            m_txtCurrentLanguage.text = Localization.currentLanguage;
            m_btnChangeLanguage.onClick.AddListener(() =>
            {
                int nextLangIndex = (Localization.curLangIndex + 1) % Localization.languageDict.Count;
                Localization.currentLanguage = Localization.languageDict.Keys.ToArray()[nextLangIndex];
                m_txtCurrentLanguage.text = Localization.currentLanguage;
            });
        }
    }
}
