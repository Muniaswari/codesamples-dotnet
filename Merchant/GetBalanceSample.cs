// # Namespaces  
using System;
using System.Collections.Generic;
// # NuGet Install          
// Visual Studio 2012 and 2010 Command:  
// Install-Package PayPalMerchantSDK        
// Visual Studio 2005 and 2008 (NuGet.exe) Command:   
// install PayPalMerchantSDK      
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using log4net;

// # Sample for GetBalance API    
// The GetBalance API Operation obtains the available balance for a PayPal account.
// This sample code uses Merchant .NET SDK to make API call. You can
// download the SDKs [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class GetBalanceSample
 {
    // # Static constructor for configuration setting
    static GetBalanceSample()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file
    private static ILog logger = LogManager.GetLogger(typeof(GetBalanceSample));
    
     // # GetBalance API Operation
     // The GetBalance API Operation obtains the available balance for a PayPal account
    public GetBalanceResponseType GetBalanceAPIOperation()
     {
         // Create the GetBalanceResponseType object
         GetBalanceResponseType responseGetBalanceResponseType = new GetBalanceResponseType();

         try
         {
             // Create the request object
             GetBalanceRequestType request = new GetBalanceRequestType();

             // Set "Yes" or "NO" to ReturnAllCurrencies
             request.ReturnAllCurrencies = "YES";

             // Create the GetBalanceReq wrapper object 
             GetBalanceReq wrapper = new GetBalanceReq();

             // Pass the request object to the wrapper
             wrapper.GetBalanceRequest = request;

             //Create the PayPalAPIInterfaceServiceService object
             PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

             // # API call
             // Invoke the GetBalance method in service object by passing the wrapper argument
             responseGetBalanceResponseType = service.GetBalance(wrapper);

             if (responseGetBalanceResponseType != null)
             {
                 // Response envelope acknowledgement
                 string acknowledgement = "GetBalance API Operation - ";
                 acknowledgement += responseGetBalanceResponseType.Ack.ToString();
                 logger.Info(acknowledgement + "\n");
                 Console.WriteLine(acknowledgement + "\n");

                 // # Success values
                 if (responseGetBalanceResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                 {
                     object obj = responseGetBalanceResponseType;

                     // Balance Amount 
                     logger.Info("Balance : " + responseGetBalanceResponseType.Balance.currencyID + " " + responseGetBalanceResponseType.Balance.value + "\n");
                     Console.WriteLine("Balance : " + responseGetBalanceResponseType.Balance.currencyID + " " + responseGetBalanceResponseType.Balance.value + "\n");
                 }
                 // # Error Values
                 else
                 {
                     Console.WriteLine(acknowledgement);
                     List<ErrorType> errorMessages = responseGetBalanceResponseType.Errors;
                     foreach (ErrorType error in errorMessages)
                     {
                         logger.Debug(error.LongMessage);
                         Console.WriteLine(error.LongMessage + "\n");
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
         return responseGetBalanceResponseType;
     }
         
 }
