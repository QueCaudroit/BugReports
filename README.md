# Bug Report Module

Code in this repository is just an exercise, and should not be used in production.

## How to install the Bug Report module

You need to have kubectl configured to a valid cluster to run this
```
bash install.sh
kubectl get deployments --watch
```
Wait until the `webapp` deployment is ready, it can take one or two minutes

Alternatively you can install it on docker-compose:
```
docker-compose up
```

## Getting the ip address of the Bug Report module

First, find the ip address of the webapp.
If you are using minikube:
```
minikube service webapp
```
If you are using another kubernetes cluster, you have to connect directly to the node where the pod is running
```
kubectl get pods
kubectl describe pods <POD_NAME>
```
If you are using docker-compose, use `localhost:5001`

## How to use the Bug Report module

### Getting a token

GET `https://{IP_ADDRESS}/FakeTokenProvider?role=admin`

The body of the response is a JWT token that you will have to provide to the other routes to authenticate. Just add the header `Authorization:Bearer {JWT_TOKEN}`. You can choose any role in the query, but the GET routes are restricted to admin users. Default is `admin`. The player id is stored in the token and incremented for each fake token created.

### Posting a Bug Report

POST `https://{IP_ADDRESS}/BugReport`

Example body:
```
{
  "BugDescription": "Plantage quand je clique sur machin",
  "Logs": "ERROR could not read property..."
}
```
Headers should include `Content-Type:application/json`.
Returns the saved Bug Report with its ID, playerID and date

### Getting the Bug Reports

GET `https://{IP_ADDRESS}/BugReport?playerID=0&date=20200223&description=cliq&logs=ERROR`

Returns the Bug Reports that match all these filters.
description/logs filters match if the text is included in the description/logs. date filter format is `yyyyMMdd`.
All filters are optional

### Posting large files

POST `https://{IP_ADDRESS}/BugReportFile?reportID={BUG_REPORT_ID}`

It should be a multipart upload.
Returns the updated bug report, with the filename and the file ID added.

### Downloading large files

GET `https://{IP_ADDRESS}/BugReportFile/{FILE_ID}`

Returns the file that was uploaded

## Design Choices

* Storing data: we use a relational database (postgres) to allow fast queries for the api users. Analysts will also be able to do more advanced queries on this database. No SQL technologies are not required for this volume and they often limit the possible queries.
* Big files storage: The ideal choice here would be some cloud storage like google cloud storage or amazon s3, But I chose to store it as files in the container because it is free.
* Containerization: I used docker because it allows us to have the same environment between developpers and with the production servers, that allows us to easily reproduce bugs and to avoid some breaking deployments

## Development and testing process

First I launched the demo weather app and containerized it to allow reproductible behaviour as soon as possible. Then I used a dockerized db to create a small development environment and started to build features upon that. Everything was manually tested because of the small size of the project and because it is not meant for production

## Things to improve before sending to production

### Security

* Better password and secrets
* Change the environment to anything but `Development` as it exposes server code when an exception occur
* Do not store files in the containers
* Only use containerized database in development environment
* Limit the quantity of data a user can send
* Use an asymetric signing algorithm for the token (not usefull if this app still provide the token)
* Add an expiration date in the token
* Validate audience and issuer for the token
* Do not commit secret / No hardcoded secret

### Performance

* Lighter docker image (with just a binary and no source files)

### Quality

* Add automated tests
* Add CI/CD
* Use a code formatter
