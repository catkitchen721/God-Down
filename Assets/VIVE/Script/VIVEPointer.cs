using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class VIVEPointer : MonoBehaviour {
	public int resolution = 30;
	public float size = 0.01f;
	public Color particleColor = Color.cyan;

	ParticleSystem ps;
	ParticleSystem.Particle[] particles;
	Renderer psRenderer;

	public bool psEnable{
		set{
			if(psRenderer == null){
				return;
			}
			psRenderer.enabled = value;
		}
		get{
			return psRenderer.enabled;
		}
	}

	private void Awake()
	{
		if (resolution < 10 || resolution > 100) {
			resolution = 10;
		}

		ps = GetComponent<ParticleSystem> ();
		psRenderer = ps.GetComponent<Renderer> ();
		particles = new ParticleSystem.Particle[resolution];
		for (int i = 0; i < resolution; ++i) {
			particles [i].startColor = particleColor;
			particles [i].startSize = size;
		}
	}

	public void SetPointLine(Vector3 pos)
	{
		pos = transform.InverseTransformPoint(pos);
		
		for (int i = 0; i < resolution; ++i) {
			particles [i].position = Vector3.Lerp (transform.localPosition, pos, (float)i / resolution);
		}

		ps.SetParticles (particles, particles.Length);
	}
}
