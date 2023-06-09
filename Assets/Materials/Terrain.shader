Shader "Custom/Terrain"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        const static int maxColorCount = 10;
        int baseColorCount;
        float3 baseColors[maxColorCount];
        float baseStartHeights[maxColorCount];
        float minHeight;
        float maxHeight;

        struct Input
        {
            float3 worldPos;
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        float inverseLerp(float a, float b, float value)
        {
            return saturate((value - a) / (b - a));
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float heightPercent = inverseLerp(minHeight, maxHeight, IN.worldPos.y);
            for (int i = 0; i < baseColorCount; i++)
            {
                float drawStrength = saturate(sign(heightPercent - baseStartHeights[i]));
                o.Albedo = o.Albedo * (1 - drawStrength) + baseColors[i] * drawStrength;
            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}
