Shader "Examples/KeywordExample"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1,1,1,1)
        [Toggle(OVERRIDE_RED_ON)]_OverrideRed("Override Red", Float) = 0
    }
    SubShader
    {
        Tags{"RenderType" = "Opaque" "Queue" = "Geometry" "RenderPipeline" = "UniversalPipeline"}
        Pass
        {
            Tags{"LightMode" = "UniversalForward"}
            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_local OVERRIDE_RED_ON __
            
            struct appdata
            {
                float4 positionOS : Position;
                float2 uv : TEXCOORD0;
                
            };  
            struct v2f
            {
                float4 positionCS : SV_Position;
                float2 uv : TEXCOORD0;
            };
            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
            CBUFFER_END
            
            v2f vert(appdata v)
            {
                v2f o;
                o.positionCS = TransformObjectToHClip(v.positionOS);
                return o;
            }

            float4 frag(v2f i) : SV_TARGET
            {
                #if OVERRIDE_RED_ON
                    return float4(1.0f,0.0f,0.0f,1.0f);
                #else
                    return _BaseColor;
                #endif
            }
            ENDHLSL
        }    
    }
}
