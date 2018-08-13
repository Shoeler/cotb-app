#!/bin/bash
set -e
version=${1:-latest}

echo "Building Images..."
(cd ../jenkins && docker build -t 127.0.0.1:30400/jenkins:$version .)

echo "Pushing Images..."-u-
(cd ../jenkins && docker push 127.0.0.1:30400/jenkins:$version)

echo "Deploying"
kubectl apply -f ../manifests/jenkins.yml
kubectl rollout status deployments jenkins

echo "Done"