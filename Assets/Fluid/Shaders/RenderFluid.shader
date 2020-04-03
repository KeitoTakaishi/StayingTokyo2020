﻿Shader "Custom/RenderFluid"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows vertex:vert
			#pragma multi_compile_instancing
			#pragma instancing_options procedural:setup
			#pragma target 3.0
			#include "../../VJUtils/ShaderUtils/utils.cginc"

			sampler2D _MainTex;

			struct Input
			{
				float2 uv_MainTex;
			};

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent :TANGENT0;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
				float2 texcoord2 : TEXCOORD2;
				uint instanceID : SV_InstanceID;
				uint vertexID : SV_VertexID;
			};

#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
			struct Parameters {
				float3 pos;
				float2 life;
			};
			StructuredBuffer<Parameters> paramsBuffer;
#endif

			void setup() {
	#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
	#endif
			}


			void vert(inout appdata v) {
	#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED

				uint _instanceID = (int)v.instanceID;
				int _vid = (int)v.vertexID;

				float4 vert = v.vertex;
				float2 uv = v.texcoord.xy;

				//float3 position = float3(0.0, 0.0, _instanceID);
				float3 p = paramsBuffer[_instanceID].pos;
				
				float id = (float)_instanceID;
				float l = paramsBuffer[_instanceID].life.x * 0.5;
				vert = mul(ScaleMatrix(float3(l, l, l)), vert);
				vert = mul(RotXMatrix(_Time.x * 10.0 + id ), vert);
				vert = mul(RotYMatrix(_Time.y + id), vert);
				vert = mul(RotZMatrix(_Time.x + 10.0 + id), vert);
				vert = mul(TranslateMatrix(p), vert);
				v.vertex = vert;
	#endif
			}



			half _Glossiness;
			half _Metallic;
			fixed4 _Color;
			UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_INSTANCING_BUFFER_END(Props)
			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}
