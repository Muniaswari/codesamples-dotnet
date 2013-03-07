using NUnit.Framework;
using PayPal.Permissions.Model;

[TestFixture]
public class PermissionsTest
{
    [Test] 
    public void GetAccessToken()
    {
        GetAccessTokenSample sample = new GetAccessTokenSample();
        GetAccessTokenResponse getAccessTokenResponse = sample.GetAccessTokenAPIOperation();
        Assert.IsNotNull(getAccessTokenResponse);
        // Please change the sample inputs according to the documentation in the sample for the following assertions:
        // Assert.AreEqual(getAccessTokenResponse.responseEnvelope.ack.ToString().Trim().ToUpper(), "SUCCESS");
        // Assert.IsNotNull(getAccessTokenResponse.token);
    }  

    [Test]
    public void RequestPermissions() 
    {
        RequestPermissionsSample sample = new RequestPermissionsSample();
        RequestPermissionsResponse requestPermissionsResponse = sample.RequestPermissionsAPIOperation();
        Assert.IsNotNull(requestPermissionsResponse);
        Assert.AreEqual(requestPermissionsResponse.responseEnvelope.ack.ToString().Trim().ToUpper(), "SUCCESS");
		Assert.IsNotNull(requestPermissionsResponse.token);
	}
}
