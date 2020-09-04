## Pet Adoption

### Summary
This is an example project used to demonstrate the microservices pattern along with a service bus. There are mainly 3 microservices,

1. Pet Info - This service stores and maintains breed information with a full crud rest API that can be consumed by other microservices.
2. User Pet Info - This microservice stores and maintains pet data of users.
3. Pet Adoption - This application exposes a Restful API with a UI written in angular 10. It consumes the pet info api and the user pet info apis.

The application has been built with,

* .Net core 3.1
* Angular 10
* EF Core 3.1
* Kafka

### Running the project
#### Pre-reqs
-  [.Net core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- LocalDB through the Visual Studio Installer or from [here](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15).
- Powershell
- Kafka (zookeeper and broker running either through [command line](https://kafka.apache.org/downloads) or on [docker](https://github.com/ashwin027/petadoption/blob/master/charts/docker-compose.yml)/[kubernetes](https://github.com/ashwin027/petadoption/tree/master/charts/kafka))

#### Running the application
* Make sure that kafka is up and running by either running through docker-compose or by deploying the helm chart on a kuberntes cluster (see [charts](https://github.com/ashwin027/petadoption/tree/master/charts) folder). 
* Add configuration to the appsettings.json file with the kafka server URL. 
* Build and run the api projects `PetInfo.Api`, `UserPetInfo.Api` and `PetAdoption`. For the UI, navigate to the `PetAdoption/ClientApp` folder and type `npm install` to get the packages installed and then type `npm start` to start the angular application.

