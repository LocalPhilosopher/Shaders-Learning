Shader "Examples/XRay"
{
    Properties
    {
//        _BaseColor("Base Color", Color) = (1,1,1,1)
//        _BaseTex("Base Texture", 2D) = "white"
        _XRayColor("X-Ray Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags{"RenderType" = "Opaque" "Queue" = "Geometry" "RenderPipeline" = "UniversalPipeline"}
        Pass
        {
            ZTest Greater
            ZWrite Off   
            Tags{"LightMode" = "UniversalForward"}
            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            #pragma vertex vert
            #pragma fragment frag
            
            struct appdata
            {
                float4 positionOS : Position;
                
            };  
            struct v2f
            {
                float4 positionCS : SV_Position;
            };

            sampler2D _BaseTex;
            
            CBUFFER_START(UnityPerMaterial)
                float4 _XRayColor;
                // float4 _BaseTex_ST;
            CBUFFER_END
            
            v2f vert(appdata v)
            {
                v2f o;
                o.positionCS = TransformObjectToHClip(v.positionOS);
                return o;
            }

            float4 frag(v2f i) : SV_TARGET
            {
                return _XRayColor;
            }
            ENDHLSL
        }    
    }
}
