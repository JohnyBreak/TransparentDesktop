using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _Image;
    [SerializeField] private AudioSource _AudioSource;

    private bool _hovered;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hovered = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        _hovered = false;
    }

    private void Update()
    {
        if (_hovered == false) 
        {
            return;
        }
    
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            _AudioSource.PlayOneShot(_AudioSource.clip);
        }
    }
}
