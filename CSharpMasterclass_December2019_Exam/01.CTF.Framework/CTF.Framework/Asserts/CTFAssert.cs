namespace CTF.Framework.Asserts
{
    using System;

    // ReSharper disable once InconsistentNaming
    public abstract class CTFAssert
    {
        public static void AreEqual(object a, object b)
        {
            throw new NotImplementedException();
        }

        public static void AreNotEqual(object a, object b)
        {
            throw new NotImplementedException();
        }

        public static void Throws<T>(Func<bool> condition)
        {
            throw new NotImplementedException();
        }
    }
}
