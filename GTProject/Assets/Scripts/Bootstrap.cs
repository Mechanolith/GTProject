using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private readonly string apiAddress = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

    void Awake()
    {
        FetchData();
    }

    void FetchData()
    {
        //Create an object for the fetch because coroutines only run on monos which have to be in the scene.
        //[Data] [Code Quality]
        //Todo: Investigate if a static class with async would work more cleanly here than a mono with a coroutine.
        GameObject fetcherObect = new GameObject();
        fetcherObect.transform.name = "Data Fetcher";
        DataFetcher dataFetcher = fetcherObect.AddComponent<DataFetcher>();
        dataFetcher.FetchBlocks(apiAddress, InitialiseStacks);
    }

    void InitialiseStacks(List<BlockData> _blockData)
    {
        FindObjectOfType<StackController>().InitialiseStacks(_blockData);
    }
}
