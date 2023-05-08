using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class DataFetcher : MonoBehaviour
{
    //[Data] [Coded Quality]
    //Todo: Is a failure callback required? Double check one there's a clearer idea of how failure should be resolved (or ignored).
    public void FetchBlocks(string _apiAddress, Action<List<BlockData>> _onComplete)
    {
        StartCoroutine(FetchBlocksInternal(_apiAddress, _onComplete));
    }

    IEnumerator FetchBlocksInternal(string _apiAddress, Action<List<BlockData>> _onComplete)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_apiAddress))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log($"[DataFetcher] Error downloading data: {webRequest.error}");
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log($"[DataFetcher] Successfully downloaded data!");

                    //If successful, convert the JSON to actual objects and return them.
                    _onComplete(ProcessIncomingData(webRequest.downloadHandler.text));
                    break;
            }
        }
    }

    List<BlockData> ProcessIncomingData(string _data)
    {
        //Read the data out of the JSON.
        List<IncomingBlockData> incomingBlocks = JsonConvert.DeserializeObject<List<IncomingBlockData>>(_data);

        List<BlockData> outputBlocks = new List<BlockData>();

        //Convert that data to a new class with better variable naming.
        foreach (var block in incomingBlocks)
        {
            outputBlocks.Add(new BlockData(block));
        }

        return outputBlocks;
    }
}