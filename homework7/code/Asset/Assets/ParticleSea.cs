﻿using UnityEngine;
using System.Collections;

public class particleSettings{
	public float angle { get; set; }
	public float radius{ get; set;}
	public float speed { get; set; }
	public float time=0f;
	public particleSettings(float r) {
		this.radius = r;
		this.angle = Random.value * 2 * Mathf.PI;
		this.speed = Random.value * Mathf.Sqrt(radius);
		this.time=r;
	}
	public Vector3 getPosition() {
		return radius * new Vector3 (Mathf.Cos(angle), 0, Mathf.Sin(angle));
	}
	public void rotate() {
		this.angle += Time.deltaTime * speed / 10;
		if (this.angle > 2 * Mathf.PI)
			this.angle -= 2 * Mathf.PI;
		this.radius += Random.value * 0.2f - 0.1f;
		if (this.radius > ParticleSea.MaxRadius)
			this.radius = ParticleSea.MaxRadius;
		if (this.radius < ParticleSea.MinRadius)
			this.radius = ParticleSea.MinRadius;
	}
}

public class ParticleSea : MonoBehaviour {

	public ParticleSystem particleSystem;
	private ParticleSystem.Particle[] particlesArray;
	private particleSettings[] psetting;
	public int seaResolution = 250;
	public static float MaxRadius = 120f;
	public static float MinRadius = 50f;
	public float radius = 100.0f;
	public Gradient colorGradient;
	public float pingPong = 0.02f;  // 游离范围 


	/*public class CirclePosition
	{
		public float radius =0f,angle=0f,time=0f;
		public CirclePosition(float radius,float angle,float time){
			this.radius=radius;
			this.angle=angle;
			this.time=time;
		}
	}*/

	void Start() {
		particlesArray = new ParticleSystem.Particle[seaResolution * seaResolution];
		psetting = new particleSettings[seaResolution * seaResolution];
		particleSystem.maxParticles = seaResolution * seaResolution;
		particleSystem.Emit(seaResolution * seaResolution);
		particleSystem.GetParticles(particlesArray);
		setInitialPosition ();

	}
	void Update() {
		RotateParticles ();
		changeColor ();
		particleSystem.SetParticles(particlesArray, particlesArray.Length);
		for (int i = 0; i < seaResolution * seaResolution; i++) {
			psetting [i].time += Time.deltaTime;  
			psetting [i].radius += Mathf.PingPong (psetting [i].time / MinRadius / MaxRadius, pingPong) - pingPong / 2.0f; 
		}
	}

	void setInitialPosition () {
		for (int i = 0; i < seaResolution; i++) {
			for (int j = 0; j < seaResolution; j++) {
				psetting[i * seaResolution + j] = new particleSettings(radius);
				particlesArray [i * seaResolution + j].position = psetting[i * seaResolution + j].getPosition();
			}
		}
		particleSystem.SetParticles(particlesArray, particlesArray.Length);
	}
	void RotateParticles () {
		for (int i = 0; i < seaResolution; i++) {
			for (int j = 0; j < seaResolution; j++) {
				psetting [i * seaResolution + j].rotate ();
				particlesArray [i * seaResolution + j].position = psetting [i * seaResolution + j].getPosition ();
			}
		}
	}
	void changeColor () {
		for (int i = 0; i < seaResolution; i++) {
			for (int j = 0; j < seaResolution; j++) {
				float value = (Time.realtimeSinceStartup - Mathf.Floor (Time.realtimeSinceStartup));
				value+= psetting [i * seaResolution + j].angle / 2 / Mathf.PI;
				while (value > 1)
					value--;
				particlesArray[i * seaResolution + j].color = colorGradient.Evaluate(value);
				//particlesArray [i * seaResolution + j].color = colorGradient.Evaluate (Random.value);
			}
		}
	}
}