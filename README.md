
## Overview

The goal of the project is to create a Custodian Microservice that can save a new custodian using distributed windows docker containers. The project includes a website, the RavenDB document store, the NATS message queue, a console application and SEQ logging via Serilog.

## Getting Started

### Installation

* Clone the repo and edit the build.ps1 to ensure that NuGet and MSBuild have valid paths.
* Build the solution using the build.ps1

```build

powershell.exe -ExecutionPolicy ByPass "& '.\build.ps1'"

```

### Docker

Windows docker runs on Windows Server 2016 and Windows 10. The project was developed on Windows 10 with updates through May 2017.

* Install Windows Docker from https://docs.docker.com/docker-for-windows/install/
* Once docker is running in the tray, right-click the whale and "Switch to Windows Containers"
* The first time containers are composed, you may need to create "NAT" network. Docker will provide you with syntax to create the network

```docker

docker-compose up -d

```

The following containers will be started. These can be seen via "docker ps"

1. container-ravendb: Runs a ravendb document store instance
2. container-messagequeue: Runs the NATS message queue
3. container-client: Runs a console application subscribed to the message queue
4. container-web: Runs a website that allows the user to enter custodian information and publish a new custodian to the message queue

### Logging

Container services are logging to a SEQ (https://getseq.net/) endpoint that is configured in the LogServiceFactory.cs. To enable logging the user should update the following to their workstation's IP address and build/compose.

```seq endpoint

private const string SeqEndpoint = "http://IPADDRESS:5341";

```