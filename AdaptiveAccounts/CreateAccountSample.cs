// # Namespaces 
using System;
using System.Collections.Generic;
using System.IO;
// # NuGet Install        
// Visual Studio 2012 and 2010 Command:  
// Install-Package PayPalAdaptiveAccountsSDK    
// Visual Studio 2005 and 2008 (NuGet.exe) Command:  
// install PayPalAdaptiveAccountsSDK    
using PayPal.AdaptiveAccounts;
using PayPal.AdaptiveAccounts.Model;
using log4net;

// # Sample for CreateAccount API Operations    
// This sample code uses AdaptiveAccounts .NET SDK to make API call. You can
// download the SDKs [here](https://github.com/paypal/sdk-packages/tree/gh-pages/adaptiveaccounts-sdk/dotnet)       
public class CreateAccountSample
{
    static CreateAccountSample()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file    
    private static ILog logger = LogManager.GetLogger(typeof(CreateAccountSample));
    
    // # Create Account   	
    public CreateAccountRequest CreateAccount() 
    {
        // Request envelope object
        RequestEnvelope envelopeRequest = new RequestEnvelope();	
	
        // The name of the person for whom the PayPal account is created:
        //  
        // * FirstName  		
        // * LastName   		
        NameType name = new NameType("John", "David");	
	
        // The address to be associated with the PayPal account:    
        //      
        // * Street1    		
        // * countrycode    		
        // * city   		
        // * state  		
        // * postalcode 		
        AddressType address = new AddressType("Ape Way", "US");		
        address.city = "Austin";		
        address.state = "TX";		
        address.postalCode ="78750";

        // The CreateAccountRequest contains the information required    
        // to create a PayPal account for a business customer   
        // Instantiating createAccountRequest with mandatory arguments: 
		//      
        // * requesteEvelope    		
        // * name   		
        // * address    		
        // * preferredlanguagecode  	
        CreateAccountRequest createAccountRequest = new CreateAccountRequest(envelopeRequest, name, address, "en_US");		
        
        // The type of account to be created    
        // Allowable values:   
 		//      
        // * Personal    	
        // * Premier        	
        // * Business         		        
        createAccountRequest.accountType = "Personal";
		
        // The code of the country to be associated with the account       		
        createAccountRequest.citizenshipCountryCode = "US";		
        
        // Phone Number to be associated with the account   		
        createAccountRequest.contactPhoneNumber ="5126914160";		

        // The three letter code for the currency to be associated with the account 	
        createAccountRequest.currencyCode ="USD";   		
        
        // Email address of person for whom the PayPal account is created   		
        createAccountRequest.emailAddress = "test@paypal.com";  	
	
        // This attribute determines whether a key or a URL is returned for the redirect URL          
        createAccountRequest.registrationType = "Web";

        // IPN URL
        //      
        // * PayPal Instant Payment Notification is a call back system that is initiated when a transaction is completed        
        // * The transaction related IPN variables will be received on the call back URL specified in the request       
        // * The IPN variables have to be sent back to the PayPal system for validation, upon validation PayPal will send a response string "VERIFIED" or "INVALID"     
        // * PayPal would continuously resend IPN if a wrong IPN is sent        
        createAccountRequest.notificationURL = "http://IPNhost";

        return createAccountRequest;
    }

    // # Create Personal Account   	
    public CreateAccountRequest CreatePersonalAccount()
    {
        // Create the CreateAccountRequest object
        CreateAccountRequest createAccountRequest = CreateAccount();

        // Used for configuration settings for the web flow 	
        CreateAccountWebOptionsType createAccountWebOptions = new CreateAccountWebOptionsType();

        // The URL to which the business redirects the PayPal user for PayPal account setup completion      		
        createAccountWebOptions.returnUrl = "http://localhost";	  

        createAccountRequest.createAccountWebOptions = createAccountWebOptions;		
        CreateAccountAPIOperations(createAccountRequest);

        return createAccountRequest;
    }	

    // # Creating PremierAccount   	
    public CreateAccountRequest CreatePremierAccount() 
    {
        // Create the CreateAccountRequest object
        CreateAccountRequest createAccountRequest = CreateAccount();

        // Set the account type as Premier  	
        createAccountRequest.accountType = "Premier";		
        CreateAccountAPIOperations(createAccountRequest);

        return createAccountRequest;
    }

    // # Creating Business Account      	
    public CreateAccountRequest CreateBusinessAccount() 
    {
        // Create the CreateAccountRequest object
        CreateAccountRequest createAccountRequest = CreateAccount();

        // Set the account type as Business  	
        createAccountRequest.accountType = "Business";
        
        // The Address of the business for which the PayPal account is created: 
        //       
        // * Street1    
        // * countrycode    
        // * city   
        // * state  
        // * postalcode 
        AddressType businessAddress = new AddressType("Ape Way", "US");		
        businessAddress.city = "Austin";		
        businessAddress.state = "TX";		
        businessAddress.postalCode = "78750";
        
        // Business Account mandatory arguments: 
        //      
        // * Business Name  
        // * Business Address   
        // * Contact Phone Number   
        BusinessInfoType businessInfo = new BusinessInfoType("Toy shop", businessAddress, "5126914161");
        
        // The type of the business for which the PayPal account is created 
        // Allowable values: 
        //      
        // * CORPORATION             
        // * GOVERNMENT           
        // * INDIVIDUAL            
        // * NONPROFIT                    
        // * PARTNERSHIP           
        // * PROPRIETORSHIP          
        businessInfo.businessType = BusinessType.INDIVIDUAL;
        
        // The average monthly transaction volume of the business   
        // Required for all countries except Japan and Australia    
        businessInfo.averageMonthlyVolume = Convert.ToDecimal("400");
        
        // The average price per transaction    
        // Required for all countries except Japan and Australia    
        businessInfo.averagePrice = Convert.ToDecimal("30");
        
        // The email address of customer service department of the business  
        businessInfo.customerServiceEmail = "customercare@toy.com";
        
        // Required for US accounts 
        businessInfo.customerServicePhone = "5126914162";
        
        // The category code for the business          
        // Required unless you specify both category and subcategory               
        // PayPal uses the industry standard Merchant Category Codes                
        businessInfo.merchantCategoryCode = Convert.ToInt32("1520");
        
        // The date of establishment for the business      
        // Optional for France         
        // For Business Accounts in the following countries:   
        //           
        // * United States      
        // * United Kingdom          
        // * Canada      
        // * Germany                    
        // * Spain              
        // * Italy            
        // * Netherlands                 
        // * Czech Republic  
        // * Sweden     
        // * Denmark        
        // Format needs to be YYYY-MM-DD    
        businessInfo.dateOfEstablishment = "2011-12-21";
        
        // The percentage of online sales for the business from 0 through 100   
        // Required for business accounts in the following countries:  
        //      
        // * United States              
        // * Canada           
        // * United Kingdom           
        // * France           
        // * Czech Republic               
        // * New Zealand                    
        // * Switzerland                    
        // * Israel  
        businessInfo.percentageRevenueFromOnline = Convert.ToInt32("70");
		
        // The venue type for sales. Required for business accounts in all countries except Czech Republic and Australia    
        // Allowable values: 
        //                      		
        // * WEB            		
        // * EBAY          		
        // * OTHER_MARKETPLACE        		
        // * OTHER        		
        List<SalesVenueType?> salesVenueList = new List<SalesVenueType?>();
        salesVenueList.Add(SalesVenueType.OTHER);
        businessInfo.salesVenue= salesVenueList;

        // A description of the sales venue                   
        // Required if salesVenue is OTHER for all countries except Czech Republic and Australia                        
        businessInfo.salesVenueDesc = "Other sales venue type";

        createAccountRequest.businessInfo = businessInfo;
        CreateAccountAPIOperations(createAccountRequest);

        return createAccountRequest;
    }

    // # CreateAccount API operations
    // The CreateAccount API operations enable you to create a PayPal account on behalf of a third party and download the SDK [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)    
    public CreateAccountResponse CreateAccountAPIOperations(CreateAccountRequest createAccountRequest) 
    {
        // Create the CreateAccountResponse object
        CreateAccountResponse responseCreateAccount = new CreateAccountResponse();

        try
        {
            // Create the AdaptiveAccounts service object to make the API call
            AdaptiveAccountsService service = new AdaptiveAccountsService();

            // # API call 
            // Invoke the CreateAccount method in service wrapper object    			
            responseCreateAccount = service.CreateAccount(createAccountRequest);
         
            if (responseCreateAccount != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "CreateAccount API operation - " + createAccountRequest.accountType;
                acknowledgement += " - " + responseCreateAccount.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values    
                if (responseCreateAccount.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    logger.Info("Create Account Key : " + responseCreateAccount.createAccountKey + "\n");
                    Console.WriteLine("Create Account Key : " + responseCreateAccount.createAccountKey + "\n");
                    // Redirection to PayPal                            				
                    // The user is redirected to PayPal to enter password for the created account                  
                    // Set the redirection URL in responseCreateAccount.redirectURL                       			
                    // Using this URL the user is redirected to PayPal                    			
                }
                // # Error Values 
                else
                {
                    List<ErrorData> errorMessages = responseCreateAccount.error;
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

        return responseCreateAccount;
    }
   
    // # Main Method
    private static void Main()
    {
        CreateAccountSample sampleCreateAccount = new CreateAccountSample();
        sampleCreateAccount.CreatePersonalAccount();
        sampleCreateAccount.CreatePremierAccount();
        sampleCreateAccount.CreateBusinessAccount();
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}

