using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Member.CHG._02.Scripts.SettingUI
{
    public class HandlerEvent : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private SoundValueChangedAnim _anim;
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Click");
            _anim.OnPointerDown();
        }
    }
}