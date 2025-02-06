using UnityEngine;
using UnityEngine.UI;

public class MenuButtonObject : MonoBehaviour
{
    public MenuKey MenuKey;
    MenuData MenuDataObject;

    public void HandleClick()
    {
        MenuDataObject.AddMenu(MenuKey);
    }

    private void OnEnable()
    {
        MenuDataObject = GameObject.FindGameObjectWithTag("MenuData").GetComponent<MenuData>();
        gameObject.gameObject.GetComponent<Button>().onClick.AddListener(HandleClick);
    }


    private void OnDisable()
    {
        gameObject.gameObject.GetComponent<Button>().onClick.RemoveListener(HandleClick);
    }

}
