using UnityEngine;
using System.Collections;


public class RenderingCameras : MonoBehaviour {

	// ******************************************************************************
	// Private Variables
	// ******************************************************************************
	#region private_variables

			private Camera mainCamera;

			private Camera leftCamera;
	
			private Camera rightCamera;
	

			private GameObject gameObj_leftCamera;

			private GameObject gameObj_rightCamera;

	
		
			// ******************************************************************************
			// Device Modeling
			// ******************************************************************************
					
			/// <summary>
			/// Distance (meters) between the centers of the device screens. 		
			/// </summary>
			
			private float screenInteraxialDistance =0.060f;
			
				
			/// <summary>
			/// The angle (in degrees) between the z-direction of the camera and the z-direction of the glass screen.
			/// </summary>
	//		[Range(-30f, 30f)]
	//		private float sensorScreenAngle = 15.0f;
			
			/// <summary>
			/// The width of a single device screen in meters.
			/// </summary>
			private float screenWidth =0.025f; //0.0311f; // 0.0175f;  

			/// <summary>
			/// The height of a single device screen in meters.
			/// </summary>
			[Range(1f, 3f)]
			private float screenHeight =0.01406f; //0.0175f; // 0.0150f; 
			
			//Aspect ratio=1280/720 =aprox 311/175
	
						
			private Vector3 _pointOfInterest = 0.7f*Vector3.forward;
			

			/// <summary>
			/// The position between the left and right camera.
			/// </summary>
	
			// ******************************************************************************
			// Anatomical Variables
			// ******************************************************************************
			/// <summary>
			/// Distance between eyes in millimeters
			/// </summary>
			[Range(0, 100)]
			private float eyeInteraxialDistance = 0.064f;  //6.8f;  //6.4f; 

            /// <summary>
            /// The average radius of a human eyeball in meters.
            /// This is a human anatomy property. It is not considered to change.
            /// </summary>
            [Range(0.0f, 4f)]
			private float eyeballRadius = 0.024f;  // world average eyball radius
			
			/// <summary>
			/// The distance between the user's eyes and the device screen.
			/// This should be a variable for each user.
			/// </summary>
			[Range(0.0f, 10f)]
			private float nearPlaneDistance = 0.06f;
			
			/// <summary>
			/// Far clipping plane.
			/// </summary>
			[Range(0.01f, 500.0f)]
			private float farPlaneDistance = 2.0f; //200f;


            private float aspect_ratio;
			private float half_height ;
			private float interaxial_difference;


			#endregion
			
			
			// ******************************************************************************
			// Unity Methods
			// ******************************************************************************
			#region unity_methods

			// Use this for initialization
			void Start () {
       
               
                mainCamera = this.GetComponent<Camera>();  //MonoCamera

              
                gameObj_leftCamera = this.transform.Find("StereoCameras/LeftCamera").gameObject;
		   			 leftCamera=gameObj_leftCamera.GetComponent<Camera> ();

				gameObj_rightCamera = this.transform.Find("StereoCameras/RightCamera").gameObject;
					 rightCamera=gameObj_rightCamera.GetComponent<Camera> ();

   

                _pointOfInterest = new Vector3(0, 0, 0.7f);  //70cm in front of the user

                aspect_ratio = screenWidth / screenHeight;
				half_height = screenHeight / 2.0f;
				interaxial_difference = (eyeInteraxialDistance - screenInteraxialDistance) / 2.0f;



				leftCamera.aspect = screenWidth/screenHeight;
				rightCamera.aspect = screenWidth/screenHeight;
	
				mainCamera.GetComponent<Camera>().aspect = leftCamera.rect.width * 2 / leftCamera.rect.height;
			
				//Setup Cameras projection matrix
				leftCamera.projectionMatrix = CalculateProjectionMatrixFromSettings(true);
				rightCamera.projectionMatrix = CalculateProjectionMatrixFromSettings(false);
			
			   // Setup positions
				SetupLocalDeviceTransformations (); 		
	}
			
			
		
			// Update is called once per frame
			void Update () {
                 if (Input.GetKeyDown(KeyCode.F2))
                      {
                      EnableStereo();
                       }

                  if (Input.GetKeyDown(KeyCode.F3))
                      {
                       DisableStereo();
                     }
            }
			#endregion
			
			
			// ******************************************************************************
			// Private Methods
			// ******************************************************************************
			#region private_methods
							
			
			/// <summary>
			/// // Calculates the viewport for each glass screen and the camera projection matrix.
			/// </summary>
					private Matrix4x4 CalculateProjectionMatrixFromSettings(bool isLeftCam)	{

                  
                     Vector3 point_of_interest_local = new Vector3();

					float left, right;
					if (isLeftCam)
					{
						// Point of interest is expressed based on the center of the glass screens.
						point_of_interest_local = _pointOfInterest - new Vector3(eyeInteraxialDistance / 2.0f, 0, 0);
						float b = point_of_interest_local.x * eyeballRadius / (point_of_interest_local.z + eyeballRadius + nearPlaneDistance);
						left = (-aspect_ratio * half_height) - b - interaxial_difference;
						right = (aspect_ratio * half_height) - b - interaxial_difference;
					}
					else
					{
						// Point of interest is expressed based on the center of the glass screens.
						point_of_interest_local = _pointOfInterest + new Vector3(eyeInteraxialDistance / 2.0f, 0, 0);
						float b = point_of_interest_local.x * eyeballRadius / (point_of_interest_local.z + eyeballRadius + nearPlaneDistance);
						left = (-aspect_ratio * half_height) - b  + interaxial_difference;
						right = (aspect_ratio * half_height) - b  + interaxial_difference;
					}
					
					float top, bottom;
					top = half_height;
					bottom = -half_height;
                 
					return CalculateOffCenterProjectionMatrix(left, right, bottom, top, nearPlaneDistance, farPlaneDistance);
				
			}
			
			
			/// <summary>
			/// Helper for setProjectionMatrix. 
			/// </summary>
		
			private Matrix4x4 CalculateOffCenterProjectionMatrix(float left, float right, float bottom, float top, float near, float far)
			{ // from http://docs.unity3d.com/ScriptReference/Camera-projectionMatrix.html
				/* 
				x 0 a 0
				0 y b 0
				0 0 c d
				0 0 e 0
				*/		
						
				float x = (2.0f * near) / (right - left);
				float y = (2.0f * near) / (top - bottom);
				float a = (right + left) / (right - left);
				float b = (top + bottom) / (top - bottom);
				float c = -(far + near) / (far - near);
				float d = -(2.0f * far * near) / (far - near);  // if far is too big, then: -(2.0 * near)
				float e = -1.0f;
				
				Matrix4x4 m = new Matrix4x4();
				m[0, 0] = x;
				m[0, 1] = 0;
				m[0, 2] = a;
				m[0, 3] = 0;
				
				m[1, 0] = 0;
				m[1, 1] = y;
				m[1, 2] = b;
				m[1, 3] = 0;
				
				m[2, 0] = 0;
				m[2, 1] = 0;
				m[2, 2] = c;
				m[2, 3] = d;
				
				m[3, 0] = 0;
				m[3, 1] = 0;
				m[3, 2] = e;
				m[3, 3] = 0;
				
				return m;
			}
			
						
			private void SetupLocalDeviceTransformations()
			{
					
			leftCamera.transform.localPosition = new Vector3(-eyeInteraxialDistance / 2.0f,0, -nearPlaneDistance);
			rightCamera.transform.localPosition = new Vector3(eyeInteraxialDistance / 2.0f,0, -nearPlaneDistance);
		
            mainCamera.transform.localPosition = (leftCamera.transform.localPosition + rightCamera.transform.localPosition) / 2.0f;
    }


    #endregion

    public void EnableStereo()
    {
        
        mainCamera.enabled = true;
        leftCamera.enabled=true;
        rightCamera.enabled = true;
    }


   
    public void DisableStereo()
    {
        mainCamera.enabled = true;
        leftCamera.enabled=false;
        rightCamera.enabled=false;
        }
}
