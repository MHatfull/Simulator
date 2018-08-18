using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Underlunchers.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace Underlunchers.Networking
{
	public class StoryDownloader : MonoBehaviour
	{
		[SerializeField] private Hosts _hosts;
		public void DownloadStory(string story, Action callback)
		{
			StartCoroutine(DownloadStoryRoutine(story, callback));
		}

		private IEnumerator DownloadStoryRoutine(string story, Action callback)
		{
			if (File.Exists(CompressedStoryPath(story)))
			{
				File.Delete(CompressedStoryPath(story));
			}
			UnityWebRequest www = UnityWebRequest.Get(_hosts.StoryBucket + "/" + story + ".story");
			yield return www.SendWebRequest();
			if(www.isNetworkError || www.isHttpError) {
				Debug.Log(www.error);
				Debug.Log(www.url);
			}
			else
			{
				File.WriteAllBytes(CompressedStoryPath(story), www.downloadHandler.data);
				Compressor.DecompressToDirectory(CompressedStoryPath(story));
				callback();
			}
		}

		private static string CompressedStoryPath(string story)
		{
			return Application.persistentDataPath + "/" + story + ".story";
		}
	}
}