using System.Collections;


using System.Collections.Generic;


using UnityEngine;


using UnityEngine.Events;


using Vuforia;



public class EasyEventHandler : MonoBehaviour, ITrackableEventHandler


{



    public UnityEvent onMarkerFound;


    public UnityEvent onMarkerLost;



    protected TrackableBehaviour mTrackableBehaviour;


    protected TrackableBehaviour.Status m_PreviousStatus;


    protected TrackableBehaviour.Status m_NewStatus;





    protected virtual void Start()


    {


        mTrackableBehaviour = GetComponent<TrackableBehaviour>();


        if (mTrackableBehaviour)


            mTrackableBehaviour.RegisterTrackableEventHandler(this);


    }



    protected virtual void OnDestroy()


    {


        if (mTrackableBehaviour)


            mTrackableBehaviour.UnregisterTrackableEventHandler(this);


    }




    public void OnTrackableStateChanged(


        TrackableBehaviour.Status previousStatus,


        TrackableBehaviour.Status newStatus)


    {


        m_PreviousStatus = previousStatus;


        m_NewStatus = newStatus;



        if (newStatus == TrackableBehaviour.Status.DETECTED ||


            newStatus == TrackableBehaviour.Status.TRACKED ||


            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)


        {


            onMarkerFound.Invoke();


        }


        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&


                 newStatus == TrackableBehaviour.Status.NO_POSE)


        {





            onMarkerLost.Invoke();


        }


        else


        {


            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND


            // Vuforia is starting, but tracking has not been lost or found yet


            // Call OnTrackingLost() to hide the augmentations


            onMarkerLost.Invoke();


        }


    }


}

