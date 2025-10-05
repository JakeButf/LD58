Shader "Custom/Painting"
{
    Properties
    {
        _MainTex ("Source (RenderTexture)", 2D) = "white" {}
        _BrushSize ("Brush Size", Range(1, 10)) = 4
        _Intensity ("Smear Intensity", Range(0, 1)) = 0.6
        _Detail ("Detail Sharpness", Range(0, 2)) = 1
        _CellScale ("Voronoi Cell Scale", Range(1, 30)) = 8
        _VoronoiMix ("Voronoi Influence", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Overlay" }
        Pass
        {
            Name "OilPaintVoronoiPass"
            ZTest Always Cull Off ZWrite Off
            HLSLPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _BrushSize;
            float _Intensity;
            float _Detail;
            float _CellScale;
            float _VoronoiMix;

            // --- Simple pseudo-random hash ---
            float2 hash(float2 p)
            {
                p = frac(p * float2(123.34, 456.21));
                p += dot(p, p + 45.32);
                return frac(float2(p.x * p.y, p.x + p.y));
            }

            // --- Voronoi function: returns distance & cell center ---
            float2 voronoi(float2 uv, out float2 id)
            {
                uv *= _CellScale;
                float2 gv = frac(uv);
                id = floor(uv);
                float minDist = 8.0;
                float2 bestId = 0;

                [unroll(3)]
                for (int y = -1; y <= 1; y++)
                {
                    [unroll(3)]
                    for (int x = -1; x <= 1; x++)
                    {
                        float2 cell = id + float2(x, y);
                        float2 rand = hash(cell);
                        float2 offset = float2(x, y) + rand - gv;
                        float d = dot(offset, offset);
                        if (d < minDist)
                        {
                            minDist = d;
                            bestId = cell + rand;
                        }
                    }
                }

                id = bestId;
                return float2(sqrt(minDist), 0);
            }

            // --- Smear / oil-like average ---
            float3 SampleOilColor(float2 uv)
            {
                int radius = (int)_BrushSize;
                float3 avg = 0;
                float count = 0;
                for (int y = -radius; y <= radius; y++)
                {
                    for (int x = -radius; x <= radius; x++)
                    {
                        float2 offset = float2(x, y) * _MainTex_TexelSize.xy;
                        avg += tex2D(_MainTex, uv + offset).rgb;
                        count += 1;
                    }
                }
                return avg / count;
            }

            fixed4 frag(v2f_img i) : SV_Target
            {
                float3 baseCol = tex2D(_MainTex, i.uv).rgb;
                float3 smeared = SampleOilColor(i.uv);

                // Voronoi distortion
                float2 id;
                float2 cell = voronoi(i.uv, id);
                float3 cellColor = tex2D(_MainTex, frac(id / _CellScale)).rgb;

                // blend voronoi and smeared colors
                float3 mixCol = lerp(smeared, cellColor, _VoronoiMix);

                // add grainy pigment variation
                float2 noiseUV = i.uv * 128.0;
                float grain = frac(sin(dot(noiseUV, float2(12.9898,78.233))) * 43758.5453);

                float3 final = lerp(baseCol, mixCol, _Intensity);
                final = pow(final, 1 + (grain - 0.5) * 0.1 * _Detail);

                return float4(final, 1);
            }
            ENDHLSL
        }
    }
    FallBack Off
}
