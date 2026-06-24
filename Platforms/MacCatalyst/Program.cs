#if MACCATALYST
using ObjCRuntime;
using UIKit;

namespace SubstitutivaJogoVelhaMVVM;

public class Program
{
    static void Main(string[] args)
    {
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
#endif
