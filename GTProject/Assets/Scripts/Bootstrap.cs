using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private readonly string apiAddress = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

    private LoadingScreen loadingScreen;

    void Awake()
    {
        loadingScreen = FindObjectOfType<LoadingScreen>();
        loadingScreen.Show();

        BlockMaterials.Initialise();
        FetchData();
    }

    void FetchData()
    {
        //Create an object for the fetch because coroutines only run on monos which have to be in the scene.
        //[Data] [Code Quality]
        //Todo: Investigate if a static class with async would work more cleanly here than a mono with a coroutine.
        GameObject fetcherObject = new GameObject();
        fetcherObject.transform.name = "Data Fetcher";

        DataFetcher dataFetcher = fetcherObject.AddComponent<DataFetcher>();
        dataFetcher.FetchBlocks(apiAddress, (_blockData) => 
        {
            InitialiseStacks(_blockData);
            loadingScreen.Hide();
            Destroy(fetcherObject);
        });
    }

    void InitialiseStacks(List<BlockData> _blockData)
    {
        FindObjectOfType<StackController>().InitialiseStacks(_blockData);
    }
}
