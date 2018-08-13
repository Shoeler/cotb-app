#!/bin/bash
kubectl create secret docker-registry acr-auth --docker-server cotbregistry.azurecr.io --docker-username clusterUser_kubernetes-cotb_cotb_cluster --docker-password 6628eb85fd1eb90149b8dac4bf78c38a --docker-email schuyler.bishop@gmail.com

