using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Underlunchers.Networking;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class StoriesDropdown : MonoBehaviour
{
	[SerializeField] private Hosts _hosts;

	private void Start()
	{
		StartCoroutine(GetStories());
	}

	private IEnumerator GetStories()
	{
		UnityWebRequest www = UnityWebRequest.Get(_hosts.StoryManagement + "/stories");
		yield return www.SendWebRequest();
		
		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log(www.error);
		}
		else
		{
			Debug.Log(www.downloadHandler.text);
			StoryList list = JsonUtility.FromJson<StoryList>(www.downloadHandler.text);
			PopulateDropdown(list.Stories);
		}

	}

	private void PopulateDropdown(IEnumerable<string> stories)
	{
		Dropdown dropdown = GetComponent<Dropdown>();
		dropdown.ClearOptions();
		dropdown.AddOptions(stories.ToList());
	}

	[Serializable]
	private class StoryList
	{
		[SerializeField] private string[] stories;
		public IEnumerable<string> Stories => stories;
	}
}
