# AET API C# Wrapper
This repo provides a C# wrapper for the AET API, documented at https://developers.aetrust.com/?version=latest.
The wrapper takes the inputs and constructs the signature and all other headers required by the AET API,
it sends the request to the API and outputs the response from the API without modifying it.
> For `PUT` & `POST` requests the wrapper passes the provided `body` to the API without any additional formatting 

## Getting started
Import the 3 class files `AetrustHttpClient.cs`, `AetrustSignature.cs` & `AmericanEstateTrust.cs` to your project  

## Sample request using the wrapper classes
```
var apiKey = "123_my_key";
var apiSecret = "123_my_secret";
var aetApiUrl = "https://sandbox.aet.dev/v2/companies/users";
var myOrigin = "http://example.com";

var body = "{\"data\":{\"type\":\"users\",\"attributes\":{\"email\":\"johndoe+2@example.com\",\"password\":\"ystt^Yj3PL\",\"confirmPassword\": \"ystt^Yj3PL\"}}}";

var client = new AetrustHttpClient(new HttpClient(), apiSecret, apiKey, myOrigin);
var request = client.CreateRequest(aetApiUrl, new HttpMethod("POST"), body);
var bodyToSend = request.Content.ReadAsStringAsync().Result;
var response = client.SendRequestAsync(request).Result;
```
