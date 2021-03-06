# MedicalRecords.API
##RESTFul API in .NET Core 3.1; EntityFramework (code-first approach);  

To run the frontend, run terminal with: ```npm run serve```  
Make sure you run backend on: localhost:44370  

##Endpoints:  
    * GET /api/patients  ...paginated list of all active patients  
    * GET /api/patients/{id} ...get the patent with given id      
    * POST /api/patients  ...adding new patient - including risk factors  
    * PUT /api/patients/{id} ...updating a patient - including risk factors (the old records are deleted the new ones are inserted)      
    * DELETE /api/patients/{id} ...soft delete of the patient (bool IsInactive set to 1)  
        
    *POST api/patientRiskFactors ...to add new item into joining table  
    *DELETE api/patientRiskFactors ...to delete the item in joining table  
    *GET /api/riskFactors  ...paginated list of all risk factors  
    *GET /api/riskFactors/{id} ...get the risk factor with given id  
    *POST /api/riskFactors ...adding new risk factor  
    *PUT /api/riskFactors/{id} ...edit & update risk factor name  

    *GET /api/user ...get the  logged in user  
    *POST /api/user/signin ...login with email and psw
    *POST /api/user/signup ...register with email and psw
    
3 modules:  
##API:   
    *Controllers  
    *Exceptions  
    *Extensions  
    *Filters   
    *Migrations  
    *ResponseModels: Pagination  
    
##Domain:   
    *Entites: Patient, RiskFactor, PatientRiskFactor  
    *Requests, Responses: DTOs  
    *Mappers  
    *Repositories: interfaces  
    *Services  
    *Extensions: DependenciesRegistration  
    
##Infrastructure:   
    *SchemaDefinitions  
    *Repositories: implmentations of Domain.Reposotories  
    *MedicalRecordsContext  
    
##Tests:   
    *incomplete; swagger used  
    
##//TODO:  
*authentication (JWT token)  - DONE in backend 

*Vue.js frontend  added, just learning  
*vuetify added, just learning  

*HATEOAS ??
