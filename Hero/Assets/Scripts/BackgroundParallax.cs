﻿using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour
{
	public Transform[] backgrounds;				//背景层列表
	public float parallaxScale;					//视差范围 The proportion of the camera's movement to move the backgrounds by.
	public float parallaxReductionFactor;		//视差递减系数 How much less each successive layer should parallax.
	public float smoothing;						//平滑度 How smooth the parallax effect should be.


	private Transform cam;						//主相机
	private Vector3 previousCamPos;				//主相机坐标


	void Awake ()
	{
		cam = Camera.main.transform;
	}


	void Start ()
	{
		// The 'previous frame' had the current frame's camera position.
		previousCamPos = cam.position;
	}


	void Update ()
	{
		// The parallax is the opposite of the camera movement since the previous frame multiplied by the scale.
		float parallax = (previousCamPos.x - cam.position.x) * parallaxScale;

		// For each successive background...
		for(int i = 0; i < backgrounds.Length; i++)
		{
			// ... set a target x position which is their current position plus the parallax multiplied by the reduction.
			float backgroundTargetPosX = backgrounds[i].position.x + parallax * (i * parallaxReductionFactor + 1);

			// Create a target position which is the background's current position but with it's target x position.
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// Lerp the background's position between itself and it's target position.
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

		// Set the previousCamPos to the camera's position at the end of this frame.
		previousCamPos = cam.position;
	}
}
