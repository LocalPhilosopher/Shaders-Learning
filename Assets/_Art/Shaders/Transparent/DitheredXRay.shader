Shader "Examples/DitheredXRay"
{
    Properties
    {
//        _BaseColor("Base Color", Color) = (1,1,1,1)
//        _BaseTex("Base Texture", 2D) = "white"
        _XRayColor("X-Ray Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags{"RenderType" = "Opaque" "Queue" = "AlphaTest" "RenderPipeline" = "UniversalPipeline"}
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
                float4 positionSS : TEXCOORD0;
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
                o.positionSS = ComputeScreenPos(o.positionCS);
                return o;
            }

            float4 frag(v2f i) : SV_TARGET
            {
                
                float2 screenUVs = i.positionSS.xy / i.positionSS.w * _ScreenParams.xy;
                float ditherThresholds[16] =
                {
                    16.0 / 17.0, 8.0 / 17.0, 14.0 / 17.0, 6.0 / 17.0,
                    4.0 / 17.0, 12.0 / 17.0, 2.0 / 17.0, 10.0 / 17.0,
                    13.0 / 17.0, 5.0 / 17.0, 15.0 / 17.0, 7.0 / 17.0,
                    1.0 / 17.0, 9.0 / 17.0, 3.0 / 17.0, 11.0 / 17.0
                };
                uint index = (uint(screenUVs.x) %4) * 4 + uint(screenUVs.y) %4;
                float threshold = ditherThresholds[index];
                if (_XRayColor.a < threshold) discard;
                return _XRayColor;
            }
            ENDHLSL
        }    
    }
}
