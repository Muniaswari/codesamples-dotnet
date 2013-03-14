// # Namespaces 
using System;
using System.Collections.Generic;
using System.IO;
// # NuGet Install        
// Visual Studio 2012 and 2010 Command:  
// Install-Package PayPalAdaptivePaymentsSDK    
// Visual Studio 2005 and 2008 (NuGet.exe) Command:  
// install PayPalAdaptivePaymentsSDK    
using PayPal.AdaptivePayments;
using PayPal.AdaptivePayments.Model;
using log4net;

// # Sample for ConvertCurrency API 
// Use the ConvertCurrency API operation to request the current foreign exchange (FX) rate for a specific amount and currency.
// This sample code uses AdaptivePayments .NET SDK to make API call. You can
// download the SDKs [here](https://github.com/paypal/sdk-packages/tree/gh-pages/adaptivepayments-sdk/dotnet)
public class ConvertCurrencySample
{
    // # Static constructor for configuration setting
    static ConvertCurrencySample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(ConvertCurrencySample));    
    
    // # ConvertCurrency API Operation  
    // The ConvertCurrency API operation to request the current foreign exchange (FX) rate for a specific amount and currency      
    public ConvertCurrencyResponse ConvertCurrencyAPIOperation()
    {
        // Create the ConvertCurrencyResponse object
        ConvertCurrencyResponse responseConvertCurrency = new ConvertCurrencyResponse();

        try
        {
            // # ConvertCurrencyRequest 
            // The ConvertCurrencyRequest message enables you to have your  
            // application get an estimated exchange rate for a list of amounts 
            // This API operation does not affect PayPal balances   
            // The code for the language in which errors are returned
            RequestEnvelope envelopeRequest = new RequestEnvelope();
            envelopeRequest.errorLanguage = "en_US";

            // CurrencyTypeList which takes two arguments:  
            //  
            // * `CurrencyCodeType` - The currency code. Allowable values are:  
            // * Australian Dollar - AUD    
            // * Brazilian Real - BRL   
            // `Note:   
            // The Real is supported as a payment currency and currency balance only for Brazilian PayPal accounts.`  
            //  
            // * Canadian Dollar - CAD  
            // * Czech Koruna - CZK 
            // * Danish Krone - DKK 
            // * Euro - EUR 
            // * Hong Kong Dollar - HKD 
            // * Hungarian Forint - HUF 
            // * Israeli New Sheqel - ILS   
            // * Japanese Yen - JPY 
            // * Malaysian Ringgit - MYR    
            // `Note:   
            // The Ringgit is supported as a payment currency and currency balance only for Malaysian PayPal accounts.`
            //  
            // * Mexican Peso - MXN 
            // * Norwegian Krone - NOK  
            // * New Zealand Dollar - NZD   
            // * Philippine Peso - PHP  
            // * Polish Zloty - PLN 
            // * Pound Sterling - GBP   
            // * Singapore Dollar - SGD 
            // * Swedish Krona - SEK    
            // * Swiss Franc - CHF  
            // * Taiwan New Dollar - TWD    
            // * Thai Baht - THB    
            // * Turkish Lira - TRY 
            // `Note:   
            // The Turkish Lira is supported as a payment currency and currency balance only for Turkish PayPal accounts.`
            //  
            // * U.S. Dollar - USD  
            // * `amount`   
            List<CurrencyType> currencyTypeList = new List<CurrencyType>();
            CurrencyType currency = new CurrencyType("USD", Convert.ToDecimal("4.00"));
            currencyTypeList.Add(currency);
            CurrencyList baseAmountList = new CurrencyList(currencyTypeList);

            // CurrencyCodeList which contains  
            //  
            // * `Currency Code` - Allowable values are:    
            // * Australian Dollar - AUD    
            // * Brazilian Real - BRL   
            // `Note:   
            // The Real is supported as a payment currency and currency balance only for Brazilian PayPal accounts.`  
            // * Canadian Dollar - CAD  
            // * Czech Koruna - CZK 
            // * Danish Krone - DKK 
            // * Euro - EUR 
            // * Hong Kong Dollar - HKD 
            // * Hungarian Forint - HUF 
            // * Israeli New Sheqel - ILS   
            // * Japanese Yen - JPY 
            // * Malaysian Ringgit - MYR    
            // `Note:   
            // The Ringgit is supported as a payment currency and currency balance  only for Malaysian PayPal accounts.` 
            // * Mexican Peso - MXN 
            // * Norwegian Krone - NOK  
            // * New Zealand Dollar - NZD   
            // * Philippine Peso - PHP  
            // * Polish Zloty - PLN 
            // * Pound Sterling - GBP   
            // * Singapore Dollar - SGD 
            // * Swedish Krona - SEK    
            // * Swiss Franc - CHF  
            // * Taiwan New Dollar - TWD    
            // * Thai Baht - THB    
            // * Turkish Lira - TRY 
            // `Note:   
            // The Turkish Lira is supported as a payment currency and currency balance only for Turkish PayPal accounts.`   
            //  
            // * U.S. Dollar - USD  
            List<String> currencyCodeList = new List<String>();
            currencyCodeList.Add("GBP");
            CurrencyCodeList convertToCurrencyList = new CurrencyCodeList(currencyCodeList);

            // ConvertCurrencyRequest which takes params:   
            //
            // * `Request Envelope` - Information common to each API operation, such    
            // as the language in which an error message is returned    
            // * `BaseAmountList` - A list of amounts with associated currencies to 
            // be converted.    
            // * `ConvertToCurrencyList` - A list of currencies to convert to.  
            ConvertCurrencyRequest requestConvertCurrency = new ConvertCurrencyRequest(envelopeRequest, baseAmountList, convertToCurrencyList);
            
            // # Create the service wrapper object to make the API call   
            AdaptivePaymentsService service = new AdaptivePaymentsService();

            // # API call   
            // Invoke the ConvertCurrency method in service wrapper object  
            responseConvertCurrency = service.ConvertCurrency(requestConvertCurrency);

            if (responseConvertCurrency != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "ConvertCurrency API operation - ";
                acknowledgement += responseConvertCurrency.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");
 
                // # Success values   
                if (responseConvertCurrency.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    if (responseConvertCurrency.estimatedAmountTable.currencyConversionList != null
                        && responseConvertCurrency.estimatedAmountTable.currencyConversionList.Count > 0)
                    {
                        IEnumerator<CurrencyConversionList> iterator = responseConvertCurrency.estimatedAmountTable.currencyConversionList.GetEnumerator();

                        while (iterator.MoveNext())
                        {
                            CurrencyConversionList currencyConversion = iterator.Current;
                            logger.Info("Amount to be Converted : " + currencyConversion.baseAmount.amount + currencyConversion.baseAmount.code + "\n");
                            Console.WriteLine("Amount to be Converted : " + currencyConversion.baseAmount.amount + currencyConversion.baseAmount.code + "\n");

                            IEnumerator<CurrencyType> currencyIterator = currencyConversion.currencyList.currency.GetEnumerator();

                            while (currencyIterator.MoveNext())
                            {
                                CurrencyType currencyType = currencyIterator.Current;
                                logger.Info("Converted amount : " + currencyType.amount + currencyType.code + "\n");
                                Console.WriteLine("Converted amount : " + currencyType.amount + currencyType.code + "\n");
                            }
                        }
                    }
                }
                // # Error Values 
                else
                {
                    List<ErrorData> errorMessages = responseConvertCurrency.error;
                    foreach (ErrorData error in errorMessages)
                    {
                        logger.Debug("API Error Message : " + error.message);
                        Console.WriteLine("API Error Message : " + error.message + "\n");
                    }
                }
            }
        }
        // # Exception log    
        catch (System.Exception ex)
        {
            // Log the exception message       
            logger.Debug("Error Message : " + ex.Message);
            Console.WriteLine("Error Message : " + ex.Message);
        }

        return responseConvertCurrency;
    }
    
    
}