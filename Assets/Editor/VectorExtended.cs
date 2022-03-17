using UnityEngine;
using System.Collections;

public static class VectorExtended
{
    public static Vector3 FromVector(this Vector3 vec1, Vector3 vec2)
    {
        vec1.x = vec2.x;
        vec1.y = vec2.y;
        vec1.z = vec2.z;
		
        return vec1;
    }
	
    public static Vector3 FromVector(this Vector3 vec1, Vector2 vec2, float z)
    {
        vec1.x = vec2.x;
        vec1.y = vec2.y;
        vec1.z = z;
		
        return vec1;
    }
	
    public static Vector2 FromVector(this Vector2 vec1, Vector2 vec2)
    {
        vec1.x = vec2.x;
        vec1.y = vec2.y;
		
        return vec1;
    }
	
    public static Vector2 FromVector(this Vector2 vec1, Vector3 vec2)
    {
        vec1.x = vec2.x;
        vec1.y = vec2.y;
		
        return vec1;
    }
}
