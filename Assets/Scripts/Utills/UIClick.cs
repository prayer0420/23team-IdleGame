using UnityEngine;
using UnityEngine.EventSystems;

public class UIClick : MonoBehaviour, IPointerClickHandler
{
    private string uiClickSFXPath = "Audio/SFX/Click";
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(uiClickSFXPath);
    }

}
