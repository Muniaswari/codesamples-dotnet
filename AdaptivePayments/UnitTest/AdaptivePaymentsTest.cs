using NUnit.Framework;
using PayPal.AdaptivePayments.Model;

[TestFixture]
public class AdaptivePaymentsTest
{    
    [Test]
	public void ConvertCurrency() 
    {
        ConvertCurrencySample sample = new ConvertCurrencySample();
		ConvertCurrencyResponse responseConvertCurrency = sample.ConvertCurrencyAPIOperation();
        Assert.IsNotNull(responseConvertCurrency);
        Assert.IsTrue(responseConvertCurrency.estimatedAmountTable.currencyConversionList.Count > 0);		
	}

    [Test]
    public void ExecutePayment()
    {
        ExecutePaymentSample sample = new ExecutePaymentSample();
        ExecutePaymentResponse responseExecutePayment = sample.ExecutePaymentAPIOperation();
        Assert.IsNotNull(responseExecutePayment);
        // Please change the sample inputs according to the documentation in the sample for the following assertion:
        // Assert.AreEqual(responseExecutePayment.responseEnvelope.ack.ToString().ToUpper(), "SUCCESS");	
    }    
    
    [Test]
    public void GetPaymentOptions()
    {
        GetPaymentOptionsSample sample = new GetPaymentOptionsSample();
        GetPaymentOptionsResponse responseGetPaymentOptions = sample.GetPaymentOptionsAPIOperation();
        Assert.IsNotNull(responseGetPaymentOptions);
        Assert.AreEqual(responseGetPaymentOptions.responseEnvelope.ack.ToString().ToUpper(), "SUCCESS");
    }

	[Test]
    public void PaymentDetails() 
    {
        PaymentDetailsSample sample = new PaymentDetailsSample();
        PaymentDetailsResponse responsePaymentDetails = sample.PaymentDetailsAPIOperation();
        Assert.IsNotNull(responsePaymentDetails);
        Assert.IsNotNull(responsePaymentDetails.status);
    }

    [Test]
    public void Pay()
    {
        PaySample sample = new PaySample();
        PayRequest requestPay = sample.SimplePayment();
        PayResponse responsePaymentDetails = sample.PayAPIOperations(requestPay);
        Assert.IsNotNull(responsePaymentDetails);
        Assert.AreEqual(responsePaymentDetails.responseEnvelope.ack.ToString().ToUpper(), "SUCCESS");

        requestPay = sample.ParallelPayment();
        responsePaymentDetails = sample.PayAPIOperations(requestPay);
        Assert.IsNotNull(responsePaymentDetails);
        Assert.AreEqual(responsePaymentDetails.responseEnvelope.ack.ToString().ToUpper(), "SUCCESS");

        requestPay = sample.ChainPayment();
        responsePaymentDetails = sample.PayAPIOperations(requestPay);
        Assert.IsNotNull(responsePaymentDetails);
        Assert.AreEqual(responsePaymentDetails.responseEnvelope.ack.ToString().ToUpper(), "SUCCESS");
    }
      
	[Test]
	public void PreapprovalDetails() 
    {
        PreapprovalDetailsSample sample = new PreapprovalDetailsSample();
        PreapprovalDetailsResponse responsePreapprovalDetails = sample.PreapprovalDetailsAPIOperation();
        Assert.IsNotNull(responsePreapprovalDetails);
        Assert.IsNotNull(responsePreapprovalDetails.status);
	}

    [Test]
    public void Preapproval()
    {
        PreapprovalSample sample = new PreapprovalSample();
        PreapprovalResponse responsePreapproval = sample.PreapprovalAPIOperation();
        Assert.IsNotNull(responsePreapproval);
        Assert.IsNotNull(responsePreapproval.preapprovalKey);
    }

    [Test]
    public void Refund()
    {
        RefundSample sample = new RefundSample();
        RefundResponse responseRefund = sample.RefundAPIOperation();
        Assert.IsNotNull(responseRefund);
        Assert.AreEqual(responseRefund.responseEnvelope.ack.ToString().ToUpper(), "SUCCESS");
    }
       
    [Test]
	public void SetPaymentOptions() 
    {
        SetPaymentOptionsSample sample = new SetPaymentOptionsSample();
        SetPaymentOptionsResponse responseSetPaymentOptions = sample.SetPaymentOptionsAPIOperation();
        Assert.IsNotNull(responseSetPaymentOptions);
        // Please change the sample inputs according to the documentation in the sample for the following assertion:
        // Assert.AreEqual(responseSetPaymentOptions.responseEnvelope.ack.ToString().ToUpper(), "SUCCESS");		
	}
}

