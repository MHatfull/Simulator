
using Underlunchers.Scene;
using Underlunchers.Stories;
using UnityEngine;
using UnityEngine.Networking;

namespace Underlunchers.Networking
{
	public class StoryLoader : NetworkBehaviour
	{
		private static string _loaded = null;

		public override void OnStartLocalPlayer()
		{
			base.OnStartLocalPlayer();
			Debug.Log("connected!");
			CmdLoadOnServer(StorySelector.CurrentStory);
		}

		[Command]
		private void CmdLoadOnServer(string story)
		{
			Debug.Log("client wants to load " + story + ", current story is " + _loaded);
			if (!string.IsNullOrEmpty(_loaded))
			{
				Debug.Log("story already running, going to load that into the client instead");
				TargetLoadStory(connectionToClient, _loaded);
			}
			else
			{
				_loaded = story;
				FindObjectOfType<ChunkedTerrain>().DownloadStory(story);
				TargetLoadStory(connectionToClient, story);
			}
			Debug.Log("Loaded is now " + _loaded);
		}

		[TargetRpc]
	 	void TargetLoadStory(NetworkConnection connection, string story)
		{
			FindObjectOfType<ChunkedTerrain>().DownloadStory(story);
		}
	}
}