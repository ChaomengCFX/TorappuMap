using System;
using UnityEngine;

[Serializable]
public abstract class ETCBase : MonoBehaviour
{
	public enum RectAnchor
	{
		UserDefined = 0,
		BottomLeft = 1,
		BottomCenter = 2,
		BottonRight = 3,
		CenterLeft = 4,
		Center = 5,
		CenterRight = 6,
		TopLeft = 7,
		TopCenter = 8,
		TopRight = 9
	}

	public enum DPadAxis
	{
		Two_Axis = 0,
		Four_Axis = 1
	}

	public enum CameraMode
	{
		Follow = 0,
		SmoothFollow = 1
	}

	public enum CameraTargetMode
	{
		UserDefined = 0,
		LinkOnTag = 1,
		FromDirectActionAxisX = 2,
		FromDirectActionAxisY = 3
	}

	public bool isUnregisterAtDisable;

	[SerializeField]
	protected RectAnchor _anchor;

	[SerializeField]
	protected Vector2 _anchorOffet;

	[SerializeField]
	protected bool _visible;

	[SerializeField]
	protected bool _activated;

	public bool enableCamera;

	public CameraMode cameraMode;

	public string camTargetTag;

	public bool autoLinkTagCam;

	public string autoCamTag;

	public Transform cameraTransform;

	public CameraTargetMode cameraTargetMode;

	public bool enableWallDetection;

	public LayerMask wallLayer;

	public Transform cameraLookAt;

	public Vector3 followOffset;

	public float followDistance;

	public float followHeight;

	public float followRotationDamping;

	public float followHeightDamping;

	public int pointId;

	public bool enableKeySimulation;

	public bool allowSimulationStandalone;

	public bool visibleOnStandalone;

	public DPadAxis dPadAxisCount;

	public bool useFixedUpdate;

	public bool isOnDrag;

	public bool isSwipeIn;

	public bool isSwipeOut;

	public bool showPSInspector;

	public bool showSpriteInspector;

	public bool showEventInspector;

	public bool showBehaviourInspector;

	public bool showAxesInspector;

	public bool showTouchEventInspector;

	public bool showDownEventInspector;

	public bool showPressEventInspector;

	public bool showCameraInspector;
}
