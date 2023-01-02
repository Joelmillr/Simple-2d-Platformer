using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bushes : MonoBehaviour
{
   // Disable the bushes when the criteria is met
   public void DisableBushes()
   {
      gameObject.SetActive(false);
   }
}
