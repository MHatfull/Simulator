// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "CameraEffects/ScreenSpaceSnow"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _CameraDepthNormalsTexture;
			float4x4 _CamToWorld;
			sampler2D _SnowTex;
			float _SnowTexScale;
			half4 _SnowColor;
			fixed _BottomThreshold;
			fixed _TopThreshold;
			half3 _CameraPos;

			fixed4 frag (v2f i) : SV_Target
			{
				half3 normal;
				float depth;

				DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, i.uv), depth, normal);
				normal = mul((float3x3) _CamToWorld, normal);

				half snowAmount = normal.g;
				half scale = (_BottomThreshold + 1 - _TopThreshold) + 1;
				snowAmount = saturate( (snowAmount - _BottomThreshold) * scale);
				
				float2 p11_22 = float2(unity_CameraProjection._11, unity_CameraProjection._22);
				float3 vpos = float3(((i.uv * 2) - 1) / p11_22, -1) * depth;
				float4 wpos = mul(_CamToWorld, float4(vpos, 1));
				wpos += float4(_CameraPos, 0) / _ProjectionParams.z;
				//wpos *= _ProjectionParams.z;

				//float height = wpos.y;

				half3 snowColor = tex2D(_SnowTex, wpos.xz * _SnowTexScale * _ProjectionParams.z) * _SnowColor;
				return half4(frac(wpos * 1000));
				//return half4(snowColor, 1);
				//half4 col = tex2D(_MainTex, i.uv); 
				//return float4(wpos.xyz, 1);
				//return lerp(col, _SnowColor, saturate(snowAmount * height));
				//float3 worldspace = i.worldDirection * depth + _WorldSpaceCameraPos;
				//return float4(frac((worldspace)) + float3(0,0,0.1), 1.0);
			}
			ENDCG
		}
	}
}
