Shader "Hidden/SliceGitch"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "Libs/SimplexNoise3D.cginc"

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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float _BlockWidth;
			float _BlockHeight;
			float _Fineness;
			float _Speed;
			float _Threshold;

			float _offSet = 0.3;

			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv;
				float4 col = float4(1.0.xxxx);
				
				_offSet = 0.3;
				if (uv.x < _offSet) {
					col = tex2D(_MainTex, float2(uv.y, 1.0 - (uv.x / _offSet)));
				}
				else if (uv.x > (1.0-_offSet)) {
					//col = tex2D(_MainTex, i.uv);
					//col.rgb = float3(1.0.xxx) - col.rgb;
					col = tex2D(_MainTex, float2( uv.y, (uv.x - (1.0 - _offSet)) / _offSet));
				}else {
					col = tex2D(_MainTex, i.uv);
				}
				
			

				return col;
			}
			ENDCG
		}
	}
}
