#!/bin/bash
set -e
version=${1:-latest}

echo "Building Images..."
(cd ../jenkins && docker build -t cotbregistry.azurecr.io/jenkins:$version .)

echo "Pushing Images..."-u-
(cd ../jenkins && docker push cotbregistry.azurecr.io/jenkins:$version)

echo "Deploying"
kubectl apply -f ../manifests/jenkins-aks.yml
kubectl rollout status deployments jenkins

echo "Done"
