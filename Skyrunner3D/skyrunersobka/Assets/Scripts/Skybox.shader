// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Doctrina/Skybox"
{
	Properties
	{
		_ColorTop("Top", Color) = (0.37, 0.52, 0.73, 0)
		_ColorMid("Middle", Color) = (0.89, 0.96, 1, 0)
		_ColorBot("Bot", Color) = (0.89, 0.96, 1, 0)
		
		_Middle("Middle", Float) = 0.5
	}

		CGINCLUDE

#include "UnityCG.cginc"

		struct appdata
	{
		float4 position : POSITION;
		float3 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		float4 position : SV_POSITION;
		float3 texcoord : TEXCOORD0;
	};

	fixed4  _ColorTop;
	fixed4  _ColorBot;
	fixed4  _ColorMid;
	float _Middle;

	v2f vert(appdata v)
	{
		v2f o;
		o.position = UnityObjectToClipPos(v.position);
		o.texcoord = v.texcoord;
		return o;
	}

	half4 frag(v2f i) : COLOR
	{
		float3 v = normalize(i.texcoord);

		//float p = clamp( v.y / clamp( v.z , 0.8, 1 ) , 0, 1 );
		
		float p = clamp( v.y , 0, 1 );
		
		//fixed4 c = _ColorTop * ( v.y - v.z  );
		//fixed4 c = _ColorTop * ( v.y - v.z * 2 );
		
		fixed4 c = lerp(_ColorBot, _ColorMid, p / _Middle) * step(p, _Middle);        
        c += lerp(_ColorMid, _ColorTop, (p - _Middle) / (1 - _Middle) * 3.0 ) * step(_Middle, p);
		
		return c;
	}

		ENDCG

		SubShader
	{
		Tags{ "RenderType" = "Skybox" "Queue" = "Background" }
			Pass
		{
			ZWrite Off
			Cull Off
			Fog { Mode Off }
			CGPROGRAM
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
	}
}