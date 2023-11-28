namespace Excel2Unity.Basic
{
	/***
	 * Author RadBear - nbhung71711@gmail.com - 2017-2023
	 ***/
	
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using RCore.Common;
	
	public class LocalizationsManager
	{
	    public static Action onLanguageChanged;
	
		public static readonly List<string> languages = new List<string>() { "english", "spanish", "japan", "chinese", "korean", "thai", };
		public static readonly string defaultLanguage = "english";
		
	    public static string currentLanguage
	    {
	        get { return PlayerPrefs.GetString("currentLanguage", defaultLanguage); }
	        set
	        {
	            if (currentLanguage != value && languages.Contains(value))
	            {
	                PlayerPrefs.SetString("currentLanguage", value);
	                Init("");
	            }
	        }
	    }
	
	    public static void Init(string pLang)
	    {
			if (languages.Contains(pLang))
				PlayerPrefs.SetString("currentLanguage", pLang);
			ExampleLocalization.Init();
			ExampleLocalization2.Init();
	        onLanguageChanged?.Invoke();
	    }
	
	    public static IEnumerator InitAsync(string pLang)
	    {
			if (languages.Contains(pLang))
				PlayerPrefs.SetString("currentLanguage", pLang);
			yield return ExampleLocalization.InitAsync();
			yield return ExampleLocalization2.InitAsync();
	        onLanguageChanged?.Invoke();
	    }
		
		public static void SetFolder(string pFolder)
		{
			ExampleLocalization.Folder = pFolder;
			ExampleLocalization2.Folder = pFolder;
		}
	
	    public static string GetSystemLanguage()
		{
			if (PlayerPrefs.HasKey("currentLanguage"))
				return PlayerPrefs.GetString("currentLanguage");
	
			var curLang = Application.systemLanguage;
			return curLang switch
			{
				SystemLanguage.English => "english",
				SystemLanguage.Spanish => "spanish",
				SystemLanguage.Japanese => "japan",
				SystemLanguage.ChineseSimplified => "chinese",
				SystemLanguage.Korean => "korean",
				SystemLanguage.Thai => "thai",
				_ => "english",
	
			};
		}
	}
	
}
