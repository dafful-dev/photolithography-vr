using System.Linq;
using UnityEngine;

public class HandleStepBase : MonoBehaviour
{
    StepObject[] StepObjects;
    HandleLearningMenu _HandleLearningMenu;
    MenuData MenuDataObject;
    protected AudioManagerUtil AudioManager;

    private void Awake()
    {
        StepObjects = gameObject.GetComponents<StepObject>();
        _HandleLearningMenu = FindFirstObjectByType<HandleLearningMenu>(FindObjectsInactive.Include);
        MenuDataObject = GameObject.FindGameObjectWithTag("MenuData").GetComponent<MenuData>();
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerUtil>();
    }

    public bool CanPerformStep()
    {
        return MenuDataObject.GetActiveMenu() == MenuKey.Learning
            && StepObjects.Any(stepObject => (
            stepObject.StepIndex == _HandleLearningMenu.StepIndex
            && stepObject.StageIndex == _HandleLearningMenu.StageIndex));
        //return MenuDataObject.GetActiveMenu() == MenuKey.Learning
        //    && _StepObject.StageIndex == _HandleLearningMenu.StageIndex
        //    && _StepObject.StepIndex == _HandleLearningMenu.StepIndex;
    }

    public void HandleCompleteStep()
    {
        _HandleLearningMenu.CompleteStep();
        AudioManager.PlayClip(AudioManager.SuccessClip);
    }
}
