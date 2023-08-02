Shader "Examples/PolarCoordinates"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Center("Center", Vector) = (.5,.5,0,0)
        _RadialScale("Radial Scale", Float) = 1
        _LengthScale("Length Scale", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry" "RenderPipeline" = "UniversalPipeline"}

        Pass
        {
            Tags{"LightMode" = "UniversalForward"}
            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #pragma vertex vert
            #pragma fragment frag
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _BaseColor;
                float2 _Center;
                float _RadialScale;
                float _LengthScale;
            CBUFFER_END

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float2 cartesianToPolar(float2 cartUV)
            {
                float2 offset = cartUV - _Center;
                float radius = length(offset) * 2;
                float angle = atan2(offset.x, offset.y) / (2.0f * PI);

                return float2(radius, angle);
            }
            
            float4 frag (v2f i) : SV_Target
            {
                float2 radialUV = cartesianToPolar(i.uv);
                radialUV.x *= _RadialScale ;
                radialUV.y *= _LengthScale;
                float4 col = tex2D(_MainTex, radialUV);
                return col;
            }
            ENDHLSL
        }
    }
}
