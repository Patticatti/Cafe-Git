Shader"Custom/BlendingShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _SecondaryTex ("Secondary Texture", 2D) = "white" {}
        _BlendAmount ("Blend Amount", Range(0, 1)) = 0.5
    }

    SubShader
{
    Tags { "RenderType"="Opaque" }

    Pass
    {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
#include "UnityCG.cginc"

struct appdata_t
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
sampler2D _SecondaryTex;
half _BlendAmount;

v2f vert(appdata_t v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    fixed4 mainTexColor = tex2D(_MainTex, i.uv);
    fixed4 secondaryTexColor = tex2D(_SecondaryTex, i.uv);
    return lerp(mainTexColor, secondaryTexColor, _BlendAmount);
}
        ENDCG
    }
}


}
