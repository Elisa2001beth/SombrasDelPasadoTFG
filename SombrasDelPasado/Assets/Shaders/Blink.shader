Shader "Custom/EyeBlinkFade"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BlinkAmount("Blink Amount", Range(0, 1)) = 0 // Cantidad de parpadeo
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

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

                sampler2D _MainTex;
                float _BlinkAmount;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                half4 frag(v2f i) : SV_Target
                {
                    // Calcular el color final
                    half4 color = tex2D(_MainTex, i.uv);
                    // Aplicar el efecto de parpadeo
                    color.a *= 1.0 - _BlinkAmount;
                    return color;
                }
                ENDCG
            }
        }
}
