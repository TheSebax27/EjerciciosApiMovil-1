package crc64e95e69e34d869711;


public class LocationListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.location.LocationListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLocationChanged:(Landroid/location/Location;)V:GetOnLocationChanged_Landroid_location_Location_Handler:Android.Gms.Location.ILocationListenerInvoker, Xamarin.GooglePlayServices.Location\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Location.LocationListener, Xamarin.GooglePlayServices.Location", LocationListener.class, __md_methods);
	}

	public LocationListener ()
	{
		super ();
		if (getClass () == LocationListener.class) {
			mono.android.TypeManager.Activate ("Android.Gms.Location.LocationListener, Xamarin.GooglePlayServices.Location", "", this, new java.lang.Object[] {  });
		}
	}

	public void onLocationChanged (android.location.Location p0)
	{
		n_onLocationChanged (p0);
	}

	private native void n_onLocationChanged (android.location.Location p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
