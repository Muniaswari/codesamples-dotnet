using NUnit.Framework;
using PayPal.AdaptiveAccounts.Model;

[TestFixture]
public class CreateAccountSampleTest
{
    [Test]
    public void CreateAccount()
    {
        CreateAccountSample sample = new CreateAccountSample();
        CreateAccountRequest requestCreateAccount = sample.CreateAccount();
        Assert.IsNotNull(requestCreateAccount);
    }

    [Test]
    public void CreateBusinessAccount()
    {
        CreateAccountSample sample = new CreateAccountSample();
        CreateAccountRequest requestCreateAccount = sample.CreateBusinessAccount();
        CreateAccountResponse responseCreateAccount = sample.CreateAccountAPIOperations(requestCreateAccount);
        Assert.AreEqual(responseCreateAccount.responseEnvelope.ack.ToString().Trim().ToUpper(), "SUCCESS");
    }

    [Test]
    public void CreatePersonalAccount()
    {
        CreateAccountSample sample = new CreateAccountSample();
        CreateAccountRequest requestCreateAccount = sample.CreatePersonalAccount();
        CreateAccountResponse responseCreateAccount = sample.CreateAccountAPIOperations(requestCreateAccount);
        Assert.AreEqual(responseCreateAccount.responseEnvelope.ack.ToString().Trim().ToUpper(), "SUCCESS");
    }

    [Test]
    public void CreatePremierAccount()
    {
        CreateAccountSample sample = new CreateAccountSample();
        CreateAccountRequest requestCreateAccount = sample.CreatePremierAccount();
        CreateAccountResponse responseCreateAccount = sample.CreateAccountAPIOperations(requestCreateAccount);
        Assert.AreEqual(responseCreateAccount.responseEnvelope.ack.ToString().Trim().ToUpper(), "SUCCESS");
    }
}


