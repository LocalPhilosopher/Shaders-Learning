Shader "Examples/CubemapReflections"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _Cubemap("Cubemap", CUBE) = "white"{}
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
            
            struct appdata
            {
                float4 positionOS : Position;
                float3 normalOS : NORMAL;
                
            };  
            struct v2f
            {
                float4 positionCS : SV_Position;
                float3 reflectWS : TEXCOORD0;
            };

            samplerCUBE _Cubemap;
            
            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
            CBUFFER_END
            
            v2f vert(appdata v)
            {
                v2f o;
                o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
                float3 normalWS = TransformObjectToWorldNormal(v.normalOS);
                float3 positionWS = mul(unity_ObjectToWorld,v.positionOS).xyz;
                float3 viewDirWS = GetWorldSpaceNormalizeViewDir(positionWS);

                o.reflectWS = reflect(-viewDirWS,normalWS);
                return o;
            }

            float4 frag(v2f i) : SV_TARGET
            {
                float4 cubemapSample = texCUBE(_Cubemap, i.reflectWS);
                return cubemapSample * _BaseColor;
            }
            ENDHLSL
        }    
    }
}
