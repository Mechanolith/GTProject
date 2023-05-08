using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private readonly string apiAddress = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

    //[Testing]
    //Todo: For inspecting. Delete later.
    [SerializeField] private List<BlockData> blocks;

    void Awake()
    {
        FetchData();
    }

    void FetchData()
    {
        //Create an object for the fetch because coroutines only run on monos which have to be in the scene.
        GameObject fetcherObect = new GameObject();
        fetcherObect.transform.name = "Data Fetcher";
        DataFetcher dataFetcher = fetcherObect.AddComponent<DataFetcher>();
        dataFetcher.FetchBlocks(apiAddress, InitialiseTowers);
    }

    void InitialiseTowers(List<BlockData> _blocks)
    {
        blocks = _blocks;
        Debug.Log($"[Bootstrap] Initialising towers with {_blocks.Count} blocks.");
    }
}
