# Notes

## Let's get these over with

- [x] Create "NewAppointmentHandler" azure function.
  - [x] This is an HTTP triggered function (HTTP POST), which will accept new appointment requests.
  - [x] If the request is valid it will be sent to a `new-appointments` queue. Otherwise it will send an error status to the caller.

- [ ]  Create "Scheduler" azure function.
  - [ ] This function will be triggered when a message is arrived to the "new-appointments" queue.
  - [ ] It will check whether if a particular barber is available and if it is it will schedule an appointment, or will reject it.
  - [ ] If an appointment is made it will be sent to the blob container "scheduled-appointments" and if it's rejected it will be in a container called "rejected-requests".

- [ ] Unit Testing to the "Schedule" function.
  - [ ] Refer the nuget package (AzureFunctions.AutoFac)
  - [ ] Refactor the function.
  - [ ] Add a unit test class
  - [ ] Add the test methods.

- [x] Deploy the function to the cloud using VS2017
  - [x] Create a Function App in the Azure portal first.
  - [x] Right-click publish the function app from VS2017.

- [ ] Create CI/CD
  - [ ] Create a project in Azure Devops
  - [ ] Create a build pipeline.
    - [ ] Show the important steps. Especially about the preference setting which you need to make in the devops portal.
  - [ ] Checkin some code and show the build happening.
  - [ ] Create a release pipeline
    - [ ] Show the important things to setup and notice in here.
    - [ ] Set up the release to happen once a successful build is done.
    - [ ] Add some code and show case this.