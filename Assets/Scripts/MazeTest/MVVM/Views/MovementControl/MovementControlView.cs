using UnityEngine;
using UnityEngine.EventSystems;
using UnityMVVM;

namespace MazeTest.MVVM.Views.MovementControl
{
    public class MovementControlView : ViewBehaviour<IMovementControlViewModel>, 
        IPointerDownHandler, 
        IPointerMoveHandler,
        IPointerUpHandler
    {

        [SerializeField] private RectTransform _stickForeground;
        [SerializeField] private RectTransform _stickBackground;

        private bool _pressed;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
            _stickForeground.gameObject.SetActive(true);
            _stickBackground.gameObject.SetActive(true);
            _stickBackground.position = eventData.position;
            _stickForeground.position = eventData.position;
            ViewModel?.SetDownPosition(eventData.position);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (_pressed)
            {
                _stickForeground.position = eventData.position;
                ViewModel?.SetPressPosition(eventData.position);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
            _stickForeground.gameObject.SetActive(false);
            _stickBackground.gameObject.SetActive(false);
            ViewModel?.OnUp();
        }
    }
}