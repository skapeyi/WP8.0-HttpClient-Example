This is sample code on how to use HttpClient with Windows Phone 8.0 to communicate with a json API.
You basically send a json object to the api and it sends back a json response, which you then deserialize and use.

There is an ApiCall class that contains other methods  for making a GET and PUT request that are not used in this example, but the usage should be the same as that of the POST request.

For the sample POST request made, the api expects a json object like this =>
```{
    "sessions": {
        "links": {
            "user": {
                "email": "username",
                "password": "Password"
            }
        }
    }
}```

If the account exists, it sends back a response after the session has been created!
```{
    "sessions": {
        "id": "{id}",
        "access_token": "{access_token}",
        "links": {
            "user": {
                "id": "id",
                "href": "http//theserverip/users/id",
                "firstname": "<FirstName>",
                "lastname": "<LastName>",
                "email": "<eml@example.com>",
                "phone": "<Phonenumber>",
                "role": "userrole",
                "postal_code": "poboxnumber",
                "street_address": "streetaddress",
                "city": "city"
            }
        }
    }
}```
