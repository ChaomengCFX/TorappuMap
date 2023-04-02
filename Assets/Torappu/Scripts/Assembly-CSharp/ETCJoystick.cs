using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ETCJoystick : ETCBase
{
	[Serializable]
	public class OnMoveStartHandler : UnityEvent
	{
	}

	[Serializable]
	public class OnMoveSpeedHandler : UnityEvent<Vector2>
	{
	}

	[Serializable]
	public class OnMoveHandler : UnityEvent<Vector2>
	{
	}

	[Serializable]
	public class OnMoveEndHandler : UnityEvent
	{
	}

	[Serializable]
	public class OnTouchStartHandler : UnityEvent
	{
	}

	[Serializable]
	public class OnTouchUpHandler : UnityEvent
	{
	}

	[Serializable]
	public class OnDownUpHandler : UnityEvent
	{
	}

	[Serializable]
	public class OnDownDownHandler : UnityEvent
	{
	}

	[Serializable]
	public class OnDownLeftHandler : UnityEvent
	{
	}

	[Serializable]
	public class OnDownRightHandler : UnityEvent
	{
	}

	public enum JoystickArea
	{
		UserDefined = 0,
		FullScreen = 1,
		Left = 2,
		Right = 3,
		Top = 4,
		Bottom = 5,
		TopLeft = 6,
		TopRight = 7,
		BottomLeft = 8,
		BottomRight = 9
	}

	public enum JoystickType
	{
		Dynamic = 0,
		Static = 1
	}

	public enum RadiusBase
	{
		Width = 0,
		Height = 1,
		UserDefined = 2
	}

	[SerializeField]
	public OnMoveStartHandler onMoveStart;

	[SerializeField]
	public OnMoveHandler onMove;

	[SerializeField]
	public OnMoveSpeedHandler onMoveSpeed;

	[SerializeField]
	public OnMoveEndHandler onMoveEnd;

	[SerializeField]
	public OnTouchStartHandler onTouchStart;

	[SerializeField]
	public OnTouchUpHandler onTouchUp;

	[SerializeField]
	public OnDownUpHandler OnDownUp;

	[SerializeField]
	public OnDownDownHandler OnDownDown;

	[SerializeField]
	public OnDownLeftHandler OnDownLeft;

	[SerializeField]
	public OnDownRightHandler OnDownRight;

	[SerializeField]
	public OnDownUpHandler OnPressUp;

	[SerializeField]
	public OnDownDownHandler OnPressDown;

	[SerializeField]
	public OnDownLeftHandler OnPressLeft;

	[SerializeField]
	public OnDownRightHandler OnPressRight;

	public JoystickType joystickType;

	public bool allowJoystickOverTouchPad;

	public RadiusBase radiusBase;

	public float radiusBaseValue;

	public ETCAxis axisX;

	public ETCAxis axisY;

	public RectTransform thumb;

	public JoystickArea joystickArea;

	public RectTransform userArea;

	public bool isTurnAndMove;

	public float tmSpeed;

	public float tmAdditionnalRotation;

	public bool tmLockInJump;

	[SerializeField]
	private bool isNoReturnThumb;

	[SerializeField]
	private bool isNoOffsetThumb;
}
