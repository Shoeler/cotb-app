# Example Build Ouput

## Source Control Checkout
```
Started by GitHub push by Aenima4six2
Checking out git https://github.com/Shoeler/cotb-app.git into /var/jenkins_home/workspace/COTB@script to read src/app/Jenkinsfile-aks
 > git rev-parse --is-inside-work-tree # timeout=10
Fetching changes from the remote Git repository
 > git config remote.origin.url https://github.com/Shoeler/cotb-app.git # timeout=10
Fetching upstream changes from https://github.com/Shoeler/cotb-app.git
 > git --version # timeout=10
using GIT_ASKPASS to set credentials COTB Github Repo
 > git fetch --tags --progress https://github.com/Shoeler/cotb-app.git +refs/heads/*:refs/remotes/origin/*
 > git rev-parse refs/remotes/origin/master^{commit} # timeout=10
 > git rev-parse refs/remotes/origin/origin/master^{commit} # timeout=10
Checking out Revision 67a1849ea4069e4ff7777cc09f197ae451ea2a38 (refs/remotes/origin/master)
 > git config core.sparsecheckout # timeout=10
 > git checkout -f 67a1849ea4069e4ff7777cc09f197ae451ea2a38
Commit message: "Add body validation in int tests"
 > git rev-list --no-walk 43f13d06112fc40b8e80a42fb18befb2f29303e8 # timeout=10
Running in Durability level: MAX_SURVIVABILITY
[Pipeline] node
Running on Jenkins in /var/jenkins_home/workspace/COTB
[Pipeline] {
[Pipeline] checkout
 > git rev-parse --is-inside-work-tree # timeout=10
Fetching changes from the remote Git repository
 > git config remote.origin.url https://github.com/Shoeler/cotb-app.git # timeout=10
Fetching upstream changes from https://github.com/Shoeler/cotb-app.git
 > git --version # timeout=10
using GIT_ASKPASS to set credentials COTB Github Repo
 > git fetch --tags --progress https://github.com/Shoeler/cotb-app.git +refs/heads/*:refs/remotes/origin/*
 > git rev-parse refs/remotes/origin/master^{commit} # timeout=10
 > git rev-parse refs/remotes/origin/origin/master^{commit} # timeout=10
Checking out Revision 67a1849ea4069e4ff7777cc09f197ae451ea2a38 (refs/remotes/origin/master)
 > git config core.sparsecheckout # timeout=10
 > git checkout -f 67a1849ea4069e4ff7777cc09f197ae451ea2a38
Commit message: "Add body validation in int tests"
[Pipeline] echo
```

## Custom Release Tagging
```
#### Parsing release tag from commit ####
[Pipeline] sh
[COTB] Running shell script
+ git rev-parse --short HEAD
[Pipeline] readFile
[Pipeline] stage
[Pipeline] { (Build Images)
[Pipeline] echo
```

## Kubernetes Deployment Build
```
#### Building Kubernetes deployment YAML ####
[Pipeline] sh
[COTB] Running shell script
+ sed s#cotbregistry\.azurecr\.io/emulator:latest#cotbregistry\.azurecr\.io/emulator:67a1849#; s#cotbregistry\.azurecr\.io/server:latest#cotbregistry\.azurecr\.io/server:67a1849# src/manifests/azure/cotb-app.yml
[Pipeline] sh
[COTB] Running shell script
+ cat src/manifests/azure/cotb-app-67a1849.yml
---
apiVersion: v1
kind: Service
metadata:
  name: server-internal
spec:
  type: NodePort
  selector:
    app: server
  ports:
    - name: http
      port: 8080
      
---
apiVersion: apps/v1
kind: Deployment
metadata:
  name: server
spec:
  replicas: 1
  selector:
    matchLabels:
      app: server
  template:
    metadata:
      labels:
        app: server
    spec:
      imagePullSecrets:
        - name: acr-auth
      containers:
        - name: server
          image: cotbregistry.azurecr.io/server:67a1849
          env:
            - name: PORT
              value: "8080"
            - name: GMG_GRILL_HOST
              value: "emulator"
            - name: GMG_STATUS_POLLING_INTERVAL
              value: "2000"
          ports:
            - containerPort: 8080


---
apiVersion: v1
kind: Service
metadata:
  name: emulator
spec:
  type: NodePort
  selector:
    app: emulator
  ports:
    - name: gmg
      protocol: UDP
      port: 8080

---
apiVersion: apps/v1
kind: Deployment
metadata:
  name: emulator
spec:
  replicas: 1
  selector:
    matchLabels:
      app: emulator
  template:
    metadata:
      labels:
        app: emulator
    spec:
      imagePullSecrets:
        - name: acr-auth
      containers:
        - name: emulator
          image: cotbregistry.azurecr.io/emulator:67a1849
          ports:
            - containerPort: 8080

            
[Pipeline] echo
```

## Application Image Build (create Docker images)
```
#### Building application images ####
[Pipeline] sh
[COTB] Running shell script
+ docker build -t cotbregistry.azurecr.io/emulator:67a1849 -t cotbregistry.azurecr.io/emulator:latest -f src/app/emulator/Dockerfile src/app/emulator
Sending build context to Docker daemon  59.39kB

Step 1/11 : FROM microsoft/dotnet:2.1-sdk
 ---> 9e243db15f91
Step 2/11 : WORKDIR /app
 ---> Using cache
 ---> 2bc53f8049d9
Step 3/11 : COPY *.sln .
 ---> Using cache
 ---> dc279676f246
Step 4/11 : COPY cotb-emulator/*.csproj ./cotb-emulator/
 ---> Using cache
 ---> 5a5c5d8183dd
Step 5/11 : COPY cotb-emulator-tests/*.csproj ./cotb-emulator-tests/
 ---> Using cache
 ---> 55877d7aa6f7
Step 6/11 : RUN dotnet restore
 ---> Using cache
 ---> a894162a92fd
Step 7/11 : COPY cotb-emulator ./cotb-emulator
 ---> Using cache
 ---> a212b783e1a0
Step 8/11 : COPY cotb-emulator-tests ./cotb-emulator-tests
 ---> Using cache
 ---> 2c03670662c4
Step 9/11 : RUN dotnet publish -c Release -o out
 ---> Using cache
 ---> 8a7583a751f2
Step 10/11 : WORKDIR /app/cotb-emulator/out
 ---> Using cache
 ---> 5cd0fd47d8b1
Step 11/11 : ENTRYPOINT dotnet cotb-emulator.dll
 ---> Using cache
 ---> a1ebfb06ab42
Successfully built a1ebfb06ab42
[Pipeline] sh
[COTB] Running shell script
+ docker build -t cotbregistry.azurecr.io/server:67a1849 -t cotbregistry.azurecr.io/server:latest -f src/app/server/Dockerfile src/app/server
Sending build context to Docker daemon  3.315MB

Step 1/13 : FROM node:8.9.1
 ---> 1934b0b038d1
Step 2/13 : WORKDIR /app
 ---> Using cache
 ---> abedfc61fe42
Step 3/13 : COPY ./cotb-web/package*.json ./cotb-web/
 ---> Using cache
 ---> 886386a933e5
Step 4/13 : COPY ./cotb-client/package*.json ./cotb-client/
 ---> Using cache
 ---> 239861411c4b
Step 5/13 : COPY ./cotb-server/package*.json ./cotb-server/
 ---> Using cache
 ---> 4612d31e7f1b
Step 6/13 : RUN npm i --prefix ./cotb-web &&      npm i --prefix ./cotb-client &&      npm i --prefix ./cotb-server
 ---> Using cache
 ---> a47010679c39
Step 7/13 : COPY ./cotb-web ./cotb-web
 ---> Using cache
 ---> b1ebc6d28beb
Step 8/13 : COPY ./cotb-client ./cotb-client
 ---> Using cache
 ---> d8a621b0e1b1
Step 9/13 : COPY ./cotb-server ./cotb-server
 ---> Using cache
 ---> c0d9fa946cda
Step 10/13 : RUN npm run build --prefix ./cotb-web &&     cp -R ./cotb-web/build ./cotb-server/public/app
 ---> Using cache
 ---> c8a8b5ae5f64
Step 11/13 : WORKDIR /app/cotb-server
 ---> Using cache
 ---> 9989911b217a
Step 12/13 : CMD npm run start:release
 ---> Using cache
 ---> 7532276cf1a8
Step 13/13 : EXPOSE 80:80
 ---> Using cache
 ---> 077604056503
Successfully built 077604056503
[Pipeline] sh
[COTB] Running shell script
+ docker build -t cotbregistry.azurecr.io/tests:67a1849 -t cotbregistry.azurecr.io/tests:latest -f src/app/tests/Dockerfile src/app/tests
Sending build context to Docker daemon  20.48kB

Step 1/4 : FROM postman/newman_ubuntu1404
 ---> f3ac24baa292
Step 2/4 : WORKDIR /app
 ---> Using cache
 ---> ce5954e4a31b
Step 3/4 : COPY collections collections
 ---> 795dc6571b97
Removing intermediate container d02735dece33
Step 4/4 : COPY envs envs
 ---> ddf10f17110b
Removing intermediate container 31c3c8e18677
Successfully built ddf10f17110b
[Pipeline] }
[Pipeline] // stage
[Pipeline] stage
[Pipeline] { (Exec Unit Tests)
[Pipeline] echo
```

## Compiled Image Unit Test Execution
```
#### Executing unit tests against all images ####
[Pipeline] sh
[COTB] Running shell script
+ docker run -i --entrypoint=npm cotbregistry.azurecr.io/server run test

> cotb-server@0.0.0 test /app/cotb-server
> mocha tests/**/*Test.js --reporter spec



  Polling Client
    ✓ Should poll For Grill Status (608ms)


  1 passing (619ms)

[Pipeline] sh
[COTB] Running shell script
+ docker run -i --entrypoint=dotnet cotbregistry.azurecr.io/emulator test ../../cotb-emulator-tests
Build started, please wait...
Build completed.

Test run for /app/cotb-emulator-tests/bin/Debug/netcoreapp2.1/cotb-emulator-tests.dll(.NETCoreApp,Version=v2.1)
Microsoft (R) Test Execution Command Line Tool Version 15.7.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

Total tests: 14. Passed: 14. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 4.7298 Seconds
[Pipeline] }
[Pipeline] // stage
[Pipeline] stage
[Pipeline] { (Push Images To Registry)
[Pipeline] echo
```

## Push Images to Azure Container Registry (ACR)
```
#### Logging into to docker registry #### 
[Pipeline] withCredentials
[Pipeline] {
[Pipeline] sh
[COTB] Running shell script
+ docker login -u cotbregistry cotbregistry.azurecr.io -p ****
WARNING! Using --password via the CLI is insecure. Use --password-stdin.
WARNING! Your password will be stored unencrypted in /root/.docker/config.json.
Configure a credential helper to remove this warning. See
https://docs.docker.com/engine/reference/commandline/login/#credentials-store

Login Succeeded
[Pipeline] }
[Pipeline] // withCredentials
[Pipeline] echo
#### Pushing all new images with release tag 67a1849 ####
[Pipeline] sh
[COTB] Running shell script
+ docker push cotbregistry.azurecr.io/emulator:67a1849
The push refers to a repository [cotbregistry.azurecr.io/emulator]
d03e362934ea: Preparing
9e53070cce14: Preparing
fa91b8980f3d: Preparing
733d9f7a67d3: Preparing
4d3d90d59670: Preparing
f6fdcaa739ca: Preparing
a5c834a10a34: Preparing
b462e01abf9a: Preparing
a532eba900bb: Preparing
d6f746292071: Preparing
5a438fb59028: Preparing
fa0c3f992cbd: Preparing
ce6466f43b11: Preparing
719d45669b35: Preparing
3b10514a95be: Preparing
f6fdcaa739ca: Waiting
a5c834a10a34: Waiting
b462e01abf9a: Waiting
a532eba900bb: Waiting
d6f746292071: Waiting
5a438fb59028: Waiting
fa0c3f992cbd: Waiting
ce6466f43b11: Waiting
719d45669b35: Waiting
3b10514a95be: Waiting
9e53070cce14: Layer already exists
4d3d90d59670: Layer already exists
733d9f7a67d3: Layer already exists
d03e362934ea: Layer already exists
a5c834a10a34: Layer already exists
d6f746292071: Layer already exists
fa91b8980f3d: Layer already exists
f6fdcaa739ca: Layer already exists
b462e01abf9a: Layer already exists
a532eba900bb: Layer already exists
fa0c3f992cbd: Layer already exists
5a438fb59028: Layer already exists
3b10514a95be: Layer already exists
ce6466f43b11: Layer already exists
719d45669b35: Layer already exists
67a1849: digest: sha256:47ed873e0bae02c908149822c27cd9ca19d4b8fcc35e687c4634f2f5c56613fc size: 3470
[Pipeline] sh
[COTB] Running shell script
+ docker push cotbregistry.azurecr.io/emulator:latest
The push refers to a repository [cotbregistry.azurecr.io/emulator]
d03e362934ea: Preparing
9e53070cce14: Preparing
fa91b8980f3d: Preparing
733d9f7a67d3: Preparing
4d3d90d59670: Preparing
f6fdcaa739ca: Preparing
a5c834a10a34: Preparing
b462e01abf9a: Preparing
a532eba900bb: Preparing
d6f746292071: Preparing
5a438fb59028: Preparing
fa0c3f992cbd: Preparing
ce6466f43b11: Preparing
719d45669b35: Preparing
3b10514a95be: Preparing
f6fdcaa739ca: Waiting
a5c834a10a34: Waiting
b462e01abf9a: Waiting
a532eba900bb: Waiting
d6f746292071: Waiting
5a438fb59028: Waiting
fa0c3f992cbd: Waiting
ce6466f43b11: Waiting
719d45669b35: Waiting
3b10514a95be: Waiting
4d3d90d59670: Layer already exists
fa91b8980f3d: Layer already exists
a5c834a10a34: Layer already exists
f6fdcaa739ca: Layer already exists
733d9f7a67d3: Layer already exists
9e53070cce14: Layer already exists
a532eba900bb: Layer already exists
d03e362934ea: Layer already exists
5a438fb59028: Layer already exists
fa0c3f992cbd: Layer already exists
719d45669b35: Layer already exists
ce6466f43b11: Layer already exists
3b10514a95be: Layer already exists
b462e01abf9a: Layer already exists
d6f746292071: Layer already exists
latest: digest: sha256:47ed873e0bae02c908149822c27cd9ca19d4b8fcc35e687c4634f2f5c56613fc size: 3470
[Pipeline] sh
[COTB] Running shell script
+ docker push cotbregistry.azurecr.io/server:67a1849
The push refers to a repository [cotbregistry.azurecr.io/server]
095a8b410473: Preparing
6d0e32dc4b19: Preparing
00d209d44bdc: Preparing
e59b38ccbc5c: Preparing
95de56591c83: Preparing
37a523915e6e: Preparing
5a479ae9610b: Preparing
bd5e46dcad85: Preparing
1977380ac453: Preparing
509ff79607a2: Preparing
b86faf8fff95: Preparing
0da372da714b: Preparing
bf3841becf9d: Preparing
63866df00998: Preparing
2f9128310b77: Preparing
d9a5f9b8d5c2: Preparing
c01c63c6823d: Preparing
37a523915e6e: Waiting
5a479ae9610b: Waiting
bd5e46dcad85: Waiting
1977380ac453: Waiting
509ff79607a2: Waiting
b86faf8fff95: Waiting
0da372da714b: Waiting
bf3841becf9d: Waiting
63866df00998: Waiting
2f9128310b77: Waiting
d9a5f9b8d5c2: Waiting
c01c63c6823d: Waiting
95de56591c83: Layer already exists
00d209d44bdc: Layer already exists
e59b38ccbc5c: Layer already exists
095a8b410473: Layer already exists
6d0e32dc4b19: Layer already exists
1977380ac453: Layer already exists
bd5e46dcad85: Layer already exists
509ff79607a2: Layer already exists
37a523915e6e: Layer already exists
0da372da714b: Layer already exists
b86faf8fff95: Layer already exists
63866df00998: Layer already exists
c01c63c6823d: Layer already exists
2f9128310b77: Layer already exists
bf3841becf9d: Layer already exists
d9a5f9b8d5c2: Layer already exists
5a479ae9610b: Layer already exists
67a1849: digest: sha256:010d565287ae59072721211d8939e7da25d57772b7da16d6665c610b67833a5e size: 3890
[Pipeline] sh
[COTB] Running shell script
+ docker push cotbregistry.azurecr.io/server:latest
The push refers to a repository [cotbregistry.azurecr.io/server]
095a8b410473: Preparing
6d0e32dc4b19: Preparing
00d209d44bdc: Preparing
e59b38ccbc5c: Preparing
95de56591c83: Preparing
37a523915e6e: Preparing
5a479ae9610b: Preparing
bd5e46dcad85: Preparing
1977380ac453: Preparing
509ff79607a2: Preparing
b86faf8fff95: Preparing
0da372da714b: Preparing
bf3841becf9d: Preparing
63866df00998: Preparing
2f9128310b77: Preparing
d9a5f9b8d5c2: Preparing
c01c63c6823d: Preparing
37a523915e6e: Waiting
5a479ae9610b: Waiting
bd5e46dcad85: Waiting
1977380ac453: Waiting
509ff79607a2: Waiting
b86faf8fff95: Waiting
0da372da714b: Waiting
bf3841becf9d: Waiting
63866df00998: Waiting
2f9128310b77: Waiting
d9a5f9b8d5c2: Waiting
c01c63c6823d: Waiting
95de56591c83: Layer already exists
00d209d44bdc: Layer already exists
095a8b410473: Layer already exists
bd5e46dcad85: Layer already exists
37a523915e6e: Layer already exists
e59b38ccbc5c: Layer already exists
5a479ae9610b: Layer already exists
1977380ac453: Layer already exists
509ff79607a2: Layer already exists
6d0e32dc4b19: Layer already exists
0da372da714b: Layer already exists
bf3841becf9d: Layer already exists
d9a5f9b8d5c2: Layer already exists
2f9128310b77: Layer already exists
c01c63c6823d: Layer already exists
63866df00998: Layer already exists
b86faf8fff95: Layer already exists
latest: digest: sha256:010d565287ae59072721211d8939e7da25d57772b7da16d6665c610b67833a5e size: 3890
[Pipeline] sh
[COTB] Running shell script
+ docker push cotbregistry.azurecr.io/tests:67a1849
The push refers to a repository [cotbregistry.azurecr.io/tests]
5f27ce297719: Preparing
c664d4f07e70: Preparing
4caaa3d277db: Preparing
eb6f793a9f43: Preparing
5f70bf18a086: Preparing
1255f0fad851: Preparing
3bd5a069ac09: Preparing
fef0f9958347: Preparing
1255f0fad851: Waiting
3bd5a069ac09: Waiting
fef0f9958347: Waiting
5f70bf18a086: Layer already exists
eb6f793a9f43: Layer already exists
1255f0fad851: Layer already exists
3bd5a069ac09: Layer already exists
4caaa3d277db: Layer already exists
fef0f9958347: Layer already exists
5f27ce297719: Pushed
c664d4f07e70: Pushed
67a1849: digest: sha256:416e7c71e9a3920e06b1cd49ed5e86d5f2eebf590a5eb8ca6eee4fc64b8841e3 size: 1985
[Pipeline] sh
[COTB] Running shell script
+ docker push cotbregistry.azurecr.io/tests:latest
The push refers to a repository [cotbregistry.azurecr.io/tests]
5f27ce297719: Preparing
c664d4f07e70: Preparing
4caaa3d277db: Preparing
eb6f793a9f43: Preparing
5f70bf18a086: Preparing
1255f0fad851: Preparing
3bd5a069ac09: Preparing
fef0f9958347: Preparing
1255f0fad851: Waiting
3bd5a069ac09: Waiting
fef0f9958347: Waiting
eb6f793a9f43: Layer already exists
4caaa3d277db: Layer already exists
5f27ce297719: Layer already exists
5f70bf18a086: Layer already exists
c664d4f07e70: Layer already exists
3bd5a069ac09: Layer already exists
fef0f9958347: Layer already exists
1255f0fad851: Layer already exists
latest: digest: sha256:416e7c71e9a3920e06b1cd49ed5e86d5f2eebf590a5eb8ca6eee4fc64b8841e3 size: 1985
[Pipeline] }
[Pipeline] // stage
[Pipeline] stage
[Pipeline] { (Deploy Integration Environment)
[Pipeline] echo
```

## Integration Environment Build & Deploy
```
#### Deploying integration environment ####
[Pipeline] sh
[COTB] Running shell script
+ kubectl apply --overwrite=true --force=true -f src/manifests/azure/cotb-app-67a1849.yml --namespace=test
service/server-internal created
deployment.apps/server created
service/emulator created
deployment.apps/emulator created
[Pipeline] sh
[COTB] Running shell script
+ kubectl rollout status deployment/emulator --namespace=test
Waiting for deployment "emulator" rollout to finish: 0 of 1 updated replicas are available...
deployment "emulator" successfully rolled out
[Pipeline] sh
[COTB] Running shell script
+ kubectl rollout status deployment/server --namespace=test
deployment "server" successfully rolled out
[Pipeline] echo

```

## Integration Test Execution
```
#### Executing integration tests ####
[Pipeline] echo
newman

cotb-app

→ Power On
  PUT http://server-internal.test.svc.cluster.local:8080/api/poweron [200 OK, 386B, 4.1s]
  ✓  Status code is 200

→ Power Off
  PUT http://server-internal.test.svc.cluster.local:8080/api/poweroff [200 OK, 386B, 4s]
  ✓  Status code is 200

→ Power Toggle
  PUT http://server-internal.test.svc.cluster.local:8080/api/powertoggle [200 OK, 386B, 4s]
  ✓  Status code is 200

→ Set Grill Temp
  PUT http://server-internal.test.svc.cluster.local:8080/api//temperature/grill/200 [200 OK, 386B, 4s]
  ✓  Status code is 200

→ Set Food Temp
  PUT http://server-internal.test.svc.cluster.local:8080/api//temperature/food/200 [200 OK, 386B, 4s]
  ✓  Status code is 200

→ Get Grill Status
  GET http://server-internal.test.svc.cluster.local:8080/api/status [200 OK, 642B, 2s]
  ✓  Status code is 200
  ✓  Schema is valid
  ✓  Body values are valid

┌─────────────────────────┬──────────┬──────────┐
│                         │ executed │   failed │
├─────────────────────────┼──────────┼──────────┤
│              iterations │        1 │        0 │
├─────────────────────────┼──────────┼──────────┤
│                requests │        6 │        0 │
├─────────────────────────┼──────────┼──────────┤
│            test-scripts │        6 │        0 │
├─────────────────────────┼──────────┼──────────┤
│      prerequest-scripts │        1 │        0 │
├─────────────────────────┼──────────┼──────────┤
│              assertions │        8 │        0 │
├─────────────────────────┴──────────┴──────────┤
│ total run duration: 22.5s                     │
├───────────────────────────────────────────────┤
│ total data received: 259B (approx)            │
├───────────────────────────────────────────────┤
│ average response time: 3.7s                   │
└───────────────────────────────────────────────┘

[Pipeline] echo

[Pipeline] echo
```

## Integration Environment Teardown
```
#### Tearing down integration environment ####
[Pipeline] sh
[COTB] Running shell script
+ kubectl delete --now=true deployments/tests --namespace test
deployment.extensions "tests" deleted
[Pipeline] sh
[COTB] Running shell script
+ kubectl delete --now=true -f src/manifests/azure/cotb-app-67a1849.yml --namespace=test
service "server-internal" deleted
deployment.apps "server" deleted
service "emulator" deleted
deployment.apps "emulator" deleted
[Pipeline] }
[Pipeline] // stage
[Pipeline] stage
[Pipeline] { (Deploy Production Environment)
[Pipeline] echo
```

## Production Deployment 
```
#### Deploying Production with phased rollout ####
[Pipeline] sh
[COTB] Running shell script
+ kubectl apply -f src/manifests/azure/cotb-app-67a1849.yml
service/server-internal unchanged
deployment.apps/server configured
service/emulator unchanged
deployment.apps/emulator configured
+ kubectl apply -f src/manifests/azure/cotb-app-public-svc.yml
service/server unchanged
[Pipeline] sh
[COTB] Running shell script
+ kubectl rollout status deployment/emulator
Waiting for deployment "emulator" rollout to finish: 1 old replicas are pending termination...
deployment "emulator" successfully rolled out
[Pipeline] sh
[COTB] Running shell script
+ kubectl rollout status deployment/server
deployment "server" successfully rolled out
[Pipeline] }
[Pipeline] // stage
[Pipeline] }
[Pipeline] // node
[Pipeline] End of Pipeline
Finished: SUCCESS
```