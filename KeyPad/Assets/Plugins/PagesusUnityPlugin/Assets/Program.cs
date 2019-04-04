using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using ViconPegasusSDK.DotNET;


namespace TestUnityVicon
{
	public class Program: MonoBehaviour
	{
		string SubjectName = "";

		ViconPegasusSDK.DotNET.Client MyClient = new ViconPegasusSDK.DotNET.Client();
        

		public Program()
		{
		}

		void Start()
		{
			print ("Starting...");

            // Make a new client
			Output_GetVersion OGV = MyClient.GetVersion();
			print("GetVersion Major: " + OGV.Major);


			// Connect to a server
			string HostName = "192.168.1.119:801";
            int noAttempts = 0;

  			print("Connecting to " + HostName + "...");
            while (!MyClient.IsConnected().Connected)
            {
                // Direct connection
                Output_Connect OC = MyClient.Connect(HostName);
                print("Connect result: " + OC.Result);

                noAttempts += 1;
                if (noAttempts == 3)
                    break;
                System.Threading.Thread.Sleep(200);
            }

			MyClient.EnableSegmentData();
		    MyClient.EnableMarkerData();
		    MyClient.EnableUnlabeledMarkerData();
            

			// get a frame from the data stream so we can inspect the list of subjects
			MyClient.GetFrame();

			Output_GetSubjectCount OGSC = MyClient.GetSubjectCount ();
			print("GetSubjectCount: "+ OGSC.Result + "|" + OGSC.SubjectCount);

			// the first subjects in the data stream will be the original subjects unmodified by pegasus
			Output_GetSubjectName OGSN = MyClient.GetSubjectName(OGSC.SubjectCount - 1);
			print("GetSubjectName: "+ OGSN.Result + "|" + OGSN.SubjectName);

			SubjectName = OGSN.SubjectName;

            // get the position of the root and point the camera at it
            Output_GetSubjectRootSegmentName OGSRSN = MyClient.GetSubjectRootSegmentName(SubjectName);
			Output_GetSegmentGlobalTranslation RootPos = MyClient.GetSegmentGlobalTranslation(SubjectName, OGSRSN.SegmentName);
		    Output_GetMarkerCount mcount = MyClient.GetMarkerCount(SubjectName);

		    Output_GetUnlabeledMarkerGlobalTranslation OGSGT = MyClient.GetUnlabeledMarkerGlobalTranslation(0);
			
			Vector3 Target = new Vector3(-(float)OGSGT.Translation[0]/1000f, (float)OGSGT.Translation[1]/1000f, (float)OGSGT.Translation[2]/1000f);

			Camera.main.transform.LookAt(Target);
		}

	    void LateUpdate()
		{
			MyClient.GetFrame();

		    Output_GetSubjectRootSegmentName OGSRSN = MyClient.GetSubjectRootSegmentName(SubjectName);



//			ApplyBoneTransform(Root);

			// keep the camera looking at the model
			Output_GetSegmentGlobalTranslation RootPos = MyClient.GetSegmentGlobalTranslation(SubjectName, OGSRSN.SegmentName);
		    if (RootPos.Occluded)
		        return;

		    Output_GetSegmentGlobalRotationQuaternion RootRotation = MyClient.GetSegmentGlobalRotationQuaternion(SubjectName, OGSRSN.SegmentName);

            Quaternion pose = new Quaternion((float)RootRotation.Rotation[0], (float)RootRotation.Rotation[1], (float)RootRotation.Rotation[2], (float)RootRotation.Rotation[3]);
			Vector3 Target = new Vector3(-(float)RootPos.Translation[0]/1000f, (float)RootPos.Translation[1]/1000f, (float)RootPos.Translation[2]/1000f);

		    gameObject.transform.position = Target;
		    gameObject.transform.rotation = pose;



		}

		private void ApplyBoneTransform(Transform Bone)
		{
			// update the bone transform from the data stream
			Output_GetSegmentLocalRotationQuaternion ORot = MyClient.GetSegmentLocalRotationQuaternion(SubjectName, Bone.gameObject.name);
			if( ORot.Result == Result.Success )
			{
				Bone.localRotation = new Quaternion(-(float)ORot.Rotation[0], (float)ORot.Rotation[1], (float)ORot.Rotation[2], -(float)ORot.Rotation[3]);
			}
			
			Output_GetSegmentLocalTranslation OTran = MyClient.GetSegmentLocalTranslation(SubjectName, Bone.gameObject.name);	
			if( OTran.Result == Result.Success )
			{
				Bone.localPosition = new Vector3(-(float)OTran.Translation[0], (float)OTran.Translation[1], (float)OTran.Translation[2]);
			}

			// recurse through children
			for( int iChild = 0; iChild < Bone.childCount; iChild++ )
			{
				ApplyBoneTransform( Bone.GetChild(iChild) );
			}
		}
	}
}

