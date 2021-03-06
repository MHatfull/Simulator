﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Underlunchers.Utils;
using UnityEngine;
using UnityEngine.Networking;
using static System.IO.File;

namespace Underlunchers.Networking
{
    public class TerrainUploader : MonoBehaviour
    {
        [SerializeField] private Hosts _hosts;
        private string SigningUrl => _hosts.StoryManagement + "/sign";

        public void SaveByes(byte[] byteArray, string location, string storyName)
        {
            CreateStoryDirectory(storyName);
            WriteAllBytes(BaseStoryPath(storyName) + "/" + location, byteArray);
        }

        private void CreateStoryDirectory(string storyName)
        {
            if (!System.IO.Directory.Exists(BaseStoryPath(storyName)))
            {
                System.IO.Directory.CreateDirectory(BaseStoryPath(storyName));
            }
        }

        public void SaveString(string content, string location, string storyName)
        {
            CreateStoryDirectory(storyName);
            WriteAllText(Application.persistentDataPath + "/" + storyName + "/" + location, content);
        }

        public void Upload(string storyName)
        {
            string archiveName = BaseStoryPath(storyName) + ".story";
            if (Exists(archiveName))
            {
                Delete(archiveName);
            }

            Compressor.CompressDirectory(BaseStoryPath(storyName), archiveName);

            StartCoroutine(UploadStory(storyName));
        }


        private static string BaseStoryPath(string name)
        {
            return Application.persistentDataPath + "/" + name;
        }

        private IEnumerator UploadStory(string storyName)
        {
            string form = "{\"name\":\"" + storyName + ".story\"}";
            UnityWebRequest signingRequest = new UnityWebRequest(SigningUrl, "POST")
            {
                uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(form)),
                downloadHandler = new DownloadHandlerBuffer()
            };
            signingRequest.SetRequestHeader("Content-Type", "application/json");
            yield return signingRequest.SendWebRequest();
            if (signingRequest.isNetworkError || signingRequest.isHttpError)
            {
                Debug.Log("error signing url " + signingRequest.error);
            }

            Debug.Log("post complete: " + signingRequest.downloadHandler.text);
            Debug.Log(signingRequest.downloadHandler.text);
            string uploadUrl = signingRequest.downloadHandler.text;

            var multipartForm = new List<IMultipartFormSection>
            {
                new MultipartFormFileSection("file", ReadAllBytes(BaseStoryPath(storyName) + ".story"),
                    "terrain.terrain", "application/octet-stream")
            };
            byte[] boundary = UnityWebRequest.GenerateBoundary();
            byte[] formSections = UnityWebRequest.SerializeFormSections(multipartForm, boundary);
            ;
            byte[] terminate = Encoding.UTF8.GetBytes(String.Concat("\r\n--", Encoding.UTF8.GetString(boundary), "--"));
            byte[] body = new byte[formSections.Length + terminate.Length];
            Buffer.BlockCopy(formSections, 0, body, 0, formSections.Length);
            Buffer.BlockCopy(terminate, 0, body, formSections.Length, terminate.Length);
            string contentType = String.Concat("multipart/form-data; boundary=", Encoding.UTF8.GetString(boundary));
            UnityWebRequest wr = new UnityWebRequest(uploadUrl, "PUT");
            UploadHandler uploader = new UploadHandlerRaw(body);
            uploader.contentType = contentType;
            wr.uploadHandler = uploader;
            wr.SetRequestHeader("Content-Type", "application/octet-stream");
            yield return wr.SendWebRequest();
            if (wr.isNetworkError || wr.isHttpError)
            {
                Debug.Log("error uploading: " + wr.error);
            }

            Debug.Log("upload complete");
        }
    }
}