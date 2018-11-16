Vicon Pegasus 1.1.1 Unity plugin patch
--------------------------------------


Delete the installed folder 'Program Files\Vicon\Pegasus1.1\UnityPlugin' and replace with the contents of this zip.


This version of the Unity plugin was tested with Unity 5.0.2 64-bit.


- To demonstrate the use of Pegasus for retargeting, import the FBX model in the Assets folder into Pegasus and use it as the target skeleton.
Then build and run the project in Unity.


- When building a standalone Unity application, Unity will not automatically copy all of the Vicon DLLs to the target folder.
The following files will need to be manually copied to the same folder as the standalone exe:

ViconDataStreamSDK_CPP.dll
ViconPegasusSDK_C.dll
boost_chrono-vc110-mt-1_55.dll
boost_system-vc110-mt-1_55.dll
boost_thread-vc110-mt-1_55.dll
