Shader "Examples/Silhouette"
{
    Properties
    {
        _ForegroundColor("FG Color", Color) = (0,0,0,1)
        _BackgroundColor("BG Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        //        ZTest Always
        //        ZWrite On
        Tags
        {
            "RenderType" = "Transparent" "Queue" = "Transparent" "RenderPipeline" = "UniversalPipeline"
        }
        Pass
        {
            Tags
            {
                "LightMode" = "UniversalForward"
            }
            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

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

            CBUFFER_START(UnityPerMaterial)
            float4 _ForegroundColor;
            float4 _BackgroundColor;
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
                float2 screenUVs = i.positionSS.xy / i.positionSS.w;
                float rawDepth = SampleSceneDepth(screenUVs);
                float scene01Depth = Linear01Depth(rawDepth, _ZBufferParams);

                float4 outputColor = lerp(_ForegroundColor, _BackgroundColor, scene01Depth);
                return outputColor;
            }
            ENDHLSL
        } 
        Pass
        {
            Name "DepthOnly"
            Tags
            {
                "LightMode" = "DepthOnly"
            }
            ZWrite On
            ColorMask 0
            HLSLPROGRAM
            #pragma vertex DepthOnlyVertex
            #pragma fragment DepthOnlyFragment
            #include "Packages/com.unity.render-pipelines.universal/Shaders/UnlitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/DepthOnlyPass.hlsl"
            #pragma multi_compile_instancing
            #pragma multi_compile_DOTS_INSTANCING_ON
            ENDHLSL
        }
    }
}
