using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
public class JSONFetch : MonoBehaviour
{

    public string apiUrl = "https://api.jsonbin.io/v3/b/6686a992e41b4d34e40d06fa";
    public apiResponse jsonInfo;

    public TMP_Text playerName;
    public TMP_Text playerLevel;
    public TMP_Text playerHealth;
    public TMP_Text playerPosition;
    public TMP_Text Inventory;
    
    void Start()
    {
        StartCoroutine(FetchJSON());
    }

    IEnumerator FetchJSON()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        yield return request.SendWebRequest();

        if(request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);        }
        else
        {
            string jsonText = request.downloadHandler.text;
            jsonInfo = JsonUtility.FromJson<apiResponse>(jsonText);
            ShowData();
        }
    }
     void ShowData()
    {
        playerName.text = $"Player Name: {jsonInfo.record.playerName}";
        playerLevel.text = $"Level: {jsonInfo.record.level}";
        playerHealth.text = $"Health: { jsonInfo.record.health}";
        playerPosition.text = $"Position: ({jsonInfo.record.position.x}, {jsonInfo.record.position.y}, {jsonInfo.record.position.z})";

        Inventory.text = "Inventory:\n";
        foreach (var item in jsonInfo.record.inventory)
        {
            Inventory.text += $"{item.itemName} - Quantity: {item.quantity}, Weight: {item.weight}\n";
        }
        
    }
}
