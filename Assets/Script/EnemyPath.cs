﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
  // Start is called before the first frame update
  WaveConfig waveConfig;
  List<Transform> waypoints;
  int waypointIndex = 0;


  // Update is called once per frame
  void Update()
  {
    move();
  }

  public void SetWaveConfig(WaveConfig waveConfig)
  {
    this.waveConfig = waveConfig;
    waypoints = waveConfig.GetWaypoints();
    transform.position = waypoints[waypointIndex].transform.position;

  }

  private void move()
  {
    if (waypoints != null && waypointIndex <= (waypoints.Count - 1))
    {
      var targetPosition = waypoints[waypointIndex].transform.position;
      var movement = waveConfig.GetMoveSpeed() * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, targetPosition, movement);
      if (transform.position == targetPosition)
      {
        waypointIndex++;
      }
    }
    else
    {
      waypointIndex = 1;
    }
  }
}
