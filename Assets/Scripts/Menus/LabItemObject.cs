using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LabItemObject : MonoBehaviour
{
    public LabItem ItemName;

    XRBaseInteractable Interactable;
    HandleIdentifyMenu IdentifyMenu;

    private void OnSelectEntered(SelectEnterEventArgs arg)
    {

        IdentifyMenu.HandleSelect(ItemName);
    }

    private void OnEnable()
    {
        Interactable = GetComponent<XRBaseInteractable>();
        IdentifyMenu = Resources.FindObjectsOfTypeAll<HandleIdentifyMenu>().FirstOrDefault();

        Interactable.selectEntered.AddListener(OnSelectEntered);
    }


    private void OnDisable()
    {
        Interactable.selectEntered.RemoveListener(OnSelectEntered);
    }


}
