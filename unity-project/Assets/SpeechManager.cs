﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
   public GameObject eventManager;


   KeywordRecognizer keywordRecognizer = null;
   Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

   // Use this for initialization
   void Start()
   {
      keywords.Add("Street Lights", () =>
      {
         eventManager.GetComponent<EventManager>().FilterData("Straßenbeleuchtung");

      });


      keywords.Add("Traffic Accidents", () =>
      {
         eventManager.GetComponent<EventManager>().FilterData("Verkehrsverstöße");

      });


      keywords.Add("Show All", () =>
      {
         eventManager.GetComponent<EventManager>().FilterData("Show All");

      });


      // Tell the KeywordRecognizer about our keywords.
      keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

      // Register a callback for the KeywordRecognizer and start recognizing!
      keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
      keywordRecognizer.Start();
   }

   private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
   {
      System.Action keywordAction;
      if (keywords.TryGetValue(args.text, out keywordAction))
      {
         keywordAction.Invoke();
      }
   }
}