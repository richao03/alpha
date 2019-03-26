using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
  // Start is called before the first frame update
  private Transform transform;

  // Desired duration of the shake effect
  private float shakeDuration = 0f;

  // A measure of magnitude for the shake. Tweak based on your preference
  private float shakeMagnitude = 0.7f;

  // A measure of how quickly the shake effect should evaporate
  private float dampingSpeed = 1.0f;

  // The initial position of the GameObject
  Vector3 initialPosition;


  void OnEnable()
  {
    initialPosition = gameObject.transform.localPosition;
  }
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (shakeDuration > 0)
    {
      gameObject.transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

      shakeDuration -= Time.deltaTime * dampingSpeed;
    }
    else
    {
      shakeDuration = 0f;
      gameObject.transform.localPosition = initialPosition;
    }
  }

  public void TriggerShake(float duration)
  {
    print("shake");
    shakeDuration = duration;
  }

}
