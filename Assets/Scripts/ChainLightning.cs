using System;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class ChainLightning : MonoBehaviour
{
  
  private GameObject[] pointObjects;
  private GameObject closestObject;
  private LineRenderer lineRenderer;
  
  [Header("Lightning Options")]
  public GameObject StartPointObject;
  public string TargetTag;
  public float MaxRand = 0.3f;
  public float MinRand = -0.3f;
  public float LightningSpeed = 0.03f;
  private void Start()
  {
    lineRenderer = GetComponent<LineRenderer>();
  }
  
  private void Update()
  {
    if (Input.GetKey(KeyCode.F))
    {
      lineRenderer.enabled = true;
      
      pointObjects = GameObject.FindGameObjectsWithTag(TargetTag);
      float closestDistance = Mathf.Infinity;
      closestObject = null;
      foreach (GameObject pointObject in pointObjects)
      {
        float distance = Vector3.Distance(pointObject.transform.position, transform.position);
        if (distance < closestDistance)
        {
          closestDistance = distance;
          closestObject = pointObject;
        }
      }
      int closestObjectIndex = Array.IndexOf(pointObjects, closestObject);
      if(closestObjectIndex != 0)
      {
        GameObject temp = pointObjects[0];
        pointObjects[0] = closestObject;
        pointObjects[closestObjectIndex] = temp;
      }
      
      lineRenderer.positionCount = pointObjects.Length;
      lineRenderer.SetPosition(0, StartPointObject.transform.position);
      for (int i = 0; i < pointObjects.Length; i++)
      {
        lineRenderer.SetPosition(i, pointObjects[i].transform.position);
      }
      AnimatePoints();
    }
    
    if (Input.GetKeyUp(KeyCode.F))
    {
      DOTween.CompleteAll();
      lineRenderer.enabled = false;
    }
  }

  private void AnimatePoints()
  {
    int positionCount = pointObjects.Length * 10;
    lineRenderer.positionCount = positionCount;
    Vector3 startPos = StartPointObject.transform.position;
    for (int i = 0; i < pointObjects.Length; i++)
    {
      for (int j = 0; j < 10; j++)
      {
        int index = i * 10 + j;
        Vector3 endPos = Vector3.Lerp(startPos, pointObjects[i].transform.position, j / 10f);
        endPos += new Vector3(Random.Range(MinRand, MaxRand), Random.Range(MinRand, MaxRand), Random.Range(MinRand, MaxRand));
        lineRenderer.SetPosition(index, startPos);
        DOTween.To(() => lineRenderer.GetPosition(index), x => lineRenderer.SetPosition(index, x), endPos, LightningSpeed);
      }
      startPos = pointObjects[i].transform.position;
    }
  }
}