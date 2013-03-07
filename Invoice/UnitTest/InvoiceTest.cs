using NUnit.Framework;
using PayPal.Invoice.Model;

[TestFixture]

public class InvoiceTest
{
    [Test]
    public void CreateAndSendInvoice()
    {
        CreateAndSendInvoiceSample sample = new CreateAndSendInvoiceSample();
        CreateAndSendInvoiceResponse responseCreateAndSendInvoice = sample.CreateAndSendInvoiceAPIOperation();
        Assert.IsNotNull(responseCreateAndSendInvoice);
        Assert.AreEqual(responseCreateAndSendInvoice.responseEnvelope.ack.ToString().Trim().ToUpper(), "SUCCESS");
        Assert.IsNotNull(responseCreateAndSendInvoice.invoiceID);
    }

    [Test]
	public void CreateInvoice() 
    {
        CreateInvoiceSample sample = new CreateInvoiceSample();
        CreateInvoiceResponse reponseCreateInvoice = sample.CreateInvoiceAPIOperation(); 
        Assert.IsNotNull(reponseCreateInvoice);
        Assert.AreEqual(reponseCreateInvoice.responseEnvelope.ack.ToString().Trim().ToUpper(), "SUCCESS");
		Assert.IsNotNull(reponseCreateInvoice.invoiceID);
	}

    [Test]
    public void GetInvoiceDetails()
    {
        GetInvoiceDetailsSample sample = new GetInvoiceDetailsSample();
        GetInvoiceDetailsResponse getInvoiceDetailsResponse = sample.GetInvoiceDetailsAPIOperation();
        Assert.IsNotNull(getInvoiceDetailsResponse);
        Assert.AreEqual(getInvoiceDetailsResponse.responseEnvelope.ack.ToString().Trim().ToUpper(), "SUCCESS");
    }    

    [Test]
	public void SearchInvoices() 
    {
        SearchInvoicesSample sample = new SearchInvoicesSample();
        SearchInvoicesResponse searchInvoicesResponse = sample.SearchInvoicesAPIOperation();
         Assert.IsNotNull(searchInvoicesResponse);
        Assert.AreEqual(searchInvoicesResponse.responseEnvelope.ack.ToString().Trim().ToUpper(), "SUCCESS");			
	}


    [Test]
    public void SendInvoice()
    {
        SendInvoiceSample sample = new SendInvoiceSample();
        SendInvoiceResponse responseSendInvoice = sample.SendInvoiceAPIOperation();
        Assert.IsNotNull(responseSendInvoice);
        // Please change the sample inputs according to the documentation in the sample for the following assertion:
        // Assert.AreEqual(responseSendInvoice.responseEnvelope.ack.ToString().Trim().ToUpper(), "SUCCESS");
    }

    [Test]
    public void UpdateInvoice()
    {
        UpdateInvoiceSample sample = new UpdateInvoiceSample();
        UpdateInvoiceResponse responseUpdateInvoice = sample.UpdateInvoiceAPIOperation();
        Assert.IsNotNull(responseUpdateInvoice);
        Assert.AreEqual(responseUpdateInvoice.responseEnvelope.ack.ToString().Trim().ToUpper(), "SUCCESS");
    }
}

