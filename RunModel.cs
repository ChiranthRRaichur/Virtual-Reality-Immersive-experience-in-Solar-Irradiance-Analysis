using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RunModel : MonoBehaviour
{
    public Button runButton;
    public Text statusText;
    public Text resultText;

    void Start()
    {
        runButton.onClick.AddListener(OnRunButtonClicked);
    }

    void OnRunButtonClicked()
    {
        StartCoroutine(RunModelCoroutine());
    }

    IEnumerator RunModelCoroutine()
    {
        statusText.text = "Running...";
        resultText.text = "";

        // Start the animation coroutine
        Coroutine animateCoroutine = StartCoroutine(AnimateResultText());

        UnityWebRequest www = UnityWebRequest.PostWwwForm("http://127.0.0.1:5000/run_model", "");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            statusText.text = "Error";
            Debug.LogError(www.error);
        }
        else
        {
            statusText.text = "Completed";
            var result = www.downloadHandler.text;
            resultText.text = result;
            Debug.Log(result);
        }

        // Stop the animation coroutine once the result is received
        StopCoroutine(animateCoroutine);
    }

    IEnumerator AnimateResultText()
    {
        while (true)
        {
            resultText.text = Random.Range(0, 100).ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
}