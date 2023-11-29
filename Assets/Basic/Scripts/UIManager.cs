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
        [SerializeField] private TextMeshProUGUI m_txtExample1;
        [SerializeField] private GameObject m_txtExample2;

        /*
		private void Start()
        {
            m_btnChangeLanguage.onClick.AddListener(() =>
            {
                int nextLangIndex = (Localization.curLangIndex + 1) % Localization.languageDict.Count;
                Localization.currentLanguage = Localization.languageDict.Keys.ToArray()[nextLangIndex];
            });
            
            Localization.onLanguageChanged += () =>
            {
                m_txtCurrentLanguage.text = Localization.currentLanguage;
                m_txtExample1.text = Localization.Get(Localization.hero_name_1).ToString();
            };
            
            m_txtCurrentLanguage.text = Localization.currentLanguage;
            
            m_txtExample1.text = Localization.Get(Localization.hero_name_1).ToString();
            m_txtExample1.text = Localization.Get("hero_name_" + IDs.HERO_1).ToString();
            
            Localization.RegisterDynamicText(m_txtExample2, Localization.hero_name_5);
            Localization.RegisterDynamicText(m_txtExample2, "hero_name_" + IDs.HERO_5);
        }
        */
        
        private IEnumerator Start()
        {
            yield return LocalizationsManager.InitAsync(null);
            
            LocalizationsManager.currentLanguage = "spanish";
            
            m_btnChangeLanguage.onClick.AddListener(() =>
            {
                int nextLangIndex = (ExampleLocalization.curLangIndex + 1) % ExampleLocalization.languageDict.Count;
                ExampleLocalization.currentLanguage = ExampleLocalization.languageDict.Keys.ToArray()[nextLangIndex];
            });
            
            ExampleLocalization.onLanguageChanged += () =>
            {
                m_txtCurrentLanguage.text = ExampleLocalization.currentLanguage;
                m_txtExample1.text = ExampleLocalization.Get(ExampleLocalization.hero_name_1).ToString();
            };
            
            m_txtCurrentLanguage.text = ExampleLocalization.currentLanguage;
            
            m_txtExample1.text = ExampleLocalization.Get(ExampleLocalization.hero_name_1).ToString();
            m_txtExample1.text = ExampleLocalization.Get("hero_name_" + IDs.HERO_1).ToString();
            
            ExampleLocalization.RegisterDynamicText(m_txtExample2, ExampleLocalization.hero_name_5);
            ExampleLocalization.RegisterDynamicText(m_txtExample2, "hero_name_" + IDs.HERO_5);
        }
    }
}
