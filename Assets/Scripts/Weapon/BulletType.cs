namespace Weapon{
public enum EAmmoType{
		Calliber_5,
		Calliber_9mm,

		Calliber_762mm,
		Calliber_556mm
	}

public class  Pair<T1,T2>{
    public T1 first;
    public T2 second;

	public Pair(T1 f, T2 s)
	{
		first = f;
		second = s;
	}

}

} //namespace Weapon