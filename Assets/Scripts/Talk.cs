using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : MonoBehaviour
{
   private Queue<string> sentences;

   void start()
   {
       sentences = new Queue<string>();
   }
}
