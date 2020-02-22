# Bug Report Module

## How to launch the Bug Report module

```
docker build -t aspnetapp:develop .
kubectl apply -f DbDeployment.yaml
kubectl apply -f DbService.yaml
kubectl apply -f WebappDeployment.yaml
kubectl apply -f WebappService.yaml
```