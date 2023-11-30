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
        [SerializeField] private TextMeshProUGUI m_txtExample2;
        [SerializeField] private GameObject m_textGameObject1;
        [SerializeField] private GameObject m_textGameObject2;

#region Separated Localization Example

        private IEnumerator Start()
        {
            yield return LocalizationsManager.InitAsync(null);

            // Change language
            LocalizationsManager.currentLanguage = "spanish";

            m_btnChangeLanguage.onClick.AddListener(() =>
            {
                int nextLangIndex = (ExampleLocalization2.curLangIndex + 1) % ExampleLocalization2.languageDict.Count;
                LocalizationsManager.currentLanguage = ExampleLocalization2.languageDict.Keys.ToArray()[nextLangIndex];
            });

            // Register an action when language changed
            LocalizationsManager.onLanguageChanged += OnLanguageChanged;

            // Register a Dynamic localized Text in sheet ExampleLocalization
            ExampleLocalization.RegisterDynamicText(m_textGameObject1, ExampleLocalization.hero_name_5);
            // Register a Dynamic localized Text in sheet ExampleLocalization2
            ExampleLocalization2.RegisterDynamicText(m_textGameObject2, "TAP_TO_COLLECT");

            OnLanguageChanged();
        }

        private void OnLanguageChanged()
        {
            // Display current language
            m_txtCurrentLanguage.text = LocalizationsManager.currentLanguage;

            // Get localized string from sheet ExampleLocalization
            m_txtExample1.text = ExampleLocalization.Get(ExampleLocalization.hero_name_1).ToString();
            // Get localized string from sheet ExampleLocalization2
            m_txtExample2.text = ExampleLocalization2.Get("DAY_X", 100).ToString();
        }

        private void OnDestroy()
        {
            ExampleLocalization.UnregisterDynamicText(m_textGameObject1);
            ExampleLocalization2.UnregisterDynamicText(m_textGameObject2);
        }

#endregion

#region Non-Separated Localization Example

        // private void Start()
        // {
        //     // Change language
        //     Localization.currentLanguage = "spanish";
        //     
        //     m_btnChangeLanguage.onClick.AddListener(() =>
        //     {
        //         int nextLangIndex = (Localization.curLangIndex + 1) % Localization.languageDict.Count;
        //         Localization.currentLanguage = Localization.languageDict.Keys.ToArray()[nextLangIndex];
        //     });
        //     
        //     // Register an action when language changed
        //     Localization.onLanguageChanged += OnLanguageChanged;
        //
        //     // Register a Dynamic localized Text
        //     Localization.RegisterDynamicText(m_textGameObject1, Localization.hero_name_5);
        //     // Register a Dynamic localized Text
        //     Localization.RegisterDynamicText(m_textGameObject2, "TAP_TO_COLLECT");
        //     
        //     OnLanguageChanged();
        // }
        //
        // private void OnLanguageChanged()
        // {
        //     // Display current language
        //     m_txtCurrentLanguage.text = Localization.currentLanguage;
        //     
        //     // Get localized string using integer key
        //     m_txtExample1.text = Localization.Get(Localization.hero_name_1).ToString();
        //     // Get localized string using string key
        //     m_txtExample2.text = Localization.Get("DAY_X", 100).ToString();
        // }
        //
        // private void OnDestroy()
        // {
        //     Localization.UnregisterDynamicText(m_textGameObject1);
        //     Localization.UnregisterDynamicText(m_textGameObject2);
        // }

#endregion
    }
}