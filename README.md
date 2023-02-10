# RazorLight with Azure Functions sample

### What is the goal Here?
* Showcase that RazorLight works with .NET 6 and Azure Functions v4
* Help you start development easily

### What does this app do?
* Uses Azure Function HTTP Trigger, deserializes the request body, creates HTML from a template powered by RazorLight and then sends email via Postmark
* Uses partials and layout with template

# Getting Started

- **Clone this repo and restore** :
  ````
  git clone https://github.com/jacekkulis/razorlight-functionapp-sample 
  dotnet restore
  ````
- **Configure**
  - Configure Postmark
    - Create new instance of [Postmark](https://postmarkapp.com/) (free trial)
  - Configure `local.settings.json`
    ```json
    {
        "IsEncrypted": false,
        "Values": {
            "AzureWebJobsStorage": "UseDevelopmentStorage=true",
            "FUNCTIONS_WORKER_RUNTIME": "dotnet",
            "PostmarkSettings:ServerToken": "<yourtoken>"
        }
    }
    ```
 - **Run**
    - Run your application with Azurite as web jobs storage

----

# License

[![MIT License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat)](/LICENSE) 