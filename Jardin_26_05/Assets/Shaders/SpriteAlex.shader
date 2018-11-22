// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/SpriteAlex" {
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_Hue("Teinte", Range(0.0, 1.0)) = 0.0
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
	}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile _ PIXELSNAP_ON
#include "UnityCG.cginc"

		struct appdata_t
	{
		float4 vertex   : POSITION;
		float4 color    : COLOR;
		float2 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		float4 vertex   : SV_POSITION;
		fixed4 color : COLOR;
		float2 texcoord  : TEXCOORD0;
		float3 worldPos : TEXCOORD1;
	};

	fixed4 _Color;

	v2f vert(appdata_t IN)
	{
		v2f OUT;
		
		float4 objectOrigin = mul(unity_ObjectToWorld, float4(0.0, 0.0, 0.0, 1.0));
		OUT.worldPos = mul(unity_ObjectToWorld, objectOrigin.xyz);
		OUT.vertex = UnityObjectToClipPos(IN.vertex);
		OUT.texcoord = IN.texcoord;
		OUT.color = IN.color * _Color;		
#ifdef PIXELSNAP_ON
		OUT.vertex = UnityPixelSnap(OUT.vertex);
#endif

		return OUT;
	}

	sampler2D _MainTex;
	sampler2D _AlphaTex;
	float _AlphaSplitEnabled;
	float _Hue;

	fixed3 rgb2hsv(float3 c)
	{
		fixed4 K = fixed4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
		fixed4 p = c.g < c.b ? fixed4(c.bg, K.wz) : fixed4(c.gb, K.xy);
		fixed4 q = c.r < p.x ? fixed4(p.xyw, c.r) : fixed4(c.r, p.yzx);

		fixed d = q.x - min(q.w, q.y);
		fixed e = 1.0e-10;
		return fixed3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
	}

	fixed3 hsv2rgb(fixed3 c)
	{
		fixed4 K = fixed4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
		fixed3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
		return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
	}

	fixed random(in fixed2 st) {
		return frac(sin(dot(st.xy, fixed2(12.9898, 78.233))) * 43758.5453123);
	}

	fixed4 SampleSpriteTexture(float2 uv)
	{
		fixed4 color = tex2D(_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
		if (_AlphaSplitEnabled)
			color.a = tex2D(_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

		return color;
	}

	fixed4 frag(v2f IN) : SV_Target
	{
	fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
	//fixed3 colorHsv = rgb2hsv(c.rgb);
	//colorHsv.r += IN.worldPos;
	//c.rgb = hsv2rgb(colorHsv);
	c.rgb *= c.a;
	return c;
	}
		ENDCG
	}
	}
}