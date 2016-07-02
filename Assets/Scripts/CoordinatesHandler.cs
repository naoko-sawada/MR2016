using UnityEngine;
using System.Collections;


public class CoordinatesHandler
{
	/// <summary>
	/// Class defining useful functions to convert the common latitude and longitude to cartesian coordinates.
	/// Lat & lng need to be converted to radians, lat to geodetic lat and then LLA2ECEF can be applied in order to obtain the cartesian coordinates.
	/// Formulas from Mathworks and Wikipedia.
	/// </summary>

	//Ellipsoid model (Earth not being a perfect sphere)

	//WGS84 ellipsoid constants
	public static readonly float WGS84A = 6378137f; //Equatorial radius
	public static readonly float WGS84B = 6356752.3142f; // Polar radius
	public static readonly float WGS84E = 0.081819190842622f; // Eccentricity
	public static readonly float WGS84F = 1.0f / 298.25722f; // Flattening

	/* LLA2ECEF from MathWorks
    https://en.wikipedia.org/wiki/Latitude#Latitude_on_the_ellipsoid
    https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#From_geodetic_to_ECEF_coordinates
    https://en.wikipedia.org/wiki/ECEF
    https://gist.github.com/klucar/1536056
    http://www.oc.nps.edu/oc2902w/coord/coordcvt.pdf

    x = ECEF X-coordinate (m)
    y = ECEF Y-coordinate (m)
    z = ECEF Z-coordinate (m)
    lat = geodetic latitude (radians)
    lon = longitude (radians)
    alt = height above WGS84 ellipsoid (m)
    */

	public static Vector3 LLA2ECEF(float lat, float lng, float alt)
	/// Converts geocentric latitude in degrees, longitude in degrees and altitude to cartesian coordinates, using the ellipsoid model
	{
		float latRad = deg2rad(lat);
		float lngRad = deg2rad(lng);
		float geodesLatRad = GeocentLat2GeodesLat(latRad, alt);
		float x, y, z;
		float WGS84N = ComputeN(geodesLatRad);
		x = (WGS84N + alt) * Mathf.Cos(geodesLatRad) * Mathf.Cos(lngRad);
		y = (WGS84N + alt) * Mathf.Cos(geodesLatRad) * Mathf.Sin(lngRad);
		z = ((1 - Mathf.Pow(WGS84E, 2)) * WGS84N + alt) * Mathf.Sin(geodesLatRad);
		//return new Vector3(x, y, z);
		return new Vector3(x, z, y);
	}

	public static float GeocentLat2GeodesLat(float geocentLat, float alt)
	/// Converts geocentric latitude in radians to geodetic latitude in radians
	{
		float WGS84N = ComputeN(geocentLat);
		return Mathf.Atan2(Mathf.Tan(geocentLat), (1 - Mathf.Pow(WGS84E, 2)));
	}

	public static float ComputeN(float lat)
	/// The normal N is the distance from the surface to the Z-axis along the ellipsoid normal and it is computed using geodetic latitude in radians
	{
		return WGS84A / Mathf.Sqrt(1 - Mathf.Pow(WGS84E, 2) * Mathf.Pow(Mathf.Sin(lat), 2));
	}

	public static float deg2rad(float deg)
	/// Converts degrees to radians
	{
		return deg * Mathf.Deg2Rad;
	}
}
