Shader "Examples/Triplanar"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _XTex ("X Texture", 2D) = "white" {}
        _YTex ("Y Texture", 2D) = "white" {}
        _ZTex ("Z Texture", 2D) = "white" {}
        _Tile("Texture Tiling", Float) = 1
        _BlendPower("Triplanar Blending", Float) = 10
    }
    SubShader
    {
        Tags {"RenderType" = "Opaque" "Queue" = "Geometry" "RenderPipeline" = "UniversalPipeline"}

        Pass
        {
            Tags{"LightMode" = "UniversalForward"}
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct v2f
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
            };

            sampler2D _XTex;
            sampler2D _YTex;
            sampler2D _ZTex;
            CBUFFER_START(UnityPerMaterial)
                float _Tile;
                float _BlendPower;
            CBUFFER_END

            v2f vert (appdata v)
            {
                v2f o;
                o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
                o.positionWS = TransformObjectToWorld(v.positionOS.xyz);
                o.normalWS = TransformObjectToWorldNormal(v.normalOS);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 xAxisUV = i.positionWS.zy * _Tile;
                float2 yAxisUV = i.positionWS.xz * _Tile;
                float2 zAxisUV = i.positionWS.xy * _Tile;

                float4 xSample = tex2D(_XTex, xAxisUV);
                float4 ySample = tex2D(_YTex, yAxisUV);
                float4 zSample = tex2D(_ZTex, zAxisUV);

                float3 weights = pow(abs(i.normalWS), _BlendPower);
                weights /= (weights.x + weights.y + weights.z);
                float4 outColor = xSample * weights.x + ySample * weights.y + zSample * weights.z;
                // float4 col = tex2D(_MainTex, i.uv);
                return outColor;
            }
            ENDHLSL
        }
    }
}
