using NUnit.Framework;
using PayPal.PayPalAPIInterfaceService.Model;

[TestFixture]
public class MerchantTest
{
    [Test]
    public void CreateRecurringPaymentsProfile()
    {
        CreateRecurringPaymentsProfileSample sample = new CreateRecurringPaymentsProfileSample();
        CreateRecurringPaymentsProfileResponseType responseCreateRecurringPaymentsProfileResponseType = sample.CreateRecurringPaymentsProfileAPIOperation();
        Assert.IsNotNull(responseCreateRecurringPaymentsProfileResponseType);
        Assert.AreEqual(responseCreateRecurringPaymentsProfileResponseType.Ack.ToString().ToUpper(), "SUCCESS");
        Assert.IsNotNull(responseCreateRecurringPaymentsProfileResponseType.CreateRecurringPaymentsProfileResponseDetails.ProfileID);
    }
    
    [Test]
    public void DoAuthorization()
    {
        DoAuthorizationSample sample = new DoAuthorizationSample();
        DoAuthorizationResponseType responseDoAuthorizationResponseType = sample.DoAuthorizationAPIOperation();
        Assert.IsNotNull(responseDoAuthorizationResponseType);
        // Please change the sample inputs according to the documentation in the sample for the following assertions:
        // Assert.AreEqual(responseDoAuthorizationResponseType.Ack.ToString().ToUpper(), "SUCCESS");
        // Assert.IsNotNull(responseDoAuthorizationResponseType.TransactionID);
    }


    [Test]
    public void DoCapture()
    {
        DoCaptureSample sample = new DoCaptureSample();
        DoCaptureResponseType responseDoCaptureResponseType = sample.DoCaptureAPIOperation();
        Assert.IsNotNull(responseDoCaptureResponseType);
        // Please change the sample inputs according to the documentation in the sample for the following assertion:
        // Assert.AreEqual(responseDoCaptureResponseType.Ack.ToString().ToUpper(), "SUCCESS");
    }

    [Test]
    public void DoDirectPayment()
    {
        DoDirectPaymentSample sample = new DoDirectPaymentSample();
        DoDirectPaymentResponseType responseDoDirectPaymentResponseType = sample.DoDirectPaymentAPIOperation();
        Assert.IsNotNull(responseDoDirectPaymentResponseType);
        Assert.AreEqual(responseDoDirectPaymentResponseType.Ack.ToString().ToUpper(), "SUCCESS");
        Assert.IsNotNull(responseDoDirectPaymentResponseType.TransactionID);
    }

    [Test]
    public void DoExpressCheckoutPayment()
    {
        DoExpressCheckoutPaymentSample sample = new DoExpressCheckoutPaymentSample();
        DoExpressCheckoutPaymentResponseType responseDoExpressCheckoutPaymentResponseType = sample.DoExpressCheckoutPaymentAPIOperation();
        Assert.IsNotNull(responseDoExpressCheckoutPaymentResponseType);
        // Please change the sample inputs according to the documentation in the sample for the following assertions:
        // Assert.AreEqual(responseDoExpressCheckoutPaymentResponseType.Ack.ToString().ToUpper(), "SUCCESS");
        // Assert.IsNotNull(responseDoExpressCheckoutPaymentResponseType.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0].TransactionID);
    }

    [Test]
    public void DoReauthorization()
    {
        DoReauthorizationSample sample = new DoReauthorizationSample();
        DoReauthorizationResponseType responseDoReauthorizationResponseType = sample.DoReauthorizationAPIOperation();
        Assert.IsNotNull(responseDoReauthorizationResponseType);
        // Please change the sample inputs according to the documentation in the sample for the following assertions:
        // Assert.AreEqual(responseDoReauthorizationResponseType.Ack.ToString().ToUpper(), "SUCCESS");
        // Assert.IsNotNull(responseDoReauthorizationResponseType.AuthorizationID);
    }

    [Test]
    public void DoReferenceTransaction()
    {
        DoReferenceTransactionSample sample = new DoReferenceTransactionSample();
        DoReferenceTransactionResponseType responseDoReferenceTransactionResponseType = sample.DoReferenceTransactionAPIOperation();
        Assert.IsNotNull(responseDoReferenceTransactionResponseType);
        Assert.AreEqual(responseDoReferenceTransactionResponseType.Ack.ToString().ToUpper(), "SUCCESS");
    }

    [Test]
    public void DoVoid()
    {
        DoVoidSample sample = new DoVoidSample();
        DoVoidResponseType responseDoVoidResponseType = sample.DoVoidAPIOperation();
        Assert.IsNotNull(responseDoVoidResponseType);
        // Please change the sample inputs according to the documentation in the sample for the following assertion:
        // Assert.AreEqual(responseDoVoidResponseType.Ack.ToString().ToUpper(), "SUCCESS");
    }

    [Test]
    public void GetBalance()
    {
        GetBalanceSample sample = new GetBalanceSample();
        GetBalanceResponseType responseGetBalanceResponseType = sample.GetBalanceAPIOperation();
        Assert.IsNotNull(responseGetBalanceResponseType);
        // Please change the sample inputs according to the documentation in the sample for the following assertions:
        // Assert.AreEqual(responseGetBalanceResponseType.Ack.ToString().ToUpper(), "SUCCESS");
        // Assert.IsTrue(responseGetBalanceResponseType.BalanceHoldings.Count > 0);
    }

    [Test]
    public void GetExpressCheckoutDetails()
    {
        GetExpressCheckoutDetailsSample sample = new GetExpressCheckoutDetailsSample();
        GetExpressCheckoutDetailsResponseType responseGetExpressCheckoutDetailsResponseType = sample.GetExpressCheckoutDetailsAPIOperation();
        Assert.IsNotNull(responseGetExpressCheckoutDetailsResponseType);
        // Please change the sample inputs according to the documentation in the sample for the following assertion:
        // Assert.AreEqual(responseGetExpressCheckoutDetailsResponseType.Ack.ToString().ToUpper(), "SUCCESS");
    }

    [Test]
    public void GetTransactionDetails()
    {
        GetTransactionDetailsSample sample = new GetTransactionDetailsSample();
        GetTransactionDetailsResponseType responseGetTransactionDetailsResponseType = sample.GetTransactionDetailsAPIOperation();
        Assert.IsNotNull(responseGetTransactionDetailsResponseType);
        Assert.AreEqual(responseGetTransactionDetailsResponseType.Ack.ToString().ToUpper(), "SUCCESS");
        Assert.IsNotNull(responseGetTransactionDetailsResponseType.PaymentTransactionDetails.PayerInfo.PayerID);
    }

    [Test]
    public void MassPay()
    {
        MassPaySample sample = new MassPaySample();
        MassPayResponseType responseMassPayResponseType = sample.MassPayAPIOperation();
        Assert.IsNotNull(responseMassPayResponseType);
        Assert.AreEqual(responseMassPayResponseType.Ack.ToString().ToUpper(), "SUCCESS");
    }

    [Test]
    public void RefundTransaction()
    {
        RefundTransactionSample sample = new RefundTransactionSample();
        RefundTransactionResponseType responseRefundTransactionResponseType = sample.RefundTransactionAPIOperation();
        Assert.IsNotNull(responseRefundTransactionResponseType);
        // Please change the sample inputs according to the documentation in the sample for the following assertion:
        // Assert.AreEqual(responseRefundTransactionResponseType.Ack.ToString().ToUpper(), "SUCCESS");
    }

    [Test]
	public void SetExpressCheckout() 
    {
        SetExpressCheckoutSample sample = new SetExpressCheckoutSample();
        SetExpressCheckoutResponseType responseSetExpressCheckoutResponseType = sample.SetExpressCheckoutAPIOperation();
        Assert.IsNotNull(responseSetExpressCheckoutResponseType);
        Assert.AreEqual(responseSetExpressCheckoutResponseType.Ack.ToString().ToUpper(), "SUCCESS");	
        Assert.IsNotNull(responseSetExpressCheckoutResponseType.Token);	
	}

    [Test]
    public void TransactionSearch()
    {
        TransactionSearchSample sample = new TransactionSearchSample();
        TransactionSearchResponseType responseTransactionSearchResponseType = sample.TransactionSearchAPIOperation();
        Assert.IsNotNull(responseTransactionSearchResponseType);
        // Please change the sample inputs according to the documentation in the sample for the following assertion:
        // Assert.AreEqual(responseTransactionSearchResponseType.Ack.ToString().ToUpper(), "SUCCESS");	
	}
}

