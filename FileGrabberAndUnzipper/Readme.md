
You c
Create Two Functions in Azure

the first one is the grabber it download a file in a blob 
the second one unzip the file in another blob

Follow the following tutorial to know to create your first function
https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-azure-function

Choose C# function 

You can also choose to deploy function using Azure Cli 

az group create --name myResourceGroup --location westeurope 

az storage account create --name <storage_name> --location westeurope --resource-group myResourceGroup --sku Standard_LRS

az functionapp create --name <app_name> --storage-account  <storage_name>  --resource-group myResourceGroup --consumption-plan-location westeurope
