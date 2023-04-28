using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "newTrackTimer", menuName = "CreateData/Create New TrackTimerData")]
    public class TrackTimerLists_Dic : ScriptableObject
    {
        public List<PointGameObject> trackTimerLists;
    }

    public class PointData
    {
        public float timer;
        public float trackIdx;
        public float trackIdy;
        public int hitPos;
        public PointData()
        {

        }

        public PointData(int timer, float trackIdx, float trackIdy,int hitPos)
        {
            this.timer = timer;
            this.trackIdx = trackIdx;
            this.trackIdy = trackIdy;
            this.hitPos = hitPos;
        }
    }

    [System.Serializable]
    public class PointGameObject : PointData
    {
        public GameObject gameObject;
    }

