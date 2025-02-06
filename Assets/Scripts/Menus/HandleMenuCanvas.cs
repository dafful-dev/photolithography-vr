using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class HandleMenuCanvas : MonoBehaviour
{
    public TextMeshProUGUI TitleText;
    public Button BackButton;
    public Button ExitButton;
    public Button HelpButton;


    MenuData MenuDataObject;
    List<MenuObject> MenuObjects;

    GameManagerUtil GameManager;

    private void Awake()
    {
        MenuDataObject = GameObject.FindGameObjectWithTag("MenuData").GetComponent<MenuData>();
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerUtil>();
        MenuObjects = Resources.FindObjectsOfTypeAll<MenuObject>().ToList();
    }


    private void OnEnable()
    {
        MenuData.OnMenuChange += RenderMenu;
        BackButton.onClick.AddListener(MenuDataObject.RemoveMenu);
        ExitButton.onClick.AddListener(GameManager.HandleExit);
        HelpButton.onClick.AddListener(() => MenuDataObject.AddMenu(MenuKey.Help));

    }

    private void OnDisable()
    {
        MenuData.OnMenuChange -= RenderMenu;
        BackButton.onClick.RemoveListener(MenuDataObject.RemoveMenu);
        ExitButton.onClick.RemoveListener(GameManager.HandleExit);
        HelpButton.onClick.RemoveAllListeners();
    }



    public void RenderMenu()
    {
        var currentMenu = MenuDataObject.GetActiveMenu();

        TitleText.text = currentMenu.ToString();


        MenuObjects.ForEach(menuObject =>
        {
            if (menuObject.MenuName == currentMenu)
                menuObject.gameObject.SetActive(true);
            else
                menuObject.gameObject.SetActive(false);
        });
    }
}
