using UnityEngine;

public static class RandomPosition
{
    public static Vector3 InCube(Vector3 leftDownPoint, Vector3 rightUpPoint)
    {
        return new Vector3(
            Random.Range(leftDownPoint.x, rightUpPoint.x),
            Random.Range(leftDownPoint.y, rightUpPoint.y),
            Random.Range(leftDownPoint.z, rightUpPoint.z));
    }

    public static Vector3 InRing(Vector3 center, Vector2 radiusVolume)
    {
        var position = center + Random.insideUnitSphere * Random.Range(radiusVolume.x, radiusVolume.y);
        position.y = center.y;
        return position;
    }
}
