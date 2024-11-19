using UnityEngine;
using UnityEngine.EventSystems;

public class UIClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        //SFX
        AudioManager.Instance.PlaySFX(AudioManager.Instance.uiClickClip);
        //TODO: VFX
    }
    
}
