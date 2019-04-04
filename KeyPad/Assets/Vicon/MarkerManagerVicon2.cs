using System;
using UnityEngine;
#if UNITY_STANDALONE_OSX
#else
using ViconPegasusSDK.DotNET;
#endif

public class MarkerManagerVicon2 : MonoBehaviour
{



    // Vicon Motion 
    public string HostNameAndPort = "192.168.1.119:801";
    public float x_adjustment = 0;
    public float y_adjustment = 0;
    public float z_adjustment = 0;
    public int numMarkers = 0;

#if UNITY_STANDALONE_OSX
#else


    private Client mViconClient = new Client();
    private Transform mOriginPoint;
    private Vector3 lastPosition;


    // Use this for initialization
    void Start () {
        CreateConnection();
        SetOriginPoint(gameObject.transform);
        lastPosition = new Vector3(-1, -1, -1);
    }
    
    // Setting the origin point
    public void SetOriginPoint(Transform origin)
    {
        mOriginPoint = origin;
    }

    // TRACKING SPECIFIC MARKERS IN VICON
    // Update is called once per frame
    //void Update()
    //{
    //    // Getting a frame from the Vicon data stream so we can inspect the list of markers 
    //    mViconClient.GetFrame();
    //    GameObject finger = GameObject.Find("IndexFinger");

    //    // TRACKING SPECIFIC MARKERS IN VICON
    //    numMarkers = (int) mViconClient.GetUnlabeledMarkerCount().MarkerCount;
    //    for (int i = 0; i < numMarkers; i++)
    //    {
    //        Output_GetUnlabeledMarkerGlobalTranslation translation = mViconClient.GetUnlabeledMarkerGlobalTranslation((uint) i);
    //        Vector3 position = new Vector3(
    //            (float)(Math.Round((translation.Translation[0] / 1000f), 9) + x_adjustment),
    //            (float)(Math.Round((translation.Translation[2] / 1000f), 9) + y_adjustment),
    //            (float)(Math.Round((translation.Translation[1] / 1000f), 9) + z_adjustment));

    //        Vector3 difference = position - lastPosition;
    //        if (position.Equals(new Vector3(0, 0, 0)))
    //        {
    //            position = lastPosition;
    //        }






    //        finger.transform.position = position;
    //        finger.transform.position = mOriginPoint.TransformPoint(position);
    //        lastPosition = finger.transform.position;

    //        // TRACKING AN OBJECT IN VICON
    //        //// Getting the names of subjects and root segments
    //        //string subjectName = mViconClient.GetSubjectName((uint)i).SubjectName;
    //        //string rootSegmentName = mViconClient.GetSubjectRootSegmentName(subjectName).SegmentName;

    //        //// subjectName by default will contain an underscore and some number - we do not want this when we find objects in Unity
    //        //string subjectNameParsed = subjectName.Split('_')[0];

    //        //// Getting and setting the position and rotation of the game object
    //        //Vector3 position = MakePositionVector(subjectName, rootSegmentName);

    //        //// If Vicon cannot get position information, it defaults to 0,0 which can cause problems with the experiment
    //        //if (position.Equals(new Vector3(0, 0, 0)))
    //        //{
    //        //    position = new Vector3(-1f, -1f, -1);
    //        //}
    //        //Quaternion rotation = MakeRotationQuaternion(subjectName, rootSegmentName);
    //        //SetTransformation(subjectNameParsed, position, rotation);
    //    }

    //    if(numMarkers == 0)
    //    {
    //        finger.transform.position = lastPosition;
    //        finger.transform.position = mOriginPoint.TransformPoint(lastPosition);
    //        lastPosition = finger.transform.position;
    //    }


    //}

    // TRACK AN OBJECT IN VICON
    // Update is called once per frame
    void Update() {
        // Getting a frame from the Vicon data stream so we can inspect the list of objects 
        mViconClient.GetFrame();

        int numSubjects = (int)mViconClient.GetSubjectCount().SubjectCount;

        if (numSubjects == 0) {
            Debug.Log("No subjects!");
        }

        //Debug.Log(numSubjects);
        //Debug.Log((int)mViconClient.GetUnlabeledMarkerCount().MarkerCount);
        for (int i = 0; i < numSubjects; i++) {
            // Getting the names of subjects and root segments
            string subjectName = mViconClient.GetSubjectName((uint)i).SubjectName;
            string rootSegmentName = mViconClient.GetSubjectRootSegmentName(subjectName).SegmentName;

            // subjectName by default will contain an underscore and some number - we do not want this when we find objects in Unity
            //var idx = subjectName.LastIndexOf("_");
            string subjectNameParsed = subjectName;//idx == -1 ? subjectName : subjectName.Substring(0, idx);//subjectName.Split('_')[0];

            // Getting and setting the position and rotation of the game object
            Vector3 position = MakePositionVector(subjectName, rootSegmentName);

            // If Vicon cannot get position information, it defaults to 0,0 which can cause problems with the experiment
            if (position.Equals(new Vector3(0, 0, 0))) {
                position = new Vector3(-1f, -1f, -1);
            }
            Quaternion rotation = MakeRotationQuaternion(subjectName, rootSegmentName);
            //Debug.Log(subjectNameParsed+ " " + position);
            SetTransformation(subjectNameParsed, position, rotation);
        }


    }

    // Create connection to server
    public void CreateConnection()
    {
        int numberofAttempts = 0;
        while (!mViconClient.IsConnected().Connected && numberofAttempts < 3)
        {
            Output_Connect connect = mViconClient.Connect(HostNameAndPort);
            System.Threading.Thread.Sleep(200);

            ++numberofAttempts;
        }

        if (mViconClient.IsConnected().Connected)
        {
            // Enable all data streams
            mViconClient.EnableUnlabeledMarkerData();
            mViconClient.EnableMarkerData();
            mViconClient.EnableSegmentData();

            Debug.Log("Connected to " + HostNameAndPort);
            return;
        }

        Debug.Log("Could not connect to " + HostNameAndPort);

    }
   
    // Creating a position vector using the Vicon data
    public Vector3 MakePositionVector(string subjectName, string rootSegmentName)
    {
        Output_GetSegmentGlobalTranslation globalPosition = mViconClient.GetSegmentGlobalTranslation(subjectName, rootSegmentName);
        Vector3 localPosition = new Vector3(
            (float)(globalPosition.Translation[0] / 100f),
            (float)(globalPosition.Translation[2] / 100f),
            (float)(globalPosition.Translation[1] / 100f)
        );

        return localPosition;
    }

    // Creating a rotation quaternion using the Vicon data
    public Quaternion MakeRotationQuaternion(string subjectName, string rootSegmentName)
    {
        Output_GetSegmentGlobalRotationQuaternion globalRotation = mViconClient.GetSegmentGlobalRotationQuaternion(subjectName, rootSegmentName);
        Quaternion localRotation = new Quaternion(
            (float) (globalRotation.Rotation[0]),
            (float) (globalRotation.Rotation[1]),
            (float) (globalRotation.Rotation[2]),
            (float) (globalRotation.Rotation[3])
        );

        return localRotation;
    }

    // Transforming the game object
    public void SetTransformation(string objectName, Vector3 position, Quaternion rotation)
    {
        GameObject resultingObject = GameObject.Find(objectName);
        resultingObject.transform.position = position;
        resultingObject.transform.position = mOriginPoint.TransformPoint(position);
        resultingObject.transform.rotation = rotation;
        resultingObject.transform.Rotate(0f, 0f, 180f, Space.Self);
        resultingObject.transform.Rotate(90f, 180f, 0f, Space.World);
        resultingObject.transform.Rotate(0f, 180f, 0f, Space.Self);
    }
#endif
}