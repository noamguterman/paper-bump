using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppsFlyerManager : MonoBehaviour
{
    void Start()
    {
        /* Mandatory - set your AppsFlyer’s Developer key. */
        //old - nEW6xKmN9Nt3aHUBFVeWFH
        //new - 8MAzUC3B2BHYVi2uYVHaSd
        AppsFlyer.setAppsFlyerKey("nEW6xKmN9Nt3aHUBFVeWFH");
        /* For detailed logging */
        /* AppsFlyer.setIsDebug (true); */

#if UNITY_IOS
          /* Mandatory - set your apple app ID
           NOTE: You should enter the number only and not the "ID" prefix */
          AppsFlyer.setAppID ("com.epc.fungame");
          AppsFlyer.trackAppLaunch ();

#elif UNITY_ANDROID
        /* Mandatory - set your Android package name */
        AppsFlyer.setAppID("com.epc.paperbump");
                /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
                AppsFlyer.init("nEW6xKmN9Nt3aHUBFVeWFH", "AppsFlyerTrackerCallbacks");
        #endif
    }
}

