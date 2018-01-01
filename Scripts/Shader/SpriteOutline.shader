// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "QipaWorld/SpriteOutline"
{
	Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _EdgeColor ("OutlineColor", Color) = (0.482,1.0,0.373,0.5)
        _Treshold ("Treshold", Float) = 0.6
        _DeltaUV ("Delta", Float) = 2
}

Category {
        Tags { "Queue"="Transparent+5" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off Lighting Off ZWrite Off ZTest Always
        BindChannels {
                Bind "Vertex", vertex
                Bind "Color", color
                Bind "TexCoord", texcoord
        }
        
        // ---- Fragment program cards
        SubShader {
                Pass {
                
                        CGPROGRAM
                        #pragma vertex vert
                        #pragma fragment frag
                        #pragma fragmentoption ARB_precision_hint_fastest
                        #include "UnityCG.cginc"

                        sampler2D _MainTex;
                        float4 _MainTex_TexelSize;
                        float _Treshold;
                        float _DeltaUV;
                        fixed4 _EdgeColor;
						fixed4 _Color;
                        struct appdata_t {
                                float4 vertex : POSITION;
                                fixed4 color : COLOR;
                                float2 texcoord : TEXCOORD0;
                        };

                        struct v2f {
                                float4 pos : POSITION;
								fixed4 color    : COLOR;
                                float2 uv[5] : TEXCOORD0;
                        };

                        v2f vert(appdata_t v )
                        {
                                v2f o;
                                o.pos = UnityObjectToClipPos (v.vertex);
                                half2 uv = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord );
								o.color = v.color;
    
								fixed delta = _DeltaUV * min(_MainTex_TexelSize.y, _MainTex_TexelSize.x);
                      
                                o.uv[0] = uv;
                                o.uv[1] = uv + fixed2(0.0, delta); //up
                                o.uv[2] = uv + fixed2(-delta,0.0); //left
                                o.uv[3] = uv + fixed2(0.0,-delta); //bottom
                                o.uv[4] = uv + fixed2(delta,0.0); //right
                                return o;
                        }

                        fixed4 frag (v2f i) : COLOR
                        {
                                fixed4 original = tex2D(_MainTex, i.uv[0]);
								
								fixed alpha = original.a;         
								original.rgb *= i.color;
								if (_Treshold != 0) {
									fixed p1 = tex2D(_MainTex, i.uv[1]).a;
									fixed p2 = tex2D(_MainTex, i.uv[2]).a;
									fixed p3 = tex2D(_MainTex, i.uv[3]).a;
									fixed p4 = tex2D(_MainTex, i.uv[4]).a;

									alpha = p1 + p2 + p3 + p4 + alpha;
									alpha /= 5;
									if (original.a < 0.5f && alpha < _Treshold)
									{
										fixed4 col = _EdgeColor * 1.5f;
										original.rgb = col.rgb;
										original.a = alpha * 2;
									}
								}
                                return original;
                        }
                        ENDCG 
                }
        }         
}
}