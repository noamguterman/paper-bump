using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IEventSystemHandler, IPointerUpHandler, IPointerDownHandler
{
	public Image backgroundImg;

	public Image joystickImg;

    //public Image arrow;

	public Vector2 InputDirection
	{
		get;
		private set;
	}

	private void Start()
	{
		InputDirection = Vector3.zero;
	}

	public virtual void OnDrag(PointerEventData eventData)
	{
		Vector2 localPoint = Vector2.zero;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImg.rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
		{
            localPoint += backgroundImg.rectTransform.sizeDelta / 2;

            localPoint.x /= backgroundImg.rectTransform.sizeDelta.x;
			localPoint.y /= backgroundImg.rectTransform.sizeDelta.y;
			float x = (backgroundImg.rectTransform.pivot.x == 1f) ? (localPoint.x * 2f + 1f) : (localPoint.x * 2f - 1f);
			float y = (backgroundImg.rectTransform.pivot.y == 1f) ? (localPoint.y * 2f + 1f) : (localPoint.y * 2f - 1f);
			InputDirection = new Vector3(x, y);
			InputDirection = ((InputDirection.magnitude > 1f) ? InputDirection.normalized : InputDirection);

            //float angle = Mathf.Rad2Deg * Mathf.Atan(InputDirection.y / InputDirection.x);
            //if (InputDirection.x < 0 && InputDirection.y < 0)
            //    angle -= 180;
            //else if (InputDirection.x < 0 && InputDirection.y > 0)
            //    angle += 180;
            //arrow.rectTransform.localEulerAngles = new Vector3(0,0, angle);


            joystickImg.rectTransform.anchoredPosition = new Vector2(InputDirection.x * (backgroundImg.rectTransform.sizeDelta.x / 3f), InputDirection.y * (backgroundImg.rectTransform.sizeDelta.y / 3f));
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
        if (Input.GetMouseButtonDown(0))
        {
            backgroundImg.transform.position = Input.mousePosition;
            InputDirection = Vector2.zero;
            joystickImg.rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            OnDrag(eventData);
        }
    }

	public void OnPointerUp(PointerEventData eventData)
	{
		InputDirection = Vector2.zero;
		joystickImg.rectTransform.anchoredPosition = Vector2.zero;
	}
}
