////////////////////////////////////////////////////////////
/////   UIButtonInteraction.cs
/////   James McNeil - 2020
////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonInteraction : MonoBehaviour
{
    public string message = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        GlobalDirector.HandleMessage(message);
    }
}
