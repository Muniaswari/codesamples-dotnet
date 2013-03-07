using System;
using System.Collections.Generic;
using System.Text;

namespace Permissions
{
    class Program
    {
        // # Main method
        private static void Main()
        {
            RequestPermissionsSample sampleRequestPermissions = new RequestPermissionsSample();
            sampleRequestPermissions.RequestPermissionsAPIOperation();

            GetAccessTokenSample sampleGetAccessToken = new GetAccessTokenSample();
            sampleGetAccessToken.GetAccessTokenAPIOperation();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
